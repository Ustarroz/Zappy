//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2014 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

namespace Gamelogic.Grids
{
	/// <summary>
	/// An interface for polar maps that make it easier to build meshes for them.
	/// </summary>
	public interface IPolarMap<TPoint> : IMap<TPoint>
		where TPoint : IGridPoint<TPoint>
	{
		/// <summary>
		/// Returns the Z angle in degrees of the given grid point at 
		/// the start of the sector.
		/// 
		/// This is useful for making a mesh for the sector band, for instance.
		/// </summary>
		float GetStartAngleZ(TPoint gridPoint);

		/// <summary>
		/// Returns the Z angle in degrees of the given grid point at 
		/// the end of the sector.
		/// 
		/// This is useful for making a mesh for the sector band, for instance.
		/// </summary>
		float GetEndAngleZ(TPoint gridPoint);

		/// <summary>
		/// Gets the inside radius of the band ath the given grid point.
		/// </summary>
		float GetInnerRadius(TPoint gridPoint);

		/// <summary>
		/// Gets the outside radius of the band ath the given grid point.
		/// </summary>
		float GetOuterRadius(TPoint gridPoint);
	}
}
