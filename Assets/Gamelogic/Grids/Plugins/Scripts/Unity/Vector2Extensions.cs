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
	/// Extension methods for Vector2.
	/// </summary>
	[Version(1,2)]
	public static class Vector2Extensions
	{
		/// <summary>
		/// Floors each component and returns the corresponding VectorPoint.
		/// </summary>
		[Version(1,7)]
		public static VectorPoint FloorToVectorPoint(this Vector2 vec)
		{
			return new VectorPoint(Mathf.FloorToInt(vec.x), Mathf.FloorToInt(vec.y));
		}

		public static Vector2 HadamardMul(this Vector2 thisVector, VectorPoint otherVector)
		{
			return new Vector2(thisVector.x * otherVector.X, thisVector.y * otherVector.Y);
		}

		public static Vector2 HadamardDiv(this Vector2 thisVector, VectorPoint otherVector)
		{
			return new Vector2(thisVector.x / otherVector.X, thisVector.y / otherVector.Y);
		}

		public static float PerpDot(this Vector2 thisVector, Vector2 otherVector)
		{
			return thisVector.x*otherVector.y - thisVector.y*otherVector.x;
		}
	}
}
