using Gamelogic.Extensions.Internal;
using UnityEditor;
using UnityEngine;

namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// Editor for displaying a Graph in the inspector.
	/// </summary>
	[Version(1, 1)]
	[CustomEditor(typeof(Shape1Graph))]
	public class Shape1GraphEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			if (GUILayout.Button("Edit"))
			{
				Shape1GraphWindow.ShowEditor((Shape1Graph)target);
			}
		}
	}

	/// <summary>
	/// Editor for displaying a Graph in the inspector.
	/// </summary>
	[Version(1, 1)]
	[CustomEditor(typeof(Shape2Graph))]
	public class Shape2GraphEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			if (GUILayout.Button("Edit"))
			{
				Shape2GraphWindow.ShowEditor((Shape2Graph)target);
			}
		}
	}

	/// <summary>
	/// Editor for displaying a Graph in the inspector.
	/// </summary>
	[Version(2)]
	[CustomEditor(typeof(Shape3Graph))]
	public class Shape3GraphEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			if (GUILayout.Button("Edit"))
			{
				Shape3GraphWindow.ShowEditor((Shape3Graph)target);
			}
		}
	}
}