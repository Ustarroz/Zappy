//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Interface for working with compound points.
	/// 
	/// Spliced points are used for periodic grids where not all
	/// faces are identical(see AbstractSplicedGrid). 
	/// 
	/// Every spliced grid can be seen as a uniform regular grid,
	/// where each cell has been divided.Therefore, a spliced point
	/// consists of a coordinate on the base grid, and an index denoting
	/// the particular cell.
	/// </summary>
	/// <typeparam name="TPoint">The type that implements this interface</typeparam>
	/// <typeparam name="TBasePoint">The type of the refular grid that underlies this grid.For example,
	/// a hexagonal grid underlies regular triangular grids</typeparam>
	[Version(1)]
	public interface ISplicedPoint<TPoint, TBasePoint> :
		IGridPoint<TPoint>,
		ISplicedVectorPoint<TPoint, TBasePoint>

		where TPoint : ISplicedVectorPoint<TPoint, TBasePoint>, IGridPoint<TPoint>
		where TBasePoint : IVectorPoint<TBasePoint>, IGridPoint<TBasePoint>
	{
		/// <summary>
		/// Why is this public?
		///		- Convenience
		///		- Algorithm Design
		/// Otherwise the user will just make a new
		/// basepoint in any case, and perhaps make a mistake.
		/// </summary>
		TBasePoint BasePoint { get; }

		/// <summary>
		/// Returns the X-coordinate of this point.
		/// </summary>
		[Version(1,2)]
		int X { get; }

		/// <summary>
		/// Returns the Y-coordinate of this point.
		/// </summary>
		[Version(1,2)]
		int Y { get; }

		//What is a better name for this proeprty?
		/// <summary>
		/// Returns the splice idnex for this point.
		/// </summary>
		int I { get; }

		TPoint IncIndex(int n);
		TPoint DecIndex(int n);
		TPoint InvertIndex();
	}
}