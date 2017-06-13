//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

// Auto-generated File

using System;
using System.Collections.Generic;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{

	public partial class PointyHexGrid<TCell>
	{
		private static readonly PointyHexPoint[] SpiralIteratorDirections = 
		{
			PointyHexPoint.East,
			PointyHexPoint.NorthEast,
			PointyHexPoint.NorthWest,
			PointyHexPoint.West,
			PointyHexPoint.SouthWest,
			PointyHexPoint.SouthEast
		};

		/// <summary>
		/// An iterator that spirals anti-clockwise around the grid origin (0, 0).
		/// </summary>
		[Version(1,7)]
		[Experimental]
		public IEnumerable<PointyHexPoint> GetSpiralIterator(int ringCount)
		{
			return GetSpiralIterator(PointyHexPoint.Zero, ringCount);
		}

		/// <summary>
		/// An iterator that spirals anti-clockwise around the given origin.
		/// </summary>
		/// <example>
		/// <code>
		/// int k = 0;
		///
		///	foreach(var point in grid.GetSpiralIterator(point, 3))
		///	{
		///		grid[point].name = k.ToString();
		///	}
		/// </code>
		/// </example>
		[Version(1,10)]
		[Experimental]
		public IEnumerable<PointyHexPoint> GetSpiralIterator(PointyHexPoint origin, int ringCount) 
		{			
			var hexPoint = origin;
			yield return hexPoint;
		
			for (var k = 1; k < ringCount; k++)
			{
				hexPoint = new PointyHexPoint(0, 0);
				hexPoint = hexPoint + SpiralIteratorDirections[4] * (k);
			
				for (var i = 0; i < 6; i++)
				{
					for (var j = 0; j < k; j++)
					{
						if (Contains(hexPoint))
						{
							yield return hexPoint;
						}

						hexPoint = hexPoint + SpiralIteratorDirections[i];
					}
				}
			}
		}
				
		/// <summary>
		/// Returns a new grid, wrapped along a Hexagon with the given side length.
		/// </summary>
		[Version(1,7)]
		public static WrappedGrid<TCell, PointyHexPoint> WrappedHexagon(int side)
		{
			var grid = Hexagon(side);
			var wrapper = new PointyHexHexagonWrapper(side);
			var wrappedGrid = new WrappedGrid<TCell, PointyHexPoint>(grid, wrapper);

			return wrappedGrid;
		}		
	}

	/// <summary>
	/// Since version 1.7
	/// </summary>
	[Experimental]
	public class PointyHexHexagonWrapper : IPointWrapper<PointyHexPoint>
	{
		private readonly PointyHexPoint[] wrappedPoints;
		private readonly Func<PointyHexPoint, int> colorFunc; 

		public PointyHexHexagonWrapper(int n)
		{
			int colorCount = 3*n*n - 3*n + 1;
			int x1 = 3*n - 2;

			wrappedPoints = new PointyHexPoint[colorCount];
			var grid = PointyHexGrid<int>.Hexagon(n);
			
			colorFunc = x => x.GetColor(colorCount, x1, 1);

			foreach (var point in grid)
			{
				int color = colorFunc(point);
				wrappedPoints[color] = point;
			}
		}

		public PointyHexPoint Wrap(PointyHexPoint p)
		{
			return wrappedPoints[colorFunc(p)];
		}
	}

	public partial struct PointyHexPoint
	{
		#region Geometry

		/// <summary>
		/// Whether this point is inside the half plane x >= x0.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInPositiveHalfPlaneX(int x0)
		{
			return X >= x0;
		}

		/// <summary>
		/// Whether this point is inside the half plane x <= x0.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInNegativeHalfPlaneX(int x0)
		{
			return X <= x0;
		}

		/// <summary>
		/// Whether this point is inside the half plane y >= x0.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInPositiveHalfPlaneY(int y0)
		{
			return Y >= y0;
		}

		/// <summary>
		/// Whether this point is inside the half plane y <= y0.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInNegativeHalfPlaneY(int y0)
		{
			return Y <= y0;
		}

		/// <summary>
		/// Whether this point is inside the half plane z >= z0.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInPositiveHalfPlaneZ(int z0)
		{
			return Z >= z0;
		}

		/// <summary>
		/// Whether this point is inside the half plane z <= z0.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInNegativeHalfPlaneZ(int z0)
		{
			return Z <= z0;
		}

		/// <summary>
		/// Whether this point is in the hexagon with the given radius 
		///	and center at the origin.
		///
		///	The origin is considered the hexagon with zero radius.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		//TODO: How should we make this consistent with 
		//exisiting shape code (where < is used instead of <=)
		public bool IsInsideHexagon(int radius)
		{
			return Magnitude() <= radius;
		}

		/// <summary>
		/// Whether this point is in the hexagon with the given radius 
		///	and given center.
		///
		///	A single point is considered a hexagon with zero radius.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInsideHexagon(PointyHexPoint center, int radius)
		{
			return (this - center).Magnitude() <= radius;
		}

		/// <summary>
		/// Returns whether this point is in either 
		///	the up triangle given by
		///
		///	(x >= x0 && y >= y0 && z >= z0)
		///
		///	or the down triangle given by
		///
		///	(z &lt;= x0 && y &lt;= y0 && z &lt;= z0)
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInsideTriangle(int x0, int y0, int z0)
		{
			return 
				(X >= x0 && Y >= y0 && Z >= z0) ||
				(X <= x0 && Y <= y0 && Z <= z0);
		}

		/// <summary>
		/// Whether this point is inside the polygon described by
		///	
		///	(x0 &lt;= X &lt;= x1) && (y0 &lt;= Y &lt;= y1) && (z0 &lt;= Z &lt;= z1)
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInsidePolygon(int x0, int x1, int y0, int y1, int z0, int z1)
		{
			return
				x0 <= X && X <= x1 &&
				y0 <= Y && Y <= y1 &&
				z0 <= Z && Z <= z1;
		}
		#endregion

		#region Colorings
		

		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color1_1.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor1_1()
		{
			return 0;
		}

		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color1_2.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor1_2()
		{
			return GetColor1_3() % 2;
		}

		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color1_3.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor1_3()
		{
			return GetColor(3, 1, 1);
		}
		
		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color2_2.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor2_2()
		{
			return (2 + (X * Y) % 2) % 2;
		}

		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color2_4.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor2_4()
		{
			return GetColor(2, 0, 2);
			//return Mathi.Mod(Mathi.Mod(X, 2) + 2 * Mathi.Mod(Y, 2), 4);
		}

		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color3_2.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor3_2()
		{
			return GetColor3_7() / 6;
		}

		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color3_7.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor3_7()
		{
			return GetColor(7, 2, 1);
			//return Mathi.Mod(Mathi.Mod(X, 7) + 5 * Y, 7);
		}

		/// <summary>
		/// Since version 1.7
		/// </summary>
		public int GetColor5_5()
		{
			return GetColor(5, 2, 1); 
		}

		/// <summary>
		/// Since version 1.7
		/// </summary>
		public int GetColor6()
		{
			return GetColor(3, 0, 2);
		}
		#endregion
	}


	public partial class FlatHexGrid<TCell>
	{
		private static readonly FlatHexPoint[] SpiralIteratorDirections = 
		{
			FlatHexPoint.North,
			FlatHexPoint.NorthWest,
			FlatHexPoint.SouthWest,
			FlatHexPoint.South,
			FlatHexPoint.SouthEast,
			FlatHexPoint.NorthEast
		};

		/// <summary>
		/// An iterator that spirals anti-clockwise around the grid origin (0, 0).
		/// </summary>
		[Version(1,7)]
		[Experimental]
		public IEnumerable<FlatHexPoint> GetSpiralIterator(int ringCount)
		{
			return GetSpiralIterator(FlatHexPoint.Zero, ringCount);
		}

		/// <summary>
		/// An iterator that spirals anti-clockwise around the given origin.
		/// </summary>
		/// <example>
		/// <code>
		/// int k = 0;
		///
		///	foreach(var point in grid.GetSpiralIterator(point, 3))
		///	{
		///		grid[point].name = k.ToString();
		///	}
		/// </code>
		/// </example>
		[Version(1,10)]
		[Experimental]
		public IEnumerable<FlatHexPoint> GetSpiralIterator(FlatHexPoint origin, int ringCount) 
		{			
			var hexPoint = origin;
			yield return hexPoint;
		
			for (var k = 1; k < ringCount; k++)
			{
				hexPoint = new FlatHexPoint(0, 0);
				hexPoint = hexPoint + SpiralIteratorDirections[4] * (k);
			
				for (var i = 0; i < 6; i++)
				{
					for (var j = 0; j < k; j++)
					{
						if (Contains(hexPoint))
						{
							yield return hexPoint;
						}

						hexPoint = hexPoint + SpiralIteratorDirections[i];
					}
				}
			}
		}
				
		/// <summary>
		/// Returns a new grid, wrapped along a Hexagon with the given side length.
		/// </summary>
		[Version(1,7)]
		public static WrappedGrid<TCell, FlatHexPoint> WrappedHexagon(int side)
		{
			var grid = Hexagon(side);
			var wrapper = new FlatHexHexagonWrapper(side);
			var wrappedGrid = new WrappedGrid<TCell, FlatHexPoint>(grid, wrapper);

			return wrappedGrid;
		}		
	}

	/// <summary>
	/// Since version 1.7
	/// </summary>
	[Experimental]
	public class FlatHexHexagonWrapper : IPointWrapper<FlatHexPoint>
	{
		private readonly FlatHexPoint[] wrappedPoints;
		private readonly Func<FlatHexPoint, int> colorFunc; 

		public FlatHexHexagonWrapper(int n)
		{
			int colorCount = 3*n*n - 3*n + 1;
			int x1 = 3*n - 2;

			wrappedPoints = new FlatHexPoint[colorCount];
			var grid = FlatHexGrid<int>.Hexagon(n);
			
			colorFunc = x => x.GetColor(colorCount, x1, 1);

			foreach (var point in grid)
			{
				int color = colorFunc(point);
				wrappedPoints[color] = point;
			}
		}

		public FlatHexPoint Wrap(FlatHexPoint p)
		{
			return wrappedPoints[colorFunc(p)];
		}
	}

	public partial struct FlatHexPoint
	{
		#region Geometry

		/// <summary>
		/// Whether this point is inside the half plane x >= x0.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInPositiveHalfPlaneX(int x0)
		{
			return X >= x0;
		}

		/// <summary>
		/// Whether this point is inside the half plane x <= x0.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInNegativeHalfPlaneX(int x0)
		{
			return X <= x0;
		}

		/// <summary>
		/// Whether this point is inside the half plane y >= x0.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInPositiveHalfPlaneY(int y0)
		{
			return Y >= y0;
		}

		/// <summary>
		/// Whether this point is inside the half plane y <= y0.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInNegativeHalfPlaneY(int y0)
		{
			return Y <= y0;
		}

		/// <summary>
		/// Whether this point is inside the half plane z >= z0.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInPositiveHalfPlaneZ(int z0)
		{
			return Z >= z0;
		}

		/// <summary>
		/// Whether this point is inside the half plane z <= z0.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInNegativeHalfPlaneZ(int z0)
		{
			return Z <= z0;
		}

		/// <summary>
		/// Whether this point is in the hexagon with the given radius 
		///	and center at the origin.
		///
		///	The origin is considered the hexagon with zero radius.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		//TODO: How should we make this consistent with 
		//exisiting shape code (where < is used instead of <=)
		public bool IsInsideHexagon(int radius)
		{
			return Magnitude() <= radius;
		}

		/// <summary>
		/// Whether this point is in the hexagon with the given radius 
		///	and given center.
		///
		///	A single point is considered a hexagon with zero radius.
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInsideHexagon(FlatHexPoint center, int radius)
		{
			return (this - center).Magnitude() <= radius;
		}

		/// <summary>
		/// Returns whether this point is in either 
		///	the up triangle given by
		///
		///	(x >= x0 && y >= y0 && z >= z0)
		///
		///	or the down triangle given by
		///
		///	(z &lt;= x0 && y &lt;= y0 && z &lt;= z0)
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInsideTriangle(int x0, int y0, int z0)
		{
			return 
				(X >= x0 && Y >= y0 && Z >= z0) ||
				(X <= x0 && Y <= y0 && Z <= z0);
		}

		/// <summary>
		/// Whether this point is inside the polygon described by
		///	
		///	(x0 &lt;= X &lt;= x1) && (y0 &lt;= Y &lt;= y1) && (z0 &lt;= Z &lt;= z1)
		///
		///	see http://devmag.org.za/2013/08/31/geometry-with-hex-coordinates/
		///
		///	Since version 1.3
		/// </summary>
		public bool IsInsidePolygon(int x0, int x1, int y0, int y1, int z0, int z1)
		{
			return
				x0 <= X && X <= x1 &&
				y0 <= Y && Y <= y1 &&
				z0 <= Z && Z <= z1;
		}
		#endregion

		#region Colorings
		

		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color1_1.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor1_1()
		{
			return 0;
		}

		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color1_2.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor1_2()
		{
			return GetColor1_3() % 2;
		}

		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color1_3.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor1_3()
		{
			return GetColor(3, 1, 1);
		}
		
		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color2_2.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor2_2()
		{
			return (2 + (X * Y) % 2) % 2;
		}

		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color2_4.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor2_4()
		{
			return GetColor(2, 0, 2);
			//return Mathi.Mod(Mathi.Mod(X, 2) + 2 * Mathi.Mod(Y, 2), 4);
		}

		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color3_2.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor3_2()
		{
			return GetColor3_7() / 6;
		}

		/// <summary>
		/// Generates a coloring with the following pattern:
		///	
		///	<img src="fh_color3_7.png" />
		///	
		///	Light Blue corresponds to 0. 
		///	Light Green corresponds to 1. 
		///	Light Yellow corresponds to 2. 
		///	Light Red corresponds to 3. 
		///	Blue corresponds to 4. 
		///	Green corresponds to 5. 
		///	Yellow corresponds to 6. 
		/// </summary>
		public int GetColor3_7()
		{
			return GetColor(7, 2, 1);
			//return Mathi.Mod(Mathi.Mod(X, 7) + 5 * Y, 7);
		}

		/// <summary>
		/// Since version 1.7
		/// </summary>
		public int GetColor5_5()
		{
			return GetColor(5, 2, 1); 
		}

		/// <summary>
		/// Since version 1.7
		/// </summary>
		public int GetColor6()
		{
			return GetColor(3, 0, 2);
		}
		#endregion
	}

}
