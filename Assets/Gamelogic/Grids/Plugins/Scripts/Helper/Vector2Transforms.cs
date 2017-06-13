//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Provides extension methods for transforming Vector2 instances.
	/// </summary>
	[Version(1)]
	public static class Vector2Transforms
	{
		public static Vector2 ReflectAboutX(this Vector2 v)
		{
			return new Vector2(v.x, -v.y);
		}

		public static Vector2 ReflectAboutY(this Vector2 v)
		{
			return new Vector2(-v.x, v.y);
		}

		/// <summary>
		/// Rotates a vector in a given angle.
		/// </summary>
		/// <param name="v">vector to rotate</param>
		/// <param name="angle">angle in degrees.</param>
		/// <returns>Rotated vector.</returns>
		public static Vector2 Rotate(this Vector2 v, float angle)
		{
			float alpha = Mathf.Deg2Rad * angle;
			float cosAngle = Mathf.Cos(alpha);
			float sinAngle = Mathf.Sin(alpha);

			float x = v.x * cosAngle - v.y * sinAngle;
			float y = v.x * sinAngle + v.y * cosAngle;

			return new Vector2(x, y);
		}

		public static Vector2 RotateAround(this Vector2 v, float angle, Vector2 axis)
		{
			return (v - axis).Rotate(angle) + axis;
		}

		public static Vector2 Rotate90(this Vector2 v)
		{
			return new Vector2(-v.y, v.x);
		}

		public static Vector2 Rotate180(this Vector2 v)
		{
			return new Vector2(-v.x, -v.y);
		}

		public static Vector2 Rotate270(this Vector2 v)
		{
			return new Vector2(v.y, -v.x);
		}

		public static Vector2 ReflectXY(this Vector2 v)
		{
			return new Vector2(v.y, v.x);
		}

		public static Vector3 XYTo3D(this Vector2 v)
		{
			return XYTo3D(v, 0);
		}

		public static Vector3 XYTo3D(this Vector2 v, float z)
		{
			return new Vector3(v.x, v.y, z);
		}

		public static Vector3 XZTo3D(this Vector2 v)
		{
			return XZTo3D(v, 0);
		}

		public static Vector3 XZTo3D(this Vector2 v, float y)
		{
			return new Vector3(v.x, y, v.y);
		}
	}
}