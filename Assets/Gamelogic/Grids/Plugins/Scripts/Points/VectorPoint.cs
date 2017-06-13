//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids
{
	/// <summary>
	/// A VectorPoint behaves like a vector, except that it's coordinates are integers.
	/// </summary>
	[Version(1)]
	[Serializable]
	[Immutable]
	public struct VectorPoint :
		IVectorPoint<VectorPoint>,
		IEquatable<VectorPoint>
	{
		public static readonly VectorPoint Zero = new VectorPoint(0, 0);

		private readonly int x;
		private readonly int y;

		public int X
		{
			get { return x; }
		}

		public int Y
		{
			get { return y; }
		}

		public VectorPoint(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public VectorPoint Translate(VectorPoint translation)
		{
			return new VectorPoint(x + translation.x, y + translation.y);
		}

		public VectorPoint Negate()
		{
			return new VectorPoint(-x, -y);
		}

		public VectorPoint ScaleDown(int r)
		{
			return new VectorPoint(
				GLMathf.FloorDiv(x, r),
				GLMathf.FloorDiv(y, r));
		}

		public VectorPoint ScaleUp(int r)
		{
			return new VectorPoint(x * r, y * r);
		}

		public VectorPoint Div(VectorPoint other)
		{
			var newX = GLMathf.FloorDiv(x, other.X);
			var newY = GLMathf.FloorDiv(y, other.Y);

			return new VectorPoint(newX, newY);
		}

		public VectorPoint Mod(VectorPoint other)
		{
			var newX = GLMathf.FloorMod(x, other.X);
			var newY = GLMathf.FloorMod(y, other.Y);

			return new VectorPoint(newX, newY);
		}

		public VectorPoint Mul(VectorPoint other)
		{
			var newX = x * other.X;
			var newY = y * other.Y;

			return new VectorPoint(newX, newY);
		}

		public VectorPoint Subtract(VectorPoint other)
		{
			return new VectorPoint(x - other.x, y - other.y);
		}

		public override string ToString()
		{
			return "(" + x + ", " + y + ")";
		}

		public bool Equals(VectorPoint other)
		{
			return (x == other.X) && (y == other.Y);
		}

		public override bool Equals(object other)
		{
			if (other is VectorPoint)
			{
				return Equals((VectorPoint)other);
			}

			return false;
		}

		public override int GetHashCode()
		{
			return x ^ y;
		}

		public static bool operator ==(VectorPoint point1, VectorPoint point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(VectorPoint point1, VectorPoint point2)
		{
			return !point1.Equals(point2);
		}

		public static VectorPoint operator +(VectorPoint point)
		{
			return point;
		}

		public static VectorPoint operator -(VectorPoint point)
		{
			return point.Negate();
		}

		public static VectorPoint operator +(VectorPoint point1, VectorPoint point2)
		{
			return point1.Translate(point2);
		}

		public static VectorPoint operator -(VectorPoint point1, VectorPoint point2)
		{
			return point1.Subtract(point2);
		}

		public static VectorPoint operator *(VectorPoint point, int n)
		{
			return point.ScaleUp(n);
		}

		public static VectorPoint operator /(VectorPoint point, int n)
		{
			return point.ScaleDown(n);
		}

		public int Magnitude()
		{
			return Mathf.Abs(x) + Mathf.Abs(y);
		}

		public VectorPoint MoveBy(VectorPoint other)
		{
			return Translate(other);
		}

		public VectorPoint MoveBackBy(VectorPoint other)
		{
			return Translate(other.Negate());
		}

		[Version(1,7)]
		public int Dot(VectorPoint other)
		{
			return x*other.x + y*other.y;
		}

		[Version(1,7)]
		public int PerpDot(VectorPoint other)
		{
			return x*other.y - y*other.x;
		}
	}
}