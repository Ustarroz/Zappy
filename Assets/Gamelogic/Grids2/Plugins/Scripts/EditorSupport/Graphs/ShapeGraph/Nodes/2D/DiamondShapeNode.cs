using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that generates a (rect) diamond shape.
	/// </summary>
	[ShapeNode("Rect/Diamond", 2)]
	class DiamondShapeNode : PrimitiveShapeNode<GridPoint2>
	{
		public Vector2 center = Vector2.zero;

		[NonNegative]
		public float radius;

		protected override IExplicitShape<GridPoint2> Generate()
		{
			return ExplicitShape.Rect.Diamond(center, radius);
		}
	}
}
