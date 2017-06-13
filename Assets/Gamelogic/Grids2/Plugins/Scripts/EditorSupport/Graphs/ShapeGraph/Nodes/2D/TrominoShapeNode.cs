using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node for creating a tromino grid shape.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.PrimitiveShapeNode{Gamelogic.Grids2.GridPoint2}" />
	[ShapeNode("Polyomino/Tromino", 2)]
	[Version(2, 3)]
	public class TrominoShapeNode : PrimitiveShapeNode<GridPoint2>
	{
		public Polyominoes.Tromino.Type type;

		protected override IExplicitShape<GridPoint2> Generate()
		{
			return Polyominoes.Tromino.Shapes[type];
		}
	}
}