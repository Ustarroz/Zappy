using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node for creating a tetromino grid shape.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.PrimitiveShapeNode{Gamelogic.Grids2.GridPoint2}" />
	[ShapeNode("Polyomino/Tetromino", 2)]
	[Version(2, 3)]
	public class TetrominoShapeNode : PrimitiveShapeNode<GridPoint2>
	{
		public Polyominoes.Tetromino.Type type;

		protected override IExplicitShape<GridPoint2> Generate()
		{
			return Polyominoes.Tetromino.Shapes[type];
		}
	}
}