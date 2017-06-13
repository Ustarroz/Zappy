namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that translates its input nodes.
	/// </summary>
	[ShapeNode("Operator/Translate", 1)]
	public class Translate1ShapeNode : ProjectShapeNode<int, IExplicitShape<int>, IExplicitShape<int>>
	{
		public int offset;

		public override IExplicitShape<int> Transform(IExplicitShape<int> input)
		{
			var shape = input.Translate(offset);
			var storageRect = GridInterval.Translate(input.Bounds, offset);

			return shape.ToExplicit(storageRect);
		}
	}
}