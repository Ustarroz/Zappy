using Gamelogic.Grids2.Graph;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	[ShapeNode("Tri/Hexagon", 2)]
	public class HexShapeForTri : PrimitiveShapeNode<GridPoint2>
	{
		public int radius = 1;

		protected override IExplicitShape<GridPoint2> Generate()
		{
			var rotated = ImplicitShape
				.Func<GridPoint2>(p => Contains(p.ToVector2()))
				.ToExplicit(new GridRect(-2 * GridPoint2.One * radius, 5 * GridPoint2.One * radius));

			return rotated;
		}

		private bool Contains(Vector2 v)
		{
			var d = Mathf.Abs(v.x + 2 * v.y) + Mathf.Abs(2 * v.x + v.y) + +Mathf.Abs(v.x - v.y);

			return d < 6 * radius - 1;
		}
	}
}