namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that centers its input nodes.
	/// </summary>
	[ShapeNode("Operator/Center", 2)]
	public class Center2ShapeNode : ProjectShapeNode<GridPoint2, IExplicitShape<GridPoint2>, IExplicitShape<GridPoint2>>
	{
		public override IExplicitShape<GridPoint2> Transform(IExplicitShape<GridPoint2> input)
		{
			return input.CenterOnOrigin();
		}
	}
}