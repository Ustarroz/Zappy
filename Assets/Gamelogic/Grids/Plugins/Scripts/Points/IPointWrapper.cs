//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Provides a function for wrapping points that is used
	/// by wrapped grids.
	/// 
	/// Since many such functions require lookup tables, 
	/// it's more suitable to provide it as a class than 
	/// providing it as a delagate.
	/// </summary>
	[Version(1,7)]
	public interface IPointWrapper<TPoint> where TPoint : IGridPoint<TPoint>
	{
		/// <summary>
		/// Returns a new point, that corresponds to the wrapped version of the 
		/// give point.
		/// </summary>
		TPoint Wrap(TPoint point);
	}
}
