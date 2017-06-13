//TODO: Should this be in extensions?

using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Class that provides methods for working woth polygons partitions.
	/// </summary>
	public static class PolygonPartition
	{
		/*

		var polygonDivision = PolygonDivision.Make()
			.OpenPolygon()
				.Add(3, 4)
				.Add(2, 4)
				.Add(3, 4)
			.OpenPolygon()

		OR

		var polygonDivision = PolygonDivision.Make(
			new[]
			{ 
				new []{{3, 4}, {2, 4}, {3, 4}}
				new []{}
			}

				

	*/


		private sealed class HalfPlane : IPointSet2
		{
			private readonly Vector2 lineDirection;
			private readonly Vector2 halfPlanePoint;

			public HalfPlane(Vector2 lineDirection, Vector2 halfPlanePoint)
			{
				this.lineDirection = lineDirection;
				this.halfPlanePoint = halfPlanePoint;
			}

			public bool Contains(Vector2 point)
			{
				return Geometry.IsInHalfPlane(point, halfPlanePoint, lineDirection);
			}
		}

		private sealed class Polygon : IPointSet2
		{
			private readonly List<HalfPlane> planes;

			public Polygon(IEnumerable<HalfPlane> halfPlanes)
			{
				planes = halfPlanes.ToList();
			}

			public bool Contains(Vector2 point)
			{
				return planes.All(p => p.Contains(point));
			}

			public static Polygon OpenPolygon(Vector2[] vertices)
			{
				var list = new List<HalfPlane>();

				for (int i = 0; i < vertices.GetLength(0) - 1; i++)
				{
					var endPoint1 = vertices[i + 1];
					var endPoint0 = vertices[i];
					list.Add(new HalfPlane(endPoint0, endPoint1 - endPoint0));
				}

				return new Polygon(list);
			}
		}

		private sealed class PolygonStack : IPointPartition2
		{
			private readonly List<Polygon> polygons;

			public PolygonStack(IEnumerable<Polygon> polygons)
			{
				this.polygons = polygons.ToList();
			}

			public int GetPartition(Vector2 point)
			{
				int index = polygons.FindIndex(p => p.Contains(point));

				return index == -1 ? polygons.Count : index;
			}
		}

		public static IPointPartition2 MakePolygonPartition(IEnumerable<Vector2[]> polygonsVertices)
		{
			var stack = new PolygonStack(polygonsVertices.Select<Vector2[], Polygon>(Polygon.OpenPolygon));
			return stack;
		}
	}

	/// <summary>
	/// Represents a set of points with a method to determine
	/// whether a given point is in the set.
	/// </summary> //TODO should this be a ImplicitShapef ?
	public interface IPointSet2
	{
		/// <summary>
		/// Determines whether this PointShape2 contains the specified point.
		/// </summary>
		/// <param name="point">The point.</param>
		/// <returns><c>true</c> if PointShape2 contains the specified point; otherwise, <c>false</c>.</returns>
		bool Contains(Vector2 point);
	}

	/// <summary>
	/// A partition of a 2D point set, with each partition denoted by a
	/// 0-based index.
	/// </summary>
	public interface IPointPartition2
	{
		int GetPartition(Vector2 point);
	}
}