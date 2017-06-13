using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// A discrete 3D vector, used to index into 3D grids.
	/// </summary>
	/// <seealso cref="System.IEquatable{GridPoint3}" />
	[Serializable]
	public struct GridPoint3 : IEquatable<GridPoint3>
	{
		#region Types
		private sealed class VectorLine : IMap<GridPoint3, GridPoint3>
		{
			private readonly GridPoint3 direction;

			public VectorLine(GridPoint3 direction)
			{
				this.direction = direction;
			}

			public GridPoint3 Forward(GridPoint3 input)
			{
				return input + direction;
			}

			public GridPoint3 Reverse(GridPoint3 output)
			{
				return output - direction;
			}
		}
		#endregion

		#region Constants

		public static readonly GridPoint3 Zero = new GridPoint3(0, 0, 0);
		public static readonly GridPoint3 One = new GridPoint3(1, 1, 1);

		#endregion

		#region Fields

		[SerializeField]
		private readonly int x;

		[SerializeField]
		private readonly int y;

		[SerializeField]
		private readonly int z;

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

		public int Z
		{
			get { return z; }
		}

		#endregion

		#region Constructors

		public GridPoint3(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		#endregion

		#region Equality

		public bool Equals(GridPoint3 other)
		{
			var areEqual = (x == other.x) && (y == other.y) && (z == other.z);
			return areEqual;
		}

		public override bool Equals(object other)
		{
			if (other.GetType() != typeof(GridPoint3))
			{
				return false;
			}

			var point = (GridPoint3)other;
			return Equals(point);
		}

		public override int GetHashCode()
		{
			//return x ^ y ^ z;
			unchecked // Wrap when overflows
			{
				int hash = (int)2166136261;

				hash = (hash * 16777619) ^ x.GetHashCode();
				hash = (hash * 16777619) ^ y.GetHashCode();
				hash = (hash * 16777619) ^ z.GetHashCode();

				return hash;
			}
		}

		#endregion

		#region Arithmetic

		public GridPoint3 Add(GridPoint3 other)
		{
			return new GridPoint3(x + other.X, y + other.Y, z + other.z);
		}

		public GridPoint3 Negate()
		{
			return new GridPoint3(-x, -y, -z);
		}

		[Obsolete("Use FloorDiv or TruncDiv instead")]
		public GridPoint3 Div(int r)
		{
			return new GridPoint3(GLMathf.FloorDiv(x, r), GLMathf.FloorDiv(y, r), GLMathf.FloorDiv(z, r));
		}

		[Version(2, 2)]
		public GridPoint3 FloorDiv(int r)
		{
			return new GridPoint3(GLMathf.FloorDiv(x, r), GLMathf.FloorDiv(y, r), GLMathf.FloorDiv(z, r));
		}

		[Version(2, 2)]
		public GridPoint3 TruncDiv(int r)
		{
			return new GridPoint3(x / r, y / r, z / r);
		}

		public GridPoint3 Mul(int r)
		{
			return new GridPoint3(x * r, y * r, z * r);
		}

		public GridPoint3 Subtract(GridPoint3 other)
		{
			return new GridPoint3(x - other.X, y - other.Y, z - other.Z);
		}

		public int Dot(GridPoint3 other)
		{
			return x * other.X + y * other.Y + z * other.Z;
		}

		//TODO: @herman I changed the PerDot method for the CrossProduct
		public GridPoint3 CrossProduct(GridPoint3 other)
		{
			var crossProductX = y * other.z - z * other.y;
			var crossProductY = z * other.y - y * other.z;
			var crossProductZ = x * other.y - y * other.x;

			return new GridPoint3(crossProductX, crossProductY, crossProductZ);
		}

		/// <summary>
		/// Gives a new point that represents the
		/// reminder when the first point is divided
		/// by the second point	component-wise. The
		/// division is integer division.
		/// </summary>
		[Obsolete("Use FloorMod or TruncMod instead")]
		public GridPoint3 Mod(GridPoint3 otherPoint)
		{
			var modX = GLMathf.FloorMod(X, otherPoint.X);
			var modY = GLMathf.FloorMod(Y, otherPoint.Y);
			var modZ = GLMathf.FloorMod(Z, otherPoint.Z);

			return new GridPoint3(modX, modY, modZ);
		}

		/// <summary>
		/// Gives a new point that represents the
		/// reminder when the first point is divided
		/// by the second point	component-wise. The
		/// division is integer division.
		/// </summary>
		[Version(2, 2)]
		public GridPoint3 FloorMod(GridPoint3 otherPoint)
		{
			var modX = GLMathf.FloorMod(X, otherPoint.X);
			var modY = GLMathf.FloorMod(Y, otherPoint.Y);
			var modZ = GLMathf.FloorMod(Z, otherPoint.Z);

			return new GridPoint3(modX, modY, modZ);
		}

		/// <summary>
		/// Gives a new point that represents the
		/// reminder when the first point is divided
		/// by the second point	component-wise. The
		/// division is integer division.
		/// </summary>
		[Version(2, 2)]
		public GridPoint3 TruncMod(GridPoint3 otherPoint)
		{
			var modX = X % otherPoint.X;
			var modY = Y % otherPoint.Y;
			var modZ = Z % otherPoint.Z;

			return new GridPoint3(modX, modY, modZ);
		}

		/// <summary>
		/// Gives a new point that represents the
		/// first point divided by the second point
		/// component-wise. The division is integer
		/// division.
		/// </summary>
		[Obsolete("Use FloorDiv or TruncDiv instead")]
		public GridPoint3 Div(GridPoint3 otherPoint)
		{
			var divX = GLMathf.FloorDiv(X, otherPoint.X);
			var divY = GLMathf.FloorDiv(Y, otherPoint.Y);
			var divZ = GLMathf.FloorDiv(Z, otherPoint.Z);

			return new GridPoint3(divX, divY, divZ);
		}

		/// <summary>
		/// Gives a new point that represents the
		/// first point divided by the second point
		/// component-wise. The division is integer
		/// division.
		/// </summary>
		[Version(2, 2)]
		public GridPoint3 FloorDiv(GridPoint3 otherPoint)
		{
			var divX = GLMathf.FloorDiv(X, otherPoint.X);
			var divY = GLMathf.FloorDiv(Y, otherPoint.Y);
			var divZ = GLMathf.FloorDiv(Z, otherPoint.Z);

			return new GridPoint3(divX, divY, divZ);
		}

		/// <summary>
		/// Gives a new point that represents the
		/// first point divided by the second point
		/// component-wise. The division is integer
		/// division.
		/// </summary>
		[Version(2, 2)]
		public GridPoint3 TruncDiv(GridPoint3 otherPoint)
		{
			var divX = X % otherPoint.X;
			var divY = Y % otherPoint.Y;
			var divZ = Z % otherPoint.Z;

			return new GridPoint3(divX, divY, divZ);
		}

		/// <summary>
		/// Gives a new point that represents the
		/// first point multiplied by the second point
		/// component-wise.
		/// </summary>
		public GridPoint3 Mul(GridPoint3 otherPoint)
		{
			var resX = X * otherPoint.X;
			var resY = Y * otherPoint.Y;
			var resZ = Z * otherPoint.Z;

			return new GridPoint3(resX, resY, resZ);
		}

		public static GridPoint3 Min(GridPoint3 point1, GridPoint3 point2)
		{
			return new GridPoint3(
				Mathf.Min(point1.x, point2.x),
				Mathf.Min(point1.y, point2.y),
				Mathf.Min(point1.z, point2.z));
		}

		public static GridPoint3 Max(GridPoint3 point1, GridPoint3 point2)
		{
			return new GridPoint3(
				Mathf.Max(point1.x, point2.x),
				Mathf.Max(point1.y, point2.y),
				Mathf.Max(point1.z, point2.z));
		}

		[Obsolete("Use Mul instead")]
		public static GridPoint3 HadamardMul(GridPoint3 p1, GridPoint3 p2)
		{
			return new GridPoint3(p1.X * p2.X, p1.Y * p2.Y, p1.Z * p2.Z);
		}

		public Vector3 ToVector3()
		{
			return new Vector3(x, y, z);
		}

		#endregion

		#region Conversions		
		/// <summary>
		/// Convert this point to GridPoint2, keeping the x and y coordinates.
		/// </summary>
		public GridPoint2 To2DXY()
		{
			return new GridPoint2(x, y);
		}

		/// <summary>
		/// Convert this point to GridPoint2, keeping the x and z coordinates.
		/// </summary>
		public GridPoint2 TO2DXZ()
		{
			return new GridPoint2(x, z);
		}

		public static implicit operator Vector3(GridPoint3 point)
		{
			return point.ToVector3();
		}
		#endregion

		#region Utility

		public override string ToString()
		{
			return "(" + x + ", " + y + "," + z + ")";
		}
		#endregion

		#region Operators

		public static bool operator ==(GridPoint3 point1, GridPoint3 point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(GridPoint3 point1, GridPoint3 point2)
		{
			return !point1.Equals(point2);
		}

		public static GridPoint3 operator +(GridPoint3 point)
		{
			return point;
		}

		public static GridPoint3 operator -(GridPoint3 point)
		{
			return point.Negate();
		}

		public static GridPoint3 operator +(GridPoint3 point1, GridPoint3 point2)
		{
			return point1.Add(point2);
		}

		public static GridPoint3 operator -(GridPoint3 point1, GridPoint3 point2)
		{
			return point1.Subtract(point2);
		}

		public static GridPoint3 operator *(GridPoint3 point, int n)
		{
			return point.Mul(n);
		}

		public static GridPoint3 operator *(int n, GridPoint3 point)
		{
			return point.Mul(n);
		}

		public static GridPoint3 operator /(GridPoint3 point, int n)
		{
			return new GridPoint3(point.x / n, point.y / n, point.z / n);
		}

		public static GridPoint3 operator *(GridPoint3 point1, GridPoint3 point2)
		{
			return point1.Mul(point2);
		}

		public static GridPoint3 operator /(GridPoint3 point1, GridPoint3 point2)
		{
			return new GridPoint3(point1.x / point2.x, point1.y / point2.y, point1.z / point2.z);
		}

		public static GridPoint3 operator %(GridPoint3 point1, GridPoint3 point2)
		{
			return new GridPoint3(point1.x % point2.x, point1.y % point2.y, point1.z % point2.z);
		}
		#endregion

		#region Colorings

		public int GetColor(ColorFunction colorFunction)
		{
			return GetColor(colorFunction.x0, colorFunction.x1, colorFunction.y1);
		}

		/// <summary>
		/// Gives a coloring of the grid such that
		/// if a point p has color k, then all points
		///	p + m[ux, 0] + n[vx, vy] have the same color
		///	for any integers a and b.
		///
		///	More information about grid colorings:
		///	http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/
		/// </summary>
		/// <param name="ux">The ux.</param>
		/// <param name="vx">The vx.</param>
		/// <param name="vy">The vy.</param>
		/// <returns>System.Int32.</returns>
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
		public IEnumerable<GridPoint3> GetVectorNeighbors(IEnumerable<GridPoint3> directions)
		{
			var thisCopy = this;

			return directions.Select(p => thisCopy + p);
		}

		public static IMap<GridPoint3, GridPoint3> GetVectorLine(GridPoint3 direction)
		{
			return new VectorLine(direction);
		}

		public static IEnumerable<IMap<GridPoint3, GridPoint3>> GetVectorLines(IEnumerable<GridPoint3> directions)
		{
			return directions.Select(d => GetVectorLine(d));
		}

		public static IEnumerable<IForwardMap<GridPoint3, GridPoint3>> GetForwardVectorRays(IEnumerable<GridPoint3> direction)
		{
			return direction.Select(d => (IForwardMap<GridPoint3, GridPoint3>)GetVectorLine(d));
		}

		public static IEnumerable<IReverseMap<GridPoint3, GridPoint3>> GetReverseVectorRays(IEnumerable<GridPoint3> direction)
		{
			return direction.Select(d => (IReverseMap<GridPoint3, GridPoint3>)GetVectorLine(d));
		}
		#endregion
	}
}