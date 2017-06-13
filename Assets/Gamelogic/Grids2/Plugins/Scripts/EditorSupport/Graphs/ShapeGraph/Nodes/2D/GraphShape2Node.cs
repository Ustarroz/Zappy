namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node for creating a 2D grid shape from the graph of a 3D shape.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.PrimitiveShapeNode{Gamelogic.Grids2.GridPoint2}" />
	[ShapeNode("Graph/Graph", 2)]
	public class GraphShape2Node : PrimitiveShapeNode<GridPoint2>
	{
		public Shape2Graph graph;

		protected override IExplicitShape<GridPoint2> Generate()
		{
			return graph.GetShape();
		}
	}
}