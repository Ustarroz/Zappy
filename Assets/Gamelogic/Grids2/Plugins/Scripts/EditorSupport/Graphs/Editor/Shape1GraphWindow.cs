namespace Gamelogic.Grids2.Graph.Editor
{
	/// <summary>
	/// A graph window for editing 1D shape graphs.
	/// </summary>
	/// <seealso cref="GraphWindow{ShapeNode{int}}" />
	public class Shape1GraphWindow : GraphWindow<ShapeNode<int>>
	{
		public static void ShowEditor(Graph<ShapeNode<int>> graph)
		{
			ShowEditorImpl<Shape1GraphWindow, ShapeNodeAttribute>(graph);
		}
	}
}