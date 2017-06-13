namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A shape of a single cell at the origin.
	/// </summary>
	[ShapeNode("All/Single", 2)]
	public class SingleShape2Node : PrimitiveShapeNode<GridPoint2>
	{
		protected override IExplicitShape<GridPoint2> Generate()
		{
			return ExplicitShape.Single2();
		}
	}
}