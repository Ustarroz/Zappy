namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node for creating a 1D grid shape from the graph of a 3D shape.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.PrimitiveShapeNode{System.Int32}" />
	[ShapeNode("Graph/Graph", 1)]
	public class GraphShape1Node : PrimitiveShapeNode<int>
	{
		public Shape1Graph graph;

		protected override IExplicitShape<int> Generate()
		{
			return graph.GetShape();
		}
	}
}