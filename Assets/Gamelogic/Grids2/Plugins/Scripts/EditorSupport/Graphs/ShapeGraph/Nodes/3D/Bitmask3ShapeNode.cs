using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A node to create a 3D bitmask shape.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.PrimitiveShapeNode{Gamelogic.Grids2.GridPoint3}" />
	[ShapeNode("Bitmask", 3)]
	[Version(2, 3)]
	public class Bitmask3ShapeNode : PrimitiveShapeNode<GridPoint3>
	{
		public string[][] mask;

		protected override IExplicitShape<GridPoint3> Generate()
		{
			return ExplicitShape.Bitmask(mask);
		}
	}
}