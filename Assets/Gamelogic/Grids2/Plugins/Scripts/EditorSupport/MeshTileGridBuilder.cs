using System.Linq;
using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// A builder that uses separate objects for each cell of a grid.
	/// </summary>
	/// <seealso cref="GridBuilder{MeshTileCell}" />
	[AddComponentMenu("Gamelogic/Grids 2/Mesh Tile Grid Builder")]
	public class MeshTileGridBuilder : GridBuilder<MeshTileCell>
	{
		#region Inspector Fields

		[SerializeField]
		private GameObject prefab = null;

		#endregion

		#region Public Properties

		public GameObject Prefab
		{
			get { return prefab; }
		}

		#endregion

		#region Public Methods

		public override void MakeCells<TPoint>(
			IGrid<TPoint, MeshTileCell> grid,
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

			cellStorage = new MeshTileCell[grid.Points.Count()];

			var pointIndex = 0;
			foreach (var point in grid.Points)
			{
				var cell = Instantiate(prefab, gameObject);
				cell.transform.localPosition = map.GridToWorld(point);

				var meshTileCellComponent = cell.GetComponent<MeshTileCell>();
				grid[point] = meshTileCellComponent;
				cellStorage[pointIndex] = meshTileCellComponent;

				pointIndex++;
			}
		}

		#endregion
	}
}