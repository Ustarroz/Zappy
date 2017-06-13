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
	/// Represents a rectangular grid.
	/// </summary>
	[Version(1)]
	[Serializable]
	public partial class RectGrid<TCell> :
		AbstractUniformGrid<TCell, RectPoint>, IEvenGrid<TCell, RectPoint, RectPoint>
	{
		private static readonly RectPoint[] SpiralIteratorDirections =
		{
			RectPoint.East,
			RectPoint.South,
			RectPoint.West,
			RectPoint.North,
		};
		
		#region Properties
		public int Width
		{
			get
			{
				return width;
			}
		}

		public int Height
		{
			get
			{
				return height;
			}
		}
		#endregion		

		#region Neighbors
		public void SetNeighborsMain()
		{
			NeighborDirections = RectPoint.MainDirections;
		}

		public void SetNeighborsDiagonals()
		{
			NeighborDirections = RectPoint.DiagonalDirections;
		}

		public void SetNeighborsMainAndDiagonals()
		{
			NeighborDirections = RectPoint.MainAndDiagonalDirections;
		}
		#endregion

		#region Storage
		public static ArrayPoint ArrayPointFromGridPoint(RectPoint point)
		{
			return new ArrayPoint(point.X, point.Y);
		}

		public static RectPoint GridPointFromArrayPoint(ArrayPoint point)
		{
			return new RectPoint(point.X, point.Y);
		}

		//TODO do we still need these?
		protected override ArrayPoint ArrayPointFromPoint(RectPoint point)
		{
			return ArrayPointFromGridPoint(point);
		}

		protected override ArrayPoint ArrayPointFromPoint(int x, int y)
		{
			return new ArrayPoint(x, y);
		}

		protected override RectPoint PointFromArrayPoint(int x, int y)
		{
			return new RectPoint(x, y);
		}
		#endregion

		#region Iterators

		[Version(1, 10)]
		public IEnumerable<RectPoint> GetSpiralIterator(int ringCount)
		{
			return GetSpiralIterator(RectPoint.Zero, ringCount);
		}

		[Version(1, 8)]
		public IEnumerable<RectPoint> GetSpiralIterator(RectPoint origin, int ringCount)
		{
			var point = origin;
			yield return point;

			for (var k = 1; k < ringCount; k++)
			{
				point += RectPoint.NorthWest;

				for (var i = 0; i < 4; i++)
				{
					for (var j = 0; j < 2*k; j++)
					{
						point += SpiralIteratorDirections[i];

						if (Contains(point))
						{
							yield return point;
						}
					}
				}
			}
		}
		#endregion

		/// <summary>
		/// Returns the neighbors so that no two are in the same line. For example, if East is among them, 
		/// then West won't be.
		/// </summary>
		public IEnumerable<RectPoint> GetPrincipleNeighborDirections()
		{
			return NeighborDirections.TakeHalf();
		}
	}
}