using System;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Represents an interval. This is the 1D analog of <see cref="Rect"/> and <see cref="Bounds"/>
	/// </summary>
	[Serializable]
	public class Interval
	{
		public readonly float left;
		public readonly float dimensions;

		public Interval(float left, float dimensions)
		{
			this.left = left;
			this.dimensions = dimensions;
		}

		public float Right
		{
			get { return left + dimensions; }
		}

		public static Interval UnionBoundingBox(Interval rect1, Interval rect2)
		{
			var bottomLeft = Mathf.Min(rect1.left, rect2.left);
			var topRight = Mathf.Max(rect1.Right, rect2.Right);
			var dimensions = topRight - bottomLeft;

			return new Interval(bottomLeft, dimensions);
		}

		public static Interval Intersection(Interval rect1, Interval rect2)
		{
			var bottomLeft = Mathf.Max(rect1.left, rect2.left);
			var topRight = Mathf.Min(rect1.Right, rect2.Right);

			var dimensions = topRight - bottomLeft;

			return new Interval(bottomLeft, dimensions);
		}

		public static Interval Translate(Interval rect, float offset)
		{
			return new Interval(rect.left + offset, rect.dimensions);
		}

		public bool Contains(float point)
		{
			return point >= left && point <= Right;
		}
	}
}