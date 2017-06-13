using System;
using System.Collections.Generic;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Represents a discrete interval. This is the 1D version of GridRect and GridBounds.
	/// </summary>
	[Serializable] //TODO: Is this closed or open? Answer: Halfopen (on the right)
	public class GridInterval : AbstractBounds<int>
	{
		public GridInterval(int point, int size) : base(point, size)
		{
		}

		public override int Extreme
		{
			get { return Point + Size; }
		}

		public override bool Contains(int point)
		{
			return point >= this.Point && point < Extreme;
		}

		public override IEnumerable<int> Points
		{
			get
			{
				for (int i = 0; i < Bounds.Size; i++)
				{
					var point = Bounds.Point + i;

					yield return point;
				}
			}
		}

		public static AbstractBounds<int> UnionBoundingBox(AbstractBounds<int> rect1, AbstractBounds<int> rect2)
		{
			var bottomLeft = Mathf.Min(rect1.Point, rect2.Point);
			var topRight = Mathf.Max(rect1.Extreme, rect2.Extreme);
			var dimensions = topRight - bottomLeft;

			return new GridInterval(bottomLeft, dimensions);
		}

		public static AbstractBounds<int> Intersection(AbstractBounds<int> rect1, AbstractBounds<int> rect2)
		{
			var bottomLeft = Mathf.Max(rect1.Point, rect2.Point);
			var topRight = Mathf.Min(rect1.Extreme, rect2.Extreme);
			var dimensions = topRight - bottomLeft;

			return new GridInterval(bottomLeft, dimensions);
		}

		public static AbstractBounds<int> Translate(AbstractBounds<int> rect, int offset)
		{
			return new GridInterval(rect.Point + offset, rect.Size);
		}


		[Version(2, 3)]
		public static AbstractBounds<int> Dilate(AbstractBounds<int> rect)
		{
			var point = rect.Point - 1;
			var size = rect.Size + 2 * 1;

			return new GridInterval(point, size);
		}

		[Version(2, 3)]
		public static AbstractBounds<int> Erode(AbstractBounds<int> rect)
		{
			var point = rect.Point + 1;
			var size = rect.Size - 2 * 1;

			return new GridInterval(point, size);
		}
	}
}