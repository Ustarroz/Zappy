using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2.Examples.Algorithms.PathFinding
{
	public class PathFindingPointyHexGrid : GridBehaviour<GridPoint2, TileCell>
	{
		public PathMode pathMode;
		public SpriteCell pathPrefab;
		public GameObject pathRoot;

		[Header("Colors")]
		public Gradient weightGradient;

		public Color blockedColor = Color.black;
		public Color pathColor = Utils.DefaultColors[6];
		public Color startColor = Utils.DefaultColors[5];
		public Color goalColor = Utils.DefaultColors[7];

		private GridPoint2 start;
		private GridPoint2 goal;
		private bool selectStart = true; //otherwise, select goal

		private Grid2<WalkableCell> walkableGrid;

		public override void InitGrid()
		{
			walkableGrid = (Grid2<WalkableCell>)Grid.CloneStructure<WalkableCell>();

			foreach (var gridPoint2 in walkableGrid.Points)
			{
				walkableGrid[gridPoint2] = Grid[gridPoint2].GetComponent<WalkableCell>();
				walkableGrid[gridPoint2].IsWalkable = true;
				float x0 = Grid.Bounds.Point.X;
				float y0 = Grid.Bounds.Point.Y;

				float width = Grid.Bounds.Extreme.X - x0;
				float height = Grid.Bounds.Extreme.Y - y0;


				var movementCost =
					+0.5f * Mathf.PerlinNoise(
						(gridPoint2.X - x0) / width,
						(gridPoint2.Y - y0) / height)

					+ .25f * Mathf.PerlinNoise(
						(gridPoint2.X - x0) / width * 2f,
						(gridPoint2.Y - y0) / height * 2f)

					+ .125f * Mathf.PerlinNoise(
						(gridPoint2.X - x0) / width * 4f,
						(gridPoint2.Y - y0) / height * 4f)

					+ .0625f * Mathf.PerlinNoise(
						(gridPoint2.X - x0) / width * 8f,
						(gridPoint2.Y - y0) / height * 8f)
					; //1 + Random.value*2;

				walkableGrid[gridPoint2].MovementCost = movementCost;
			}

			var min = Grid.Min(pcp => walkableGrid[pcp.point].MovementCost);
			var max = Grid.Max(pcp => walkableGrid[pcp.point].MovementCost);

			foreach (var walkableCell in walkableGrid.Cells)
			{
				if (min != max)
					walkableCell.MovementCost = 1 + 1 * (walkableCell.MovementCost - min) / (max - min);

				walkableCell.Color = weightGradient.Evaluate((walkableCell.MovementCost - 1) / 2);
			}

			start = walkableGrid.First().point;
			goal = walkableGrid.Last().point;

			UpdatePath();
		}


		/// <summary>
		/// Returns the Euclidean distance (in world units)
		/// between the given grid points
		/// </summary>
		private float EuclideanDistance(GridPoint2 p, GridPoint2 q)
		{
			float dX = GridMap.GridToWorld(p).x - GridMap.GridToWorld(q).x;
			float dY = GridMap.GridToWorld(p).y - GridMap.GridToWorld(q).y;

			float distance = Mathf.Sqrt(dX * dX + dY * dY);

			return distance;
		}

		public void OnLeftClick(GridPoint2 clickedPoint)
		{
			ToggleCellWalkability(clickedPoint);
			UpdatePath();
		}

		public void OnRightClick(GridPoint2 clickedPoint)
		{
			SetStartOrGoal(clickedPoint);
			UpdatePath();
		}

		private void ToggleCellWalkability(GridPoint2 clickedPoint)
		{
			walkableGrid[clickedPoint].IsWalkable = !walkableGrid[clickedPoint].IsWalkable;

			var color = walkableGrid[clickedPoint].IsWalkable
				? weightGradient.Evaluate((walkableGrid[clickedPoint].MovementCost - 1) / 2)
				: blockedColor;
			walkableGrid[clickedPoint].Color = color;
		}

		private void SetStartOrGoal(GridPoint2 clickedPoint)
		{
			if (selectStart && clickedPoint != goal)
			{
				start = clickedPoint;
				selectStart = false;
			}
			else if (clickedPoint != start)
			{
				goal = clickedPoint;
				selectStart = true;
			}
		}

		public IEnumerable<GridPoint2> GetGridPath()
		{
			var path = Grids2.Algorithms.AStar(
				walkableGrid,
				start,
				goal,
				(p, q) => PointyHexPoint.ManhattanNorm(p - q),
				c => walkableGrid[c].IsWalkable,
				PointyHexPoint.GetOrthogonalNeighbors,
				(p, q) => 1);

			return path;
		}

		public IEnumerable<GridPoint2> GetEuclideanPath()
		{
			var path = Grids2.Algorithms.AStar(
				walkableGrid,
				start,
				goal,
				EuclideanDistance,
				c => walkableGrid[c].IsWalkable,
				PointyHexPoint.GetOrthogonalNeighbors,
				EuclideanDistance);

			return path;
		}

		public IEnumerable<GridPoint2> GetWeightedPath()
		{
			var path = Grids2.Algorithms.AStar(
				walkableGrid,
				start,
				goal,
				(p, q) => PointyHexPoint.ManhattanNorm(p - q) * WalkableCell.MinCost,
				c => walkableGrid[c].IsWalkable,
				PointyHexPoint.GetOrthogonalNeighbors,
				GetMovementCost);

			return path;
		}

		/// <summary>
		/// Gets the cost of moving between the cells at the given grid points,
		/// asuming cells are neighbors.
		/// </summary>
		private float GetMovementCost(GridPoint2 p1, GridPoint2 p2)
		{
			return (walkableGrid[p1].MovementCost +
					walkableGrid[p2].MovementCost) / 2.0f;
		}

		private void UpdatePath()
		{
			if (Application.isPlaying)
			{
				pathRoot.transform.DestroyChildren();
			}
			else
			{
				pathRoot.transform.DestroyChildrenImmediate();
			}

			IEnumerable<GridPoint2> path = null;

			switch (pathMode)
			{
				case PathMode.GridPath:
					path = GetGridPath();
					break;
				case PathMode.EuclideanPath:
					path = GetEuclideanPath();
					break;
				case PathMode.WeightedPath:
					path = GetWeightedPath();
					break;
			}

			if (path == null)
			{
				//then there is no path between the start and goal.
				return;
			}

			foreach (var point in path)
			{
				var pathNode = Instantiate(pathPrefab);

				pathNode.transform.parent = pathRoot.transform;
				pathNode.transform.localScale = Vector3.one * 0.5f;
				pathNode.transform.localPosition = GridMap.GridToWorld(point);

				if (point == start)
				{
					pathNode.Color = startColor;
				}
				else if (point == goal)
				{
					pathNode.Color = goalColor;
				}
				else
				{
					pathNode.Color = pathColor;
				}
			}
		}
	}
}