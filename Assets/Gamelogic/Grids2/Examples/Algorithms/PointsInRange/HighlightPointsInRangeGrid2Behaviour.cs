using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Grids2.Examples.Algorithms.PathFinding;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gamelogic.Grids2.Examples.Algorithms.PointsInRange
{
	public class HighlightPointsInRangeGrid2Behaviour : GridBehaviour<GridPoint2, TileCell>
	{
		public PathMode pathMode;
		public float range;
		public Gradient cellMovementCostColor;
		public Gradient cellDistanceColor;
		public Color obstacleColor;

		public SpriteCell pathPrefab;
		public GameObject pathRoot;

		private Grid2<WalkableCell> walkableGrid;
		private GridPoint2 start;
		private Grid2<SpriteCell> pathGrid;
		private Dictionary<GridPoint2, float> pointsInRange;

		//We use this map instead of the Map provided by the GridBehviour
		//for Euclidean distance calculations so that the scale is the same
		//as for the other distance metrics. Normally, you can use the same map
		//as the one you use for the grid.
		//
		//We could alternatively have scaled the value returned by the distance function.
		//This is lsightly neater, and more robust against changes of the grid and map.
		private IMap<Vector2, GridPoint2> distanceMap;

		public override void InitGrid()
		{
			//Using cell dimensions of Vector2.one means two orthogonal neighbors are
			//1 unit apart, similar to the other metrics.
			distanceMap = Map.Func<Vector2, GridPoint2>(p => RectPoint.RoundToGridPoint(p), p => p.ToVector2());

			//We create a copy of the grid which has a more convenient type signature.
			//We cast all values to Walkable cell, and the entire grid to a RectGrid.
			walkableGrid = (Grid2<WalkableCell>)Grid.CloneStructure<WalkableCell>();

			foreach (var gridPoint2 in walkableGrid.Points)
			{
				walkableGrid[gridPoint2] = Grid[gridPoint2].GetComponent<WalkableCell>();
				walkableGrid[gridPoint2].IsWalkable = true;

				walkableGrid[gridPoint2].MovementCost = 1.0f;
			}

			walkableGrid.Apply(SetupWalkableCell);

			if (Application.isPlaying)
			{
				pathRoot.transform.DestroyChildren();
			}
			else
			{
				pathRoot.transform.DestroyChildrenImmediate();
			}

			start = GridPoint2.Zero;

			//We create a duplicate grid to keep the path nodes
			pathGrid = (Grid2<SpriteCell>)Grid.CloneStructure<GridPoint2, SpriteCell>(InstantiatePathCell);

			UpdateRange();
		}

		public void Update()
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				SetNewStartPoint(start + RectPoint.North);
			}

			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				SetNewStartPoint(start + RectPoint.South);
			}

			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				SetNewStartPoint(start + RectPoint.East);
			}

			if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				SetNewStartPoint(start + RectPoint.West);
			}
		}

		public void OnLeftClick(GridPoint2 clickedPoint)
		{
			ToggleCellWalkability(clickedPoint);
			UpdateRange();
		}

		public void OnRightClick(GridPoint2 clickedPoint)
		{
			SetNewStartPoint(clickedPoint);

			UpdateRange();
		}

		private void SetupWalkableCell(WalkableCell cell)
		{
			cell.IsWalkable = true;
			var movementCost = 1 + Random.value;
			cell.MovementCost = movementCost;
			cell.Color = cellMovementCostColor.Evaluate(movementCost - 1);
		}

		private SpriteCell InstantiatePathCell(GridPoint2 point)
		{
			var cell = Instantiate(pathPrefab);

			cell.transform.parent = pathRoot.transform;
			cell.transform.localPosition = GridMap.GridToWorld(point);
			cell.transform.localScale = Vector3.one * 0.5f;
			cell.name = "SpriteCell";
			cell.Color = cellDistanceColor.Evaluate(1);
			cell.gameObject.SetActive(false);

			return cell;
		}

		public Dictionary<GridPoint2, float> GetGridPath()
		{
			var cost = Grids2.Algorithms.GetPointsInRangeCost(
				walkableGrid,
				start,
				RectPoint.GetOrthogonalAndDiagonalNeighbors,
				p => walkableGrid[p].IsWalkable,
				(p, q) => 1,
				range);

			return cost;
		}

		public Dictionary<GridPoint2, float> GetEuclideanPath()
		{
			var cost = Grids2.Algorithms.GetPointsInRangeCost(
				walkableGrid,
				start,
				RectPoint.GetOrthogonalAndDiagonalNeighbors,
				p => walkableGrid[p].IsWalkable,
				EuclideanDistance,
				range);

			return cost;
		}

		public Dictionary<GridPoint2, float> GetWeightedPath()
		{
			var cost = Grids2.Algorithms.GetPointsInRangeCost(
				walkableGrid,
				start,
				RectPoint.GetOrthogonalAndDiagonalNeighbors,
				p => walkableGrid[p].IsWalkable,
				GetMovementCost,
				range);

			return cost;
		}

		private void UpdateRange()
		{
			if (pointsInRange != null)
			{
				foreach (var point in pointsInRange.Keys)
				{
					pathGrid[point].gameObject.SetActive(false);
				}
			}

			switch (pathMode)
			{
				case PathMode.GridPath:
					pointsInRange = GetGridPath();
					break;
				case PathMode.EuclideanPath:
					pointsInRange = GetEuclideanPath();
					break;
				case PathMode.WeightedPath:
				default:
					pointsInRange = GetWeightedPath();
					break;
			}

			if (pointsInRange == null)
			{
				return; //then there is no path between the start and goal.
			}

			var maxCost = pointsInRange.Values.Max();

			foreach (var point in pointsInRange.Keys)
			{
				var cell = pathGrid[point];
				cell.gameObject.SetActive(true);
				cell.Color = cellDistanceColor.Evaluate(pointsInRange[point] / maxCost);
			}
		}

		private void SetNewStartPoint(GridPoint2 point)
		{
			if (!walkableGrid.Contains(point) || !walkableGrid[point].IsWalkable)
				return;

			start = point;

			UpdateRange();
		}

		private void ToggleCellWalkability(GridPoint2 selectedPoint)
		{
			var cell = walkableGrid[selectedPoint];

			cell.IsWalkable = !cell.IsWalkable;
			cell.Color = cell.IsWalkable ? cellMovementCostColor.Evaluate((cell.MovementCost - 1) / 2) : obstacleColor;
		}

		//Returns the Euclidean distance (in world units)
		//between the given grid points
		private float EuclideanDistance(GridPoint2 p, GridPoint2 q)
		{
			float dX = distanceMap.Forward(p.ToVector2()).X - distanceMap.Forward(q.ToVector2()).X;
			float dY = distanceMap.Forward(p.ToVector2()).Y - distanceMap.Forward(q.ToVector2()).Y;

			//float dX = distanceMap[p].x - distanceMap[q].x;
			//float dY = distanceMap[p].y - distanceMap[q].y;

			float distance = Mathf.Sqrt(dX * dX + dY * dY);

			return distance;
		}

		/**
		Gets the cost of moving between the cells at the given grid points, 
		asuming cells are neighbors.
	*/

		private float GetMovementCost(GridPoint2 p1, GridPoint2 p2)
		{
			return (walkableGrid[p1].MovementCost +
					walkableGrid[p2].MovementCost) / 2;
		}

	}
}