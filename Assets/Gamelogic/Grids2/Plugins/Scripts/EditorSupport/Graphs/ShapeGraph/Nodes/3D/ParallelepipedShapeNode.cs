namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that generates a parallelepiped shape.
	/// </summary>
	[ShapeNode("All/Parallelepiped", 3)]
	class ParallelepipedShapeNode : PrimitiveShapeNode<GridPoint3>
	{
		public InspectableGridPoint3 dimensions;

		protected override IExplicitShape<GridPoint3> Generate()
		{
			var dimensionsGridPoint = dimensions.GetGridPoint();

			return ExplicitShape.Parallelepiped(dimensionsGridPoint);
		}
	}
}