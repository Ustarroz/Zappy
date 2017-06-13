namespace Gamelogic.Grids2
{
	/// <summary>
	/// Represents a 3x3 matrices with integer values.
	/// </summary>
	public struct Matrixi33
	{
		private readonly int
			a, b, c,
			d, e, f,
			g, h, i;

		public Matrixi33(int a, int b, int c, int d, int e, int f, int g, int h, int i)
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

		public int GetValueAt(int row, int col)
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

			return 0;
		}

		#region Equality

		public bool Equals(Matrixi33 other)
		{
			var areEqual =
				(a == other.a) && (b == other.b) && (c == other.c) &&
				(d == other.d) && (e == other.e) && (f == other.f) &&
				(g == other.g) && (h == other.h) && (i == other.i);

			return areEqual;
		}

		public override bool Equals(object other)
		{
			if (other.GetType() != typeof(Matrixi33))
			{
				return false;
			}

			var point = (Matrixi33)other;

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

		public Matrixi33 Add(Matrixi33 other)
		{
			return new Matrixi33(
				a + other.a, b + other.b, c + other.c,
				d + other.d, e + other.e, f + other.f,
				g + other.g, h + other.h, i + other.i);
		}

		public Matrixi33 Negate()
		{
			return new Matrixi33(
				-a, -b, -c,
				-d, -e, -f,
				-g, -h, -i);
		}

		public Matrixi33 Div(int r)
		{
			return new Matrixi33(
				a / r, b / r, c / r,
				d / r, e / r, f / r,
				g / r, h / r, i / r);
		}

		public Matrixi33 Mul(int r)
		{
			return new Matrixi33(
				a * r, b * r, c * r,
				d * r, e * r, f * r,
				g * r, h * r, i * r);
		}

		public int Det()
		{
			return a * (e * i - f * h) - b * (d * i - f * g) + c * (d * h - e * g);
		}

		public Matrixi33 Transpose()
		{
			return new Matrixi33(
				a, d, g,
				b, e, h,
				c, f, i);
		}

		public Matrixi33 Mul(Matrixi33 other)
		{
			return new Matrixi33(
				a * other.a + b * other.d + c * other.g, a * other.b + b * other.e + c * other.h, a * other.c + b * other.f + c * other.i,
				d * other.a + e * other.d + f * other.g, d * other.b + e * other.e + f * other.h, d * other.c + e * other.f + f * other.i,
				g * other.a + h * other.d + i * other.g, g * other.b + h * other.e + i * other.h, g * other.c + h * other.f + i * other.i
				);
		}

		public Matrixi33 Div(Matrixi33 other)
		{
			//return Mul(other.Inv());
			return Mul(other);
		}

		public Matrixi33 Subtract(Matrixi33 other)
		{
			return new Matrixi33(
				a - other.a, b - other.b, c - other.c,
				d - other.d, e - other.e, f - other.f,
				g - other.g, h - other.h, i - other.i);
		}

		#endregion

		#region Operators

		public static bool operator ==(Matrixi33 point1, Matrixi33 point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(Matrixi33 point1, Matrixi33 point2)
		{
			return !point1.Equals(point2);
		}

		public static Matrixi33 operator +(Matrixi33 point)
		{
			return point;
		}

		public static Matrixi33 operator -(Matrixi33 point)
		{
			return point.Negate();
		}

		public static Matrixi33 operator +(Matrixi33 point1, Matrixi33 point2)
		{
			return point1.Add(point2);
		}

		public static Matrixi33 operator -(Matrixi33 point1, Matrixi33 point2)
		{
			return point1.Subtract(point2);
		}

		public static Matrixi33 operator *(Matrixi33 point, int n)
		{
			return point.Mul(n);
		}

		public static Matrixi33 operator /(Matrixi33 point, int n)
		{
			return point.Div(n);
		}

		public static Matrixi33 operator *(Matrixi33 point1, Matrixi33 point2)
		{
			return point1.Mul(point2);
		}

		public static Matrixi33 operator /(Matrixi33 point1, Matrixi33 point2)
		{
			return point1.Div(point2);
		}

		#endregion
	}
}
