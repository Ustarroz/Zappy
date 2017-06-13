//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Non generic interface for grid points.
	/// </summary>
	public interface IGridPoint
	{
		 
	}

	/// <summary>
	/// Represents a "point" that is used to access a cell in a Grid. 
	/// 
	/// For built-in 2D grids, these points are often 2D integer vectors, or spliced vectors, and hence they implement
	/// additional interfaces such as IVectorPoint, ISplicedPoint, andISplicedVectorPoint.These points supports
	/// arithmetic, [colorings](http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/), and some other geometric operations. 
	/// 
	/// In general, points do not "know" their neighbors.Use the grid methods IGrid&lt;TCell, TPoint&gt;.GetNeighbors and
	/// IGrid&lt;TCell, TPoint&gt;.GetAllNeighbors to make queries about a point's neighbors.
	/// 
	/// GridPoint base classes must be immutable for many of the algorithms to work correctly.In particular,
	/// GridPoints are used as keys in dictionaries and sets.
	/// 
	/// It is also a good idea to overload the `==` and `!=` operators.
	/// </summary>
	[Version(1)]
	public interface IGridPoint<TPoint> : IEquatable<TPoint>, IGridPoint
		where TPoint : IGridPoint<TPoint>
	{
		/// <summary>
		/// The lattice distance between two points.
		/// 
		/// Two points should have a distance of 1 if and only if they are neighbors.
		/// </summary>
		int DistanceFrom(TPoint other);

		/// <summary>
		/// For spliced grids, this is the index of the splice.
		/// 
		/// For Uniform grids, this is always 0.
		/// </summary>
		int SpliceIndex
		{
			get;
		}

		/// <summary>
		/// For spliced grids, this is the number of slices for all points.
		/// 
		/// For Uniform grids, this is always 1.
		/// </summary>
		int SpliceCount
		{
			get;
		}
	}
}