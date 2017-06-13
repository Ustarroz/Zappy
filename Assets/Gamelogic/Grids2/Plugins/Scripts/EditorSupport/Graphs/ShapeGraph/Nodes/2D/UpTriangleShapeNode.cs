using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that generates a up triangle shape.
	/// </summary>
	[ShapeNode("Hex/Up Triangle", 2)]
	class UpTriangleShapeNode : PrimitiveShapeNode<GridPoint2>
	{
		public Vector2 center = Vector2.zero;

		[NonNegative]
		public float radius = 1;

		protected override IExplicitShape<GridPoint2> Generate()
		{
			return ExplicitShape.Hex.UpTriangle(center, radius);
		}
	}
}
