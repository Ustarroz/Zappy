namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that translates its input nodes.
	/// </summary>
	[ShapeNode("Operator/Translate", 3)]
	public class Translate3ShapeNode : ProjectShapeNode<GridPoint3, IExplicitShape<GridPoint3>, IExplicitShape<GridPoint3>>
	{
		public InspectableGridPoint3 offset;

		public override IExplicitShape<GridPoint3> Transform(IExplicitShape<GridPoint3> input)
		{
			var offset1 = offset.GetGridPoint();
			var shape = input.Translate(offset1);
			var storageRect = GridBounds.Translate(input.Bounds, offset1);

			return shape.ToExplicit(storageRect);
		}
	}
}