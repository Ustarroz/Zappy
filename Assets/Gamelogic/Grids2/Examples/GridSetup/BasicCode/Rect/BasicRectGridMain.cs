using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class BasicRectGridMain : GLMonoBehaviour
	//You do not need to extend from GLMonoBehaviour, you can also extend from
	//MonoBehaviour.
	{
		[Tooltip("The prefab to use for cells.")]
		public SpriteCell cellPrefab;

		[Tooltip("All our cells will use this as root to keep the scene neat.")]
		public GameObject gridRoot;

		[Tooltip("The dimensions for the grid to use.")]
		public InspectableGridPoint2 gridDimensions;

		[Tooltip("How the grid should be horizontal aligned on screen")]
		public HorizontalAlignment horizontalAlignment;

		[Tooltip("How the grid should be vertical aligned on screen")]
		public VerticalAlignment verticalAlignment;

		[Tooltip("How the grid should be horizontal aligned depending on the pivot of the cell used")]
		public HorizontalAlignment horizontalAnchorPivot;

		[Tooltip("How the grid should be vertical aligned depending on the pivot of the cell used")]
		public VerticalAlignment verticalAnchorPivot;

		[Tooltip("Used to color the cells.")]
		public ColorFunction colorFunction;

		[Tooltip("The colors to use to color the cells.")]
		public ColorList colors;

		public static Rect ScreenRect
		{
			get
			{
				var height = Camera.main.orthographicSize * 2;
				var width = Screen.width * height / Screen.height;

				return new Rect(-width / 2.0f, -height / 2.0f, width, height);
			}
		}

		public void Start()
		{
			BuildGrid();
		}

		private void BuildGrid()
		{
			var grid = CreateGrid();
			var map = CreateMap(grid);

			//This method is provided as a utility method
			//because grid setup is so often the same.

			//However, if you look at its implementation,
			//You will see it does not do anything magical,
			//and it is easy to use and modify the code
			//directly if you need more control.
			GridBuilderUtils.InitTileGrid(grid, map, cellPrefab, gridRoot, InitCell);
		}

		private Grid2<SpriteCell> CreateGrid()
		{
			var dimensions = gridDimensions.GetGridPoint();
			var shape = ImplicitShape.Parallelogram(dimensions);
			var storage = shape.ToExplicit(new GridRect(GridPoint2.Zero, dimensions));
			var grid = new Grid2<SpriteCell>(storage);

			return grid;
		}

		private GridMap<GridPoint2> CreateMap(IExplicitShape<GridPoint2> grid)
		{
			var cellDimensions = cellPrefab.Sprite.rect.size;

			//static invocation is used here because the Unity compiler
			//gets confused otherwise.
			var scale = cellDimensions.To3DXY(1);

			var spaceMap = Map.Linear(Matrixf33.Scale(scale));
			var roundMap = Map.RectRound();

			var spaceAlignMap = spaceMap.AlignGridInRect(grid, p => scale,
				new Bounds(ScreenRect.center, ScreenRect.size),
				horizontalAlignment, verticalAlignment);
			var spaceAlignAnchorMap = spaceAlignMap.AnchorPivotInRect(grid, p => scale, horizontalAnchorPivot,
				verticalAnchorPivot);

			var gridMap = new GridMap<GridPoint2>(spaceAlignAnchorMap, roundMap);

			return gridMap;
		}

		private void InitCell(GridPoint2 point, SpriteCell cell)
		{
			cell.Color = colors[point.GetColor(colorFunction)];
		}
	}
}