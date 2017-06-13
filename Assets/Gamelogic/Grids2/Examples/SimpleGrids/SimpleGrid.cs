using System;
using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class PhysicalGrid<TPoint, TCell>
	{
		#region Private Fields

		private IGrid<TPoint, TCell> grid;
		private GridMap<TPoint> gridMap;
		private GameObject gameObject;

		#endregion

		#region Public Properties

		public IGrid<TPoint, TCell> Grid
		{
			get { return grid; }
		}

		public GridMap<TPoint> GridMap
		{
			get { return gridMap; }
		}

		public GameObject GameObject
		{
			get { return gameObject; }
		}

		#endregion

		#region Constructors

		public PhysicalGrid(
			IGrid<TPoint, TCell> grid,
			GridMap<TPoint> gridMap,
			GameObject gameObject)
		{
			this.grid = grid;
			this.gridMap = gridMap;
			this.gameObject = gameObject;
		}

		#endregion

		#region Public Methods

		public T GetComponent<T>()
			where T : GridBehaviour<TPoint, TCell>
		{
			return gameObject.GetComponent<T>();
		}

		public T AddComponent<T>()
			where T : GridBehaviour<TPoint, TCell>
		{
			var behavior = gameObject.AddComponent<T>();
			behavior.__InitPrivateFields(null, grid, gridMap);

			return behavior;
		}

		#endregion
	}

	public static class SimpleGrid
	{
		#region Public Static Methods

		public static IGrid<GridPoint2, T> Rect<T>(int width, int height)
		{
			var dimensions = new GridPoint2(width, height);

			var grid = ImplicitShape
				.Parallelogram(dimensions)
				.ToExplicit(new GridRect(GridPoint2.Zero, dimensions))
				.ToGrid<T>();

			return grid;
		}

		public static IGrid<GridPoint2, T> Rect<T>(int width, int height, T initialElement)
		{
			return Rect(width, height, () => initialElement);
		}

		public static IGrid<GridPoint2, T> Rect<T>(int width, int height, Func<T> createCell)
		{
			var grid = Rect<T>(width, height);

			grid.Fill(createCell);

			return grid;
		}

		public static IGrid<GridPoint2, T> Hex<T>(int radius)
		{
			var grid = ExplicitShape.Hex.Hexagon(radius)
				.ToGrid<T>();

			return grid;
		}

		public static IGrid<GridPoint2, T> Hex<T>(int radius, T initialElement)
		{
			return Hex(radius, () => initialElement);
		}

		public static IGrid<GridPoint2, T> Hex<T>(int radius, Func<T> createCell)
		{
			var grid = Hex<T>(radius);

			grid.Fill(createCell);

			return grid;
		}

		/// <summary>
		/// Creates a Pysical Grid.
		/// </summary>
		/// <param name="root">GameObject where all the cells are going to be stored.</param>
		/// <param name="width">Width of the grid.</param>
		/// <param name="height">Height of the grid.</param>
		/// <param name="prefab">Prefab to use for the cells on the grid.</param>
		/// <param name="cellDimensions">The cell's dimensions.</param>
		public static PhysicalGrid<GridPoint2, T> RectXY<T>(
			GameObject root,
			int width,
			int height,
			T prefab,
			Vector2 cellDimensions)
			where T : Component
		{
			var spaceMap = Map.Linear(Matrixf33.Scale(cellDimensions.To3DXY(1)));
			var center = new Vector2(width, height) / 2 - Vector2.one * 0.5f;
			spaceMap = spaceMap.PreTranslate(-center);

			var grid = Rect<T>(width, height);
			var roundMap = Map.RectRound();
			var map = new GridMap<GridPoint2>(spaceMap, roundMap);

			GridBuilderUtils.InitTileGrid(grid, map, prefab, root, (point, cell) => { });

			//root.AddComponent<PhysicalGrid<GridPoint2, T>>();
			return new PhysicalGrid<GridPoint2, T>(grid, map, root);
		}

		/// <summary>
		/// Creates a Mesh grid using the mesh data provided for each cell.
		/// </summary>
		/// <param name="mesh">Mesh to use to place the generated mesh grid.</param>
		/// <param name="width">Width of the grid.</param>
		/// <param name="height">Height of the grid.</param>
		/// <param name="prefab">Prefab to use for the cells on the grid.</param>
		/// <param name="meshData">The mesh data information to use for the cells.</param>
		/// <param name="cellDimensions">The cell's dimensions.</param>
		public static IGrid<GridPoint2, T> RectXY<T>(
			Mesh mesh,
			int width,
			int height,
			MeshData meshData,
			Vector2 cellDimensions)
		{
			var spaceMap = Map.Linear(Matrixf33.Scale(cellDimensions.To3DXY(1)));
			var center = new Vector2(width, height) / 2 - Vector2.one * 0.5f;
			spaceMap = spaceMap.PreTranslate(-center);

			var roundMap = Map.RectRound();
			var map = new GridMap<GridPoint2>(spaceMap, roundMap);
			var grid = Rect<T>(width, height);

			GridBuilderUtils.InitMesh(mesh, grid, map, meshData, false, true);

			return grid;
		}

		#endregion
	}
}