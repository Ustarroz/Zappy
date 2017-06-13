using System.Collections.Generic;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// An explicit shape that has for each of its points a value, called a cell, associated.
	/// </summary>
	/// <typeparam name="TPoint">Type of the Point.</typeparam>
	/// <typeparam name="TCell">Type of the Cell.</typeparam>
	public interface IGrid<TPoint, TCell> :
		IGrid<TPoint>,
		IEnumerable<PointCellPair<TPoint, TCell>>
	{
		/// <summary>
		/// Gets or sets the cell at the specified point.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns>TCell.</returns>
		TCell this[TPoint point] { get; set; }

		/// <summary>
		/// Returns all the cells of this grid, that is,
		/// the values associated with all points in the grid.
		/// </summary>
		/// <value>The cells.</value>
		IEnumerable<TCell> Cells { get; }
	}

	/// <summary>
	/// An explicit shape that has for each of its points a value associated.
	/// </summary>
	/// <typeparam name="TPoint">Type of the Point.</typeparam>
	public interface IGrid<TPoint> : IExplicitShape<TPoint>
	{
		/// <summary>
		/// Returns a grid with exactly the same structure, but potentially holding
		/// elements of a different type.
		/// </summary>
		/// <typeparam name="TNewCell"></typeparam>
		/// <returns>Cloned Grid.</returns>
		IGrid<TPoint, TNewCell> CloneStructure<TNewCell>();
	}
}