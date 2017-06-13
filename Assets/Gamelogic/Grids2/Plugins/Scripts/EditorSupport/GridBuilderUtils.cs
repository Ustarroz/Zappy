using System;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Class that provides utility methods for working with grids.
	/// </summary>
	[Version(2)]
	public static class GridBuilderUtils
	{
		public static Bounds ScreenBounds
		{
			get
			{
				var center = new Vector3(
					-Screen.width / 2f,
					-Screen.height / 2f,
					-(Screen.width * Screen.height) / 2f);

				var size = new Vector3(
					Screen.width,
					Screen.height,
					Screen.width * Screen.height
					);

				return new Bounds(center, size);
			}
		}

		/// <summary>
		/// A convenience method to initialize a grid of SpriteCells from code.
		/// </summary>
		/// <param name="grid">A grid in which newly instantiate cells will be placed.</param>
		/// <param name="map">A map that will convert between grid points and world points.</param>
		/// <param name="cellPrefab">The prefab that will be used to instantiate cells from.</param>
		/// <param name="gridRoot">The object to use as root for the instantiated cells.</param>
		/// <param name="initCellAction">A function that can be used to initialize the cell at the given point.</param>
		/// <typeparam name="TPoint">The point type of the grid to initialize.</typeparam>
		/// <typeparam name="TCell">The cell type of the grid.</typeparam>
		public static void InitTileGrid<TPoint, TCell>(
			IGrid<TPoint, TCell> grid,
			GridMap<TPoint> map,
			TCell cellPrefab,
			GameObject gridRoot,
			Action<TPoint, TCell> initCellAction)

			where TCell : Component
		{
			grid.Fill(p => InitCell(map, cellPrefab, gridRoot, initCellAction, p));
		}

		public static void UpdateTileGridOrientations<TPoint, TCell>(
			IGrid<TPoint, TCell> grid,
			GridMap<TPoint> map)

			where TCell : MonoBehaviour
		{
			grid.Apply(p => UpdateOrientation(grid, map, p));
		}

		private static TCell InitCell<TPoint, TCell>(
			GridMap<TPoint> map,
			TCell cellPrefab,
			GameObject gridRoot,
			Action<TPoint, TCell> initCellAction,
			TPoint point)

			where TCell : Component
		{
			var cell = GLMonoBehaviour.Instantiate(cellPrefab, gridRoot);
			cell.name = cellPrefab.name + " " + point;
			cell.transform.localPosition = map.GridToWorld(point);

			initCellAction(point, cell);

			return cell;
		}

		private static void UpdateOrientation<TPoint, TCell>(
			IGrid<TPoint, TCell> grid,
			GridMap<TPoint> map,
			TPoint point)

			where TCell : MonoBehaviour
		{
			var cell = grid[point];

			var newForward = map.GridToWorld(map.DeRound(point) + new Vector3(0, 0, 1)) - cell.transform.localPosition;
			cell.transform.forward = newForward;

			var newRight = map.GridToWorld(map.DeRound(point) + new Vector3(1, 0, 0)) - cell.transform.localPosition;
			cell.transform.right = newRight;

			var newUp = map.GridToWorld(map.DeRound(point) + new Vector3(0, 1, 0)) - cell.transform.localPosition;
			cell.transform.right = newUp;
		}

		public static void InitMesh(
			Mesh mesh,
			IExplicitShape<int> grid,
			GridMap<int> map,
			MeshData meshData,
			bool doubleSided,
			bool flipTriangles)
		{
			mesh.vertices = meshData.GetVertices(grid, map).ToArray();
			mesh.triangles = meshData.GetTriangles(grid, doubleSided, flipTriangles).ToArray();
			mesh.uv = meshData.GetUVs(grid).ToArray();
			//mesh.normals = meshData1.GetNormals(grid, map, doubleSided, flipTriangles).ToArray();
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			//mesh.normals
		}

		public static void InitMesh(
			Mesh mesh,
			IExplicitShape<GridPoint2> grid,
			GridMap<GridPoint2> map,
			MeshData meshData,
			bool doubleSided,
			bool flipTriangles)
		{
			mesh.vertices = meshData.GetVertices(grid, map).ToArray();
			mesh.triangles = meshData.GetTriangles(grid, doubleSided, flipTriangles).ToArray();
			mesh.uv = meshData.GetUVs(grid).ToArray();
			//mesh.normals = meshData1.GetNormals(grid, map, doubleSided, flipTriangles).ToArray();
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			//mesh.normals
		}

		public static void InitMesh(
			Mesh mesh,
			IExplicitShape<GridPoint3> grid,
			GridMap<GridPoint3> map,
			MeshData meshData,
			bool doubleSided,
			bool flipTriangles)
		{
			mesh.vertices = meshData.GetVertices(grid, map).ToArray();
			mesh.triangles = meshData.GetTriangles(grid, doubleSided, flipTriangles).ToArray();
			mesh.uv = meshData.GetUVs(grid).ToArray();
			//mesh.normals = meshData1.GetNormals(grid, map, doubleSided, flipTriangles).ToArray();
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			//mesh.normals
		}
	}
}