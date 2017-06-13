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
	/// A grid where cells have an even number of neighbors. In an even grid each neighbor has an opposite neighbor.
	/// </summary>
	[Version(1)]
	public interface IEvenGrid<TCell, TPoint, TBasePoint> : IVectorGrid<TCell, TPoint, TBasePoint>
		where TPoint : ISplicedVectorPoint<TPoint, TBasePoint>, IGridPoint<TPoint>
		where TBasePoint : IVectorPoint<TBasePoint>, IGridPoint<TBasePoint>
	{
		/// <summary>
		/// This is the set of neighbor directions so that it contains only one of 
		/// the neighbor directions of a pair of opposites.
		/// </summary>
		IEnumerable<TPoint> GetPrincipleNeighborDirections();
	}
}