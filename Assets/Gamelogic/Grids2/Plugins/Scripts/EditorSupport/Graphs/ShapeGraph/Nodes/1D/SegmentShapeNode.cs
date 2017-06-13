namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A node for making a segment shape.
	/// </summary>
	[ShapeNode("Segment", 1)]
	public class SegmentShapeNode : PrimitiveShapeNode<int>
	{
		public int size = 1;

		protected override IExplicitShape<int> Generate()
		{
			return ExplicitShape.Segment(size);
		}
	}
}