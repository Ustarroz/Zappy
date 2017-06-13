using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class BasicLineGridMain : GLMonoBehaviour
	//You do not need to extend from GLMonoBehaviour, you can also extend from
	//MonoBehaviour.
	{
		[Tooltip("The prefab to use for cells.")]
		public SpriteCell cellPrefab;

		[Tooltip("All our cells will use this as root to keep the scene neat.")]
		public GameObject gridRoot;

		[Tooltip("The dimensions for the grid to use.")]
		public int gridSize;

		[Tooltip("Used to color the cells.")]
		public int colorFunction;

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

			//This method is provided as a utility method
			//because grid setup is so often the same.

			//However, if you look at its implementation,
			//You will see it does not do anything magical,
			//and it is easy to use and modify the code
			//directly if you need more control.
			GridBuilderUtils.InitTileGrid(grid, map, cellPrefab, gridRoot, InitCell);
			GridBuilderUtils.UpdateTileGridOrientations(grid, map);
		}

		private Grid1<SpriteCell> CreateGrid()
		{
			var shape = ImplicitShape.Segment(0, gridSize);
			var storage = shape.ToExplicit(new GridInterval(0, gridSize));
			var grid = new Grid1<SpriteCell>(storage);

			return grid;
		}

		private GridMap<int> CreateMap()
		{
			var cellDimensions = cellPrefab.Sprite.rect.size;
			var scale = VectorExtensions.To3DXY(cellDimensions, 1);

			var spaceMap =
				Map
					.Translate(new Vector3(0, 1, 0))
					.ComposeWith(Map.Polar(3, 1.0f / gridSize))
					.ComposeWith(Map.Linear(Matrixf33.Scale(scale)));

			var roundMap = Map.PointRound();
			var gridMap = new GridMap<int>(spaceMap, roundMap);

			return gridMap;
		}

		private void InitCell(int point, SpriteCell cell)
		{
			//TODO Make a 1D color function method somewhere
			//Rename ColorFunction to ColorFunction2
			cell.Color = colors[point % colorFunction];
		}
	}
}