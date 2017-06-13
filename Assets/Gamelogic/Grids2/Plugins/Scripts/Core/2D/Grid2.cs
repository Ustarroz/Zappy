using System.Collections.Generic;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// 2D implementation of a <see cref="IGrid{TPoint,TCell}"/>.
	/// </summary>
	/// <typeparam name="TCell">Type of the Cell.</typeparam>
	public sealed class Grid2<TCell> : AbstractGrid<GridPoint2, TCell>
	{
		#region Constants

		private readonly TCell[,] cells;
		private readonly IExplicitShape<GridPoint2> shape;

		#endregion

		#region Public Properties

		public override TCell this[GridPoint2 point]
		{
			get
			{
				var accessPoint = point - shape.Bounds.Point;
				return cells[accessPoint.X, accessPoint.Y];
			}

			set
			{
				var accessPoint = point - shape.Bounds.Point;
				cells[accessPoint.X, accessPoint.Y] = value;
			}
		}

		public override IEnumerable<GridPoint2> Points
		{
			get { return shape.Points; }
		}

		public override AbstractBounds<GridPoint2> Bounds
		{
			get { return shape.Bounds; }
		}

		//TODO: Move to Algorithms
		private static readonly GridPoint2[] SpiralIteratorDirections =
		{
			RectPoint.East,
			RectPoint.South,
			RectPoint.West,
			RectPoint.North,
		};

		private static readonly GridPoint2[] HexSpiralIteratorDirections =
		{
			PointyHexPoint.East,
			PointyHexPoint.SouthEast,
			PointyHexPoint.SouthWest,
			PointyHexPoint.West,
			PointyHexPoint.NorthWest,
			PointyHexPoint.NorthEast
		};

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Grid1{TCell}"/> class with
		/// the specified shape.
		/// Cells are not initialized, they have the default value of type <c>TCell</c>.
		/// </summary>
		/// <param name="shape">The shape of this grid.</param>
		public Grid2(IExplicitShape<GridPoint2> shape)
		{
			this.shape = shape;
			var storageDimensions = shape.Bounds.Size;

			cells = new TCell[storageDimensions.X, storageDimensions.Y];
		}

		#endregion

		#region Public Methods

		public override IGrid<GridPoint2, TNewCell> CloneStructure<TNewCell>()
		{
			return new Grid2<TNewCell>(shape);
		}

		public override bool Contains(GridPoint2 point)
		{
			return shape.Contains(point);
		}

		#endregion

		#region Iterators

		//TODO: Move to Algorithms
		public IEnumerable<GridPoint2> GetSpiralIterator(GridPoint2 origin, int ringCount)
		{
			var point = origin;
			yield return point;

			for (var k = 1; k < ringCount; k++)
			{
				point += RectPoint.NorthWest;

				for (var i = 0; i < 4; i++)
				{
					for (var j = 0; j < 2 * k; j++)
					{
						point += SpiralIteratorDirections[i];

						if (Contains(point))
						{
							yield return point;
						}
					}
				}
			}
		}

		public IEnumerable<GridPoint2> GetHexSpiralIterator(GridPoint2 origin, int ringCount)
		{
			var point = origin;
			yield return point;

			for (var k = 1; k < ringCount; k++)
			{
				point += PointyHexPoint.NorthWest;

				for (var i = 0; i < 6; i++)
				{
					for (var j = 0; j < k; j++)
					{
						point += HexSpiralIteratorDirections[i];

						if (Contains(point))
						{
							yield return point;
						}
					}
				}
			}
		}

		#endregion
	}
}