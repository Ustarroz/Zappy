using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Algorithms;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Struct for representing a pair of integers.
	/// </summary>
	public struct Pair
	{
		public readonly int m;
		public readonly int n;

		public Pair(int m, int n)
		{
			if (m <= n)
			{
				this.m = m;
				this.n = n;
			}
			else
			{
				this.m = n;
				this.n = m;
			}
		}
	}

	/// <summary>
	/// Contains various mesh maps used by builders.
	/// </summary>
	public static class MeshMap
	{
		public static void SubDivide(
			int[] triangles, 
			Vector3[] vertexDirections,
			out int[]newTriangles,
			out Vector3[] newVertexDirections)
		{
			var newTriangleIndices = new Dictionary<Pair, int>();
			var newVertexDirectionsList = new List<Vector3>();
			int triangleIndex = 0;
			var newTrianglesList = new List<int>();

			for (int i = 0; i < triangles.Length/3; i++)
			{
				int a = triangles[3 * i];
				int b = triangles[3 * i + 1];
				int c = triangles[3 * i + 2];

				var pairs = new List<Pair>()
				{
					new Pair(a, a),
					new Pair(a, b),
					new Pair(b, b),
					new Pair(b, c),
					new Pair(c, c),
					new Pair(c, a),
				};

				foreach (var pair in pairs)
				{
					triangleIndex = UpdateTriangles(vertexDirections, newTriangleIndices, pair, triangleIndex, newVertexDirectionsList);
				}

				newTrianglesList.Add(newTriangleIndices[pairs[0]]);
				newTrianglesList.Add(newTriangleIndices[pairs[1]]);
				newTrianglesList.Add(newTriangleIndices[pairs[5]]);

				newTrianglesList.Add(newTriangleIndices[pairs[1]]);
				newTrianglesList.Add(newTriangleIndices[pairs[2]]);
				newTrianglesList.Add(newTriangleIndices[pairs[3]]);

				newTrianglesList.Add(newTriangleIndices[pairs[3]]);
				newTrianglesList.Add(newTriangleIndices[pairs[5]]);
				newTrianglesList.Add(newTriangleIndices[pairs[1]]);

				newTrianglesList.Add(newTriangleIndices[pairs[3]]);
				newTrianglesList.Add(newTriangleIndices[pairs[4]]);
				newTrianglesList.Add(newTriangleIndices[pairs[5]]);
			}

			newVertexDirections = newVertexDirectionsList.ToArray();
			newTriangles = newTrianglesList.ToArray();
		}

		private static int UpdateTriangles(Vector3[] vertexDirections, Dictionary<Pair, int> newTriangleIndices, Pair pair,
			int triangleIndex, List<Vector3> newVertexDirections)
		{
			if (!newTriangleIndices.ContainsKey(pair))
			{
				newTriangleIndices[pair] = triangleIndex;
				newVertexDirections.Add((vertexDirections[pair.m] + vertexDirections[pair.n])/2);
				triangleIndex++;
			}
			return triangleIndex;
		}
	}

	/// <summary>
	/// Interface for maps for mesh grids.
	/// </summary>
	[Experimental]
	[Version(1, 14)]
	public interface IMeshMap<TPoint> where TPoint : IGridPoint<TPoint>
	{
		IMap<TPoint> Map2D { get; }
		
		IEnumerable<Vector3> GetVertices(TPoint point);

		IEnumerable<Vector2> GetUVs(TPoint point);

		IEnumerable<int> GetTriangles(TPoint point, int vertexIndex);
	}

	/// <summary>
	/// Map for pointy hex mesh grids.
	/// </summary>
	[Experimental]
	[Version(1, 14)]
	public class PointyHexMeshMap : IMeshMap<PointyHexPoint>
	{
		private static readonly Vector3 PreScale = new Vector3(2 / GLMathf.Sqrt3, 1, 1);
		private readonly Vector2 cellDimensions;
		private readonly PointyHexMap map;

		private static readonly Vector3[] VertexDirections =
		{
			new Vector3(0, 1f/2),
			new Vector3(GLMathf.Sqrt3/4, 1f/4),
			new Vector3(GLMathf.Sqrt3/4, -1f/4),
			new Vector3(0, -1f/2),
			new Vector3(-GLMathf.Sqrt3/4, -1f/4),
			new Vector3(-GLMathf.Sqrt3/4, 1f/4),
			Vector3.zero
		};

		private readonly int[] Triangles =
		{
			6, 0, 1,
			6, 1, 2,
			6, 2, 3,
			6, 3, 4,
			6, 4, 5,
			6, 5, 0
		};

		private Vector3[] vertexDirections2;
		private int[] triangles2;

		public PointyHexMeshMap(Vector2 cellDimensions, PointyHexMap map)
		{
			this.cellDimensions = cellDimensions;
			this.map = map;
			triangles2 = Triangles;
			vertexDirections2 = VertexDirections;
		}

		public IMap<PointyHexPoint> Map2D
		{
			get { return map; }
			
		}

		public IEnumerable<Vector3> GetVertices(PointyHexPoint point)
		{
			return vertexDirections2.Select(v => v
				.HadamardMul(PreScale)
				.HadamardMul(cellDimensions.To3DXY()) + map[point].To3DXY());
		}

		public IEnumerable<Vector2> GetUVs(PointyHexPoint point)
		{
			return vertexDirections2.Select(v => new Vector2(v.x, v.y) + Vector2.one*0.5f)
				;
		}

		public IEnumerable<int> GetTriangles(PointyHexPoint point, int vertexIndex)
		{
			return triangles2.Select(t => t + vertexDirections2.Length*vertexIndex);
		}
	}

	/// <summary>
	/// Map for pointy brick mesh grids.
	/// </summary>
	[Experimental]
	[Version(1, 14)]
	public class PointyBrickMeshMap : IMeshMap<PointyHexPoint>
	{
		private readonly Vector2 cellDimensions;

		private static readonly Vector3[] VertexDirections =
		{
			new Vector3(-1/2f, -1/2f, 0),
			new Vector3(-1/2f, 1/2f, 0),
			new Vector3(1/2f, 1/2f, 0),
			new Vector3(1/2f, -1/2f, 0)
		};

		private static readonly int[] Triangles =
		{
			0, 1, 2,
			2, 3, 0
		};

		public PointyBrickMeshMap(Vector2 cellDimensions)
		{
			this.cellDimensions = cellDimensions;
		}

		public IMap<PointyHexPoint> Map2D { get; private set; }

		public IEnumerable<Vector3> GetVertices(PointyHexPoint point)
		{
			return VertexDirections.Select(v => v.HadamardMul(cellDimensions.To3DXY()));
		}

		public IEnumerable<Vector2> GetUVs(PointyHexPoint point)
		{
			return VertexDirections.Select(v => new Vector2(v.x, v.y) + Vector2.one*0.5f);
		}

		public IEnumerable<int> GetTriangles(PointyHexPoint point, int vertexIndex)
		{
			return Triangles.Select(t => t + 4*vertexIndex);
		}
	}

	/// <summary>
	/// Map for flat hex mesh grids.
	/// </summary>
	[Experimental]
	[Version(1, 14)]
	public class FlatHexMeshMap : IMeshMap<FlatHexPoint>
	{
		private static readonly Vector3 PreScale = new Vector3(1, 2 / GLMathf.Sqrt3, 1);

		private readonly Vector2 cellDimensions;

		private static readonly Vector3[] VertexDirections =
		{
			new Vector3(0, 1f/2).YXZ(),
			new Vector3(GLMathf.Sqrt3/4, 1f/4).YXZ(),
			new Vector3(GLMathf.Sqrt3/4, -1f/4).YXZ(),
			new Vector3(0, -1f/2).YXZ(),
			new Vector3(-GLMathf.Sqrt3/4, -1f/4).YXZ(),
			new Vector3(-GLMathf.Sqrt3/4, 1f/4).YXZ(),
			Vector3.zero
		};
		
		private static readonly int[] Triangles =
		{
			6, 1, 0,
			6, 2, 1,
			6, 3, 2,
			6, 4, 3,
			6, 5, 4,
			6, 0, 5
		};

		public FlatHexMeshMap(Vector2 cellDimensions)
		{
			this.cellDimensions = cellDimensions;
		}


		public IMap<FlatHexPoint> Map2D { get; private set; }

		public IEnumerable<Vector3> GetVertices(FlatHexPoint point)
		{
			return VertexDirections.Select(v => v.
				HadamardMul(PreScale).
				HadamardMul(cellDimensions.To3DXY()));
		}

		public IEnumerable<Vector2> GetUVs(FlatHexPoint point)
		{
			return VertexDirections.Select(v => new Vector2(v.x, v.y) + Vector2.one*0.5f);
		}

		public IEnumerable<int> GetTriangles(FlatHexPoint point, int vertexIndex)
		{
			return Triangles.Select(t => t + 7*vertexIndex);
		}
	}

	/// <summary>
	/// Map for flat brick mesh grids.
	/// </summary>
	[Experimental]
	[Version(1, 14)]
	public class FlatBrickMeshMap : IMeshMap<FlatHexPoint>
	{
		private readonly Vector2 cellDimensions;

		private static readonly Vector3[] VertexDirections =
		{
			new Vector3(-1/2f, -1/2f, 0).YXZ(),
			new Vector3(-1/2f, 1/2f, 0).YXZ(),
			new Vector3(1/2f, 1/2f, 0).YXZ(),
			new Vector3(1/2f, -1/2f, 0).YXZ()
		};

		private static readonly int[] Triangles =
		{
			0, 1, 2,
			2, 3, 0
		};

		public FlatBrickMeshMap(Vector2 cellDimensions)
		{
			this.cellDimensions = cellDimensions;
		}

		public IMap<FlatHexPoint> Map2D { get; private set; }

		public IEnumerable<Vector3> GetVertices(FlatHexPoint point)
		{
			return VertexDirections.Select(v => v.HadamardMul(cellDimensions.To3DXY()));
		}

		public IEnumerable<Vector2> GetUVs(FlatHexPoint point)
		{
			return VertexDirections.Select(v => new Vector2(v.x, v.y) + Vector2.one*0.5f);
		}

		public IEnumerable<int> GetTriangles(FlatHexPoint point, int vertexIndex)
		{
			return Triangles.Select(t => t + 4*vertexIndex);
		}
	}

	/// <summary>
	/// Map for rect mesh grids.
	/// </summary>
	[Experimental]
	[Version(1, 14)]
	public class RectMeshMap : IMeshMap<RectPoint>
	{
		private readonly Vector2 cellDimensions;

		private static readonly Vector3[] VertexDirections =
		{
			new Vector3(-1/2f, -1/2f, 0),
			new Vector3(-1/2f, 1/2f, 0),
			new Vector3(1/2f, 1/2f, 0),
			new Vector3(1/2f, -1/2f, 0)
		};

		private static readonly int[] Triangles =
		{
			0, 1, 2,
			2, 3, 0
		};

		private RectMap map;

		public RectMeshMap(Vector2 cellDimensions, RectMap map)
		{
			this.cellDimensions = cellDimensions;
			this.map = map;
		}

		public IMap<RectPoint> Map2D { get; private set; }

		public IEnumerable<Vector3> GetVertices(RectPoint point)
		{
			/*return VertexDirections
				.Select(v=>
				        v.HadamardMul(PreScale) v.HadamardMul(cellDimensions.To3DXY()));*/

			return VertexDirections.Select(v => v
                .HadamardMul(cellDimensions.To3DXY()) + map[point].To3DXY());
		}

		public IEnumerable<Vector2> GetUVs(RectPoint point)
		{
			return VertexDirections.Select(v => new Vector2(v.x, v.y) + Vector2.one*0.5f);
		}

		public IEnumerable<int> GetTriangles(RectPoint point, int vertexIndex)
		{
			return Triangles.Select(t => t + 4*vertexIndex);
		}
	}

	/// <summary>
	/// Map for diamond mesh grids.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids.IMeshMap{Gamelogic.Grids.DiamondPoint}" />
	[Experimental]
	[Version(1, 14)]
	public class DiamondMeshMap : IMeshMap<DiamondPoint>
	{
		private readonly Vector2 cellDimensions;

		private static readonly Vector3[] VertexDirections =
		{
			new Vector3(-1/2f, 0, 0),
			new Vector3(0, 1/2f, 0),
			new Vector3(1/2f, 0, 0),
			new Vector3(0, -1/2f, 0)
		};

		private static readonly int[] Triangles =
		{
			0, 1, 2,
			2, 3, 0
		};

		public DiamondMeshMap(Vector2 cellDimensions)
		{
			this.cellDimensions = cellDimensions;
		}

		public IMap<DiamondPoint> Map2D { get; private set; }

		public IEnumerable<Vector3> GetVertices(DiamondPoint point)
		{
			return VertexDirections.Select(v => v.HadamardMul(cellDimensions.To3DXY()));
		}

		public IEnumerable<Vector2> GetUVs(DiamondPoint point)
		{
			return VertexDirections.Select(v => new Vector2(v.x, v.y) + Vector2.one*0.5f);
		}

		public IEnumerable<int> GetTriangles(DiamondPoint point, int vertexIndex)
		{
			return Triangles.Select(t => t + 4*vertexIndex);
		}
	}
}