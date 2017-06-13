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
	/// Used to indicate hat grids of this point type can support vertex grids.
	/// Replaces ISupportsVertexGrid that is now aplied to grids and not points.
	/// </summary>
	[Version(1,1)]
	public interface ISupportsVertices<TVertexPoint>
	{
		/// <summary>
		/// Returns the vertices of the point in the dual grid.
		/// </summary>
		IEnumerable<TVertexPoint> GetVertices();
	}
}
