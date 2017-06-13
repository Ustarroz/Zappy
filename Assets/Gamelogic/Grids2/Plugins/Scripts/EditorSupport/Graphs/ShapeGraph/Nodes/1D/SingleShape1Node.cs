namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Represents a single point.
	/// </summary>
	[ShapeNode("All/Single", 1)]
	public class SingleShape1Node : PrimitiveShapeNode<int>
	{
		protected override IExplicitShape<int> Generate()
		{
			return ExplicitShape.Single1();
		}
	}
}