using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// A grid map is used to convert from grid space to world space.
	/// 
	/// The map is composed from two separate maps. The space map takes a point in grid space,
	/// and positions it in world space. The points from grid space need not be discrete. For example,
	/// in a rect grid, the point (0.5, 0.5) represents the top right vertex of the cell at (0,0).
	/// 
	/// The round map makes continuous grid points discrete. I a rect grid, for example, the point (0.3, 0.3)
	/// is rounded to (0, 0).
	/// </summary>
	/// <typeparam name="TDiscreteType">The type of the t discrete type.</typeparam>
	/// <seealso cref="Gamelogic.Grids2.IGridMap{TDiscreteType, Vector3, Vector3}" />
	public class GridMap<TDiscreteType> : IGridMap<TDiscreteType, Vector3, Vector3>
	{
		private readonly IMap<Vector3, Vector3> gridToWorldSpaceMap;
		private readonly IMap<Vector3, TDiscreteType> roundMap;

		public GridMap(
			IMap<Vector3, Vector3> gridToWorldSpaceMap,
			IMap<Vector3, TDiscreteType> roundMap)
		{
			this.gridToWorldSpaceMap = gridToWorldSpaceMap;
			this.roundMap = roundMap;
		}

		public IMap<Vector3, Vector3> SpaceMap
		{
			get { return gridToWorldSpaceMap; }
		}

		public IMap<Vector3, TDiscreteType> RoundMap
		{
			get { return roundMap; }
		}

		public Vector3 GridToWorld(TDiscreteType input)
		{
			return GridToWorld(DeRound(input));
		}

		public Vector3 GridToWorld(Vector3 input)
		{
			return gridToWorldSpaceMap.Forward(input);
		}

		public TDiscreteType WorldToGridToDiscrete(Vector3 output)
		{
			return roundMap.Forward(WorldToGrid(output));
		}

		public Vector3 WorldToGrid(Vector3 output)
		{
			return gridToWorldSpaceMap.Reverse(output);
		}

		public Vector3 DeRound(TDiscreteType point)
		{
			return roundMap.Reverse(point);
		}

		public TDiscreteType Round(Vector3 point)
		{
			return roundMap.Forward(point);
		}
	}
}