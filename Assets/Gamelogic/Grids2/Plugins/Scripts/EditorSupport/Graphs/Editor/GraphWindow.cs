using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Gamelogic.Extensions.Editor;
using Gamelogic.Extensions.Editor.Internal;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// The GraphPreset class encapsulates a pre-defined graph so
	/// that they can be used in the graph editor.
	/// </summary>
	/// <typeparam name="TNode">The type of the t node.</typeparam>
	public class GraphPreset<TNode> where TNode : BaseNode
	{
		public string Name
		{
			get; set;
		}

		public Action<Graph<TNode>> AddTo
		{
			get; set;
		}

		protected static string GetNodeName<T>()
		{
			var type = typeof(T);

			return type.GetCustomAttributes(typeof(GraphEditorAttribute), true).Cast<GraphEditorAttribute>().First().name;
		}


	}

	/// <summary>
	/// A window for editing graphs.
	/// </summary>
	[Version(1, 1)]
	public class GraphWindow<TNode> : EditorWindow
		where TNode : BaseNode
	{
		public Dictionary<int, SerializedObject> serializedObjects = new Dictionary<int, SerializedObject>();

		private Graph<TNode> graph;
		private bool addLink;
		private TNode inputWindow;

		private Vector2 mainScrollPosition = Vector2.zero;
		private Vector2 toolbarScrollPosition = Vector2.zero;

		private Type[] nodeTypes;

		private TNode EditingTitle;

		private GUIStyle centeredLabel;

		private List<GraphPreset<TNode>> defaultPresets = new List<GraphPreset<TNode>>();
		private GUIStyle CenteredLabel
		{
			get
			{
				if (centeredLabel == null)
				{
					centeredLabel = new GUIStyle(GUI.skin.label);
					centeredLabel.alignment = TextAnchor.LowerCenter;
				}

				return centeredLabel;
			}
		}

		private const string TitleEditControlName = "TitleEdit";

		public void OnGUI()
		{
			EditorGUILayout.BeginHorizontal();
			DrawToolbar();

			GLEditorGUI.VerticalLine();

			mainScrollPosition = EditorGUILayout.BeginScrollView(mainScrollPosition);

			GUILayout.Label("", GUILayout.Width(2000), GUILayout.Height(1000));
			AddLink();
			DrawWindows();
			DrawCurves();
			HandleNodeDeleteButtons();
			EditorGUILayout.EndScrollView();
			EditorGUILayout.EndHorizontal();
		}

		protected static void ShowEditorImpl<TWindow, TNodeAttribute>(Graph<TNode> graph)
			where TWindow : GraphWindow<TNode>
		{

			var editor = GetWindow<TWindow>(graph.GetType());

			editor.graph = graph;
			editor.UpdateSerializableObjects();
			editor.addLink = false;

			editor.nodeTypes = typeof(Graph<TNode>).Assembly.GetTypes().Where
				(t => t.IsSubclassOf(typeof(TNode)) &&
					t.GetCustomAttributes(true).Any(a => a is TNodeAttribute)).ToArray();
		}

		protected virtual List<GraphPreset<TNode>> GetPresets()
		{
			return defaultPresets;
		}

		public static string GetNodeName(Type type)
		{
			return type.GetCustomAttributes(typeof(GraphEditorAttribute), true).Cast<GraphEditorAttribute>().First().name;
		}

		private void HandleNodeDeleteButtons()
		{
			var e = Event.current;

			if (e.type != EventType.mouseDown) return;

			var mousePos = e.mousePosition;

			foreach (var window in graph.Nodes)
			{
				foreach (var input in window.Inputs)
				{
					var x = (input.rect.x + input.rect.width + window.rect.x) / 2;
					var y = (input.rect.y + input.rect.height / 2 + window.rect.y + window.rect.height / 2) / 2;

					var pos = new Vector2(x, y);

					if ((mousePos - pos).magnitude <= 10)
					{
						window.RemoveNodeInput(input);

						e.Use();
						ApplySerializedProperties();
						Repaint();
						return;
					}
				}
			}
		}

		public static T AddNodeToGraph<T>(Graph<TNode> graph) where T : TNode
		{
			var type = typeof(T);
			var name = type.GetCustomAttributes(typeof(GraphEditorAttribute), true).Cast<GraphEditorAttribute>().First().name;

			return (T)graph.AddNode(type, Vector2.zero, name);
		}

		private void DrawCurves()
		{
			if (graph != null && graph.Nodes != null)
			{
				foreach (var window in graph.Nodes)
				{
					foreach (var input in window.Inputs)
					{
						EditorUtils.DrawNodeCurve(input.rect, window.rect);
					}
				}
			}
		}

		private void AddLink()
		{
			if (!addLink) return;

			var e = Event.current;
			var mousePos = e.mousePosition;

			EditorUtils.DrawNodeCurve(inputWindow.rect, mousePos);
			Repaint();

			if (e.type != EventType.mouseDown) return;

			var outputWindow = graph.Nodes.FirstOrDefault(t => t.rect.Contains(mousePos));

			if (outputWindow != null)
			{
				outputWindow.AddNodeInput(inputWindow);
				serializedObjects[outputWindow.id].ApplyModifiedProperties();
			}

			e.Use();

			addLink = false;
			inputWindow = null;
		}

		private void DrawToolbar()
		{
			toolbarScrollPosition = EditorGUILayout.BeginScrollView(toolbarScrollPosition, GUILayout.Width(120));
			EditorGUILayout.BeginVertical();

			GraphToolbarLabel("Options", true);

			if (GraphToolbarButton("Clear"))
			{
				if (EditorUtility.DisplayDialog("Warning", "This will remove all nodes from the Graph. This action can not be undone.", "Ok",
					"Cancel"))
				{
					graph.Clear();
					UpdateSerializableObjects();
				}
			}

			if (GraphToolbarButton("Recompute"))
			{
				graph.Recompute();
				ApplySerializedProperties();
			}

			var presets = GetPresets();

			if (presets.Any())
			{
				GraphToolbarLabel("Presets", true);

				foreach (var preset in presets)
				{
					if (GraphToolbarButton(preset.Name))
					{
						if (EditorUtility.DisplayDialog("Warning", "This will remove all nodes from the Graph. This action can not be undone.", "Ok",
					"Cancel"))
						{
							graph.Clear();
							AddPreset(preset);
							UpdateSerializableObjects();
						}
					}
				}
			}

			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			GraphToolbarLabel("Nodes", true);

			if (nodeTypes != null)
			{
				var groupedNodeTypes = nodeTypes.GroupBy(
					type =>
						type.GetCustomAttributes(typeof(GraphEditorAttribute), true)
							.Cast<GraphEditorAttribute>()
							.First()
							.folder);

				foreach (var groupedNodeType in groupedNodeTypes)
				{
					GraphToolbarLabel(groupedNodeType.Key, false);

					foreach (var type in groupedNodeType)
					{
						var graphAtt = type.GetCustomAttributes(typeof(GraphEditorAttribute), true).Cast<GraphEditorAttribute>().First();

						if (GraphToolbarButton(graphAtt.name))
						{
							AddNode(type);
						}
					}
				}
			}
			else
			{
				GraphToolbarLabel("No nodes found", false);
			}

			EditorGUILayout.EndVertical();
			EditorGUILayout.EndScrollView();
		}

		private void AddPreset(GraphPreset<TNode> preset)
		{
			preset.AddTo(graph);
		}

		private bool GraphToolbarButton(string label)
		{
			return GUILayout.Button(label, EditorStyles.toolbarButton);
		}

		private void GraphToolbarLabel(string label, bool bold)
		{
			GUILayout.Label(label, bold ? EditorStyles.boldLabel : EditorStyles.label);
		}

		private void DrawWindows()
		{
			BeginWindows();

			if (graph != null && graph.Nodes != null)
			{
				int i = 0;

				foreach (var nodeWindow in graph.Nodes)
				{
					if (EditingTitle == nodeWindow)
					{
						if (Event.current.type == EventType.MouseDown)
						{
							var titleArea = nodeWindow.rect;
							titleArea.height = 16;

							if (!titleArea.Contains(Event.current.mousePosition))
							{
								EditingTitle = null;
								Repaint();
							}
						}

						if (Event.current.keyCode == KeyCode.Return)
						{
							EditingTitle = null;
							Repaint();
						}
					}

					//var number = "(" + nodeWindow.id + ")";
					var nodeWindowCopy = nodeWindow;

					var originalColor = GUI.color;
					GUI.color = nodeWindowCopy.enable ? originalColor : Color.red;

					nodeWindow.rect = GUILayout.Window(i, nodeWindow.rect, n => DrawNode(n, (TNode)nodeWindowCopy), "");

					GUI.color = originalColor;

					i++;
				}
			}

			EndWindows();
		}

		private void ApplySerializedProperties()
		{
			foreach (var serializedObject in serializedObjects.Values)
			{
				serializedObject.ApplyModifiedProperties();
			}
		}

		public void OnEnable()
		{
			if (graph != null)
			{
				UpdateSerializableObjects();
			}
		}

		public void UpdateSerializableObjects()
		{
			serializedObjects.Clear();

			foreach (var node in graph.Nodes)
			{
				serializedObjects[node.id] = new SerializedObject(node);
			}
		}

		public T AddNode<T>() where T : TNode
		{
			return (T)AddNode(typeof(T));
		}

		public TNode AddNode(Type type/*, Vector2 mainScrollPosition*/)
		{
			var node = graph.AddNode(type, mainScrollPosition, type.GetCustomAttributes(typeof(GraphEditorAttribute), true).Cast<GraphEditorAttribute>().First().name);

			UpdateSerializableObjects();

			return node;
		}

		public void RemoveNode(TNode window)
		{
			graph.RemoveNode(window);
			UpdateSerializableObjects();
		}

		public void DrawNode(int id, TNode window)
		{
			float oldWidth = EditorGUIUtility.labelWidth;
			EditorGUIUtility.labelWidth = 100;

			DrawTitle(window);

			EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

			if (GUILayout.Button("Remove", EditorStyles.toolbarButton))
			{
				if (EditorUtility.DisplayDialog("Warning", "This will remove the node from the Graph. This action can not be undone.", "Ok",
					"Cancel"))
				{
					RemoveNode(window);
				}

				return;
			}

			var serializedObject = serializedObjects[window.id];
			serializedObject.Update();

			var baseNode = (BaseNode)serializedObject.targetObject;
			var enableButtonLabel = baseNode.enable ? "Disable" : "Enable";

			if (GUILayout.Button(enableButtonLabel, EditorStyles.toolbarButton))
			{
				baseNode.enable = !baseNode.enable;
				return;
			}

			if (GUILayout.Button("Add Link", EditorStyles.toolbarButton))
			{
				addLink = true;
				inputWindow = window;
			}

			GUILayout.FlexibleSpace();

			EditorGUILayout.EndHorizontal();

			var property = serializedObjects[window.id].GetIterator();
			bool isFirst = true;

			while (property.NextVisible(isFirst))
			{
				isFirst = false;

				if (property.name != "m_Script" && property.name != "output")
				{
					EditorGUILayout.PropertyField(property, true);
				}
			}

			EditorGUIUtility.labelWidth = oldWidth;
			if (GUI.changed)
			{
				serializedObject.ApplyModifiedProperties();
			}

			GUI.DragWindow();
		}

		private void DrawTitle(TNode window)
		{
			var titleArea = new Rect(0, 0, window.rect.width, 16);
			GUILayout.BeginArea(titleArea);
			GUILayout.BeginHorizontal();

			if (EditingTitle == window)
			{
				EditorGUI.BeginChangeCheck();
				GUI.SetNextControlName(TitleEditControlName);
				window.name = GUILayout.TextField(window.name, GUILayout.Width(window.rect.width));

				GUILayout.FlexibleSpace();
				GUI.Button(new Rect(-20, -20, 0, 0), "");
				GUILayout.FlexibleSpace();

				if (GUI.GetNameOfFocusedControl() != TitleEditControlName)
				{
					EditingTitle = null;
				}

				if (EditorGUI.EndChangeCheck())
				{
					EditorUtility.SetDirty(window);
				}
			}
			else
			{
				GUI.SetNextControlName(TitleEditControlName);
				GUI.TextField(new Rect(-20, -20, 0, 0), "");

				GUILayout.FlexibleSpace();
				if (GUILayout.Button(window.name, CenteredLabel))
				{
					EditingTitle = window;
					GUI.FocusControl(TitleEditControlName);
				}
				GUILayout.FlexibleSpace();
			}

			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}

		public void OnDisable()
		{
			ApplySerializedProperties();

			if (graph != null)
			{
				graph.Save();
			}
		}
	}
}