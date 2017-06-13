//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System.Collections.Generic;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// A grid space is an object that can determine whether a point is inside it our not. 
	/// Unlike grids, it does not contain data, and therefore there is no data at points.
	/// </summary>
	/// <typeparam name="TPoint"></typeparam>
	[Version(1)]
	public interface IGridSpace<TPoint> : IEnumerable<TPoint>
		where TPoint : IGridPoint<TPoint>
	{
		/// <summary>
		/// Returns whether a point is inside the grid.
		/// </summary>
		/// <remarks>
		/// Use this method to control the shape of the grid.
		/// </remarks>
		bool Contains(TPoint point);
	}
}