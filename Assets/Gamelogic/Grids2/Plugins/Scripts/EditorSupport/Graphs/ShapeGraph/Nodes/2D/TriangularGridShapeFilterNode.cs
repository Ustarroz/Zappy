namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Filters the input shapes so that they can be used for a triangular grid.
	/// </summary>
	/// <seealso cref="ProjectShapeNode{GridPoint2, IExplicitShape{GridPoint2}, IExplicitShape{GridPoint2}}" />
	[ShapeNode("Shape Filters/Triangular", 2)]
	public class TriangularGridShapeFilterNode : ProjectShapeNode<GridPoint2, IExplicitShape<GridPoint2>, IExplicitShape<GridPoint2>>
	{
		public override IExplicitShape<GridPoint2> Transform(IExplicitShape<GridPoint2> input)
		{
			return input.Where(x => x.GetColor(3, 1, 1) != 0);
		}

		public override void Recompute()
		{
			//NOP
		}
	}
}