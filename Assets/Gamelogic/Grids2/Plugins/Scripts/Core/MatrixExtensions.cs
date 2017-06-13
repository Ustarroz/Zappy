using UnityEngine;

namespace Gamelogic.Grids2
{

	/// <summary>
	/// Provides extension methods for matrices.
	/// </summary>
	public static class MatrixExtensions
	{
		public static Vector2 Mul(this Vector2 v, Matrixf22 m)
		{
			return new Vector2(
				v.x * m.a + v.y * m.c,
				v.x * m.b + v.y * m.d);
		}

		public static GridPoint2 Mul(this GridPoint2 v, Matrixi22 m)
		{
			return new GridPoint2(
				v.X * m.a + v.Y * m.c,
				v.X * m.b + v.Y * m.d);
		}

		public static Vector3 Mul(this Vector3 v, Matrixf33 m)
		{
			return new Vector3(
				v.x * m.a + v.y * m.d + v.z * m.g,
				v.x * m.b + v.y * m.e + v.z * m.h,
				v.x * m.c + v.y * m.f + v.z * m.i);
		}
	}
}
