namespace Gamelogic.Grids2
{
	/// <summary>
	/// An implicit shape is a representation of a (discrete) point set
	/// that can determine whether a given point is in the shape or not.
	/// </summary>
	/// <typeparam name="TPoint">Type of the Point.</typeparam>
	public interface IImplicitShape<in TPoint>
	{
		/// <summary>
		/// Determines whether this implicit shape contains the specified point.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns><c>true</c> if this shape contains the specified point; otherwise, <c>false</c>.</returns>
		bool Contains(TPoint point);
	}
}