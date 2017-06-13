namespace Gamelogic.Grids2
{
	/// <summary>
	/// Simple struct that holds a readonly <see cref="TPoint"/> and <see cref="TCell"/>.
	/// This is the type over which grids are enumerable.
	/// </summary>
	/// <typeparam name="TPoint">Type of the Point.</typeparam>
	/// <typeparam name="TCell">Type of the Cell.</typeparam>
	public struct PointCellPair<TPoint, TCell>
	{
		public readonly TPoint point;
		public readonly TCell cell;

		/// <summary>
		/// Initializes a new instance of the <see cref="PointCellPair{TPoint, TCell}"/> struct.
		/// </summary>
		/// <param name="point">The point of this point cell pair.</param>
		/// <param name="cell">The cell of this point cell pair.</param>
		public PointCellPair(TPoint point, TCell cell)
		{
			this.point = point;
			this.cell = cell;
		}
	}
}