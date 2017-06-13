namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Swap the coordinates of the shape.
	/// </summary>
	/// <seealso cref="ProjectShapeNode{TPoint,TInput,TOutput}"/>
	[ShapeNode("Operator/Swap Coordinates 2D", 2)]
	class SwapCoordinatesShape2Node : ProjectShapeNode<GridPoint2, IExplicitShape<GridPoint2>, IExplicitShape<GridPoint2>>
	{
		public override IExplicitShape<GridPoint2> Transform(IExplicitShape<GridPoint2> input)
		{
			return input.SwapXY();
		}
	}
}
