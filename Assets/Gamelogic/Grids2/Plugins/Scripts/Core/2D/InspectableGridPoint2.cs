using System;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Grid points are immutable, and cannot be used in the inspector.
	/// This class can be used instead.
	/// </summary>
	[Serializable]
	public struct InspectableGridPoint2
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
		/// Gets the grid point that this inspectable grid point represents.
		/// </summary>
		/// <returns>GridPoint2.</returns>
		public GridPoint2 GetGridPoint()
		{
			return new GridPoint2(x, y);
		}
	}
}