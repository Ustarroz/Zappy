using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that generates a sphere shape.
	/// </summary>
	[ShapeNode("Rect/Sphere", 3)]
	class SphereShapeNode : PrimitiveShapeNode<GridPoint3>
	{
		public float radius = 1;

		protected override IExplicitShape<GridPoint3> Generate()
		{
			int extent = Mathf.CeilToInt(radius);

			return ImplicitShape
				.Sphere(radius)
				.ToExplicit(new GridBounds(
					GridPoint3.One * -extent,
					GridPoint3.One * (2 * extent + 1)));
		}
	}
}