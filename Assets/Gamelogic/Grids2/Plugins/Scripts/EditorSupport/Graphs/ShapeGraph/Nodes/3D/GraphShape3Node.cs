namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node for creating a 3D grid shape from the graph of a 3D shape.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.PrimitiveShapeNode{Gamelogic.Grids2.GridPoint3}" />
	[ShapeNode("Graph/Graph", 3)]
	public class GraphShape3Node : PrimitiveShapeNode<GridPoint3>
	{
		public Shape3Graph graph;

		protected override IExplicitShape<GridPoint3> Generate()
		{
			return graph.GetShape();
		}
	}
}