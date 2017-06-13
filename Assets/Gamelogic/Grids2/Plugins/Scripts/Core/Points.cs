using System.Collections.Generic;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Provides constants and methods for working with points in rect grids.
	/// </summary>
	public static class RectPoint
	{
		#region Constants
		public static readonly GridPoint2 North = new GridPoint2(0, 1); //TODO: use unity directions instead?
		public static readonly GridPoint2 East = new GridPoint2(1, 0);
		public static readonly GridPoint2 South = new GridPoint2(0, -1);
		public static readonly GridPoint2 West = new GridPoint2(-1, 0);

		public static readonly GridPoint2 NorthEast = North + East;
		public static readonly GridPoint2 NorthWest = North + West;
		public static readonly GridPoint2 SouthWest = South + West;
		public static readonly GridPoint2 SouthEast = South + East;

		public static readonly IMap<GridPoint2, GridPoint2> HorizontalLine = GridPoint2.GetVectorLine(East);

		public static readonly IMap<GridPoint2, GridPoint2> VerticalLine = GridPoint2.GetVectorLine(North);

		public static readonly IEnumerable<IMap<GridPoint2, GridPoint2>> OrthogonalLines
			= GridPoint2.GetVectorLines(new List<GridPoint2>() { East, North });

		public static readonly IEnumerable<IMap<GridPoint2, GridPoint2>> DiagonalLines
			= GridPoint2.GetVectorLines(new List<GridPoint2>() { NorthEast, NorthWest });

		public static readonly StructList<GridPoint2> OrthogonalDirections = new StructList<GridPoint2>
		{
			East,
			North,
			West,
			South
		};


		public static readonly StructList<GridPoint2> DiagonalDirections = new StructList<GridPoint2>
		{
			NorthEast,
			NorthWest,
			SouthWest,
			SouthEast
		};

		public static readonly StructList<GridPoint2> OrthogonalAndDiagonalDirections = new StructList<GridPoint2>
		{
			East,
			NorthEast,
			North,
			NorthWest,
			West,
			SouthWest,
			South,
			SouthEast
		};

		#endregion

		#region NeighborFunctions 
		public static IEnumerable<GridPoint2> GetOrthogonalNeighbors(GridPoint2 point)
		{
			return point.GetVectorNeighbors(OrthogonalDirections);
		}

		public static IEnumerable<GridPoint2> GetDiagonalNeighbors(GridPoint2 point)
		{
			return point.GetVectorNeighbors(DiagonalDirections);
		}

		public static IEnumerable<GridPoint2> GetOrthogonalAndDiagonalNeighbors(GridPoint2 point)
		{
			return point.GetVectorNeighbors(OrthogonalAndDiagonalDirections);
		}
		#endregion

		#region Transforms
		public static GridPoint2 Rotate180(GridPoint2 point)
		{
			return new GridPoint2(-point.X, -point.Y);
		}

		public static GridPoint2 Rotate270(GridPoint2 point)
		{
			return new GridPoint2(point.Y, -point.X);
		}

		public static GridPoint2 ReflectAboutX(GridPoint2 point)
		{
			return new GridPoint2(point.X, -point.Y);
		}

		public static GridPoint2 ReflectAboutY(GridPoint2 point)
		{
			return new GridPoint2(-point.X, point.Y);
		}

		public static GridPoint2 Rotate90(GridPoint2 point)
		{
			return new GridPoint2(-point.Y, point.X);
		}
		#endregion

		#region Norms

		public static float EuclideanNorm(GridPoint2 point)
		{
			return Mathf.Sqrt(point.X * point.X + point.Y * point.Y);
		}

		public static int ManhattanNorm(GridPoint2 point)
		{
			return Mathf.Abs(point.X) + Mathf.Abs(point.Y);
		}

		public static int ChebychevNorm(GridPoint2 point)
		{
			return Mathf.Max(Mathf.Abs(point.X), Mathf.Abs(point.Y));
		}

		/// <summary>
		/// Knights the norm.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns>System.Int32.</returns>
		public static int KnightNorm(GridPoint2 point)
		{

			int x = Mathf.Abs(point.X);
			int y = Mathf.Abs(point.Y);

			if (y > x)
			{
				x = Mathf.Abs(point.Y);
				y = Mathf.Abs(point.X);
			}

			if (x == 2 && y == 2)
				return 4;

			if (x == 1 && y == 0)
				return 3;

			if (y == 0 || y / (float)x <= 0.5f)
			{
				int initX = 0;
				int xClass = x % 4;

				if (xClass == 0)
					initX = x / 2;
				else if (xClass == 1)
					initX = 1 + (x / 2);
				else if (xClass == 2)
					initX = 1 + (x / 2);
				else
					initX = 1 + ((x + 1) / 2);


				if (xClass > 1)
					return initX - (y % 2);
				else
					return initX + (y % 2);
			}
			else
			{
				int diagonal = x - ((x - y) / 2);

				if ((x - y) % 2 == 0)
				{
					if (diagonal % 3 == 0)
						return (diagonal / 3) * 2;
					if (diagonal % 3 == 1)
						return ((diagonal / 3) * 2) + 2;
					else
						return ((diagonal / 3) * 2) + 2;
				}
				else
				{
					return ((diagonal / 3) * 2) + 1;
				}
			}

		}

		#endregion

		#region Round

		public static GridPoint2 RoundToGridPoint(Vector2 point)
		{
			return new GridPoint2(
				Mathf.RoundToInt(point.x),
				Mathf.RoundToInt(point.y));
		}

		#endregion
	}

	/// <summary>
	/// Provides constants and methods for working with points in pointy hex grids.
	/// </summary>
	public static class PointyHexPoint
	{
		#region Constants

		public static readonly float Sqrt3 = Mathf.Sqrt(3);

		public static readonly GridPoint2 East = new GridPoint2(1, 0);
		public static readonly GridPoint2 NorthEast = new GridPoint2(0, 1);
		public static readonly GridPoint2 NorthWest = new GridPoint2(-1, 1);
		public static readonly GridPoint2 West = new GridPoint2(-1, 0);
		public static readonly GridPoint2 SouthWest = new GridPoint2(0, -1);
		public static readonly GridPoint2 SouthEast = new GridPoint2(1, -1);

		/// <summary>
		/// The default space map transform for hex grids. This should be used with a uniform scale
		/// matrix (with the value of the cell width) to get a regular hex grid.
		/// </summary>
		/// <remarks>This is typically useful when constructing mesh cells.</remarks>
		public static readonly Matrixf33 SpaceMapTransform = new Matrixf33(
			1, 0, 0,
			0.5f, 1.5f / Sqrt3, 0,
			0, 0, 1);

		/// <summary>
		/// The raw space map transform for hex grids. This should be used with a scale matrix with
		/// values (width, height, 1) to get cells with the desired with and height.
		/// </summary>
		/// <remarks>This is typically useful when constructing maps for tile grids.</remarks>
		public static readonly Matrixf33 RawSpaceMapTransform = new Matrixf33(
			1, 0, 0,
			0.5f, 0.75f, 0,
			0, 0, 1);

		public static readonly IEnumerable<GridPoint2> Directions = new StructList<GridPoint2>
		{
			East,
			NorthEast,
			NorthWest,
			West,
			SouthWest,
			SouthEast
		};

		public static readonly IEnumerable<IMap<GridPoint2, GridPoint2>> OrthogonalLines
			= GridPoint2.GetVectorLines(new List<GridPoint2>() { East, NorthEast, NorthWest });
		#endregion

		#region Neighbors

		public static IEnumerable<GridPoint2> GetOrthogonalNeighbors(GridPoint2 point)
		{
			return point.GetVectorNeighbors(Directions);
		}

		#endregion

		#region Transforms
		public static GridPoint2 Rotate60(GridPoint2 point)
		{
			return new GridPoint2(-point.Y, point.X + point.Y);
		}

		public static GridPoint2 Rotate120(GridPoint2 point)
		{
			return new GridPoint2(-point.X - point.Y, point.X);
		}

		public static GridPoint2 Rotate180(GridPoint2 point)
		{
			return new GridPoint2(-point.X, -point.Y);
		}

		public static GridPoint2 Rotate240(GridPoint2 point)
		{
			return new GridPoint2(point.Y, -point.X - point.Y);
		}

		public static GridPoint2 Rotate300(GridPoint2 point)
		{
			return new GridPoint2(point.X + point.Y, -point.X);
		}

		public static GridPoint2 ReflectAboutY(GridPoint2 point)
		{
			return new GridPoint2(-point.X - point.Y, point.Y);
		}

		public static GridPoint2 ReflectAboutX(GridPoint2 point)
		{
			return new GridPoint2(point.X + point.Y, -point.Y);
		}

		public static GridPoint2 Rotate60AndReflectAboutY(GridPoint2 point)
		{
			return ReflectAboutY(Rotate60(point));
		}

		public static GridPoint2 Rotate120AndReflectAboutY(GridPoint2 point)
		{
			return ReflectAboutY(Rotate120(point));
		}

		public static GridPoint2 Rotate180AndReflectAboutY(GridPoint2 point)
		{
			return ReflectAboutY(Rotate180(point));
		}

		public static GridPoint2 Rotate240AndReflectAboutY(GridPoint2 point)
		{
			return ReflectAboutY(Rotate240(point));
		}

		public static GridPoint2 Rotate300AndReflectAboutY(GridPoint2 point)
		{
			return ReflectAboutY(Rotate300(point));
		}
		#endregion

		#region Norms
		public static float EuclideanNorm(GridPoint2 point)
		{
			int minusZ = point.X + point.Y;

			return Mathf.Sqrt(point.X * point.X + point.Y * point.Y + minusZ * minusZ);
		}

		public static int ManhattanNorm(GridPoint2 point)
		{
			return (Mathf.Abs(point.X) + Mathf.Abs(point.Y) + Mathf.Abs(point.X + point.Y)) / 2;
		}

		public static int HexNorm(GridPoint2 point)
		{
			return Mathf.Max(Mathf.Abs(point.X), Mathf.Abs(point.Y), Mathf.Abs(point.X + point.Y));
		}

		public static int UpTriPseudoNorm(GridPoint2 point)
		{
			return Mathf.Max(point.X, point.Y, -point.X - point.Y);
		}

		public static int DownTriPseudoNorm(GridPoint2 point)
		{
			return Mathf.Max(-point.X, -point.Y, point.X + point.Y);
		}

		public static int StarNorm(GridPoint2 point)
		{
			return Mathf.Min(UpTriPseudoNorm(point), DownTriPseudoNorm(point));
		}
		#endregion

		#region Round

		public static GridPoint2 RoundToGridPoint(Vector2 vec)
		{
			var rx = Mathf.RoundToInt(vec.x);
			var ry = Mathf.RoundToInt(vec.y);
			var z = -vec.x - vec.y;
			var rz = Mathf.RoundToInt(z);

			var xDelta = Mathf.Abs(rx - vec.x);
			var yDelta = Mathf.Abs(ry - vec.y);
			var zDelta = Mathf.Abs(rz - z);

			if (xDelta > yDelta && xDelta > zDelta)
			{
				rx = -ry - rz;
			}
			else if (yDelta > zDelta)
			{
				ry = -rx - rz;
			}

			return new GridPoint2(rx, ry);
		}

		#endregion

	}

	/// <summary>
	/// Provides constants and methods for working with points in block grids.
	/// </summary>
	public static class BlockPoint
	{
		#region Constant

		public static readonly GridPoint3 North = new GridPoint3(0, 0, 1); //TODO: use unity directions instead?
		public static readonly GridPoint3 East = new GridPoint3(1, 0, 0);
		public static readonly GridPoint3 South = new GridPoint3(0, 0, -1);
		public static readonly GridPoint3 West = new GridPoint3(-1, 0, 0);
		public static readonly GridPoint3 Up = new GridPoint3(0, 1, 0);
		public static readonly GridPoint3 Down = new GridPoint3(0, -1, 0);

		public static readonly StructList<GridPoint3> OrthogonalDirections = new StructList<GridPoint3>
		{
			East,
			North,
			Up,
			West,
			South,
			Down
		};

		#endregion

		#region Norms

		public static float EuclideanNorm(GridPoint3 point)
		{
			var squareMagnitude =
				point.X * point.X +
				point.Y * point.Y +
				point.Z * point.Z;

			return Mathf.Sqrt(squareMagnitude);
		}

		public static int ManhattanNorm(GridPoint3 point)
		{
			return Mathf.Abs(point.X) + Mathf.Abs(point.Y) + Mathf.Abs(point.Z);
		}

		public static int ChebychevNorm(GridPoint3 point)
		{
			return Mathf.Max(Mathf.Abs(point.X), Mathf.Abs(point.Y), Mathf.Abs(point.Z));
		}

		#endregion

		#region Round

		public static GridPoint3 RoundToGridPoint(Vector3 point)
		{
			return new GridPoint3(
				Mathf.RoundToInt(point.x),
				Mathf.RoundToInt(point.y),
				Mathf.RoundToInt(point.z));
		}

		#endregion
	}

	/// <summary>
	/// Class with methods for working with hex block points.
	/// </summary>
	[Version(2, 2)]
	public static class HexBlockPoint
	{
		#region Constants
		public static readonly GridPoint3 East = new GridPoint3(1, 0, 0);
		public static readonly GridPoint3 NorthEast = new GridPoint3(0, 0, 1);
		public static readonly GridPoint3 NorthWest = new GridPoint3(-1, 0, 1);
		public static readonly GridPoint3 West = new GridPoint3(-1, 0, 0);
		public static readonly GridPoint3 SouthWest = new GridPoint3(0, 0, -1);
		public static readonly GridPoint3 SouthEast = new GridPoint3(1, 0, -1);

		public static readonly GridPoint3 Up = new GridPoint3(0, 1, 0);
		public static readonly GridPoint3 Down = new GridPoint3(0, -1, 0);

		public static readonly IEnumerable<GridPoint3> Directions = new StructList<GridPoint3>
		{
			East,
			NorthEast,
			NorthWest,
			West,
			SouthWest,
			SouthEast,
			Up,
			Down
		};

		public static readonly IEnumerable<IMap<GridPoint3, GridPoint3>> OrthogonalLines
			= GridPoint3.GetVectorLines(new List<GridPoint3>() { East, NorthEast, NorthWest, Up });

		#endregion

		#region Neighbors

		public static IEnumerable<GridPoint3> GetOrthogonalNeighbors(GridPoint3 point)
		{
			return point.GetVectorNeighbors(Directions);
		}

		#endregion


		#region Round

		public static GridPoint3 RoundToGridPoint(Vector3 vec)
		{
			var vec2D = vec.To2DXZ();
			var point2D = PointyHexPoint.RoundToGridPoint(vec2D);
			var y = Mathf.RoundToInt(vec.y);

			return point2D.To3DXZ(y);
		}

		#endregion

	}
}