//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013-2015 Gamelogic (Pty) Ltd  //
//----------------------------------------------//
using System;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Class for holding properties for generating a Poisson disk sample set.
	/// </summary>
	[Version(1,8)]
	[Serializable]
	public class PoissonDiskProperties
	{
		/// <summary>
		/// Number of points tried per iteration.
		/// </summary>
		public int pointCount;

		/// <summary>
		/// The minimum distance between points.
		/// </summary>
		public float minimumDistance;

		/// <summary>
		/// The rectangle in which points are generated.
		/// </summary>
		public SerializableRect range;
	}

	/// <summary>
	/// Class used for keeping the proeprties of a rectangle.
	/// </summary>
	[Serializable]
	[Version(1,8)]
	public class SerializableRect
	{
		public float left;
		public float top;
		public float width;
		public float height;

		public Rect ToRect()
		{
			return new Rect(left, top, width, height);
		}
	}
}