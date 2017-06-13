using System;
using System.Collections.Generic;
using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Examples
{
	public class SimpleGridExample : GLMonoBehaviour
	{
		#region Private Fields

		[SerializeField]
		[Tooltip("The size of the grid.")]
		private int gridSize = 10;

		[SerializeField]
		[Tooltip("The prefab to use for cells.")]
		private TileCell cellPrefab;

		[SerializeField]
		[Tooltip("The mesh data to use for mesh cells.")]
		private MeshData meshData;

		private PhysicalGrid<GridPoint2, TileCell> tileGrid;
		private IGrid<GridPoint2, bool> meshGrid;
		private Mesh mesh;

		#endregion

		#region Protected Fields

		[SerializeField]
		[Tooltip("The colors to use to color cells.")]
		[ContextMenuItem("Reset", "ResetColors")]
		protected ColorList colors;

		[SerializeField]
		[Tooltip("The color function to use to color cells.")]
		protected ColorFunction colorFunction =
			new ColorFunction {x0 = 1, x1 = 1, y1 = 1};

		#endregion

		#region Public Methods

		// This mehthod clear all the grids created by itself through the other methods.
		[InspectorButton]
		public void ClearAllGrids()
		{
			if (!Application.isPlaying) throw new Exception("Must be on play mode");

			transform.DestroyChildrenUniversal();

			DestroyUniversal(gameObject.GetComponent<GridEventTrigger>());

			if (this.GetRequiredComponent<MeshFilter>().sharedMesh != null)
			{
				DestroyUniversal(this.GetRequiredComponent<MeshFilter>().sharedMesh);
				mesh = null;
			}

			tileGrid = null;
			meshGrid = null;
		}

		// Adds a tile grid using the cellPrefab.
		[InspectorButton]
		public void AddTileGrid()
		{
			if (!Application.isPlaying) throw new Exception("Must be on play mode");

			ClearAllGrids();

			// Create a grid of GridSize by GridSize, using the cellPrefab.
			tileGrid = SimpleGrid.RectXY(gameObject, gridSize, gridSize, cellPrefab, new Vector2(50, 50));

			// Get (or add) a grid's event trigger controller to attach events to change the color of the cell when clicking it.
			var eventTrigger = tileGrid.AddComponent<GridEventTrigger>();

			eventTrigger.UICamera = Camera.main;
			eventTrigger.OnLeftMouseButtonDown.AddListener(point =>
				{
					tileGrid.Grid[point].GetRequiredComponent<SpriteRenderer>().color = colors.Count > 0 ? colors[0] : Color.white;
				}
			);
			eventTrigger.OnRightMouseButtonDown.AddListener(point =>
				{
					tileGrid.Grid[point].GetRequiredComponent<SpriteRenderer>().color = colors.Count > 0 ? colors[1] : Color.black;
				}
			);

			// Colorize the grid.
			foreach (var point in tileGrid.Grid.Points)
			{
				tileGrid.Grid[point].GetComponent<SpriteRenderer>().color = colors[point.GetColor(colorFunction) % colors.Count];
			}
		}

		#endregion

		#region Private Members

		// Adds a mesh grid using the meshData.
		[InspectorButton]
		private void AddMeshGrid()
		{
			if (!Application.isPlaying) throw new Exception("Must be on play mode");

			ClearAllGrids();

			var meshFilter = gameObject.GetComponent<MeshFilter>();

			// Create a mesh grid of GridSize by GridSize, using the meshData.
			mesh = new Mesh();
			meshGrid = SimpleGrid.RectXY<bool>(mesh, gridSize, gridSize, meshData, new Vector2(50, 50));

			// Colorize the vertex feeded to the mesh.
			var vertexColors = new List<Color>();
			var vertexPerCell = 4;

			foreach (var point in meshGrid.Points)
			{
				var colorIndex = point.GetColor(colorFunction.x0, colorFunction.x1, colorFunction.y1);

				for (var i = 0; i < vertexPerCell; i++)
				{
					vertexColors.Add(colors[colorIndex]);
				}
			}

			mesh.colors = vertexColors.ToArray();

			// Set the new mesh
			meshFilter.sharedMesh = mesh;
		}

		#endregion
	}
}