using System;
using System.Collections.Generic;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Discrete version of the <see cref="UnityEngine.Bounds"/> class. Represents a axis-aligned cuboid with
	/// with vertices at grid points.
	/// </summary>
	[Serializable]
	public class GridBounds : AbstractBounds<GridPoint3>
	{
		public GridBounds(GridPoint3 point, GridPoint3 size)
			: base(point, size)
		{
		}

		public override GridPoint3 Extreme
		{
			get { return Point + Size; }
		}

		public override bool Contains(GridPoint3 point)
		{
			return Point.X <= point.X && point.X < Point.X + Size.X
				&& Point.Y <= point.Y && point.Y < Point.Y + Size.Y
				&& Point.Z <= point.Z && point.Z < Point.Z + Size.Z;
		}

		public override IEnumerable<GridPoint3> Points
		{
			get
			{
				for (int k = 0; k < Bounds.Size.Z; k++)
				{
					for (int j = 0; j < Bounds.Size.Y; j++)
					{
						for (int i = 0; i < Bounds.Size.X; i++)
						{
							var point = Bounds.Point + new GridPoint3(i, j, k);

							yield return point;
						}
					}
				}
			}
		}

		/// <summary>
		/// Returns the bounding box of the union of the two given GridsBounds objects.
		/// </summary>
		/// <param name="bounds1">The rect1.</param>
		/// <param name="bounds2">The rect2.</param>
		/// <returns>GridBounds.</returns>
		public static AbstractBounds<GridPoint3> UnionBoundingBox(AbstractBounds<GridPoint3> bounds1, AbstractBounds<GridPoint3> bounds2)
		{
			var bottomLeft = GridPoint3.Min(bounds1.Point, bounds2.Point);
			var topRight = GridPoint3.Max(bounds1.Extreme, bounds2.Extreme);

			var dimensions = topRight - bottomLeft;

			return new GridBounds(bottomLeft, dimensions);
		}

		public static GridBounds Intersection(AbstractBounds<GridPoint3> bounds1, AbstractBounds<GridPoint3> bounds2)
		{
			var bottomLeft = GridPoint3.Max(bounds1.Point, bounds2.Point);
			var topRight = GridPoint3.Min(bounds1.Extreme, bounds2.Extreme);

			var dimensions = topRight - bottomLeft;

			return new GridBounds(bottomLeft, dimensions);
		}

		public static AbstractBounds<GridPoint3> Translate(AbstractBounds<GridPoint3> bounds, GridPoint3 offset)
		{
			return new GridBounds(bounds.Point + offset, bounds.Size);
		}

		[Version(2, 3)]
		public static AbstractBounds<GridPoint3> Dilate(AbstractBounds<GridPoint3> bounds)
		{
			var point = bounds.Point - GridPoint3.One;
			var size = bounds.Size + 2 * GridPoint3.One;

			return new GridBounds(point, size);
		}

		[Version(2, 3)]
		public static AbstractBounds<GridPoint3> Erode(AbstractBounds<GridPoint3> bounds)
		{
			var point = bounds.Point + GridPoint3.One;
			var size = bounds.Size - 2 * GridPoint3.One;

			return new GridBounds(point, size);
		}
	}
}