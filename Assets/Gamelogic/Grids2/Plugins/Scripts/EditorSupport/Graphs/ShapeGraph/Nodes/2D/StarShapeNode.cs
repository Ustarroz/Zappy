using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that generates a star shape.
	/// </summary>
	[ShapeNode("Hex/Star", 2)]
	class StarShapeNode : PrimitiveShapeNode<GridPoint2>
	{
		public Vector2 center = Vector2.zero;

		[NonNegative]
		public float radius = 1;

		protected override IExplicitShape<GridPoint2> Generate()
		{
			return ExplicitShape.Hex.Star(center, radius);
		}
	}
}
