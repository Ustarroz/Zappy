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
	/// This point type is the type of vertex anchor point of the type parmater.
	/// The vertex anchor is the point from which all the vertices are calculated.
	/// 
	/// For example, if TriPoint implements ISupportsVertexGrid&lt;HexPoint&gt;, then
	/// HexPoint will implement IVertex&lt;TriPoint&gt;.
	/// 
	/// (This class replaces IVertexAnchor).
	/// </summary>
	[Version(1,1)]
	public interface IVertex<TPoint>
	{
		//TPoint PointFromVertexAnchor();

		/// <summary>
		/// Get the coordinates of the faces that corresponds to this point treated as a vertex.
		/// </summary>
		IEnumerable<TPoint> GetVertexFaces();
	}

	
}