namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that translates its input nodes.
	/// </summary>
	[ShapeNode("Operator/Translate", 2)]
	public class Translate2ShapeNode : ProjectShapeNode<GridPoint2, IExplicitShape<GridPoint2>, IExplicitShape<GridPoint2>>
	{
		public InspectableGridPoint2 offset;

		public override IExplicitShape<GridPoint2> Transform(IExplicitShape<GridPoint2> input)
		{
			var offset1 = offset.GetGridPoint();
			var shape = input.Translate(offset1);
			var storageRect = GridRect.Translate(input.Bounds, offset1);

			return shape.ToExplicit(storageRect);
		}
	}
}