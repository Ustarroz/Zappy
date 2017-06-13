//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// A VectorPoint is a point that is also an algebraic vector. 
	/// </summary>
	[Version(1)]
	public interface IVectorPoint<TPoint> : ISplicedVectorPoint<TPoint, TPoint>
		where TPoint : IVectorPoint<TPoint>
	{
		int X { get; }
		int Y { get; }

		/// <summary>
		/// Scales this vector by the given amount.
		/// </summary>
		/// <example>
		/// <code>
		/// v.ScaleUp(1)
		/// v.ScaleUp(n) ==  v.ScaleUp(n - 1).Translate(v)
		/// </code>
		/// </example>
		TPoint ScaleDown(int r);
		TPoint ScaleUp(int r);

		/// <summary>
		/// Integer divides a point by another point component by component.
		/// </summary>
		/// <remarks>Remainders are always positive.</remarks>
		/// <example>(-5, 5) Div(2, 2) == (-3, 2)</example>
		[Version(1,7)]
		TPoint Div(TPoint other);

		/// <summary>
		/// Integer divides a point component by component and returns the remainder. 
		/// </summary>
		/// <remarks>Remainders are always positive.</remarks>
		/// <example>(-5, 5) Mod (2, 2) == (1, 1)</example>
		[Version(1, 7)]
		TPoint Mod(TPoint other);

		/// <summary>
		/// Multiplies two points component by component. 
		/// </summary>
		/// <example>	(-5, 5) Mul (2, 2) == (-10, 10)</example>
		[Version(1, 7)]
		TPoint Mul(TPoint other);

		int Magnitude();
	}
}
