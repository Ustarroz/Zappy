using System.Collections.Generic;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// 3D implementation of a <see cref="IGrid{TPoint,TCell}"/>.
	/// </summary>
	/// <typeparam name="TCell">Type of the Cell.</typeparam>
	public sealed class Grid3<TCell> : AbstractGrid<GridPoint3, TCell>
	{
		#region Constants

		private readonly TCell[,,] cells;
		private readonly IExplicitShape<GridPoint3> shape;

		#endregion

		#region Public Properties
		public override TCell this[GridPoint3 point]
		{
			get
			{
				var accessPoint = point - shape.Bounds.Point;
				return cells[accessPoint.X, accessPoint.Y, accessPoint.Z];
			}

			set
			{
				var accessPoint = point - shape.Bounds.Point;
				cells[accessPoint.X, accessPoint.Y, accessPoint.Z] = value;
			}
		}

		public override IEnumerable<GridPoint3> Points
		{
			get { return shape.Points; }
		}

		public override AbstractBounds<GridPoint3> Bounds
		{
			get { return shape.Bounds; }
		}

		#endregion

		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Grid1{TCell}"/> class with
		/// the specified shape.
		/// Cells are not initialized, they have the default value of type <c>TCell</c>.
		/// </summary>
		/// <param name="shape">The shape of this grid.</param>
		public Grid3(IExplicitShape<GridPoint3> shape)
		{
			this.shape = shape;
			var storageDimensions = shape.Bounds.Size;

			cells = new TCell[storageDimensions.X, storageDimensions.Y, storageDimensions.Z];
		}

		#endregion

		#region Public Methods

		public override IGrid<GridPoint3, TNewCell> CloneStructure<TNewCell>()
		{
			return new Grid3<TNewCell>(shape);
		}

		public override bool Contains(GridPoint3 point)
		{
			return shape.Contains(point);
		}

		#endregion
	}
}