//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System.Collections.Generic;
using Gamelogic.Extensions.Algorithms;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Represents a diamond grid. At its simplest, diamond grids are square grids that 
	/// are rotated 45 degrees.
	/// 
	/// By using suitable dimensions in the map, DiamondGrids
	/// can be used to implement isometric grids.
	/// </summary>
	[Version(1)]
	public partial class DiamondGrid<TCell> : AbstractUniformGrid<TCell, DiamondPoint>, IEvenGrid<TCell, DiamondPoint, DiamondPoint>
	{
		private static readonly DiamondPoint[] SpiralIteratorDirections =
		{
			DiamondPoint.SouthEast,
			DiamondPoint.SouthWest,
			DiamondPoint.NorthWest,
			DiamondPoint.NorthEast
		};


		#region Storage
		protected override ArrayPoint ArrayPointFromPoint(DiamondPoint point)
		{
			return ArrayPointFromGridPoint(point);
		}

		protected override ArrayPoint ArrayPointFromPoint(int x, int y)
		{
			return ArrayPointFromGridPoint(new DiamondPoint(x, y));
		}

		protected override DiamondPoint PointFromArrayPoint(int x, int y)
		{
			return GridPointFromArrayPoint(new ArrayPoint(x, y));
		}
		#endregion

		#region Neighbors
		public static DiamondPoint GridPointFromArrayPoint(ArrayPoint point)
		{
			return new DiamondPoint(point.X, point.Y);
		}

		public static ArrayPoint ArrayPointFromGridPoint(DiamondPoint point)
		{
			return new ArrayPoint(point.X, point.Y);
		}

		public void SetNeighborsDiagonals()
		{
			NeighborDirections = DiamondPoint.DiagonalDirections;
		}

		public void SetNeighborsMain()
		{
			NeighborDirections = DiamondPoint.MainDirections;
		}

		public void SetNeighborsMainAndDiagonals()
		{
			NeighborDirections = DiamondPoint.MainAndDiagonalDirections;
		}
		#endregion

		#region Iterators
		
		[Version(1,10)]
		public IEnumerable<DiamondPoint> GetSpiralIterator(int ringCount)
		{
			return GetSpiralIterator(DiamondPoint.Zero, ringCount);
		}

		[Version(1,10)]
		public IEnumerable<DiamondPoint> GetSpiralIterator(DiamondPoint origin, int ringCount)
		{
			var point = origin;

			if (Contains(point))
			{
				yield return point;
			}

			for (var k = 1; k < ringCount; k++)
			{
				point += DiamondPoint.North;

				for (var i = 0; i < 4; i++)
				{
					for (var j = 0; j < 2 * k; j++)
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

		public IEnumerable<DiamondPoint> GetPrincipleNeighborDirections()
		{
			return NeighborDirections.TakeHalf();
		}
	}
}