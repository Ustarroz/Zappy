using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node for creating a pentomino grid shape.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.PrimitiveShapeNode{Gamelogic.Grids2.GridPoint2}" />
	[ShapeNode("Polyomino/Pentomino", 2)]
	[Version(2, 3)]
	public class PentominoShapeNode : PrimitiveShapeNode<GridPoint2>
	{
		public Polyominoes.Pentomino.Type type;

		protected override IExplicitShape<GridPoint2> Generate()
		{
			return Polyominoes.Pentomino.Shapes[type];
		}
	}
}