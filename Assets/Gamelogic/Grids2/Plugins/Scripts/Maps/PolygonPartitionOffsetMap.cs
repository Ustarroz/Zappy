namespace Gamelogic.Grids2
{
	/// <summary>
	/// A map that can effectively change the rounding of another map.
	/// 
	/// This map calculates the offset for a point from the cell center. It then
	/// checks the partition to see in which partition it falls. Each partition
	/// has a offset, this offset is added to the center grid point for the final
	/// grid point.
	/// </summary>
	/// <seealso cref="IMap{Vector3, GridPoint2}" />
	/*[Obsolete]
	public class PolygonPartitionOffsetMap : IMap<Vector3, GridPoint2>
	{
		private readonly IPointPartition2 partition;
		private readonly IMap<Vector3, GridPoint2> map;
		private readonly IList<GridPoint2> offsets;

		public PolygonPartitionOffsetMap(
			IMap<Vector3, GridPoint2> map, 
			IPointPartition2 partition, 
			IList<GridPoint2> offsets)
		{
			this.partition = partition;
			this.offsets = offsets;
			this.map = map;
		}

		public Vector3 Reverse(GridPoint2 output)
		{
			return map.Reverse(output);
		}

		public GridPoint2 Forward(Vector3 input)
		{
			var gridPointBase = map.Forward(input);
			var center = map.Reverse(gridPointBase);
			var offset = input - center;

			return offsets[partition.GetPartition(offset)] + gridPointBase;
		}
	}*/
}