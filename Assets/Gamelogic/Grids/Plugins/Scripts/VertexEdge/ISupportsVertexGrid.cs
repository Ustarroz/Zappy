//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Indicates that a grid supports an vertex grid.
	/// </summary>
	[Version(1,1)]
	public interface ISupportsVertexGrid<TPoint>
		where TPoint : IGridPoint<TPoint>
	{
		/// <summary>
		/// Makes a grid that corresponds to the vertices of this grid.
		/// 
		/// If point is inside this grid, then all of point.GetVertices() 
		/// are in the grid returned by this method.
		/// </summary>
		IGrid<TNewCell, TPoint> MakeVertexGrid<TNewCell>();
	}
}