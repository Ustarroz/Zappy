//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions.Algorithms;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Defines extension methods for the IGrid interface. 
	/// This is implemented as an extension so that implementers need not
	/// extend from a common base class, but provide it to their clients.
	/// </summary>
	[Version(1)]
	public static class GridExtensions
	{
		/// <summary>
		/// Returns a new grid in the same shape as the given grid, with the same contents casted to the new type.
		/// </summary>
		public static IGrid<TNewCell, TPoint> CastValues<TNewCell, TPoint>(this IGrid<TPoint> grid) 
			where TPoint : IGridPoint<TPoint>
		{
			if (grid == null)
			{
				throw new ArgumentNullException("grid");
			}

			var newGrid = grid as IGrid<TNewCell, TPoint>;

			if (newGrid != null) return newGrid;

			newGrid = grid.CloneStructure<TNewCell>();

			foreach (var point in grid)
			{
				newGrid[point] = (TNewCell) grid[point];
			}

			return newGrid;
		}

		/// <summary>
		/// Returns a shallow copy of the given grid.
		/// </summary>
		public static IGrid<TCell, TPoint> Clone<TCell, TPoint>(this IGrid<TCell, TPoint> grid) 
			where TPoint : IGridPoint<TPoint>
		{
			if (grid == null)
			{
				throw new ArgumentNullException("grid");
			}

			var newGrid = grid.CloneStructure<TCell>();

			foreach (var point in grid)
			{
				newGrid[point] = grid[point];
			}

			return newGrid;
		}

		/// <summary>
		/// Only return neighbors of the point that are inside the grid, as defined by IsInside,
		/// that also satisfies the predicate includePoint.
		/// 
		/// It is equivalent to GetNeighbors(point).Where(includePoint).
		/// </summary>
		[Version(1,7)]
		public static IEnumerable<TPoint> GetNeighbors<TCell, TPoint>(
			this IGrid<TCell, TPoint> grid,
			TPoint point, Func<TPoint, bool> includePoint)

			where TPoint : IGridPoint<TPoint>
		{
			return
				(from neighbor in grid.GetAllNeighbors(point)
				 where grid.Contains(neighbor) && includePoint(neighbor)
				 select neighbor);
		}

		/// <summary>
		/// Only return neighbors of the point that are inside the grid, as defined by IsInside, 
		/// whose associated cells also satisfy the predicate includeCell.
		/// 
		/// It is equivalent to GetNeighbors(point).Where(p =&gt; includeCell(grid[p])
		/// </summary>
		[Version(1,7)]
		public static IEnumerable<TPoint> GetNeighbors<TCell, TPoint>(
			this IGrid<TCell, TPoint> grid,
			TPoint point, Func<TCell, bool> includeCell)

			where TPoint : IGridPoint<TPoint>
		{
			return
				(from neighbor in grid.GetAllNeighbors(point)
				 where grid.Contains(neighbor) && includeCell(grid[neighbor])
				 select neighbor);
		}

		/// <summary>
		/// Returns all neighbors of this point that satisfies the condition, 
		/// regardless of whether they are in the grid or not.
		/// </summary>
		public static IEnumerable<TPoint> GetAllNeighbors<TCell, TPoint>(
			this IGrid<TCell, TPoint> grid,
			TPoint point, Func<TPoint, bool> includePoint)

			where TPoint : IGridPoint<TPoint>
		{
			return grid.GetAllNeighbors(point).Where(includePoint);
		}

		/// <summary>
		/// Returns a list of all points whose associated cells also satisfy the predicate include.
		/// 
		/// It is equivalent to GetNeighbors(point).Where(p =&gt; includeCell(grid[p])
		/// </summary>
		[Version(1,7)]
		public static IEnumerable<TPoint> WhereCell<TCell, TPoint>(
			this IGrid<TCell, TPoint> grid, Func<TCell, bool> include)

			where TPoint : IGridPoint<TPoint>
		{
			return grid.Where(p => include(grid[p]));
		}


		/// <summary>
		/// Only return neighbors of the point that are inside the grid, as defined by Contains.
		/// </summary>
		public static IEnumerable<TPoint> GetNeighbors<TCell, TPoint>(
			this IGrid<TCell, TPoint> grid,
			TPoint point)

			where TPoint : IGridPoint<TPoint>
		{
			//return grid.GetNeighbors(point, (TPoint p) => true);
			return 
				from neighbor in grid.GetAllNeighbors(point)
				where grid.Contains(neighbor)
				select neighbor;
		}

		/// <summary>
		/// Returns whether the point is outside the grid.
		/// </summary>
		/// <remarks>implementers This method must be consistent with IsInside, and hence
		/// is not overridable.</remarks>
		public static bool IsOutside<TCell, TPoint>(
			this IGrid<TCell, TPoint> grid,
			TPoint point)

			where TPoint : IGridPoint<TPoint>
		{
			return !grid.Contains(point);
		}

		/// <summary>
		/// Returns a list of cells that correspond to the list of points.
		/// </summary>
		public static IEnumerable<TCell> SelectValuesAt<TCell, TPoint>(
			this IGrid<TCell, TPoint> grid,
			IEnumerable<TPoint> pointList)

			where TPoint : IGridPoint<TPoint>
		{
			return pointList.Select(x => grid[x]);
		}

		/// <summary>
		/// Shuffles the contents of a grid.
		/// </summary>
		[Version(1,6)]
		public static void Shuffle<TCell, TPoint>(
			this IGrid<TCell, TPoint> grid)

			where TPoint : IGridPoint<TPoint>
		{
			var points = grid.ToList();

			if (points.Count <= 1)
			{
				return; //nothing to shuffle
			}
			
			var shuffledPoints = grid.ToList();
			shuffledPoints.Shuffle();

			var tmpGrid = grid.CloneStructure<TCell>();

			for (int i = 0; i < points.Count; i++)
			{
				tmpGrid[points[i]] = grid[shuffledPoints[i]];
			}

			foreach (var point in grid)
			{
				grid[point] = tmpGrid[point];
			}
		}

		/// <summary>
		/// The same as `grid[point]`. This method is included to make
		/// it easier to construct certain LINQ expressions, for example
		/// 
		/// grid.Select(grid.GetCell)
		/// grid.Where(p =&gt; p.GetColor4_2() == 0).Select(grid.GetCell)
		/// </summary>
		[Version(1,7)]
		public static TCell GetCell<TCell, TPoint>(
			this IGrid<TCell, TPoint> grid, TPoint point)
			
			where TPoint : IGridPoint<TPoint>
		{
			return grid[point];
		}

		/// <summary>
		/// The same as `grid[point] = value`. This method is provided 
		/// to be consistent with GetCell.
		/// </summary>
		[Version(1,7)]
		public static void SetCell<TCell, TPoint>(
			this IGrid<TCell, TPoint> grid, TPoint point, TCell value)

			where TPoint : IGridPoint<TPoint>
		{
			grid[point] = value;
		}

		/// <summary>
		/// Returns the points in a grid neighborhood around the given center.
		/// </summary>
		[Version(1,8)]
		public static IEnumerable<RectPoint> GetNeighborHood<T>(this RectGrid<T> grid, RectPoint center, int radius)
		{
			for (int i = center.X - radius; i <= center.X + radius; i++)
			{
				for (int j = center.Y - radius; j <= center.Y + radius; j++)
				{
					var neighborhoodPoint = new RectPoint(i, j);

					if (grid.Contains(neighborhoodPoint))
					{
						yield return neighborhoodPoint;
					}
				}
			}
		}

		/// <summary>
		/// Fills all cells of a grid with the given value.
		/// </summary>
		[Version(1,10)]
		public static void Fill<TCell, TPoint>(this IGrid<TCell, TPoint> grid, TCell value)
			where TPoint : IGridPoint<TPoint>
		{
			foreach (var point in grid)
			{
				grid[point] = value;
			}
		}

		/// <summary>
		/// Fills all cells of a grid with the value returned by createValue.
		/// </summary>
		[Version(1, 10)]
		public static void Fill<TCell, TPoint>(this IGrid<TCell, TPoint> grid, Func<TCell> createValue)
			where TPoint : IGridPoint<TPoint>
		{
			foreach (var point in grid)
			{
				grid[point] = createValue();
			}
		}

		/// <summary>
		/// Fills the cell of each point of a grid with the value returned by createValue when passed the point as a parameter.
		/// </summary>
		[Version(1, 10)]
		public static void Fill<TCell, TPoint>(this IGrid<TCell, TPoint> grid, Func<TPoint, TCell> createValue)
			where TPoint : IGridPoint<TPoint>
		{
			foreach (var point in grid)
			{
				grid[point] = createValue(point);
			}
		}

		/// <summary>
		/// Clones the given grid, and fills all cells of the cloned grid with the given value.
		/// </summary>
		[Version(1, 10)]
		public static IGrid<TNewCell, TPoint> CloneStructure<TNewCell,  TPoint>(this IGrid<TPoint> grid, TNewCell value)
			where TPoint : IGridPoint<TPoint>
		{
			var newGrid = grid.CloneStructure<TNewCell>();
			newGrid.Fill(value);

			return newGrid;
		}

		/// <summary>
		/// Clones the given grid, and fills all cells of the cloned grid 
		/// with the value returned by createValue.
		/// </summary>
		[Version(1, 10)]
		public static IGrid<TNewCell, TPoint> CloneStructure<TNewCell, TPoint>(this IGrid<TPoint> grid, Func<TNewCell> createValue)
			where TPoint : IGridPoint<TPoint>
		{
			var newGrid = grid.CloneStructure<TNewCell>();
			newGrid.Fill(createValue);

			return newGrid;
		}

		/// <summary>
		/// Clones the given grid, and fills the cell at each point of the cloned grid with the value 
		/// returned by createValue when the point is passed as a parameter.
		/// </summary>
		[Version(1, 10)]
		public static IGrid<TNewCell, TPoint> CloneStructure<TNewCell, TPoint>(this IGrid<TPoint> grid, Func<TPoint, TNewCell> createValue)
			where TPoint : IGridPoint<TPoint>
		{
			var newGrid = grid.CloneStructure<TNewCell>();
			newGrid.Fill(createValue);

			return newGrid;
		}

		/// <summary>
		/// Applies the given action to all cells in the grid.
		/// </summary>
		/// <example>grid.Apply(cell =&gt; cell.Color = Color.red);</example>
		[Version(1, 10)]
		public static void Apply<TCell, TPoint>(this IGrid<TCell, TPoint> grid, Action<TCell> action)
			where TPoint : IGridPoint<TPoint>
		{
			foreach (var cell in grid.Values)
			{
				action(cell);
			}
		}

		/// <summary>
		/// Transforms all values in this grid using the given transformation.
		/// </summary>
		/// <example>gridOfNumbers.TransformValues(x =&gt; x + 1);</example>
		[Version(1, 10)]
		public static void TransformValues<TCell, TPoint>(this IGrid<TCell, TPoint> grid, Func<TCell, TCell> transformation)
			where TPoint : IGridPoint<TPoint>
		{
			foreach (var point in grid)
			{
				grid[point] = transformation(grid[point]);
			}
		}
	}
}