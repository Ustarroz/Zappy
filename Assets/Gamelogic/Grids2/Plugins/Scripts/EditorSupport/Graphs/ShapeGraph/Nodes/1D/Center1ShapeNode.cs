namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that centers its input nodes.
	/// </summary>
	[ShapeNode("Operator/Center", 1)]
	public class Center1ShapeNode : ProjectShapeNode<int, IExplicitShape<int>, IExplicitShape<int>>
	{
		public override IExplicitShape<int> Transform(IExplicitShape<int> input)
		{
			return input.CenterOnOrigin();
		}
	}
}