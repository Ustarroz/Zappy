using System;
using System.IO;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Object = UnityEngine.Object;

namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// Contains methods for menu commands.
	/// </summary>
	public static class GraphEditorUtils
	{
		public static T AddNodeToGraph<TNode, T>(Graph<TNode> graph)
			where T : TNode
			where TNode : BaseNode
		{
			var type = typeof(T);
			var name = type.GetCustomAttributes(typeof(GraphEditorAttribute), true).Cast<GraphEditorAttribute>().First().name;

			return (T)graph.AddNode(type, Vector2.zero, name);
		}

		/// <summary>
		/// Creates and adds a new unlinked node to the graph.
		/// </summary>
		/// <typeparam name="TNode">The type of the node to add.</typeparam>
		/// <param name="graph">Graph where the node will be added</param>
		/// <param name="nodeType">Node to add.</param>
		/// <param name="initialPosition">The initial position the node will be displayed in the visual representation.</param>
		/// <param name="name">Name of the node</param>
		/// <returns>The newly created node.</returns>
		public static TNode AddNode<TNode>(this Graph<TNode> graph, Type nodeType, Vector2 initialPosition, string name)
			where TNode : BaseNode
		{
			var node = (TNode)ScriptableObject.CreateInstance(nodeType);

			node.id = graph.__IdCounter;
			node.name = name;
			node.rect = new Rect(initialPosition.x, initialPosition.y, node.rect.width, node.rect.height);

			graph.__Nodes.Add(node);
			graph.__IdCounter++;

			AssetDatabase.AddObjectToAsset(node, graph);
			AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(node));

			EditorUtility.SetDirty(graph);

			return node;
		}

		/// <summary>
		/// Un-links this node from other nodes, destroys it, and removes it from the graph.
		/// </summary>
		/// <param name="graph">Graph from where the node will be removed.</param>
		/// <param name="node">The node to remove.</param>
		public static void RemoveNode<TNode>(this Graph<TNode> graph, TNode node)
			where TNode : BaseNode
		{
			graph.__Nodes.Remove(node);

			foreach (var graphNode in graph.__Nodes)
			{
				graphNode.RemoveNodeInput(node);
			}

			//var path = AssetDatabase.GetAssetOrScenePath(node);
			Object.DestroyImmediate(node, true);

			//AssetDatabase.ImportAsset(path);
			//AssetDatabase.SaveAssets();
			EditorUtility.SetDirty(graph);
		}

		/// <summary>
		/// Removes all nodes from this graph.
		/// </summary>
		public static void Clear<TNode>(this Graph<TNode> graph)
			where TNode : BaseNode
		{
			foreach (var node in graph.__Nodes)
			{
				Object.DestroyImmediate(node, true);
			}

			graph.__Nodes.Clear();

			//var path = AssetDatabase.GetAssetOrScenePath(this);

			//AssetDatabase.ImportAsset(path);
			//AssetDatabase.SaveAssets();
			EditorUtility.SetDirty(graph);
		}

		/// <summary>
		/// Brings up a save file dialog that allows the user to specify a location to
		/// save a new graph, makes a new graph, and saves it to the specified
		/// location.
		/// </summary>
		public static TGraph MakeNewGraph<TGraph>(string defaultName, string subExtension) where TGraph : ScriptableObject
		{
			var graph = ScriptableObject.CreateInstance<TGraph>();

			var path = Selection.activeObject == null ? null : AssetDatabase.GetAssetPath(Selection.activeObject);

			if (string.IsNullOrEmpty(path))
			{
				path = EditorUtility.SaveFilePanel(
					"Create new Graph",
					"Assets",
					defaultName + "." + subExtension + ".asset",
					"asset");

				if (string.IsNullOrEmpty(path))
				{
					return graph;
				}

				path = "Assets" + path.Substring(Application.dataPath.Length);

			}
			else
			{
				if (File.Exists(path))
					path = Path.GetDirectoryName(path);

				path += "/" + defaultName + "." + subExtension + ".asset";
			}

			if (!path.Equals(string.Empty))
			{
				Debug.Log(path);

				AssetDatabase.CreateAsset(graph, path);
				AssetDatabase.SaveAssets();
			}

			return graph;
		}

		[MenuItem("Assets/Create/Grids/Presets/Rect Grid")]
		public static void MakePresetRectGrid()
		{
			var spaceMap = MakeNewGraph<SpaceMapGraph>("MapGraph", "spacemapgraph");

			SpaceMapGraphWindow.presets[0].AddTo(spaceMap);

			var shape = MakeNewGraph<Shape2Graph>("ShapeGraph", "shape2graph");

			Shape2GraphWindow.presets[0].AddTo(shape);

			MakeTileGridBuilder(spaceMap, shape);
		}

		private static void MakeTileGridBuilder(SpaceMapGraph map, Shape2Graph shape)
		{
			var path = Selection.activeObject == null ? null : AssetDatabase.GetAssetPath(Selection.activeObject);

			if (string.IsNullOrEmpty(path))
				return;

			if (File.Exists(path))
				path = Path.GetDirectoryName(path);

			path += "/Grid.prefab";

			//Debug.Log(path);
			//Debug.Log(Application.dataPath);

			var builder = TileGridBuilder.Create(map, shape, RoundType.Rect);

			var prefab = PrefabUtility.CreateEmptyPrefab(path);
			PrefabUtility.ReplacePrefab(builder.gameObject, prefab);
			Object.DestroyImmediate(builder.gameObject);
		}

		/// <summary>
		/// Save the asset database.
		/// </summary>
		public static void Save<TNode>(this Graph<TNode> graph)
			where TNode : BaseNode
		{
			//AssetDatabase.SaveAssets();
			//AssetDatabase.Refresh();
		}
	}
}