using System;
using System.Linq;
using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2
{

	/// <summary>
	/// A grid builder that uses a single mesh for the entire grid.
	/// </summary>
	/// <seealso cref="GridBuilder{MeshCell}" />
	[AddComponentMenu("Gamelogic/Grids 2/Mesh Grid Builder")]
	public class MeshGridBuilder : GridBuilder<MeshCell>
	{
		#region Inspector Fields

		//TODO: implement doubleSided
		//[SerializeField]
		private bool doubleSided = false;

		[SerializeField]
		private bool flipTriangles = false;

		[SerializeField]
		private MeshData meshData;

		#endregion

		#region Private Fields
		private MeshFilter meshFilter;
		#endregion

		#region Properties
		public bool DoubleSided
		{
			get { return doubleSided; }
		}

		public bool FlipTriangles
		{
			get { return flipTriangles; }
		}

		public MeshData MeshData
		{
			get { return meshData; }
		}
		#endregion

		#region Public Methods

		public override void MakeCells<TPoint>(
			IGrid<TPoint, MeshCell> grid,
			GridMap<TPoint> map)
		{
			meshFilter = this.GetRequiredComponent<MeshFilter>();
			var mesh = new Mesh();

			switch (ShapeGraph.gridType)
			{
				case GridType.Grid1:
					GridBuilderUtils.InitMesh(
						mesh,
						(IGrid<int, MeshCell>)grid,
						(GridMap<int>)(object)map,
						meshData,
						doubleSided,
						flipTriangles);
					break;

				case GridType.Grid2:
					GridBuilderUtils.InitMesh(
						mesh,
						(IGrid<GridPoint2, MeshCell>)grid,
						(GridMap<GridPoint2>)(object)map,
						meshData,
						doubleSided,
						flipTriangles);
					break;

				case GridType.Grid3:
					GridBuilderUtils.InitMesh(
						mesh,
						(IGrid<GridPoint3, MeshCell>)grid,
						(GridMap<GridPoint3>)(object)map,
						meshData,
						doubleSided,
						flipTriangles);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			meshFilter.sharedMesh = mesh;
			mesh.UploadMeshData(false); //TODO: Check whether this should be true or false

			cellStorage = new MeshCell[grid.Cells.Count()];

			var meshCellIndex = 0;
			foreach (var meshCell in grid.Cells)
			{
				cellStorage[meshCellIndex] = meshCell;

				meshCellIndex++;
			}
		}
		#endregion

		#region Private methods
		[InspectorButton]
		private void BuildGrid()
		{
			__CellStorage = null;
			Build();
		}

		#endregion
	}
}