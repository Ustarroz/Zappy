//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System.Collections;
using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class StressTestHex : GLMonoBehaviour
	{
		public Camera uiCamera;
		public TileCell cellPrefab;
		public GameObject gridRoot;
		public int cellsPerIteration = 1000;
		public int width = 500;
		public int height = 500;

		private IGrid<GridPoint2, TileCell> grid;
		private GridMap<GridPoint2> map;
		private int totalCellCount;

		public void Start()
		{
			StartCoroutine(BuildGrid());
		}

		public void OnGUI()
		{
			GUILayout.TextField("Hexes: " + totalCellCount);
		}

		public void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Vector3 worldPosition = uiCamera.ScreenToWorldPoint(Input.mousePosition);

				var hexPoint = map.WorldToGridToDiscrete(worldPosition);

				if (grid.Contains(hexPoint))
				{
					if (grid[hexPoint] != null)
					{
						grid[hexPoint].gameObject.SetActive(!grid[hexPoint].gameObject.activeInHierarchy);
					}
				}
			}

			if (Input.GetKey(KeyCode.UpArrow))
			{
				uiCamera.transform.position = uiCamera.transform.position + Vector3.up * 10f;
			}

			if (Input.GetKey(KeyCode.DownArrow))
			{
				uiCamera.transform.position = uiCamera.transform.position + Vector3.down * 10f;
			}

			if (Input.GetKey(KeyCode.LeftArrow))
			{
				uiCamera.transform.position = uiCamera.transform.position + Vector3.left * 10f;
			}

			if (Input.GetKey(KeyCode.RightArrow))
			{
				uiCamera.transform.position = uiCamera.transform.position + Vector3.right * 10f;
			}
		}

		public IEnumerator BuildGrid()
		{
			totalCellCount = 0;
			var dimensions = new GridPoint2(width, height);

			grid = ImplicitShape
				.Parallelogram(dimensions)
				.ToExplicit(new GridRect(GridPoint2.Zero, dimensions))
				.ToGrid<TileCell>();


			map = new GridMap<GridPoint2>(
				Map.Linear(PointyHexPoint.SpaceMapTransform).PreScale(Vector3.one * 200),
				Map.HexRound());

			int cellCount = 0;

			foreach (var point in grid.Points)
			{
				var cell = Instantiate(cellPrefab);

				cell.transform.parent = gridRoot.transform;

				var worldPoint = map.GridToWorld(point);

				cell.transform.localPosition = worldPoint;

				cellCount++;
				totalCellCount++;

				grid[point] = cell;

				if (cellCount >= cellsPerIteration)
				{
					cellCount = 0;
					yield return null;
				}
			}
		}
	}
}