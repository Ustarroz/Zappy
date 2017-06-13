using System;
using System.Collections.Generic;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// This class provides methods that
	/// can be used to construct a mesh for a grid. Two common cases are provided:
	///		<see cref="UniformGridMeshData"/> (for grids where all cells are identical, such as rect and hex grids)
	/// and <see cref="PeriodicGridMeshData"/> (for grids where patches of the grid repeat periodically, such as triangular grids)
	/// </summary>
	/// <seealso cref="ScriptableObject" />
	[Serializable, Abstract]
	public class MeshData : ScriptableObject
	{
		[Abstract]
		public virtual IEnumerable<Vector3> GetVertices(IExplicitShape<int> shape, GridMap<int> map)
		{
			throw new NotSupportedException();
		}

		[Abstract]
		public virtual IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint2> shape, GridMap<GridPoint2> map)
		{
			throw new NotSupportedException();
		}

		[Abstract]
		public virtual IEnumerable<Vector3> GetVertices(IExplicitShape<GridPoint3> shape, GridMap<GridPoint3> map)
		{
			throw new NotSupportedException();
		}

		[Abstract]
		public virtual IEnumerable<int> GetTriangles(IExplicitShape<int> explicitShape, bool flip, bool b)
		{
			throw new NotSupportedException();
		}
		[Abstract]
		public virtual IEnumerable<int> GetTriangles(IExplicitShape<GridPoint2> explicitShape, bool flip, bool b)
		{
			throw new NotSupportedException();
		}
		[Abstract]
		public virtual IEnumerable<int> GetTriangles(IExplicitShape<GridPoint3> explicitShape, bool flip, bool b)
		{
			throw new NotSupportedException();
		}
		[Abstract]
		public virtual IEnumerable<Vector2> GetUVs(IExplicitShape<int> shape)
		{
			throw new NotSupportedException();
		}

		[Abstract]
		public virtual IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint2> shape)
		{
			throw new NotSupportedException();
		}
		[Abstract]
		public virtual IEnumerable<Vector2> GetUVs(IExplicitShape<GridPoint3> shape)
		{
			throw new NotSupportedException();
		}

		[Abstract]
		public virtual IEnumerable<Vector3> GetNormals(IExplicitShape<int> explicitShape, GridMap<int> gridMap, bool flip)
		{
			throw new NotSupportedException();
		}

		[Abstract]
		public virtual IEnumerable<Vector3> GetNormals(IExplicitShape<GridPoint2> explicitShape, GridMap<GridPoint2> gridMap, bool flip)
		{
			throw new NotSupportedException();
		}

		[Abstract]
		public virtual IEnumerable<Vector3> GetNormals(IExplicitShape<GridPoint3> explicitShape, GridMap<GridPoint3> gridMap, bool flip)
		{
			throw new NotSupportedException();
		}
	}
}