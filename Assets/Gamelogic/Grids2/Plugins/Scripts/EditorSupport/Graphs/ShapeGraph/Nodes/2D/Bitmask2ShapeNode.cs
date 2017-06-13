using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A node to create a 2D bitmask shape.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.PrimitiveShapeNode{Gamelogic.Grids2.GridPoint2}" />
	[ShapeNode("Polyomino/Bitmask", 2)]
	[Version(2, 3)]
	public class Bitmask2ShapeNode : PrimitiveShapeNode<GridPoint2>
	{
		public string[] mask;

		protected override IExplicitShape<GridPoint2> Generate()
		{
			return ExplicitShape.Bitmask(mask);
		}
	}
}