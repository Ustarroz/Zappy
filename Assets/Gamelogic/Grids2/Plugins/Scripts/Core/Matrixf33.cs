using System;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Represents a 3x3 matrices with float values.
	/// </summary>
	public struct Matrixf33
	{
		public static readonly Matrixf33 Identity = new Matrixf33(1, 0, 0, 0, 1, 0, 0, 0, 1);
		public static readonly Matrixf33 Zero = new Matrixf33(0, 0, 0, 0, 0, 0, 0, 0, 0);
		public static readonly Matrixf33 One = new Matrixf33(1, 1, 1, 1, 1, 1, 1, 1, 1);

		public readonly float
			a, b, c,
			d, e, f,
			g, h, i;

		public Matrixf33(float a, float b, float c, float d, float e, float f, float g, float h, float i)
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
			this.e = e;
			this.f = f;
			this.g = g;
			this.h = h;
			this.i = i;
		}

		//TODO: remove (not efficient)
		public float GetValueAt(int row, int col)
		{
			if (row == 0 && col == 0)
				return a;

			if (row == 0 && col == 1)
				return b;

			if (row == 0 && col == 2)
				return c;

			if (row == 1 && col == 0)
				return d;

			if (row == 1 && col == 1)
				return e;

			if (row == 1 && col == 2)
				return f;

			if (row == 2 && col == 0)
				return g;

			if (row == 2 && col == 1)
				return h;

			if (row == 2 && col == 2)
				return i;

			return 0.0f;
		}

		public override string ToString()
		{
			return string.Format(
				"[({0}, {1}, {2})," +
				" ({3}, {4}, {5})," +
				" ({6}, {7}, {8})]",
				a, b, c,
				d, e, f,
				g, h, i
				);
		}

		#region Equality

		public bool Equals(Matrixf33 other)
		{
			var areEqual =
				(a == other.a) && (b == other.b) && (c == other.c) &&
				(d == other.d) && (e == other.e) && (f == other.f) &&
				(g == other.g) && (h == other.h) && (i == other.i);

			return areEqual;
		}

		public override bool Equals(object other)
		{
			if (other.GetType() != typeof(Matrixf33))
			{
				return false;
			}

			var point = (Matrixf33)other;

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
				hash = (hash * 16777619) ^ e.GetHashCode();
				hash = (hash * 16777619) ^ f.GetHashCode();
				hash = (hash * 16777619) ^ g.GetHashCode();
				hash = (hash * 16777619) ^ h.GetHashCode();

				return hash;
			}
		}

		#endregion

		#region Arithmetic

		public Matrixf33 Add(Matrixf33 other)
		{
			return new Matrixf33(
				a + other.a, b + other.b, c + other.c,
				d + other.d, e + other.e, f + other.f,
				g + other.g, h + other.h, i + other.i);
		}

		public Matrixf33 Negate()
		{
			return new Matrixf33(
				-a, -b, -c,
				-d, -e, -f,
				-g, -h, -i);
		}

		public Matrixf33 Div(float r)
		{
			return new Matrixf33(
				a / r, b / r, c / r,
				d / r, e / r, f / r,
				g / r, h / r, i / r);
		}

		public Matrixf33 Mul(float r)
		{
			return new Matrixf33(
				a * r, b * r, c * r,
				d * r, e * r, f * r,
				g * r, h * r, i * r);
		}

		public float Det()
		{
			return a * (e * i - f * h) - b * (d * i - f * g) + c * (d * h - e * g);
		}

		public Matrixf33 Transpose()
		{
			return new Matrixf33(
				a, d, g,
				b, e, h,
				c, f, i);
		}

		public Matrixf33 Inv()
		{
			float determinant = +a * (e * i - f * h)
						- d * (b * i - h * c)
						+ g * (b * f - e * c);

			if (determinant == 0)
				throw new DivideByZeroException("The matrix has a determinant of value 0.");

			float inverseDeterminant = 1 / determinant;

			float a1 = (e * i - f * h) * inverseDeterminant;
			float b1 = -(d * i - g * f) * inverseDeterminant;
			float c1 = (d * h - g * e) * inverseDeterminant;
			float d1 = -(b * i - h * c) * inverseDeterminant;
			float e1 = (a * i - g * c) * inverseDeterminant;
			float f1 = -(a * h - b * g) * inverseDeterminant;
			float g1 = (b * f - c * e) * inverseDeterminant;
			float h1 = -(a * f - c * d) * inverseDeterminant;
			float i1 = (a * e - b * d) * inverseDeterminant;

			return new Matrixf33(
				a1, d1, g1,
				b1, e1, h1,
				c1, f1, i1);
		}

		public Matrixf33 Mul(Matrixf33 other)
		{
			return new Matrixf33(
				a * other.a + b * other.d + c * other.g, a * other.b + b * other.e + c * other.h, a * other.c + b * other.f + c * other.i,
				d * other.a + e * other.d + f * other.g, d * other.b + e * other.e + f * other.h, d * other.c + e * other.f + f * other.i,
				g * other.a + h * other.d + i * other.g, g * other.b + h * other.e + i * other.h, g * other.c + h * other.f + i * other.i
				);
		}

		public Matrixf33 Div(Matrixf33 other)
		{
			return Mul(other.Inv());
		}

		public Matrixf33 Subtract(Matrixf33 other)
		{
			return new Matrixf33(
				a - other.a, b - other.b, c - other.c,
				d - other.d, e - other.e, f - other.f,
				g - other.g, h - other.h, i - other.i);
		}

		#endregion

		#region Operators

		public static bool operator ==(Matrixf33 point1, Matrixf33 point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(Matrixf33 point1, Matrixf33 point2)
		{
			return !point1.Equals(point2);
		}

		public static Matrixf33 operator +(Matrixf33 point)
		{
			return point;
		}

		public static Matrixf33 operator -(Matrixf33 point)
		{
			return point.Negate();
		}

		public static Matrixf33 operator +(Matrixf33 point1, Matrixf33 point2)
		{
			return point1.Add(point2);
		}

		public static Matrixf33 operator -(Matrixf33 point1, Matrixf33 point2)
		{
			return point1.Subtract(point2);
		}

		public static Matrixf33 operator *(Matrixf33 point, int n)
		{
			return point.Mul(n);
		}

		public static Matrixf33 operator /(Matrixf33 point, int n)
		{
			return point.Div(n);
		}

		public static Matrixf33 operator *(Matrixf33 point1, Matrixf33 point2)
		{
			return point1.Mul(point2);
		}

		public static Matrixf33 operator /(Matrixf33 point1, Matrixf33 point2)
		{
			return point1.Div(point2);
		}

		#endregion

		#region Static Methods
		//TODO Rename to scale
		public static Matrixf33 Scale(float scaleFactor)
		{
			return Scale(Vector3.one * scaleFactor);
		}

		public static Matrixf33 Scale(Vector3 scaleVector)
		{
			return new Matrixf33(
				scaleVector.x, 0.0f, 0.0f,
				0.0f, scaleVector.y, 0.0f,
				0.0f, 0.0f, scaleVector.z);
		}

		public static Matrixf33 RotateX(float angle)
		{
			return new Matrixf33(
				1.0f, 0.0f, 0.0f,
				0.0f, Mathf.Cos(angle), -Mathf.Sin(angle),
				0.0f, Mathf.Sin(angle), Mathf.Cos(angle));
		}

		public static Matrixf33 RotateY(float angle)
		{
			return new Matrixf33(
				Mathf.Cos(angle), 0.0f, Mathf.Sin(angle),
				0.0f, 1.0f, 0.0f,
				-Mathf.Sin(angle), 0.0f, Mathf.Cos(angle));
		}

		public static Matrixf33 RotateZ(float angle)
		{
			return new Matrixf33(
				Mathf.Cos(angle), -Mathf.Sin(angle), 0.0f,
				Mathf.Sin(angle), Mathf.Cos(angle), 0.0f,
				0.0f, 0.0f, 1.0f);
		}

		public static Matrixf33 RotateXYZ(Vector3 euler)
		{
			return RotateX(euler.x) * RotateY(euler.y) * RotateZ(euler.z);
		}

		public static Matrixf33 RotateZYX(Vector3 euler)
		{
			return RotateZ(euler.z) * RotateY(euler.y) * RotateX(euler.x);
		}

		#endregion
	}
}
