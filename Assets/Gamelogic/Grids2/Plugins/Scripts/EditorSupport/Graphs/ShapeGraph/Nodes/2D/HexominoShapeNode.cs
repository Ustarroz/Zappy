using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node for creating a hexomino grid shape.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.PrimitiveShapeNode{Gamelogic.Grids2.GridPoint2}" />
	[ShapeNode("Polyomino/Hexomino", 2)]
	[Version(2, 3)]
	public class HexominoShapeNode : PrimitiveShapeNode<GridPoint2>
	{
		public Polyominoes.Hexomino.Type type;

		protected override IExplicitShape<GridPoint2> Generate()
		{
			return Polyominoes.Hexomino.Shapes[type];
		}
	}
}