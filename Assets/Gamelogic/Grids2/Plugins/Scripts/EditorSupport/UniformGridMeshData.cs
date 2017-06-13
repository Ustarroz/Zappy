using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Used to specify mesh data for uniform grids.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.MeshData" />
	/// <remarks>Uniform grids use the same cell data for each cell.</remarks>
	[CreateAssetMenu(fileName = "MeshData", menuName = "Grids/MeshData/Uniform")]
	[Serializable]
	public class UniformGridMeshData : MeshData
	{
		private bool flipped;
		private const float Half = 0.5f;

		public Vector3[] vertices =
		{
			new Vector3(-Half, -Half, 0),
			new Vector3(-Half, Half, 0),
			new Vector3(Half, Half, 0),
			new Vector3(Half, -Half, 0),
		};

		public int[] triangles =
		{
			0, 3, 1,
			3, 2, 1
		};

		public Vector2[] uvs =
		{
			new Vector2(0, 0),
			new Vector2(0, 1),
			new Vector3(1, 1),
			new Vector3(1, 0),
		};

		public Vector3[] normals =
		{
			Vector3.back,
			Vector3.back,
			Vector3.back,
			Vector3.back
		};

		public override IEnumerable<Vector3> GetVertices(IExplicitShape<int> shape, GridMap<int> map)
		{
			return shape.Points.SelectMany(p => vertices.Select(q => map.GridToWorld(q + map.DeRound(p))));
		}

		public override IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint2> shape, GridMap<GridPoint2> map)
		{
			return shape.Points.SelectMany(p => vertices.Select(q => map.GridToWorld(q + map.DeRound(p))));
		}

		public override IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint3> shape, GridMap<GridPoint3> map)
		{
			return shape.Points.SelectMany(p => vertices.Select(q => map.GridToWorld(q + map.DeRound(p))));
		}

		//TODO doubleSided
		public override IEnumerable<int> GetTriangles(IExplicitShape<int> shape, bool doubleSided, bool flip)
		{
			if (doubleSided) return null;

			if (flip) return GetTrianglesFlipped(shape);

			return GetTrianglesNormal(shape);

		}

		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<int> shape)
		{
			return shape.Points.SelectMany((p, i) => triangles.Select(t => t + i * vertices.Length));
		}

		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<int> shape)
		{
			var flippedTriangles = new int[triangles.Length];

			for (int i = 0; i < triangles.Length; i += 3)
			{
				flippedTriangles[i] = triangles[i + 1];
				flippedTriangles[i + 1] = triangles[i];
				flippedTriangles[i + 2] = triangles[i + 2];
			}

			return shape.Points.SelectMany((p, i) => flippedTriangles.Select(t => t + i * vertices.Length));
		}

		public override IEnumerable<int> GetTriangles(IExplicitShape<GridPoint2> shape, bool doubleSided, bool flip)
		{
			if (doubleSided) return null;

			if (flip) return GetTrianglesFlipped(shape);

			return GetTrianglesNormal(shape);

		}

		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<GridPoint2> shape)
		{
			return shape.Points.SelectMany((p, i) => triangles.Select(t => t + i * vertices.Length));
		}

		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<GridPoint2> shape)
		{
			var flippedTriangles = new int[triangles.Length];

			for (int i = 0; i < triangles.Length; i += 3)
			{
				flippedTriangles[i] = triangles[i + 1];
				flippedTriangles[i + 1] = triangles[i];
				flippedTriangles[i + 2] = triangles[i + 2];
			}

			return shape.Points.SelectMany((p, i) => flippedTriangles.Select(t => t + i * vertices.Length));
		}
		public override IEnumerable<int> GetTriangles(IExplicitShape<GridPoint3> shape, bool doubleSided, bool flip)
		{
			if (doubleSided) return null;

			if (flip) return GetTrianglesFlipped(shape);

			return GetTrianglesNormal(shape);

		}

		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<GridPoint3> shape)
		{
			return shape.Points.SelectMany((p, i) => triangles.Select(t => t + i * vertices.Length));
		}

		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<GridPoint3> shape)
		{
			var flippedTriangles = new int[triangles.Length];

			for (int i = 0; i < triangles.Length; i += 3)
			{
				flippedTriangles[i] = triangles[i + 1];
				flippedTriangles[i + 1] = triangles[i];
				flippedTriangles[i + 2] = triangles[i + 2];
			}

			return shape.Points.SelectMany((p, i) => flippedTriangles.Select(t => t + i * vertices.Length));
		}

		public override IEnumerable<Vector2> GetUVs(IExplicitShape<int> shape)
		{
			return shape.Points.SelectMany(p => uvs);
		}

		public override IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint2> shape)
		{
			return shape.Points.SelectMany(p => uvs);
		}

		public override IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint3> shape)
		{
			return shape.Points.SelectMany(p => uvs);
		}

		public override IEnumerable<Vector3> GetNormals(IExplicitShape<int> shape, GridMap<int> map, bool flip)
		{
			return shape.Points.SelectMany(p => vertices.Select((q, i) => map.GridToWorld(q + map.DeRound(p) + (flip ? -normals[i] : normals[i])).normalized));
		}

		public override IEnumerable<Vector3> GetNormals(IExplicitShape<GridPoint2> explicitShape, GridMap<GridPoint2> gridMap, bool flip)
		{
			return explicitShape.Points.SelectMany(p => vertices.Select((q, i) => gridMap.GridToWorld(q + gridMap.DeRound(p) + (flip ? -normals[i] : normals[i])).normalized));
		}

		public override IEnumerable<Vector3> GetNormals(IExplicitShape<GridPoint3> explicitShape, GridMap<GridPoint3> gridMap, bool flip)
		{
			return explicitShape.Points.SelectMany(p => vertices.Select((q, i) => gridMap.GridToWorld(q + gridMap.DeRound(p) + (flip ? -normals[i] : normals[i])).normalized));
		}
	}
}