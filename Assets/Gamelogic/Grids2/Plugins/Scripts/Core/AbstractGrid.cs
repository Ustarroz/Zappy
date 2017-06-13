using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Base class of concrete implementations of the IGrid interface.
	/// </summary>
	/// <typeparam name="TPoint">The point type oif this grid.</typeparam>
	/// <typeparam name="TCell">The cell type of this grid.</typeparam>
	/// <seealso cref="Gamelogic.Grids2.IGrid{TPoint, TCell}" />
	public abstract class AbstractGrid<TPoint, TCell> : IGrid<TPoint, TCell>
	{
		#region Public Properties

		public abstract TCell this[TPoint point] { get; set; }

		public abstract IEnumerable<TPoint> Points { get; }
		public abstract AbstractBounds<TPoint> Bounds { get; }

		public IEnumerable<TCell> Cells
		{
			get { return Points.Select<TPoint, TCell>(point => this[point]); }
		}

		#endregion

		#region Public Methods

		public abstract bool Contains(TPoint point);
		public abstract IGrid<TPoint, TNewCell> CloneStructure<TNewCell>();

		public IEnumerator<PointCellPair<TPoint, TCell>> GetEnumerator()
		{
			return Points.Select(point => new PointCellPair<TPoint, TCell>(
				point,
				this[point])).GetEnumerator();
		}

		#endregion

		#region Private Members

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}