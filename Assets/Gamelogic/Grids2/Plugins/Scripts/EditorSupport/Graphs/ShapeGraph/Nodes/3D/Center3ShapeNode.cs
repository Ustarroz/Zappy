namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that centers its input nodes.
	/// </summary>
	[ShapeNode("Operator/Center", 3)]
	public class Center3ShapeNode : ProjectShapeNode<GridPoint3, IExplicitShape<GridPoint3>, IExplicitShape<GridPoint3>>
	{
		public override IExplicitShape<GridPoint3> Transform(IExplicitShape<GridPoint3> input)
		{
			return input.CenterOnOrigin();
		}
	}
}