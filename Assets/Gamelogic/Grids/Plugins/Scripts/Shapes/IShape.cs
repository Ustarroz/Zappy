using System.Collections.Generic;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Interface for grid shapes.
	/// </summary>
	/// <typeparam name="TPoint">The type of the point.</typeparam>
	/// <seealso cref="System.Collections.Generic.IEnumerable{TPoint}" />
	public interface IShape<TPoint> : IEnumerable<TPoint>
		where TPoint : IGridPoint<TPoint>
	{
		bool Contains(TPoint point);
	}
}
