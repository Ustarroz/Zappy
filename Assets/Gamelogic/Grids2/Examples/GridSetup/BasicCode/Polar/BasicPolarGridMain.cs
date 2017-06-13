using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class BasicPolarGridMain : GLMonoBehaviour
	//You do not need to extend from GLMonoBehaviour, you can also extend from
	//MonoBehaviour.
	{
		[Tooltip("The prefab to use for cells.")]
		public SpriteCell cellPrefab;

		[Tooltip("All our cells will use this as root to keep the scene neat.")]
		public GameObject gridRoot;

		[Tooltip("The dimensions for the grid to use.")]
		public InspectableGridPoint2 gridDimensions;

		[Tooltip("Allows to scale the since of the grid.")]
		public float scaleFactor = 1.0f;

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

		public void OnValidate()
		{
			scaleFactor = Mathf.Max(1.0f, scaleFactor);
		}

		public void Start()
		{
			BuildGrid();
		}

		private void BuildGrid()
		{
			var grid = CreateGrid();
			var map = CreateMap(grid);

			GridBuilderUtils.InitTileGrid(grid, map, cellPrefab, gridRoot, InitCell);
			GridBuilderUtils.UpdateTileGridOrientations(grid, map);
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
			var scale = new Vector3(1, .85f, 1); //VectorExtensions.To3DXY(cellDimensions, 1);

			var spaceMap =
				Map
					.Translate(new Vector3(0, 3.2f, 0))
					.ComposeWith(Map.Linear(Matrixf33.Scale(scale)))
					.ComposeWith(Map.Polar(cellDimensions.y, 1.0f / gridDimensions.x));

			var roundMap = Map.RectRound();
			var parallelogramMap = Map.ParallelogramWrapX(gridDimensions.x);
			var composeMap = parallelogramMap.ComposeWith(roundMap);

			var spaceAlignMap = spaceMap.AlignGridInRect(grid, p => scale,
				new Bounds(ScreenRect.center, ScreenRect.size),
				horizontalAlignment, verticalAlignment);
			var spaceAlignAnchorMap = spaceAlignMap.AnchorPivotInRect(grid, p => scale, horizontalAnchorPivot,
				verticalAnchorPivot);

			var gridMap = new GridMap<GridPoint2>(spaceAlignAnchorMap, composeMap);

			return gridMap;
		}

		private void InitCell(GridPoint2 point, SpriteCell cell)
		{
			cell.Color = colors[point.GetColor(colorFunction)];
		}
	}
}