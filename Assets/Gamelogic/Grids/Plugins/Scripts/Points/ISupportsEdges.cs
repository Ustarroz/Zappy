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
	/// Used to indicated that grids of this point type can support edge grids.
	/// Replaces ISupportsEdgeGrid that is now applied to grids, not Interface
	/// </summary>
	[Version(1,1)]
	public interface ISupportsEdges<TVertexPoint>
	{
		IEnumerable<TVertexPoint> GetEdges();
	}
}
