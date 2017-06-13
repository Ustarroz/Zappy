using System.Collections.Generic;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Represents a point set that is an implicit shape, and in addition,
	/// can generate all the points it contains.
	/// </summary>
	/// <typeparam name="TPoint">The point type.</typeparam>
	public interface IExplicitShape<TPoint> : IImplicitShape<TPoint>
	{
		/// <summary>
		/// Gets all the points this shape contains.
		/// </summary>
		/// <value>The points.</value>
		IEnumerable<TPoint> Points { get; }

		AbstractBounds<TPoint> Bounds { get; }
	}
}