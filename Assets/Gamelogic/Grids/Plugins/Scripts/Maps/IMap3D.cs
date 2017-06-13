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
	/// An IMap maps 3D world coordinates to Grid coordinates and vice versa. 
	/// 
	/// Many grids provide 2D maps, which can be converted to standard 3D maps 
	/// using commands such as To3DXY.
	/// 
	/// You can also provide your own maps, either as implementations of IMap, or IMap3D.
	/// </summary>
	[Version(1)]
	public interface IMap3D<TPoint>
		where TPoint : IGridPoint<TPoint>
	{
		/// <summary>
		/// Gets a world point given a grid point.
		/// </summary>
		Vector3 this[TPoint point]
		{
			get;
		}

		/// <summary>
		/// Gets a grid point given a world point.
		/// </summary>
		TPoint this[Vector3 point]
		{
			get;
		}

		IMap<TPoint> To2D();
	}
}