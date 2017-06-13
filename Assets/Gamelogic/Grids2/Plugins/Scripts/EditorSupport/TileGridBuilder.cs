using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;
using Gamelogic.Grids2.Graph;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// A builder that uses separate objects for each cell of a grid.
	/// </summary>
	/// <seealso cref="GridBuilder{TileCell}" />
	[AddComponentMenu("Gamelogic/Grids 2/Tile Grid Builder")]
	public class TileGridBuilder : GridBuilder<TileCell>
	{
		[SerializeField]
		private GameObject prefab;

		public GameObject Prefab
		{
			get { return prefab; }
		}

		[EditorInternal]
		public GameObject __Prefab
		{
			set { prefab = value; }
		}

		public override void MakeCells<TPoint>(
			IGrid<TPoint, TileCell> grid,
			GridMap<TPoint> map)
		{
			if (Application.isPlaying)
			{
				gameObject.transform.DestroyChildren();
			}
			else
			{
				gameObject.transform.DestroyChildrenImmediate();
			}

			cellStorage = new TileCell[grid.Points.Count()];

			var pointIndex = 0;

			foreach (var point in grid.Points)
			{
				var cell = Instantiate(prefab, gameObject);
				cell.transform.localPosition = map.GridToWorld(point);

				var spriteCellComponent = cell.GetComponent<TileCell>();
				grid[point] = spriteCellComponent;
				cellStorage[pointIndex] = spriteCellComponent;

				pointIndex++;
			}
		}

		public static TileGridBuilder Create(SpaceMapGraph map, Shape2Graph shape, RoundType roundType)
		{
			var go = new GameObject();

			var builder = go.AddComponent<TileGridBuilder>();
			builder.spaceMapGraph = map;
			builder.gridShapeGraph = new GridShapeGraph
			{
				gridType = GridType.Grid2,
				shape2Graph = shape
			};

			builder.roundType = roundType;

			return builder;
		}

		[InspectorButton]
		private void BuildGrid()
		{
			if (prefab == null)
			{
				Debug.LogError("No prefab is attached to this grid builder.", this);
				return;
			}

			__CellStorage = null;
			Build();
		}
	}
}