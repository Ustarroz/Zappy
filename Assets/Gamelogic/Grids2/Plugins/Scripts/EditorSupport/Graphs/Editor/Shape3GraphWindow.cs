namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// A graph window for editing 2D shape graphs.
	/// </summary>
	/// <seealso cref="GraphWindow{ShapeNode{GridPoint3}}" />
	public class Shape3GraphWindow : GraphWindow<ShapeNode<GridPoint3>>
	{
		public static void ShowEditor(Graph<ShapeNode<GridPoint3>> graph)
		{
			ShowEditorImpl<Shape3GraphWindow, ShapeNodeAttribute>(graph);
		}
	}
}