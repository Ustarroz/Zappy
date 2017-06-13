using System;
using System.Collections.Generic;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Represents an axis-aligned rectangle. This is the discrete version of
	/// <see cref="Rect"/>.
	/// </summary>
	[Serializable]
	public class GridRect : AbstractBounds<GridPoint2>
	{
		public GridRect(GridPoint2 point, GridPoint2 size) : base(point, size)
		{ }

		public override GridPoint2 Extreme
		{
			get { return Point + Size; }
		}

		public override bool Contains(GridPoint2 point)
		{
			return Point.X <= point.X && point.X < Point.X + Size.X
				&& Point.Y <= point.Y && point.Y < Point.Y + Size.Y;
		}

		public override IEnumerable<GridPoint2> Points
		{
			get
			{
				for (int j = 0; j < Bounds.Size.Y; j++)
				{
					for (int i = 0; i < Bounds.Size.X; i++)
					{
						var point = Bounds.Point + new GridPoint2(i, j);

						yield return point;
					}
				}
			}
		}

		public static GridRect UnionBoundingBox(AbstractBounds<GridPoint2> rect1, AbstractBounds<GridPoint2> rect2)
		{
			var bottomLeft = GridPoint2.Min(rect1.Point, rect2.Point);
			var topRight = GridPoint2.Max(rect1.Extreme, rect2.Extreme);
			var dimensions = topRight - bottomLeft;

			return new GridRect(bottomLeft, dimensions);
		}

		public static GridRect Intersection(AbstractBounds<GridPoint2> rect1, AbstractBounds<GridPoint2> rect2)
		{
			var bottomLeft = GridPoint2.Max(rect1.Point, rect2.Point);
			var topRight = GridPoint2.Min(rect1.Extreme, rect2.Extreme);

			var dimensions = topRight - bottomLeft;

			return new GridRect(bottomLeft, dimensions);
		}

		public static AbstractBounds<GridPoint2> Translate(AbstractBounds<GridPoint2> rect, GridPoint2 offset)
		{
			return new GridRect(rect.Point + offset, rect.Size);
		}

		[Version(2, 3)]
		public static AbstractBounds<GridPoint2> Dilate(AbstractBounds<GridPoint2> rect)
		{
			var point = rect.Point - GridPoint2.One;
			var size = rect.Size + 2 * GridPoint2.One;

			return new GridRect(point, size);
		}

		[Version(2, 3)]
		public static AbstractBounds<GridPoint2> Erode(AbstractBounds<GridPoint2> rect)
		{
			var point = rect.Point + GridPoint2.One;
			var size = rect.Size - 2 * GridPoint2.One;

			return new GridRect(point, size);
		}
	}
}