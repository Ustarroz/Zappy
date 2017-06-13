namespace Gamelogic.Grids2
{
	/// <summary>
	/// Represents a 2x2 matrices with integer values.
	/// </summary>
	public struct Matrixi22
	{
		public readonly int a, b, c, d;

		public Matrixi22(int a, int b, int c, int d)
		{
			this.a = a;
			this.b = b;
			this.c = c;
			this.d = d;
		}

		//TODO: remove (not efficient)
		public int GetValueAt(int row, int col)
		{
			if (row == 0 && col == 0)
				return a;

			if (row == 0 && col == 1)
				return b;

			if (row == 1 && col == 0)
				return c;

			if (row == 1 && col == 1)
				return d;

			return 0;
		}

		public override string ToString()
		{
			return string.Format("[({0},{1});({2},{3})]", a, b, c, d);
		}

		#region Equality

		public bool Equals(Matrixi22 other)
		{
			var areEqual = (a == other.a) && (b == other.b) && (c == other.c) && (d == other.d);

			return areEqual;
		}

		public override bool Equals(object other)
		{
			if (other.GetType() != typeof(Matrixi22))
			{
				return false;
			}

			var point = (Matrixi22)other;

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

		public Matrixi22 Add(Matrixi22 other)
		{
			return new Matrixi22(
				a + other.a,
				b + other.b,
				c + other.c,
				d + other.d);
		}

		public Matrixi22 Negate()
		{
			return new Matrixi22(-a, -b, -c, -d);
		}

		public Matrixi22 Div(int r)
		{
			return new Matrixi22(
				a / r,
				b / r,
				c / r,
				d / r);
		}

		public Matrixi22 Mul(int r)
		{
			return new Matrixi22(
				a * r,
				b * r,
				c * r,
				d * r);
		}

		public int Det()
		{
			return a * d - b * c;
		}

		public Matrixi22 Mul(Matrixi22 other)
		{
			return new Matrixi22(
				a * other.a + b * other.c,
				a * other.b + b * other.d,
				c * other.a + c * other.c,
				d * other.b + d * other.d
				);
		}

		public Matrixi22 Div(Matrixi22 other)
		{
			//return Mul(other.Inv());
			return Mul(other);
		}

		public Matrixi22 Subtract(Matrixi22 other)
		{
			return new Matrixi22(
				a - other.a,
				b - other.b,
				c - other.c,
				d - other.d);
		}

		#endregion

		#region Operators

		public static bool operator ==(Matrixi22 point1, Matrixi22 point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(Matrixi22 point1, Matrixi22 point2)
		{
			return !point1.Equals(point2);
		}

		public static Matrixi22 operator +(Matrixi22 point)
		{
			return point;
		}

		public static Matrixi22 operator -(Matrixi22 point)
		{
			return point.Negate();
		}

		public static Matrixi22 operator +(Matrixi22 point1, Matrixi22 point2)
		{
			return point1.Add(point2);
		}

		public static Matrixi22 operator -(Matrixi22 point1, Matrixi22 point2)
		{
			return point1.Subtract(point2);
		}

		public static Matrixi22 operator *(Matrixi22 point, int n)
		{
			return point.Mul(n);
		}

		public static Matrixi22 operator /(Matrixi22 point, int n)
		{
			return point.Div(n);
		}

		public static Matrixi22 operator *(Matrixi22 point1, Matrixi22 point2)
		{
			return point1.Mul(point2);
		}

		public static Matrixi22 operator /(Matrixi22 point1, Matrixi22 point2)
		{
			return point1.Div(point2);
		}

		#endregion
	}
}
