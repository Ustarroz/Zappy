using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that generates a square shape.
	/// </summary>
	[ShapeNode("Rect/Square", 2)]
	public class SquareShapeNode : PrimitiveShapeNode<GridPoint2>
	{
		public Vector2 center;

		[NonNegative]
		public float radius;

		protected override IExplicitShape<GridPoint2> Generate()
		{
			return ExplicitShape.Rect.Square(center, radius);
		}
	}
}