namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// The output node for 2D shape graphs.
	/// </summary>
	/// <seealso cref="ProjectShapeNode{GridPoint2, IExplicitShape{GridPoint2}, IExplicitShape{GridPoint2}}" />
	[ShapeNode("Output/Output", 2)]
	public class OutputShape2Node : ProjectShapeNode<GridPoint2, IExplicitShape<GridPoint2>, IExplicitShape<GridPoint2>>
	{
		public override IExplicitShape<GridPoint2> Transform(IExplicitShape<GridPoint2> input)
		{
			return input;
		}
	}
}