
using System.Collections.Generic;

namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// A graph window for editing 2D shape graphs.
	/// </summary>
	/// <seealso cref="GraphWindow{ShapeNode{GridPoint2}}" />
	public class Shape2GraphWindow : GraphWindow<ShapeNode<GridPoint2>>
	{
		public static List<GraphPreset<ShapeNode<GridPoint2>>> presets = new List<GraphPreset<ShapeNode<GridPoint2>>>
		{
			new GraphPreset<ShapeNode<GridPoint2>>
			{
				Name = "Rectangle",
				AddTo = graph =>
				{
					var outputNode = AddNodeToGraph<OutputShape2Node>(graph);
					var shapeNode = AddNodeToGraph<ParallelogramShapeNode>(graph);

					shapeNode.dimensions.x = 10;
					shapeNode.dimensions.y = 10;

					outputNode.AddNodeInput(shapeNode);
				}
			},

			new GraphPreset<ShapeNode<GridPoint2>>
			{
				Name = "Hexagon",
				AddTo = graph =>
				{
					var outputNode = AddNodeToGraph<OutputShape2Node>(graph);
					var shapeNode = AddNodeToGraph<HexagonShapeNode>(graph);

					shapeNode.radius = 5;

					outputNode.AddNodeInput(shapeNode);
				}
			},

			new GraphPreset<ShapeNode<GridPoint2>>
			{
				Name = "Hex Rectangle",
				AddTo = graph =>
				{
					var outputNode = AddNodeToGraph<OutputShape2Node>(graph);
					var shapeNode = AddNodeToGraph<HexRectangleShapeNode>(graph);

					shapeNode.dimensions.x = 9;
					shapeNode.dimensions.y = 9;

					shapeNode.type = HexRectangleShapeNode.RectangleType.Fat;

					outputNode.AddNodeInput(shapeNode);
				}
			}
		};

		public static void ShowEditor(Graph<ShapeNode<GridPoint2>> graph)
		{
			ShowEditorImpl<Shape2GraphWindow, ShapeNodeAttribute>(graph);
		}

		protected override List<GraphPreset<ShapeNode<GridPoint2>>> GetPresets()
		{
			return presets;
		}
	}
}