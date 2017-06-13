//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// A partial vector point is a point that can be translated by "adding" a vector point.
	/// 
	/// Partial vectors can be seen as a pair, one of which is a vector(of type TVectorPoint).
	/// All the operations actually operate on the vector of this pair.
	/// 
	/// Partial vector points are used in SplicedGrids, where the second of the pair is an index
	/// that denotes the sub-cell.For example, for a tri point, the vector is a hex point, and
	/// the index denotes whether the point refers to the up or down triangle.
	/// </summary>
	/// <typeparam name="TPoint">The type that implements this interface.</typeparam>
	/// <typeparam name="TVectorPoint">The type used to translate TPoints.</typeparam>
	[Version(1)]
	public interface ISplicedVectorPoint<TPoint, TVectorPoint>
		where TPoint : ISplicedVectorPoint<TPoint, TVectorPoint>
		where TVectorPoint : IVectorPoint<TVectorPoint>
	{
		/// <summary>
		/// Translate this point by the given vector.
		/// </summary>
		TPoint Translate(TVectorPoint vector);

		/// <summary>
		/// Returns a new point with the vector component negated.
		/// </summary>
		TPoint Negate();

		/// <summary>
		/// Translates this point by the negation of the given vector.
		/// </summary>
		TPoint Subtract(TVectorPoint vector);

		/// <summary>
		/// If a spliced vectors u and v has base vector B and index I, 
		/// </summary>
		/// <remarks>This operation is the same as 
		/// <code>new SplicedVector(u.B.Translate(v.B), (u.I + v.I) % SpliceCount))
		/// </code>
		/// </remarks>
		TPoint MoveBy(TPoint splicedVector);

		/// <summary>
		/// If a spliced vectors u and v has base vector B and index I
		/// </summary>
		/// <remarks>
		/// <code>
		/// new SplicedVector(u.B.Subtract(v.B), (SpliceCount + u.I - v.I) % SpliceCount))
		/// </code>
		/// </remarks>
		TPoint MoveBackBy(TPoint splicedVector);
	}
}