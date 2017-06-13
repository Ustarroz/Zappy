using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class BasicCubeGridMain : GLMonoBehaviour
	{
		[Tooltip("The prefab to use for cells.")]
		public MeshTileCell cellPrefab;

		[Tooltip("All our cells will use this as root to keep the scene neat.")]
		public GameObject gridRoot;

		[Tooltip("The dimensions for the grid to use.")]
		public InspectableGridPoint2 gridDimensions;

		[Tooltip("Used to color the cells.")]
		public ColorFunction colorFunction;

		[Tooltip("The colors to use to color the cells.")]
		public ColorList colors;

		public static Rect ScreenRect
		{
			get
			{
				return new Rect(-Screen.width / 2f, -Screen.height / 2f, Screen.width, Screen.height);
			}
		}

		public void Start()
		{
			BuildGrid();
		}

		private void BuildGrid()
		{
			var grid = CreateGrid();
			var map = CreateMap();

			foreach (var point in grid.Points)
			{
				var cell = Instantiate(cellPrefab, gridRoot);

				cell.transform.localPosition = map.Reverse(point);
				InitCell(point, cell);
				grid[point] = cell;
			}
		}

		private Grid2<MeshTileCell> CreateGrid()
		{
			var dimensions = gridDimensions.GetGridPoint();
			var shape = ImplicitShape.Parallelogram(dimensions);
			var storage = shape.ToExplicit(new GridRect(GridPoint2.Zero, dimensions));
			var grid = new Grid2<MeshTileCell>(storage);

			return grid;
		}

		private IMap<Vector3, GridPoint2> CreateMap()
		{
			var cellDimensions = cellPrefab.SharedMesh.bounds.size;
			var spaceMap = Map.Linear(Matrixf33.Scale(cellDimensions));
			var roundMap = Map.RectRound();

			return spaceMap.ComposeWith(roundMap);
		}

		private void InitCell(GridPoint2 point, MeshTileCell cell)
		{
			cell.Color = colors[point.GetColor(colorFunction)];
		}
	}
}