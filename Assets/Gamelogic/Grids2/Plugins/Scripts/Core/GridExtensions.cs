using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Provides extension methods for grids.
	/// </summary>
	public static class GridExtensions
	{
		public static TCell GetValue<TPoint, TCell>(this IGrid<TPoint, TCell> grid, TPoint point)
		{
			return grid[point];
		}

		public static void SetValue<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid,
			TPoint point,
			TCell value)
		{
			grid[point] = value;
		}

		public static IEnumerable<TPoint> GetPoints<TPoint>(
			this IGrid<TPoint> grid,
			Func<TPoint, IEnumerable<TPoint>> getAllPoints,
			TPoint point)
		{
			return getAllPoints(point).Where(grid.Contains);
		}

		/// <summary>
		/// Selects all the points in the list also in the grid.
		/// </summary>
		/// <typeparam name="TPoint">The type of the t point.</typeparam>
		/// <param name="points">The points.</param>
		/// <param name="grid">The grid.</param>
		/// <returns>IEnumerable&lt;TPoint&gt;.</returns>
		public static IEnumerable<TPoint> In<TPoint>(this IEnumerable<TPoint> points, IGrid<TPoint> grid)
		{
			return points.Where(grid.Contains);
		}

		public static IGrid<TPoint, TNewCell> CloneStructure<TPoint, TNewCell>(
			this IGrid<TPoint> grid,
			TNewCell initialValue)
		{
			var newGrid = grid.CloneStructure<TNewCell>();

			foreach (var point in newGrid.Points)
			{
				newGrid[point] = initialValue;
			}

			return newGrid;
		}

		public static IGrid<TPoint, TNewCell> CloneStructure<TPoint, TNewCell>(
			this IGrid<TPoint> grid,
			Func<TNewCell> generateItem)
		{
			var newGrid = grid.CloneStructure<TNewCell>();

			foreach (var point in newGrid.Points)
			{
				newGrid[point] = generateItem();
			}

			return newGrid;
		}

		public static IGrid<TPoint, TNewCell> CloneStructure<TPoint, TNewCell>(
			this IGrid<TPoint> grid,
			Func<TPoint, TNewCell> generateItem)
		{
			var newGrid = grid.CloneStructure<TNewCell>();

			foreach (var point in newGrid.Points)
			{
				newGrid[point] = generateItem(point);
			}

			return newGrid;
		}

		public static void Fill<TPoint, TCell>(this IGrid<TPoint, TCell> grid, TCell item)
		{
			foreach (var point in grid.Points)
			{
				grid[point] = item;
			}
		}

		public static void Fill<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid,
			Func<TCell> generateItem)
		{
			foreach (var point in grid.Points)
			{
				grid[point] = generateItem();
			}
		}

		public static void Fill<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid,
			Func<TPoint, TCell> generateItem)
		{
			foreach (var point in grid.Points)
			{
				grid[point] = generateItem(point);
			}
		}

		public static void Apply<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid,
			Action<TPoint> action)
		{
			foreach (var point in grid.Points)
			{
				action(point);
			}
		}



		public static void Apply<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid,
			Action<TCell> action)
		{
			foreach (var point in grid.Points)
			{
				action(grid[point]);
			}
		}

		public static void Transform<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid,
			Func<TCell, TCell> transform)
		{
			foreach (var point in grid.Points)
			{
				grid[point] = transform(grid[point]);
			}
		}

		public static void Transform<TPoint, TCell>(
			this IGrid<TPoint, TCell> grid,
			Func<TPoint, TCell, TCell> transform)
		{
			foreach (var point in grid.Points)
			{
				grid[point] = transform(point, grid[point]);
			}
		}

		/// <summary>
		/// Returns a shallow copy of the given grid.
		/// </summary>
		public static IGrid<TPoint, TCell> Clone<TPoint, TCell>(this IGrid<TPoint, TCell> grid)
		{
			if (grid == null)
			{
				throw new ArgumentNullException("grid");
			}

			var newGrid = grid.CloneStructure<TCell>();

			foreach (var point in grid.Points)
			{
				newGrid[point] = grid[point];
			}

			return newGrid;
		}

		/*public static IGrid<TPoint, TNewCell> CastValues<TPoint, TOldCell, TNewCell>(this IGrid<TPoint, TOldCell> grid)
		{
			if (grid == null)
			{
				throw new ArgumentNullException("grid");
			}

			var newGrid = grid as IGrid<TPoint, TNewCell>;

			if (newGrid != null) return newGrid;

			newGrid = grid.CloneStructure<TNewCell>();
			
			foreach (var point in grid.Points)
			{
				newGrid[point] = (TNewCell) grid[point];
			}

			return newGrid;
		}*/

		public static IGrid<int, TCell> ToGrid<TCell>(this IExplicitShape<int> shape)
		{
			return new Grid1<TCell>(shape);
		}

		public static IGrid<GridPoint2, TCell> ToGrid<TCell>(this IExplicitShape<GridPoint2> shape)
		{
			return new Grid2<TCell>(shape);
		}

		public static IGrid<GridPoint3, TCell> ToGrid<TCell>(this IExplicitShape<GridPoint3> shape)
		{
			return new Grid3<TCell>(shape);
		}
	}
}