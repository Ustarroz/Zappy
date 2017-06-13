using Gamelogic.Extensions.Internal;
using UnityEditor;
using UnityEngine;

namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// Editor for displaying a Graph in the inspector.
	/// </summary>
	[Version(1, 1)]
	[CustomEditor(typeof(SpaceMapGraph))]
	public class MapGraphEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			if (GUILayout.Button("Edit"))
			{
				SpaceMapGraphWindow.ShowEditor((SpaceMapGraph)target);
			}
		}
	}
}