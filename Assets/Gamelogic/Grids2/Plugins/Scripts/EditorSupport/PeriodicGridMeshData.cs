using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Used to specify mesh data for periodic grids.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.MeshData" />
	/// <remarks>Periodic grids use the same grid data for cells with the same color. The color is determined
	/// by the specified coloring.</remarks>
	[CreateAssetMenu(fileName = "MeshData", menuName = "Grids/MeshData/Periodic")]
	[Serializable]
	public class PeriodicGridMeshData : MeshData
	{
		public ColorFunction colorFunction;

		private bool flipped;
		private const float Half = 0.5f;

		public List<List<int>> test1;

		public Vector3Array[] vertices =
		{
			new Vector3Array
			{
				//Use this type of initialization because
				//collection initializer does not work in inspector
				data = new List<Vector3>
				{
					new Vector3(-Half, -Half, 0),
					new Vector3(-Half, Half, 0),
					new Vector3(Half, Half, 0),
					new Vector3(Half, -Half, 0),
				}
			}
		};

		public IntArray[] triangles =
		{
			new IntArray
			{
				data = new List<int>
				{
					0, 3, 1,
					3, 2, 1
				}
			}
		};

		public Vector2Array[] uvs =
		{
			new Vector2Array()
			{
				data = new List<Vector2>
				{
					new Vector2(0, 0),
					new Vector2(0, 1),
					new Vector3(1, 1),
					new Vector3(1, 0),
				}
			}
		};

		public Vector3Array[] normals =
		{
			new Vector3Array
			{
				data = new List<Vector3>
				{
					Vector3.back,
					Vector3.back,
					Vector3.back,
					Vector3.back
				}
			}
		};

		public override IEnumerable<Vector3> GetVertices(IExplicitShape<int> shape, GridMap<int> map)
		{
			return shape
				.Points
				.SelectMany(p => vertices[GLMathf.FloorMod((int)p, colorFunction.x0)]
					.Select(q => map.GridToWorld(q + map.DeRound(p))));
		}

		public override IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint2> shape, GridMap<GridPoint2> map)
		{
			return shape
				.Points
				.SelectMany(p => vertices[p.GetColor(colorFunction)]
					.Select(q => map.GridToWorld(q + map.DeRound(p))));
		}

		public override IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint3> shape, GridMap<GridPoint3> map)
		{
			return shape
				.Points
				.SelectMany(p => vertices[p.GetColor(colorFunction)]
					.Select(q => map.GridToWorld(q + map.DeRound(p))));
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
			var points = shape.Points.ToArray();
			var triangleOffsets = new int[shape.Points.Count()];

			triangleOffsets[0] = 0;

			for (int i = 1; i < triangleOffsets.Length; i++)
			{
				triangleOffsets[i] = triangleOffsets[i - 1] + vertices[GLMathf.FloorMod(points[i - 1], colorFunction.x0)].Count;
			}

			return shape
				.Points
				.SelectMany((p, i) => triangles[GLMathf.FloorMod(p, colorFunction.x0)]
				.Select(t => t + triangleOffsets[i]));
		}

		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<int> shape)
		{
			var flippedTriangles = new IntArray[triangles.Length];

			for (int j = 0; j < triangles.Length; j++)
			{
				flippedTriangles[j] = new IntArray()
				{
					data = triangles[j].ToList()
				};

				for (int i = 0; i < triangles[j].Count; i += 3)
				{
					flippedTriangles[j][i] = triangles[j][i + 1];
					flippedTriangles[j][i + 1] = triangles[j][i];
					flippedTriangles[j][i + 2] = triangles[j][i + 2];
				}
			}

			return shape
				.Points
				.SelectMany((p, i) => flippedTriangles[GLMathf.FloorMod(p, colorFunction.x0)]
					.Select(t => t + i * vertices.Length));
		}

		public override IEnumerable<int> GetTriangles(IExplicitShape<GridPoint2> shape, bool doubleSided, bool flip)
		{
			if (doubleSided) return null;

			if (flip) return GetTrianglesFlipped(shape);

			return GetTrianglesNormal(shape);

		}

		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<GridPoint2> shape)
		{
			var points = shape.Points.ToArray();
			var triangleOffsets = new int[shape.Points.Count()];

			triangleOffsets[0] = 0;

			for (int i = 1; i < triangleOffsets.Length; i++)
			{
				triangleOffsets[i] = triangleOffsets[i - 1] + vertices[points[i - 1].GetColor(colorFunction)].Count;
			}

			return shape
				.Points
				.SelectMany((p, i) => triangles[p.GetColor(colorFunction)]
				.Select(t => t + triangleOffsets[i]));
		}

		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<GridPoint2> shape)
		{
			var flippedTriangles = new IntArray[triangles.Length];

			for (int j = 0; j < triangles.Length; j++)
			{
				flippedTriangles[j] = new IntArray()
				{
					data = triangles[j].ToList()
				};

				for (int i = 0; i < triangles[j].Count; i += 3)
				{
					flippedTriangles[j][i] = triangles[j][i + 1];
					flippedTriangles[j][i + 1] = triangles[j][i];
					flippedTriangles[j][i + 2] = triangles[j][i + 2];
				}
			}

			return shape
				.Points
				.SelectMany((p, i) => flippedTriangles[p.GetColor(colorFunction)]
					.Select(t => t + i * vertices.Length));
		}
		public override IEnumerable<int> GetTriangles(IExplicitShape<GridPoint3> shape, bool doubleSided, bool flip)
		{
			if (doubleSided) return null;

			if (flip) return GetTrianglesFlipped(shape);

			return GetTrianglesNormal(shape);

		}

		private IEnumerable<int> GetTrianglesNormal(IExplicitShape<GridPoint3> shape)
		{
			var points = shape.Points.ToArray();
			var triangleOffsets = new int[shape.Points.Count()];

			triangleOffsets[0] = 0;

			for (int i = 1; i < triangleOffsets.Length; i++)
			{
				triangleOffsets[i] = triangleOffsets[i - 1] + vertices[points[i - 1].GetColor(colorFunction)].Count;
			}

			return shape
				.Points
				.SelectMany((p, i) => triangles[p.GetColor(colorFunction)]
				.Select(t => t + triangleOffsets[i]));
		}

		private IEnumerable<int> GetTrianglesFlipped(IExplicitShape<GridPoint3> shape)
		{
			var flippedTriangles = new IntArray[triangles.Length];

			for (int j = 0; j < triangles.Length; j++)
			{
				flippedTriangles[j] = new IntArray()
				{
					data = triangles[j].ToList()
				};

				for (int i = 0; i < triangles[j].Count; i += 3)
				{
					flippedTriangles[j][i] = triangles[j][i + 1];
					flippedTriangles[j][i + 1] = triangles[j][i];
					flippedTriangles[j][i + 2] = triangles[j][i + 2];
				}
			}

			return shape
				.Points
				.SelectMany((p, i) => flippedTriangles[p.GetColor(colorFunction)]
					.Select(t => t + i * vertices.Length));
		}

		public override IEnumerable<Vector2> GetUVs(IExplicitShape<int> shape)
		{
			return shape.Points.SelectMany(p => uvs[GLMathf.FloorMod(p, colorFunction.x0)]);
		}

		public override IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint2> shape)
		{
			return shape.Points.SelectMany(p => uvs[p.GetColor(colorFunction)]);
		}

		public override IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint3> shape)
		{
			return shape.Points.SelectMany(p => uvs[p.GetColor(colorFunction)]);
		}

		public override IEnumerable<Vector3> GetNormals(IExplicitShape<int> shape, GridMap<int> map, bool flip)
		{
			return shape
				.Points
				.SelectMany(p => vertices[GLMathf.FloorMod(p, colorFunction.x0)]
					.Select((q, i) => map.GridToWorld(q + map.DeRound(p) + (flip ? -normals[GLMathf.FloorMod(p, colorFunction.x0)][i] : normals[GLMathf.FloorMod(p, colorFunction.x0)][i])).normalized));
		}

		public override IEnumerable<Vector3> GetNormals(IExplicitShape<GridPoint2> explicitShape, GridMap<GridPoint2> gridMap, bool flip)
		{
			return
				explicitShape
					.Points
					.SelectMany(p => vertices[p.GetColor(colorFunction)]
						.Select((q, i) => gridMap.GridToWorld(q + gridMap.DeRound(p) + (flip ? -normals[p.GetColor(colorFunction)][i] : normals[p.GetColor(colorFunction)][i])).normalized));
		}

		public override IEnumerable<Vector3> GetNormals(IExplicitShape<GridPoint3> explicitShape, GridMap<GridPoint3> gridMap, bool flip)
		{
			return explicitShape
				.Points
				.SelectMany(p => vertices[p.GetColor(colorFunction)]
					.Select((q, i) => gridMap.GridToWorld(q + gridMap.DeRound(p) + (flip ? -normals[p.GetColor(colorFunction)][i] : normals[p.GetColor(colorFunction)][i])).normalized));
		}
	}
}