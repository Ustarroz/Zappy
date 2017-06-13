using System.Collections.Generic;
using UnityEngine;

namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// A graph window for space map graphs.
	/// </summary>
	public class SpaceMapGraphWindow : GraphWindow<SpaceMapNode<Vector3>>
	{
		public static List<GraphPreset<SpaceMapNode<Vector3>>> presets = new List<GraphPreset<SpaceMapNode<Vector3>>>
		{
			new GraphPreset<SpaceMapNode<Vector3>>
			{
				Name = "Rect Map",
				AddTo = graph =>
				{
					var outputNode = AddNodeToGraph<OutputSpaceMapNode>(graph);
					var shapeNode = AddNodeToGraph<RectSpaceMap>(graph);

					outputNode.AddNodeInput(shapeNode);
				}
			}
		};

		public static void ShowEditor(Graph<SpaceMapNode<Vector3>> graph)
		{
			ShowEditorImpl<SpaceMapGraphWindow, SpaceMapNodeAttribute>(graph);
		}
	}
}