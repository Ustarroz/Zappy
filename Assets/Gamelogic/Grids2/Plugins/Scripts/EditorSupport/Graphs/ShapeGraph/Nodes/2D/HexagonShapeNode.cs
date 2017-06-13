using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that generates a hexagon shape.
	/// </summary>
	[ShapeNode("Hex/Hexagon", 2)]
	public class HexagonShapeNode : PrimitiveShapeNode<GridPoint2>
	{
		public Vector2 center;

		[NonNegative]
		public float radius = 1;

		protected override IExplicitShape<GridPoint2> Generate()
		{
			return ExplicitShape.Hex.Hexagon(center, radius);
		}
	}
}
