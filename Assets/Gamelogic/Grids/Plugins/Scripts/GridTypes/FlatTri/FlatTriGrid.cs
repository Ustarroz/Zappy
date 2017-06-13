//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//



using System;
using System.Linq;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// A grid of triangles in the flat orientation. All triangles have one horizontal edge.
	/// </summary>
	[Version(1)]
	[Serializable]
	public partial class FlatTriGrid<TCell> : AbstractSplicedGrid<TCell, FlatTriPoint, PointyHexPoint>
	{
		#region Implementation
		protected override FlatTriPoint MakePoint(PointyHexPoint basePoint, int index)
		{
			return new FlatTriPoint(basePoint.X, basePoint.Y, index);
		}

		protected override IGrid<TCell[], PointyHexPoint> MakeUnderlyingGrid(int width, int height)
		{
			return new PointyHexGrid<TCell[]>(width, height);
		}
		#endregion

		#region StorageInfo
		public static PointyHexPoint GridPointFromArrayPoint(ArrayPoint point)
		{
			return PointyHexGrid<TCell>.GridPointFromArrayPoint(point);
		}

		public static ArrayPoint ArrayPointFromGridPoint(PointyHexPoint point)
		{
			return PointyHexGrid<TCell>.ArrayPointFromGridPoint(point);
		}
		#endregion

		#region Vertices and Edges
		public IGrid<TNewCell, PointyHexPoint> MakeVertexGrid<TNewCell>()
		{
			var vertices = this.SelectMany(x => x.GetVertices());
			var storage = PointyHexGrid<TNewCell>.CalculateStorage(vertices);
			var offset = PointyHexGrid<TNewCell>.GridPointFromArrayPoint(storage.offset);

			return new PointyHexGrid<TNewCell>(storage.dimensions.X, storage.dimensions.Y, x => IsInsideVertexGrid(x + offset), offset);
		}

		/// <summary>
		/// Makes an edge grid for this grid.
		/// </summary>
		public IGrid<TNewCell, PointyRhombPoint> MakeEdgeGrid<TNewCell>()
		{
			var edgeOffset = GridOrigin;

			var offset = new PointyHexPoint(1, 1);
			return new PointyRhombGrid<TNewCell>(width + 2, height + 2, x => IsInsideEdgeGrid(x - offset), edgeOffset.GetEdgeAnchor().BasePoint - offset);

		}
		#endregion
	}
}