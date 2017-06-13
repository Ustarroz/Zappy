using System;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Grid points are immutable, and cannot be used in the inspector.
	/// This class can be used instead.
	/// </summary>
	[Serializable]
	public struct InspectableGridPoint3
	{
		/// <summary>
		/// The x coordinate of this InspectableGridPoint2.
		/// </summary>
		public int x;

		/// <summary>
		/// The y coordinate of this InspectableGridPoint2.
		/// </summary>
		public int y;

		/// <summary>
		/// The z coordinate of this InspectableGridPoint2.
		/// </summary>
		public int z;

		public GridPoint3 GetGridPoint()
		{
			return new GridPoint3(x, y, z);
		}
	}
}