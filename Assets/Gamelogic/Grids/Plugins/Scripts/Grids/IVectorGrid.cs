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
	/// IVectorGrids are build on (spliced) vector points. They are regular and uniform.
	/// </summary>
	[Version(1)]
	public interface IVectorGrid<TCell, TPoint, TBasePoint> : IGrid<TCell, TPoint>
		where TPoint : ISplicedVectorPoint<TPoint, TBasePoint>, IGridPoint<TPoint>
		where TBasePoint : IVectorPoint<TBasePoint>, IGridPoint<TBasePoint>
	{
		IEnumerable<TPoint> GetNeighborDirections(int cellIndex);
	}
}