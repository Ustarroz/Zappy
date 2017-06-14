using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class ZappyGrid : GLMonoBehaviour
	{
		[Tooltip("The prefab to use for cells.")]
		public MeshTileCell cellPrefab;

		[Tooltip("All our cells will use this as root to keep the scene neat.")]
		public GameObject gridRoot;

		[Tooltip("The dimensions for the grid to use.")]
		public InspectableGridPoint3 gridDimensions;

		[Tooltip("Used to color the cells.")]
		public ColorFunction colorFunction;

		[Tooltip("The colors to use to color the cells.")]
		public ColorList colors;

		public void Start()
		{
			BuildGrid();
		}

		private void BuildGrid()
		{
			var grid = CreateGrid();
			var map = CreateMap();

			GridBuilderUtils.InitTileGrid(grid, map, cellPrefab, gridRoot, InitCell);
		}

		private Grid3<MeshTileCell> CreateGrid()
		{
			var dimensions = gridDimensions.GetGridPoint();
			var shape = ImplicitShape.Parallelepiped(dimensions);
			var storage = shape.ToExplicit(new GridBounds(GridPoint3.Zero, dimensions));
			var grid = new Grid3<MeshTileCell>(storage);

			return grid;
		}

		private GridMap<GridPoint3> CreateMap()
		{
			var cellDimensions = cellPrefab.SharedMesh.bounds.size;

			var spaceMap = Map.Linear(Matrixf33.Scale(cellDimensions));
			var roundMap = Map.BlockRound();
			var gridMap = new GridMap<GridPoint3>(spaceMap, roundMap);

			return gridMap;
		}

		private void InitCell(GridPoint3 point, MeshTileCell cell)
		{
			cell.Color = colors[point.GetColor(colorFunction) + point.Z];
		}
	}
}