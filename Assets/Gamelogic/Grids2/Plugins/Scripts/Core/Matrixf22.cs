using System;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Represents a 2x2 matrices with float values.
	/// </summary>
	public struct Matrixf22
	{
		public static readonly Matrixf22 Identity = new Matrixf22(1, 0, 0, 1);
		public static readonly Matrixf22 Zero = new Matrixf22(0, 0, 0, 0);
		public static readonly Matrixf22 One = new Matrixf22(1, 1, 1, 1);

		public readonly float a, b, c, d;

		public Matrixf22(float a, float b, float c, float d)
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
		}

		//TODO: remove (not efficient)
		public float GetValueAt(int row, int col)
		{
			if (row == 0 && col == 0)
				return a;

			if (row == 0 && col == 1)
				return b;

			if (row == 1 && col == 0)
				return c;

			if (row == 1 && col == 1)
				return d;

			return 0.0f;
		}

		public override string ToString()
		{
			return string.Format("[({0},{1});({2},{3})]", a, b, c, d);
		}

		#region Equality

		public bool Equals(Matrixf22 other)
		{
			var areEqual = (a == other.a) && (b == other.b) && (c == other.c) && (d == other.d);

			return areEqual;
		}

		public override bool Equals(object other)
		{
			if (other.GetType() != typeof(Matrixf22))
			{
				return false;
			}

			var point = (Matrixf22)other;

			return Equals(point);
		}

		public override int GetHashCode()
		{
			unchecked // Wrap when overflows
			{
				int hash = (int)2166136261;

				hash = (hash * 16777619) ^ a.GetHashCode();
				hash = (hash * 16777619) ^ b.GetHashCode();
				hash = (hash * 16777619) ^ c.GetHashCode();
				hash = (hash * 16777619) ^ d.GetHashCode();

				return hash;
			}
		}

		#endregion

		#region Arithmetic

		public Matrixf22 Add(Matrixf22 other)
		{
			return new Matrixf22(
				a + other.a,
				b + other.b,
				c + other.c,
				d + other.d);
		}

		public Matrixf22 Negate()
		{
			return new Matrixf22(-a, -b, -c, -d);
		}

		public Matrixf22 Div(float r)
		{
			return new Matrixf22(
				a / r,
				b / r,
				c / r,
				d / r);
		}

		public Matrixf22 Mul(float r)
		{
			return new Matrixf22(
				a * r,
				b * r,
				c * r,
				d * r);
		}

		public float Det()
		{
			return a * d - b * c;
		}

		public Matrixf22 Inv()
		{
			float determinant = a * d - b * c;

			if (determinant == 0)
				throw new DivideByZeroException("The matrix has a determinant of value 0.");

			float invdet = 1 / determinant;

			float a1 = d * invdet;
			float b1 = -b * invdet;
			float c1 = -c * invdet;
			float d1 = a * invdet;

			return new Matrixf22(a1, b1, c1, d1);
		}

		public Matrixf22 Mul(Matrixf22 other)
		{
			var newA = a * other.a + b * other.c;
			var newB = a * other.b + b * other.d;
			var newC = c * other.a + d * other.c;
			var newD = c * other.b + d * other.d;

			return new Matrixf22(newA, newB, newC, newD);
		}

		public Matrixf22 Div(Matrixf22 other)
		{
			return Mul(other.Inv());
		}

		public Matrixf22 Subtract(Matrixf22 other)
		{
			return new Matrixf22(
				a - other.a,
				b - other.b,
				c - other.c,
				d - other.d);
		}

		#endregion

		#region Operators

		public static bool operator ==(Matrixf22 point1, Matrixf22 point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(Matrixf22 point1, Matrixf22 point2)
		{
			return !point1.Equals(point2);
		}

		public static Matrixf22 operator +(Matrixf22 point)
		{
			return point;
		}

		public static Matrixf22 operator -(Matrixf22 point)
		{
			return point.Negate();
		}

		public static Matrixf22 operator +(Matrixf22 point1, Matrixf22 point2)
		{
			return point1.Add(point2);
		}

		public static Matrixf22 operator -(Matrixf22 point1, Matrixf22 point2)
		{
			return point1.Subtract(point2);
		}

		public static Matrixf22 operator *(Matrixf22 point, float n)
		{
			return point.Mul(n);
		}

		public static Matrixf22 operator /(Matrixf22 point, float n)
		{
			return point.Div(n);
		}

		public static Matrixf22 operator *(Matrixf22 m1, Matrixf22 m2)
		{
			return m1.Mul(m2);
		}

		public static Vector2 operator *(Vector2 v, Matrixf22 m)
		{
			return v.Mul(m);
		}

		public static Matrixf22 operator /(Matrixf22 m1, Matrixf22 m2)
		{
			return m1.Div(m2);
		}

		#endregion

		#region Static Methods

		public static Matrixf22 ScaleMatrix(float scaleFactor)
		{
			return ScaleMatrix(Vector2.one * scaleFactor);
		}

		public static Matrixf22 ScaleMatrix(Vector2 scaleVector)
		{
			return new Matrixf22(
				scaleVector.x, 0.0f,
				0.0f, scaleVector.y);
		}

		public static Matrixf22 RotateClockwise(float angle)
		{
			return new Matrixf22(
				Mathf.Cos(angle), Mathf.Sin(angle),
				-Mathf.Sin(angle), Mathf.Cos(angle));
		}

		public static Matrixf22 RotateCounterClockwise(float angle)
		{
			return new Matrixf22(
				Mathf.Cos(angle), -Mathf.Sin(angle),
				Mathf.Sin(angle), Mathf.Cos(angle));
		}

		#endregion
	}
}