using System;

namespace Gamelogic.Grids2
{

	/// <summary>
	/// The type of built-in grids.
	/// </summary>
	[Serializable]
	public enum GridType
	{
		/// <summary>
		/// Built-in 1D grid.
		/// </summary>
		Grid1,

		/// <summary>
		/// Built-in 2D grid.
		/// </summary>

		Grid2,
		/// <summary>
		/// Built-in 3D grid.
		/// </summary>
		Grid3
	}

	/// <summary>
	/// Contains extension methods for the GridType enum.
	/// </summary>
	public static class GridTypeExtensions
	{
		public static int GetDimension(this GridType gridType)
		{
			switch (gridType)
			{
				case GridType.Grid1:
					return 1;
				case GridType.Grid2:
					return 2;
				case GridType.Grid3:
					return 3;
				default:
					throw new NotSupportedException("This operation is not supported for " + gridType);
			}
		}
	}
}