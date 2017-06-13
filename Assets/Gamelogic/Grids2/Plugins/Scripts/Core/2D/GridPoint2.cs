using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// A discrete 2D vector, used to index into 2D grids.
	/// </summary>
	/// <seealso cref="System.IEquatable{GridPoint2}" />
	[Serializable]
	public struct GridPoint2 : IEquatable<GridPoint2>
	{
		#region Types
		private sealed class VectorLine : IMap<GridPoint2, GridPoint2>
		{
			private readonly GridPoint2 direction;

			public VectorLine(GridPoint2 direction)
			{
				this.direction = direction;
			}

			public GridPoint2 Forward(GridPoint2 input)
			{
				return input + direction;
			}

			public GridPoint2 Reverse(GridPoint2 output)
			{
				return output - direction;
			}
		}
		#endregion

		#region Constants

		public static readonly GridPoint2 Zero = new GridPoint2(0, 0);
		public static readonly GridPoint2 One = new GridPoint2(1, 1);

		#endregion

		#region Fields

		[SerializeField]
		private readonly int x;

		[SerializeField]
		private readonly int y;

		#endregion

		#region Properties

		public int X
		{
			get { return x; }
		}

		public int Y
		{
			get { return y; }
		}

		private int Z
		{
			get { return -x - y; }
		}

		#endregion

		#region Constructors

		public GridPoint2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		#endregion

		#region Equality

		public bool Equals(GridPoint2 other)
		{
			bool areEqual = (x == other.x) && (y == other.y);
			return areEqual;
		}

		public override bool Equals(object other)
		{
			if (other.GetType() != typeof(GridPoint2))
			{
				return false;
			}

			var point = (GridPoint2)other;
			return Equals(point);
		}

		public override int GetHashCode()
		{
			//return x ^ y;

			unchecked // Wrap when overflows
			{
				int hash = (int)2166136261;

				hash = (hash * 16777619) ^ x.GetHashCode();
				hash = (hash * 16777619) ^ y.GetHashCode();

				return hash;
			}
		}

		#endregion

		#region Arithmetic

		public GridPoint2 Add(GridPoint2 other)
		{
			return new GridPoint2(x + other.X, y + other.Y);
		}

		public GridPoint2 Negate()
		{
			return new GridPoint2(-x, -y);
		}

		[Obsolete("Use FloorDiv or TruncDiv instead")]
		public GridPoint2 Div(int r)
		{
			return new GridPoint2(GLMathf.FloorDiv(x, r), GLMathf.FloorDiv(y, r));
		}

		[Version(2, 2)]
		public GridPoint2 FloorDiv(int r)
		{
			return new GridPoint2(GLMathf.FloorDiv(x, r), GLMathf.FloorDiv(y, r));
		}

		[Version(2, 2)]
		public GridPoint2 TruncDiv(int r)
		{
			return new GridPoint2(x / r, y / r);
		}

		public GridPoint2 Mul(int r)
		{
			return new GridPoint2(x * r, y * r);
		}

		public GridPoint2 Subtract(GridPoint2 other)
		{
			return new GridPoint2(x - other.X, y - other.Y);
		}

		public int Dot(GridPoint2 other)
		{
			return x * other.X + y * other.Y;
		}

		public int PerpDot(GridPoint2 other)
		{
			return x * other.Y - y * other.x;
		}

		public int HexDot(GridPoint2 other)
		{
			return Dot(other) + Z * other.Z;
		}

		public GridPoint2 Perp()
		{
			return new GridPoint2(-y, x);
		}

		/// <summary>
		/// Gives a new point that represents the
		/// reminder when the first point is divided
		/// by the second point component-wise.The
		///	division is integer division.
		/// </summary>
		[Obsolete("Use FloorMod or TruncMod instead")]
		public GridPoint2 Mod(GridPoint2 otherPoint)
		{
			return new GridPoint2(
				GLMathf.FloorMod(X, otherPoint.X),
				GLMathf.FloorMod(Y, otherPoint.Y));
		}

		[Version(2, 2)]
		public GridPoint2 FloorMod(GridPoint2 otherPoint)
		{
			return new GridPoint2(
				GLMathf.FloorMod(X, otherPoint.X),
				GLMathf.FloorMod(Y, otherPoint.Y));
		}

		[Version(2, 2)]
		public GridPoint2 TruncMod(GridPoint2 otherPoint)
		{
			return new GridPoint2(X % otherPoint.X, Y % otherPoint.Y);
		}

		/// <summary>
		/// Gives a new point that represents the
		/// first point divided by the second point
		/// component-wise.The division is integer
		/// division.
		/// </summary>
		[Obsolete("Use FloorDiv or TruncDiv instead")]
		public GridPoint2 Div(GridPoint2 otherPoint)
		{
			return new GridPoint2(
				GLMathf.FloorDiv(X, otherPoint.X),
				GLMathf.FloorDiv(Y, otherPoint.Y));
		}

		/// <summary>
		/// Gives a new point that represents the
		/// first point divided by the second point
		/// component-wise.The division is integer
		/// division.
		/// </summary>
		[Version(2, 2)]
		public GridPoint2 FloorDiv(GridPoint2 otherPoint)
		{
			return new GridPoint2(
				GLMathf.FloorDiv(X, otherPoint.X),
				GLMathf.FloorDiv(Y, otherPoint.Y));
		}

		/// <summary>
		/// Gives a new point that represents the
		/// first point divided by the second point
		/// component-wise.The division is integer
		/// division.
		/// </summary>
		[Version(2, 2)]
		public GridPoint2 TruncDiv(GridPoint2 otherPoint)
		{
			return new GridPoint2(X / otherPoint.X, Y / otherPoint.Y);
		}

		/// <summary>
		/// Gives a new point that represents the
		/// first point multiplied by the second point
		///	component-wise.
		/// </summary>
		/// <param name="otherPoint">The other point.</param>
		/// <returns>GridPoint2.</returns>
		public GridPoint2 Mul(GridPoint2 otherPoint)
		{
			return new GridPoint2(X * otherPoint.X, Y * otherPoint.Y);
		}

		public static GridPoint2 Min(GridPoint2 point1, GridPoint2 point2)
		{
			return new GridPoint2(Mathf.Min(point1.x, point2.x), Mathf.Min(point1.y, point2.y));
		}

		public static GridPoint2 Max(GridPoint2 point1, GridPoint2 point2)
		{
			return new GridPoint2(Mathf.Max(point1.x, point2.x), Mathf.Max(point1.y, point2.y));
		}

		[Obsolete("Use Mul instead")]
		public static GridPoint2 HadamardMul(GridPoint2 p1, GridPoint2 p2)
		{
			return new GridPoint2(p1.X * p2.X, p1.Y * p2.Y);
		}

		public Vector2 ToVector2()
		{
			return new Vector2(x, y);
		}

		#endregion

		#region Conversions		
		/// <summary>
		/// Converts this point to GridPoint 3.
		/// </summary>
		/// <param name="newZ">The new z coordinate.</param>
		public GridPoint3 To3DXY(int newZ = 0)
		{
			return new GridPoint3(x, y, newZ);
		}

		/// <summary>
		/// Converts the point to GridPoint2.
		/// </summary>
		/// <param name="newY">The new y coordinate.</param>
		public GridPoint3 To3DXZ(int newY = 0)
		{
			return new GridPoint3(x, newY, y);
		}

		public static implicit operator Vector2(GridPoint2 point)
		{
			return point.ToVector2();
		}

		#endregion

		#region Utility

		public override string ToString()
		{
			return "(" + x + ", " + y + ")";
		}

		#endregion

		#region Operators

		public static bool operator ==(GridPoint2 point1, GridPoint2 point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(GridPoint2 point1, GridPoint2 point2)
		{
			return !point1.Equals(point2);
		}

		public static GridPoint2 operator +(GridPoint2 point)
		{
			return point;
		}

		public static GridPoint2 operator -(GridPoint2 point)
		{
			return point.Negate();
		}

		public static GridPoint2 operator +(GridPoint2 point1, GridPoint2 point2)
		{
			return point1.Add(point2);
		}

		public static GridPoint2 operator -(GridPoint2 point1, GridPoint2 point2)
		{
			return point1.Subtract(point2);
		}

		public static GridPoint2 operator *(GridPoint2 point, int n)
		{
			return point.Mul(n);
		}

		public static GridPoint2 operator *(int n, GridPoint2 point)
		{
			return point.Mul(n);
		}

		public static GridPoint2 operator /(GridPoint2 point, int n)
		{
			return new GridPoint2(point.x / n, point.y / n);
		}

		public static GridPoint2 operator *(GridPoint2 point1, GridPoint2 point2)
		{
			return point1.Mul(point2);
		}

		public static GridPoint2 operator /(GridPoint2 point1, GridPoint2 point2)
		{
			return new GridPoint2(point1.x / point2.x, point1.y / point2.y);
		}

		public static GridPoint2 operator %(GridPoint2 point1, GridPoint2 point2)
		{
			return new GridPoint2(point1.x % point2.x, point1.y % point2.y);
		}
		#endregion

		#region Colorings

		public int GetColor(ColorFunction colorFunction)
		{
			return GetColor(colorFunction.x0, colorFunction.x1, colorFunction.y1);
		}
		/**
			Gives a coloring of the grid such that 
			if a point p has color k, then all points
			p + m[ux, 0] + n[vx, vy] have the same color
			for any integers a and b.

			More information anout grid colorings:
			http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/

			@since 1.7
		*/

		public int GetColor(int ux, int vx, int vy)
		{
			int colorCount = ux * vy;

			float a = (x * vy - y * vx) / (float)colorCount;
			float b = (y * ux) / (float)colorCount;

			int m = Mathf.FloorToInt(a);
			int n = Mathf.FloorToInt(b);

			int baseVectorX = m * ux + n * vx;
			int baseVectorY = n * vy;

			int offsetX = GLMathf.FloorMod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathf.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}

		#endregion

		#region Neighbors and Lines
		public IEnumerable<GridPoint2> GetVectorNeighbors(IEnumerable<GridPoint2> directions)
		{
			var thisCopy = this;

			return directions.Select(p => thisCopy + p);
		}

		public static IMap<GridPoint2, GridPoint2> GetVectorLine(GridPoint2 direction)
		{
			return new VectorLine(direction);
		}

		public static IEnumerable<IMap<GridPoint2, GridPoint2>> GetVectorLines(IEnumerable<GridPoint2> direction)
		{
			return direction.Select<GridPoint2, IMap<GridPoint2, GridPoint2>>(GetVectorLine);
		}

		public IEnumerable<IForwardMap<GridPoint2, GridPoint2>> GetForwardVectorRays(IEnumerable<GridPoint2> direction)
		{
			return direction.Select(d => (IForwardMap<GridPoint2, GridPoint2>)GetVectorLine(d));
		}

		public IEnumerable<IReverseMap<GridPoint2, GridPoint2>> GetReverseVectorRays(IEnumerable<GridPoint2> direction)
		{
			return direction.Select(d => (IReverseMap<GridPoint2, GridPoint2>)GetVectorLine(d));
		}
		#endregion
	}
}
