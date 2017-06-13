using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions.Algorithms;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// This class provide generic functions for common grid operations, such as finding a
	/// shortest path or connected shapes.
	/// </summary>
	public static class Algorithms
	{
		#region Graph Algorithms

		/// <summary>
		/// The set of points is connected if there is a path from one point to each other point in the set. A path
		/// exists between two points if isConnected returns true for the two points, or there exists a third point that has
		/// a path to both.
		/// </summary>
		/// <example>
		/// Checks whether the list of points are connected by color.
		/// <code>
		/// IsConnected(grid, points, (p, q) => grid[p].Color == grid[q].Color)
		/// </code>
		/// </example>
		/// <typeparam name="TPoint">The type of point of the grid that this algorithm takes.</typeparam>
		/// <param name="grid">The grid on which to do the check.</param>
		/// <param name="points">The set of points to check. It is assumed all points are in the grid.</param>
		/// <param name="getConnectedCells">The function to use to get the connected cells of the grid.</param>
		/// <returns>Returns true if the collection of points are connected, false otherwise.</returns>
		/// <remarks><para>Only points inside the given grid are considered. If this set is empty, by convention,
		/// false is returned.
		/// </para>
		/// <para>The <c>getConnectedCells</c> function can also be considered a function that returns the neighbors
		/// of a point. The function <c>getConnectedCells</c> must be symmetric in the sense that if p is in the set of
		/// points connected to q, then q is in the set of points connected to p. The function need not filter points
		/// that lie outside the grid, this is done by this algorithm.
		/// </para>
		/// </remarks>
		/// <exception cref="ArgumentNullException"><c>grid</c>, <c>points</c> or <c>getConnectedCells</c> is null.</exception>
		public static bool IsConnected<TPoint>(
			IGrid<TPoint> grid,
			IEnumerable<TPoint> points,
			Func<TPoint, IEnumerable<TPoint>> getConnectedCells
			)
		{
			if (grid == null) throw new ArgumentNullException("grid");
			if (points == null) throw new ArgumentNullException("points");
			if (getConnectedCells == null) throw new ArgumentNullException("getConnectedCells");

			var gridPoints = points.In(grid).ToList();

			if (!gridPoints.Any()) return false;


			var openSet = new HashSet<TPoint>();
			var closedSet = new HashSet<TPoint>();

			openSet.Add(gridPoints.First());

			while (!openSet.IsEmpty())
			{
				var current = openSet.First();

				openSet.Remove(current);
				closedSet.Add(current);

				//Adds all connected neighbors that
				//are in the grid and in the collection
				var connectedNeighbors = from neighbor in getConnectedCells(current).In(grid)
										 where !closedSet.Contains(neighbor)
											 && gridPoints.Contains(neighbor)
										 select neighbor;

				openSet.AddRange(connectedNeighbors);
			}

			return gridPoints.All(closedSet.Contains);
		}

		/// <summary>
		/// The set is connected if the set of points are neighbor-connected, and isNeighborsConnected return true for each
		/// two neighbors in the set.Two points are connected if they are neighbors, or one point has
		/// a neighbor that is neighbor-connected with the other point.
		///  
		/// Another way to put this is, this function returns true if there is a set that connects point1
		/// to point2.
		/// </summary>
		/// <example>
		/// Checks whether the two points are connected by color.
		/// <code>
		/// IsConnected(grid, p1, p2, (p, q) => grid[p].Color == grid[q].Color)
		/// </code>
		/// </example>
		/// <typeparam name="TCell">The type of cell of the grid that this algorithm takes.</typeparam>
		/// <typeparam name="TPoint">The type of point of the grid that this algorithm takes.</typeparam>
		/// <param name="grid">The grid on which to do the check</param>
		/// <param name="point1">The first point to check.</param>
		/// <param name="point2">The second point to check.</param>
		/// <param name="getAllNeighbors">The function to use to get all neighbors of the grid.</param>
		/// <param name="isNeighborsConnected">The function to use to check whether two neighbors are connected.</param>
		/// <returns>Returns true if the two points are in a connected set.</returns>
		public static bool IsConnected<TPoint, TCell>(
			IGrid<TPoint, TCell> grid,
			TPoint point1,
			TPoint point2,
			Func<TPoint, IEnumerable<TPoint>> getAllNeighbors,
			Func<TPoint, TPoint, bool> isNeighborsConnected)
		{
			var openSet = new HashSet<TPoint>() { point1 };
			var closedSet = new HashSet<TPoint>();

			while (!openSet.IsEmpty())
			{
				var current = openSet.First();

				if (current.Equals(point2))
				{
					return true;
				}

				openSet.Remove(current);
				closedSet.Add(current);

				var connectedNeighbors =
					from neighbor in getAllNeighbors(current).In(grid)
					where !closedSet.Contains(neighbor) && isNeighborsConnected(current, neighbor)
					select neighbor;

				openSet.AddRange(connectedNeighbors);
			}

			return false;
		}

		/// <summary>
		/// TODO: @herman
		/// </summary>
		/// <typeparam name="TCell">The type of cell of the grid that this algorithm takes.</typeparam>
		/// <typeparam name="TPoint">The type of point of the grid that this algorithm takes.</typeparam>
		/// <param name="grid">The grid from which to get the connected set.</param>
		/// <param name="point">Point where the check start.</param>
		/// <param name="getConnectedPoints">This function is used to get the connected Points.</param>
		/// <returns>Returns a list of points connected to the given point.</returns>
		// TODO to Explicit shape
		public static HashSet<TPoint> GetConnectedSet<TCell, TPoint>(
			IGrid<TPoint, TCell> grid,
			TPoint point,
			Func<TPoint, IEnumerable<TPoint>> getConnectedPoints)
		{
			var openSet = new HashSet<TPoint>() { point };
			var closedSet = new HashSet<TPoint>();

			while (!openSet.IsEmpty())
			{
				var current = openSet.First();

				openSet.Remove(current);
				closedSet.Add(current);

				var connectedNeighbors =
					from neighbor in getConnectedPoints(current).In(grid)
					where !closedSet.Contains(neighbor)
					select neighbor;

				openSet.AddRange(connectedNeighbors);
			}

			return closedSet;
		}

		//TODO: @herman, check the 'See' line.
		/// <summary>
		/// Find the shortest path between a start and goal node.
		/// 
		/// The distance between nodes(as defined by TPoint.DistanceFrom)
		/// are used as the heuristic and actual cost between nodes.In some cases the
		/// result may be unintuitive, and an overload specifying a different
		/// cost should be used.
		/// 
		/// See AStar&lt;TCell, TPoint&gt;(IGrid&lt;TCell, TPoint&gt;, TPoint, TPoint, Func&lt;TPoint, TPoint, float&gt;, Func&lt;TCell, bool&gt;, Func&lt;TPoint, TPoint, float&gt;)
		/// </summary>
		/// <typeparam name="TPoint"></typeparam>
		/// <param name="grid">Grid in which the operation is performed.</param>
		/// <param name="start">Point where the path must start.</param>
		/// <param name="goal">Point where the path must end.</param>
		/// <param name="heuristicCostEstimate">This function is used to calculate the heuristic.</param>
		/// <param name="isAccessible">This function is used to check if a point is accessible within the grid.</param>
		/// <param name="getConnectedPoints">This function is used to get the connected points of a given point.</param>
		/// <param name="neighborToNeighborCost">This fuction is used to calculate the cost of moving to a given neighbor.</param>
		/// <returns>The list of nodes on the path in order.If no path is possible, null is returned.</returns>
		public static IEnumerable<TPoint> AStar<TPoint>(
			IGrid<TPoint> grid,
			TPoint start,
			TPoint goal,
			Func<TPoint, TPoint, float> heuristicCostEstimate,
			Func<TPoint, bool> isAccessible,
			Func<TPoint, IEnumerable<TPoint>> getConnectedPoints,
			Func<TPoint, TPoint, float> neighborToNeighborCost)
		{
			var closedSet = new HashSet<TPoint>();

			// The set of tentative nodes to be evaluated
			var openSet = new HashSet<TPoint>() { start };

			// The map of navigated nodes.
			var cameFrom = new Dictionary<TPoint, TPoint>();

			// Cost from start along best known path.
			var gScore = new Dictionary<TPoint, float>();
			gScore[start] = 0;

			// Estimated total cost from start to goal through y.
			var fScore = new Dictionary<TPoint, float>();
			fScore[start] = gScore[start] + heuristicCostEstimate(start, goal);

			var currentNeighbors = new List<TPoint>();

			while (!openSet.IsEmpty())
			{
				var current = FindNodeWithLowestScore(openSet, fScore);

				if (current.Equals(goal))
				{
					return ReconstructPath(cameFrom, goal);
				}

				openSet.Remove(current);
				closedSet.Add(current);

				CheckGridNeighbors(getConnectedPoints(current), grid, currentNeighbors);

				for (int i = 0; i < currentNeighbors.Count; i++)
				{
					var neighborPoint = currentNeighbors[i];
					if (!isAccessible(neighborPoint))
						continue;

					var tentativeGScore = gScore[current] + neighborToNeighborCost(current, neighborPoint);

					if (closedSet.Contains(neighborPoint) && tentativeGScore >= gScore[neighborPoint])
						continue;

					if (openSet.Contains(neighborPoint) && !(tentativeGScore < gScore[neighborPoint]))
						continue;

					cameFrom[neighborPoint] = current;
					gScore[neighborPoint] = tentativeGScore;
					fScore[neighborPoint] = gScore[neighborPoint] + heuristicCostEstimate(neighborPoint, goal);

					if (openSet.Contains(neighborPoint))
						continue;

					openSet.Add(neighborPoint);
				}
			}

			return null;
		}

		//TODO: Refactor with method below
		//[Experimental]
		/// <summary>
		/// Find the shortest path between a start and goal node.
		/// 
		/// The distance between nodes(as defined by TPoint.DistanceFrom)
		/// are used as the hesuristic and actual cost between nodes.In some cases the
		/// result may be unintuitive, and an overload specifying a different
		/// cost should be used.
		/// 
		/// See AStar&lt;TCell, TPoint&gt;(IGrid&lt;TCell, TPoint&gt;, TPoint, TPoint, Func&lt;TPoint, TPoint, float&gt;, Func&lt;TCell, bool&gt;, Func&lt;TPoint, TPoint, float&gt;)
		/// </summary>
		/// <typeparam name="TPoint"></typeparam>
		/// <param name="grid">Grid in which the operation is performed.</param>
		/// <param name="start">Point where the path must start.</param>
		/// <param name="goal">Point where the path must end.</param>
		/// <param name="heuristicCostEstimate">This function is used to calculate the heuristic.</param>
		/// <param name="isAccessible">This function is used to check if a point is accessible within the grid.</param>
		/// <param name="getConnectedCells">This function is used to get the connected cells of a given point.</param>
		/// <param name="neighborToNeighborCost">This fuction is used to calculate the cost of moving to a given neighbor.</param>
		/// <returns>The list of nodes on the path in order.If no path is possible, null is returned.</returns>
		//TODO: Didn't touch this when optimizing the algorightms but should be the same as AStar above (@omarrojo)
		private static IEnumerable<TPoint> AStarBenchmark<TPoint>(
			IGrid<TPoint> grid,
			TPoint start,
			TPoint goal,
			Func<TPoint, TPoint, float> heuristicCostEstimate,
			Func<TPoint, bool> isAccessible,
			Func<TPoint, IEnumerable<TPoint>> getConnectedCells,
			Func<TPoint, TPoint, float> neighborToNeighborCost)
		{
			var closedSet = new HashSet<TPoint>(new EqualsComparer<TPoint>());

			// The set of tentative nodes to be evaluated
			var openSet = new HashSet<TPoint>(new EqualsComparer<TPoint>()) { start };

			// The map of navigated nodes.
			var cameFrom = new Dictionary<TPoint, TPoint>(new EqualsComparer<TPoint>());

			// Cost from start along best known path.
			var gScore = new Dictionary<TPoint, float>(new EqualsComparer<TPoint>());
			gScore[start] = 0;

			// Estimated total cost from start to goal through y.
			var fScore = new Dictionary<TPoint, float>(new EqualsComparer<TPoint>());
			fScore[start] = gScore[start] + heuristicCostEstimate(start, goal);

			while (!openSet.IsEmpty())
			{
				var current = FindNodeWithLowestScore(openSet, fScore);

				if (current.Equals(goal))
				{
					return ReconstructPath(cameFrom, goal);
				}

				openSet.Remove(current);
				closedSet.Add(current);

				var currentNodeNeighbors = getConnectedCells(current).In(grid);

				var accessibleNeighbors = from neighbor in currentNodeNeighbors
										  where isAccessible(neighbor)
										  select neighbor;

				foreach (var neighbor in accessibleNeighbors)
				{
					var tentativeGScore = gScore[current] + neighborToNeighborCost(current, neighbor);

					if (closedSet.Contains(neighbor))
					{
						if (tentativeGScore >= gScore[neighbor])
						{
							continue;
						}
					}

					if (!openSet.Contains(neighbor) || tentativeGScore < gScore[neighbor])
					{
						cameFrom[neighbor] = current;
						gScore[neighbor] = tentativeGScore;
						fScore[neighbor] = gScore[neighbor] + heuristicCostEstimate(neighbor, goal);

						if (!openSet.Contains(neighbor))
						{
							openSet.Add(neighbor);
						}
					}
				}
			}

			return null;
		}

		/// <summary>
		/// A generic function that returns the points in range
		/// based on a given start point, moveRange, and a function that returns the
		/// cost of moving between neighboring cells.
		/// </summary>
		/// <typeparam name="TPoint">The type of the grid point.</typeparam>
		/// <param name="grid">The grid to use for the calculations.</param>
		/// <param name="start">The starting point for the range calculation.</param>
		/// <param name="getConnectedPoints">A function that returns a set of points connected to this point.</param>
		/// <param name="isAccessible">Whether the cell at the given point can be reached.</param>
		/// <param name="getCellMoveCost">A function that returns the move cost given a cell.</param>
		/// <param name="moveRange">The range from which to return cells.</param>
		/// <returns>IEnumerable&lt;TPoint&gt;.</returns>
		/// <author>Justin Kivak</author>
		public static IEnumerable<TPoint> GetPointsInRange<TPoint>(
			IGrid<TPoint> grid,
			TPoint start,
			Func<TPoint, IEnumerable<TPoint>> getConnectedPoints,
			Func<TPoint, bool> isAccessible,
			Func<TPoint, TPoint, float> getCellMoveCost,
			float moveRange)
		{
			// Nodes in range
			var closedSet = new HashSet<TPoint>();
			var costToReach = new Dictionary<TPoint, float>();
			var openSet = new StructList<TPoint> { start };

			// Cost from start along best known path up to current point
			var costToReachContains = grid.CloneStructure(false);

			costToReach[start] = 0;

			var currentNeighbors = new List<TPoint>();

			while (!openSet.IsEmpty())
			{
				// Process current node
				var current = FindNodeWithLowestScore(openSet, costToReach);
				openSet.Remove(current);
				closedSet.Add(current);

				CheckGridNeighbors(getConnectedPoints(current), grid, currentNeighbors);

				for (int i = 0; i < currentNeighbors.Count; i++)
				{
					var neighbor = currentNeighbors[i];

					if (!closedSet.Contains(neighbor) &&
						!openSet.Contains(neighbor) &&
						isAccessible(neighbor) &&
						(costToReach[current] + getCellMoveCost(current, neighbor) <= moveRange)
						)
					{
						// Cost of current node + neighbor's move cost
						float newCost = costToReach[current] + getCellMoveCost(current, neighbor);

						if (costToReachContains[neighbor])
						{
							if (costToReach[neighbor] > newCost)
							{
								costToReach[neighbor] = newCost;
							}
						}
						else
						{
							costToReach[neighbor] = newCost;
							costToReachContains[neighbor] = true;
						}

						openSet.Add(neighbor);
					}
				}
			}

			return closedSet;
		}

		/// <summary>
		/// A generic function that returns the points in range
		/// based on a given start point, moveRange, and a function that returns the
		/// cost of moving between neighboring cells.
		/// 
		/// author Justin Kivak
		/// </summary>
		/// <example>
		/// Example usage:
		/// <code>
		/// var tilesInRange = Algorithms.GetPointsInRange(
		///		grid,
		///		start,
		///		tile1, tile2 => DistanceBetween(tile1, tile2),
		///		5f);
		/// </code>
		/// </example>
		/// <param name="grid">The grid to use for the calculations</param>
		/// <param name="start">The starting point for the range calculation</param>
		/// <param name="getConnectedPoints">This function is used to get the connected Points to a point.</param>
		/// <param name="isAcessible">Whether the given cell can be reached</param>
		///  <param name="getCellMoveCost">A function that returns the move cost given a cell</param>
		///  <param name="moveRange">The range from which to return cells</param>
		public static IEnumerable<TPoint> GetPointsInRange<TPoint>(
			IGrid<TPoint> grid,
			TPoint start,
			Func<TPoint, IEnumerable<TPoint>> getConnectedPoints,
			Func<TPoint, bool> isAcessible,
			Func<TPoint, TPoint, int> getCellMoveCost,
			int moveRange)
		{
			// Nodes in range
			var closedSet = new HashSet<TPoint>();
			var costToReach = new Dictionary<TPoint, int>();
			var openSet = new StructList<TPoint> { start };

			// Cost from start along best known path up to current point
			var costToReachContains = grid.CloneStructure(false);

			costToReach[start] = 0;

			var currentNeighbors = new List<TPoint>();

			while (!openSet.IsEmpty())
			{
				// Process current node
				var current = FindNodeWithLowestScore(openSet, costToReach);
				openSet.Remove(current);
				closedSet.Add(current);

				CheckGridNeighbors(getConnectedPoints(current), grid, currentNeighbors);

				for (int i = 0; i < currentNeighbors.Count; i++)
				{
					var neighbor = currentNeighbors[i];

					if (!closedSet.Contains(neighbor) &&
						!openSet.Contains(neighbor) &&
						isAcessible(neighbor) &&
						(costToReach[current] + getCellMoveCost(current, neighbor) <= moveRange)
						)
					{
						// Cost of current node + neighbor's move cost
						int newCost = costToReach[current] + getCellMoveCost(current, neighbor);

						if (costToReachContains[neighbor])
						{
							if (costToReach[neighbor] > newCost)
							{
								costToReach[neighbor] = newCost;
							}
						}
						else
						{
							costToReach[neighbor] = newCost;
							costToReachContains[neighbor] = true;
						}

						openSet.Add(neighbor);
					}
				}
			}

			return closedSet;
		}

		/// <summary>
		/// A generic function that returns the points in range
		/// based on a given start point, moveRange, and a function that returns the
		/// cost of moving between neighboring cells.
		/// 
		/// author Justin Kivak
		/// </summary>
		/// <example>
		/// Example usage:
		/// <code>
		/// var costs = Algorithms.GetPointsInRangeCost(
		/// 		grid,
		/// 		start,
		/// 		tile1, tile2 => DistanceBetween(tile1, tile2),
		/// 		5f);
		/// </code>
		/// </example>
		/// <param name="grid">The grid to use for the calculations</param>
		/// <param name="start">The starting point for the range calculation</param>
		/// <param name="isAcessible">Whether the cell at the given point can be reached.</param>
		/// <param name="getCellMoveCost">A function that returns the move cost given a cell</param>
		/// <param name="moveRange">The range from which to return cells</param>
		/// <param name="getConnectedPoints">This function is used to get the connected Points to a point.</param>
		public static Dictionary<TPoint, float> GetPointsInRangeCost<TPoint>(
			IGrid<TPoint> grid,
			TPoint start,
			Func<TPoint, IEnumerable<TPoint>> getConnectedPoints,
			Func<TPoint, bool> isAcessible,
			Func<TPoint, TPoint, float> getCellMoveCost,
			float moveRange)

		{
			// Nodes in range
			var closedSet = new HashSet<TPoint>();
			var costToReach = new Dictionary<TPoint, float>();
			var openSet = new StructList<TPoint> { start };

			// Cost from start along best known path up to current point
			var costToReachContains = grid.CloneStructure(false);

			costToReach[start] = 0;

			var currentNeighbors = new List<TPoint>();

			while (!openSet.IsEmpty())
			{
				// Process current node
				var current = FindNodeWithLowestScore(openSet, costToReach);
				openSet.Remove(current);

				closedSet.Add(current);

				CheckGridNeighbors(getConnectedPoints(current), grid, currentNeighbors);

				for (int i = 0; i < currentNeighbors.Count; i++)
				{
					var neighbor = currentNeighbors[i];

					if (!closedSet.Contains(neighbor) &&
						!openSet.Contains(neighbor) &&
						isAcessible(neighbor) &&
						(costToReach[current] + getCellMoveCost(current, neighbor) <= moveRange)
						)
					{
						// Cost of current node + neighbor's move cost
						float newCost = costToReach[current] + getCellMoveCost(current, neighbor);

						if (costToReachContains[neighbor])
						{
							if (costToReach[neighbor] > newCost)
							{
								costToReach[neighbor] = newCost;
							}
						}
						else
						{
							costToReach[neighbor] = newCost;
							costToReachContains[neighbor] = true;
						}

						openSet.Add(neighbor);
					}
				}
			}

			return costToReach;
		}

		/// <summary>
		/// Gets all the points (and their costs) from a given point in a given range. This result is stored and returned
		/// in a dictionary.
		/// </summary>
		/// <typeparam name="TPoint">The type of the point used on the grid.</typeparam>
		/// <param name="grid">The grid used to make the calculations.</param>
		/// <param name="start">Point where the calculations will start.</param>
		/// <param name="getConnectedPoints">This function is used to get the connected points to a given point.</param>
		/// <param name="isAccessible">This function is used to know if a point is acessible within the grid.</param>
		/// <param name="getCellMoveCost">This function is used to get the movement cost from moving to a cell.</param>
		/// <param name="moveRange">Range of movement where make the calculations.</param>
		/// <returns></returns>
		public static Dictionary<TPoint, int> GetPointsInRangeCost<TPoint>(
			IGrid<TPoint> grid,
			TPoint start,
			Func<TPoint, IEnumerable<TPoint>> getConnectedPoints,
			Func<TPoint, bool> isAccessible,
			Func<TPoint, TPoint, int> getCellMoveCost,
			int moveRange)
		{
			// Nodes in range
			var closedSet = new HashSet<TPoint>();
			var costToReach = new Dictionary<TPoint, int>();
			var openSet = new StructList<TPoint> { start };

			// Cost from start along best known path up to current point
			var costToReachContains = grid.CloneStructure(false);

			costToReach[start] = 0;

			var currentNeighbors = new List<TPoint>();

			while (!openSet.IsEmpty())
			{
				// Process current node
				var current = FindNodeWithLowestScore(openSet, costToReach);
				openSet.Remove(current);

				closedSet.Add(current);

				CheckGridNeighbors(getConnectedPoints(current), grid, currentNeighbors);

				for (int i = 0; i < currentNeighbors.Count; i++)
				{
					var neighbor = currentNeighbors[i];

					if (!closedSet.Contains(neighbor) &&
						!openSet.Contains(neighbor) &&
						isAccessible(neighbor) &&
						(costToReach[current] + getCellMoveCost(current, neighbor) <= moveRange)
						)
					{
						// Cost of current node + neighbor's move cost
						int newCost = costToReach[current] + getCellMoveCost(current, neighbor);

						if (costToReachContains[neighbor])
						{
							if (costToReach[neighbor] > newCost)
							{
								costToReach[neighbor] = newCost;
							}
						}
						else
						{
							costToReach[neighbor] = newCost;
							costToReachContains[neighbor] = true;
						}

						openSet.Add(neighbor);
					}
				}
			}

			return costToReach;
		}

		#endregion

		#region Lines

		//TODO: @herman, check at the examples.
		/// <summary>
		/// Returns a list containing lines connected to the given points. A line is a list of points.
		/// Only returns correct results for square or hex grids.
		/// </summary>
		/// <example>
		/// <code>
		/// private bool IsSameColour(point1, point2)
		/// {
		/// 		return grid[point1].Color == grid[point2].Color;
		/// }
		/// 
		/// private SomeMethod()
		/// {
		/// 		...
		/// 		var rays = GetConnectedRays&lt;ColourCell, PointyHexPoint, PointyHexNeighborIndex&gt;(
		/// 			grid, point, IsSameColour);
		/// 		...
		/// }
		/// </code>
		/// You can of course also use a lambda expression, like this:
		/// <code>
		/// //The following code returns all lines that radiate from the given point
		/// GetConnectedRays&lt;ColourCell, PointyHexPoint, PointyHexNeighborIndex&gt;(
		/// 		grid, point, (x, y) => grid[x].Color == grid[y].Color);
		/// </code>
		/// </example>
		/// <typeparam name="TPoint">The type of point of the grid that this algorithm takes.</typeparam>
		/// <param name="grid">Grid in which the calculations are made.</param>
		/// <param name="point">Point where the calculations start.</param>
		/// <param name="rayGenerators"></param> //TODO: Remove this argument? Make that as part of the rayGenerator?
		/// <param name="isNeighborsConnected">
		/// A functions that returns true or false, depending on whether
		/// two points can be considered connected when they are neighbors.For example, if you want
		/// rays of points that refer to cells of the same color, you can pass in a functions that
		/// compares the DefaultColors of cells.
		/// </param>
		public static IEnumerable<IEnumerable<TPoint>> GetConnectedRays<TPoint>(
			IImplicitShape<TPoint> grid,
			TPoint point,
			IEnumerable<IForwardMap<TPoint, TPoint>> rayGenerators,
			Func<TPoint, TPoint, bool> isNeighborsConnected)
		{
			var lines = new List<IEnumerable<TPoint>>();

			foreach (var rayGenertor in rayGenerators)
			{
				var line = new StructList<TPoint>();

				var rayEnd = point;

				while (grid.Contains(rayEnd) && isNeighborsConnected(point, rayEnd))
				{
					line.Add(rayEnd);
					rayEnd = rayGenertor.Forward(rayEnd);
				}

				if (line.Count > 1)
				{
					lines.Add(line);
				}
			}

			return lines;
		}

		/// <summary>
		/// Gets the longest of the rays connected to this cell.
		/// <see cref="GetConnectedRays{TPoint}"/>
		/// </summary>
		/// <typeparam name="TPoint">The type of point of the grid that this algorithm takes.</typeparam>
		/// <param name="grid">Grid in which the calculations are made.</param>
		/// <param name="point">Point where the calculations start.</param>
		/// <param name="rayGenerators">List of ray generators.</param> //TODO is Generator the right word?
		/// <param name="isNeighborsConnected">
		/// A functions that returns true or false, depending on whether
		/// two points can be considered connected when they are neighbors.For example, if you want
		/// rays of points that refer to cells of the same color, you can pass in a functions that
		/// compares the DefaultColors of cells.
		/// </param>
		public static IEnumerable<TPoint> GetLongestConnectedRay<TPoint>(
			IImplicitShape<TPoint> grid,
			TPoint point,
			IEnumerable<IForwardMap<TPoint, TPoint>> rayGenerators,
			Func<TPoint, TPoint, bool> isNeighborsConnected)
		{
			var rays = GetConnectedRays(grid, point, rayGenerators, isNeighborsConnected);

			return GetBiggestShape(rays);
		}

		/// <summary>
		/// Gets the longest line of connected points that contains this point.
		/// <see cref="GetConnectedRays{TPoint}"/>
		/// </summary>
		/// <typeparam name="TPoint">The type of point of the grid that this algorithm takes.</typeparam>
		/// <param name="grid">Grid in which the calculations are made.</param>
		/// <param name="point">Point where the calculations start.</param>
		/// <param name="lineGenerators">List of line generators.</param> //TODO is Generator the right word?
		/// <param name="isNeighborsConnected">
		/// A functions that returns true or false, depending on whether
		/// two points can be considered connected when they are neighbors.For example, if you want
		/// rays of points that refer to cells of the same color, you can pass in a functions that
		/// compares the DefaultColors of cells.
		/// </param>
		public static IEnumerable<IEnumerable<TPoint>> GetConnectedLines<TPoint>(
			IImplicitShape<TPoint> grid,
			TPoint point,
			IEnumerable<IMap<TPoint, TPoint>> lineGenerators,
			Func<TPoint, TPoint, bool> isNeighborsConnected)
		{
			var lines = new List<IEnumerable<TPoint>>();

			foreach (var lineGenerator in lineGenerators)
			{
				var line = new StructList<TPoint>();
				var edge = point;

				//go forwards
				while (grid.Contains(edge) && isNeighborsConnected(point, edge))
				{
					edge = lineGenerator.Forward(edge);
				}

				//TPoint oppositeNeighbor = point.MoveBy(direction.Negate());
				edge = lineGenerator.Reverse(edge);

				//go backwards
				while (grid.Contains(edge) && isNeighborsConnected(point, edge))
				{
					line.Add(edge);
					edge = lineGenerator.Reverse(edge);
				}

				if (line.Count > 1)
				{
					lines.Add(line);
				}
			}

			return lines;
		}

		/// <summary>
		/// Get the longest line of points connected to the given point
		/// </summary>
		/// <typeparam name="TPoint">The type of point of the grid that this algorithm takes.</typeparam>
		/// <param name="grid">Grid in which the calculations are made.</param>
		/// <param name="point">Point where the calculations start.</param>
		/// <param name="lineGenerators">List of line generators.</param> //TODO is Generator the right word?
		/// <param name="isNeighborsConnected">
		/// A functions that returns true or false, depending on whether
		/// two points can be considered connected when they are neighbors.For example, if you want
		/// rays of points that refer to cells of the same color, you can pass in a functions that
		/// compares the DefaultColors of cells.
		/// </param>
		public static IEnumerable<TPoint> GetLongestConnectedLine<TPoint>(
			IImplicitShape<TPoint> grid,
			TPoint point,
			IEnumerable<IMap<TPoint, TPoint>> lineGenerators,
			Func<TPoint, TPoint, bool> isNeighborsConnected)
		{
			var lines = GetConnectedLines(grid, point, lineGenerators, isNeighborsConnected);

			return GetBiggestShape(lines);
		}

		#endregion

		#region Shapes (Collections of points)

		//TODO use new shapes
		/// <summary>
		/// Gets the biggest shape (by number of points) in the given list.
		/// </summary>
		/// <typeparam name="TPoint">The type of point of the shapes.</typeparam>
		/// <param name="shapes">Each shape is represented as a list of points.</param>
		public static IEnumerable<TPoint> GetBiggestShape<TPoint>(
			IEnumerable<IEnumerable<TPoint>> shapes)
		{
			return shapes.MaxBy(x => x.Count());
		}

		/// <summary>
		/// Checks whether all the points in smallShape are contained in the bigShape.
		/// </summary>
		/// <typeparam name="TPoint">The type of point of the shapes.</typeparam>
		/// <param name="bigShape">List of point representing the big shape.</param>
		/// <param name="smallShape">List of point representing the small shape.</param>
		/// <returns></returns>
		public static bool Contains<TPoint>(IEnumerable<TPoint> bigShape, IEnumerable<TPoint> smallShape)
		{
			return smallShape.All(bigShape.Contains);
		}

		/// <summary>
		/// Checks if two shapes are equivalent. This happen when all shape1 points are contained in shape2 and vice versa.
		/// </summary>
		/// <typeparam name="TPoint">The type of point of the shapes.</typeparam>
		/// <param name="shape1">List of points of the shape1.</param>
		/// <param name="shape2">List of points of the shape2.</param>
		/// <returns></returns>
		public static bool IsEquivalent<TPoint>(IEnumerable<TPoint> shape1, IEnumerable<TPoint> shape2)
		{
			if (ReferenceEquals(shape1, shape2))
			{
				return true;
			}

			return Contains(shape1, shape2) && Contains(shape2, shape1);
		}

		//TODO: Should this be an extension instead?
		/// <summary>
		/// Transform each point in the list with the give point transformation.
		/// </summary>
		/// <typeparam name="TPoint">The type of point of the shapes.</typeparam>
		/// <param name="shape">List of points of the shape.</param>
		/// <param name="pointTransformation">This function is used to transform all points of the shape.</param>
		public static IEnumerable<TPoint> TransformShape<TPoint>(
			IEnumerable<TPoint> shape,
			Func<TPoint, TPoint> pointTransformation)
		{
			return from point in shape
				   select pointTransformation(point);
		}

		/// <summary>
		/// Applies a function to the shapes and then checks if the results are equivalent.
		/// </summary>
		/// <typeparam name="TPoint">The type of point of the shapes.</typeparam>
		/// <param name="shape1">List of points of the shape1.</param>
		/// <param name="shape2">List of points of the shape2.</param>
		/// <param name="toCanonicalPosition">This function is used to translate the points of the shapes.</param>
		public static bool IsEquivalentUnderTranslation<TPoint>(
			IEnumerable<TPoint> shape1,
			IEnumerable<TPoint> shape2,
			Func<IEnumerable<TPoint>, IEnumerable<TPoint>> toCanonicalPosition)
		{
			return IsEquivalent(
				toCanonicalPosition(shape1),
				toCanonicalPosition(shape2));
		}

		/// <summary>
		/// Applies a function to the shapes and then checks if the results are equivalent.
		/// </summary>
		/// <typeparam name="TPoint">The type of point of the shapes.</typeparam>
		/// <param name="shape1">List of points of the shape1.</param>
		/// <param name="shape2">List of points of the shape2.</param>
		/// <param name="pointTransformations">List of transformed points.</param>
		/// <param name="toCanonicalPosition">This function is used to translate the points of the shapes.</param>
		public static bool IsEquivalentUnderTransformsAndTranslation<TPoint>(
			IEnumerable<TPoint> shape1,
			IEnumerable<TPoint> shape2,
			IEnumerable<Func<TPoint, TPoint>> pointTransformations,
			Func<IEnumerable<TPoint>, IEnumerable<TPoint>> toCanonicalPosition)
		{
			var canonicalShape1 = toCanonicalPosition(shape1);
			var canonicalShape2 = toCanonicalPosition(shape2);

			if (IsEquivalent<TPoint>(canonicalShape1, canonicalShape2))
			{
				return true;
			}

			foreach (var pointTransformation in pointTransformations)
			{
				canonicalShape2 = toCanonicalPosition(TransformShape<TPoint>(shape2, pointTransformation));

				if (IsEquivalent<TPoint>(canonicalShape1, canonicalShape2))
				{
					return true;
				}
			}

			return false;
		}

		#endregion

		#region FilterType

		/// <summary>
		/// Creates a new grid where the neighbors of a point are aggregate to the grid changing the type of the points.
		/// </summary>
		/// <typeparam name="TPoint">The type of the points of the grid.</typeparam>
		/// <typeparam name="TResultCell">The type of the points of the resulting grid.</typeparam>
		/// <param name="grid">Grid in which the operations are performed.</param>
		/// <param name="getNeighborHood">This function is used to get all the neighborhood of a given point.</param>
		/// <param name="aggregator">This function is used to aggregate a list of point into a grid.</param>
		public static IGrid<TPoint, TResultCell>
			AggregateNeighborhood<TPoint, TResultCell>(
				IGrid<TPoint> grid,
				Func<TPoint, IEnumerable<TPoint>> getNeighborHood,
				Func<IEnumerable<TPoint>, TResultCell> aggregator)
		{
			var newGrid = grid.CloneStructure<TResultCell>();

			foreach (var point in newGrid.Points)
			{
				newGrid[point] = aggregator(getNeighborHood(point));
			}

			return newGrid;
		}

		/// <summary>
		/// Creates a new grid where the neighbors of a point are aggregated using a function.
		/// </summary>
		/// <typeparam name="TPoint">The type of the points of the grid.</typeparam>
		/// <typeparam name="TCell">The type of the cells of the grid.</typeparam>
		/// <param name="grid">Grid in which the operations are performed.</param>
		/// <param name="getNeighborHood">This function is used to get all the neighborhood of a given point.</param>
		/// <param name="aggregator">This function is used to aggregate a list of point into a grid.</param>
		public static void
			AggregateNeighborhood<TCell, TPoint>(
				IGrid<TPoint, TCell> grid,
				Func<TPoint, IEnumerable<TPoint>> getNeighborHood,
				Func<IEnumerable<TPoint>, TCell> aggregator)
		{
			var newGrid = grid.CloneStructure<TCell>();

			foreach (var point in newGrid.Points)
			{
				newGrid[point] = aggregator(getNeighborHood(point));
			}

			foreach (var point in grid.Points)
			{
				grid[point] = newGrid[point];
			}
		}

		#endregion

		#region Helpers

		/// <summary>
		/// It search for the point with the lowest score in a list of points using a dictionary that pair points with scores.
		/// </summary>
		/// <typeparam name="TPoint">The type of the points.</typeparam>
		/// <param name="list">List that hold the Points.</param>
		/// <param name="scoreTable">Dictionary that pairs the points with their score.</param>
		private static TPoint FindNodeWithLowestScore<TPoint>(IList<TPoint> list, IDictionary<TPoint, float> scoreTable)
		{
			var minItem = list[0];
			var minScore = scoreTable[minItem];

			for (int i = 1; i < list.Count; i++)
			{
				var item = list[i];
				var score = scoreTable[item];

				if (score < minScore)
				{
					minScore = score;
					minItem = item;
				}
			}

			return minItem;
		}

		/// <summary>
		/// It search for the point with the lowest score in a IEnumerable of points using a dictionary that pair points with scores.
		/// </summary>
		/// <typeparam name="TPoint">The type of the points.</typeparam>
		/// <param name="list">IEnumerable that hold the Points.</param>
		/// <param name="scoreTable">Dictionary that pairs the points with their score.</param>
		private static TPoint FindNodeWithLowestScore<TPoint>(IEnumerable<TPoint> list, IDictionary<TPoint, float> scoreTable)
		{
			//return list.MinBy(x => scoreTable[x]);

			//UnityEngine.Debug.Log(list.ListToString());

			var first = true;
			TPoint minPoint = default(TPoint);
			var min = 0f;

			if (list.Count() == 0)
			{
				throw new ArgumentException("The list of Points can't be empty");
			}

			foreach (var point in list)
			{
				if (!first)
				{
					if (scoreTable[point] < min)
					{
						min = scoreTable[point];
						minPoint = point;
					}
				}
				else
				{
					minPoint = point;
					min = scoreTable[minPoint];

					first = false;
				}
			}

			return minPoint;
		}

		/// <summary>
		/// It search for the point with the lowest score in a list of points using a dictionary that pair points with scores.
		/// </summary>
		/// <typeparam name="TPoint">The type of the points.</typeparam>
		/// <param name="list">List that hold the Points.</param>
		/// <param name="scoreTable">Dictionary that pairs the points with their score.</param>
		private static TPoint FindNodeWithLowestScore<TPoint>(IList<TPoint> list, IDictionary<TPoint, int> scoreTable)
		{
			var minItem = list[0];
			var minScore = scoreTable[minItem];

			for (int i = 1; i < list.Count; i++)
			{
				var item = list[i];
				var score = scoreTable[item];

				if (score < minScore)
				{
					minScore = score;
					minItem = item;
				}
			}

			return minItem;
		}

		//TODO remove construction of list!
		/// <summary>
		/// It construct a list of Points that represent a path by using a Dictionary that hold the information from where the point came from.
		/// </summary>
		/// <typeparam name="TPoint">The type of the point.</typeparam>
		/// <param name="cameFrom">Dictionary holding the information from where the point came from.</param>
		/// <param name="currentNode">Node that start the path.</param>
		private static IList<TPoint> ReconstructPath<TPoint>(
			Dictionary<TPoint, TPoint> cameFrom,
			TPoint currentNode)
		{
			IList<TPoint> path = new StructList<TPoint>();

			ReconstructPath(cameFrom, currentNode, path);

			return path;
		}

		/// <summary>
		/// It construct a list of Points that represent a path by using a Dictionary that hold the information from where the point came from.
		/// </summary>
		/// <typeparam name="TPoint">The type of the point.</typeparam>
		/// <param name="cameFrom">Dictionary holding the information from where the point came from.</param>
		/// <param name="currentNode">Current node of the execution.</param>
		/// <param name="path">List with the actual constructed path.</param>
		private static void ReconstructPath<TPoint>(Dictionary<TPoint, TPoint> cameFrom, TPoint currentNode, IList<TPoint> path)
		{
			if (cameFrom.ContainsKey(currentNode))
			{
				ReconstructPath(cameFrom, cameFrom[currentNode], path);
			}

			path.Add(currentNode);
		}

		/// <summary>
		/// Only return neighbors of the point that are inside the grid, as defined by Contains.
		/// </summary>
		private static void CheckGridNeighbors<TPoint>(
			IEnumerable<TPoint> potentialNeighbors,
			IGrid<TPoint> grid,
			IList<TPoint> result)
		{
			result.Clear();

			foreach (var neighbor in potentialNeighbors)
			{
				if (grid.Contains(neighbor))
				{
					result.Add(neighbor);
				}
			}
		}

		#endregion
	}
}
