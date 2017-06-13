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
	/// A point type implements this interface if that point type can be the edge grid of TPoint.
	/// 
	/// For example, FlatRhombPoints are the points of the edge grid for FlatHexPoints, and hence
	/// FlatRombPoint implements "IEdge&lt;FlatHexPoint&gt;."
	/// (This class replaces IEdgeAnchor).
	/// </summary>
	[Version(1,1)]
	public interface IEdge<TPoint>
	{
		/// <summary>
		/// Get the coordinates of the faces that corresponds to this point treated as an edge.
		/// </summary>
		IEnumerable<TPoint> GetEdgeFaces();
	}
}