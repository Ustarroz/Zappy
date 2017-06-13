//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2014 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids
{
	/// <summary>
	/// A one-way map that converts grid points to worls points. One-way maps are useful 
	/// for maps that can automatically invert the map, such as VoronoiMap.
	/// </summary>
	[Version(1,8)]
	public interface IGridToWorldMap<TPoint> where TPoint : IGridPoint<TPoint>
	{
		#region Properties

		/// <summary>
		/// Gets a world point given a grid point.
		/// </summary>
		Vector2 this[TPoint point]
		{
			get;
		}

		#endregion
	}
}