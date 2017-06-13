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
	/// A grid for flat hexagons, that is, hexagons with two horizontal edges.
	/// </summary>
	[Version(1)]
	[Serializable]
	public partial class FlatHexGrid<TCell> : AbstractUniformGrid<TCell, FlatHexPoint>, IEvenGrid<TCell, FlatHexPoint, FlatHexPoint>
	{
		#region Storage
		protected override FlatHexPoint PointFromArrayPoint(int aX, int aY)
		{
			return GridPointFromArrayPoint(new ArrayPoint(aX, aY));
		}

		protected override ArrayPoint ArrayPointFromPoint(int hX, int hY)
		{
			return ArrayPointFromGridPoint(new FlatHexPoint(hX, hY));
		}

		protected override ArrayPoint ArrayPointFromPoint(FlatHexPoint hexPoint)
		{
			return ArrayPointFromGridPoint(hexPoint);
		}

		public static FlatHexPoint GridPointFromArrayPoint(ArrayPoint point)
		{
			return new FlatHexPoint(point.X, point.Y);
		}

		public static ArrayPoint ArrayPointFromGridPoint(FlatHexPoint point)
		{
			return new ArrayPoint(point.X, point.Y);
		}

		public IEnumerable<FlatHexPoint> GetPrincipleNeighborDirections()
		{
			return NeighborDirections.TakeHalf();
		}
		#endregion

		#region Wrapped Grids

		/// <summary>
		/// Returns a grid wrapped horizontally along a parallelogram.
		/// </summary>
		[Version(1,7)]
		public static WrappedGrid<TCell, FlatHexPoint> HorizontallyWrappedRectangle(int width, int height)
		{
			var grid = Rectangle(width, height);
			var wrapper = new FlatHexHorizontalWrapper(width);
			var wrappedGrid = new WrappedGrid<TCell, FlatHexPoint>(grid, wrapper);

			return wrappedGrid;
		}
		#endregion
	}
}