//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//



using System;
using System.Collections.Generic;
using Gamelogic.Extensions.Algorithms;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// A grid for pointy hexagons, that is, hexagons with two vertical edges.
	/// </summary>
	[Version(1)]
	[Serializable]
	public partial class PointyHexGrid<TCell> :
		AbstractUniformGrid<TCell, PointyHexPoint>,
		IEvenGrid<TCell, PointyHexPoint, PointyHexPoint>
	{
		#region Storage
		protected override PointyHexPoint PointFromArrayPoint(int aX, int aY)
		{
			return GridPointFromArrayPoint(new ArrayPoint(aX, aY));
		}

		protected override ArrayPoint ArrayPointFromPoint(int hX, int hY)
		{
			return ArrayPointFromPoint(new PointyHexPoint(hY, hX));
		}

		protected override ArrayPoint ArrayPointFromPoint(PointyHexPoint hexPoint)
		{
			return ArrayPointFromGridPoint(hexPoint);
		}

		public static PointyHexPoint GridPointFromArrayPoint(ArrayPoint point)
		{
			return new PointyHexPoint(point.X, point.Y);
		}

		public static ArrayPoint ArrayPointFromGridPoint(PointyHexPoint point)
		{
			return new ArrayPoint(point.X, point.Y);
		}

		public IEnumerable<PointyHexPoint> GetPrincipleNeighborDirections()
		{
			return NeighborDirections.TakeHalf();
		}
		#endregion

		#region Wrapped Grids

		/// <summary>
		/// Returns a grid wrapped horizontally along a parallelogram.
		/// </summary>
		[Version(1,7)]
		public static WrappedGrid<TCell, PointyHexPoint> HorizontallyWrappedRectangle(int width, int height)
		{
			var grid = Rectangle(width, height);
			var wrapper = new PointyHexHorizontalRectangleWrapper(width);
			var wrappedGrid = new WrappedGrid<TCell, PointyHexPoint>(grid, wrapper);

			return wrappedGrid;
		}
		#endregion
	}
}