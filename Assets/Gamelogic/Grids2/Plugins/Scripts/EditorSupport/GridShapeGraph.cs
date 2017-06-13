using Gamelogic.Extensions;
using System;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Class for selecting the grid type and setting an associated graph.
	/// </summary>
	/// <remarks>Only one of the graphs is used depending on the value of <c>gridType</c>.</remarks>
	[Serializable]
	public class GridShapeGraph
	{
		public GridType gridType;


		[WarningIfNull(
			"You need to set the space map graph.You can create a new shape graph from the " +
			"Assets/Create/Grids/ShapeGraph menu.")]
		public Shape1Graph shape1Graph;

		[WarningIfNull(
			"You need to set the space map graph.You can create a new shape graph from the " +
			"Assets/Create/Grids/ShapeGraph menu.")]
		public Shape2Graph shape2Graph;

		[WarningIfNull(
			"You need to set the space map graph.You can create a new shape graph from the " +
			"Assets/Create/Grids/ShapeGraph menu.")]
		public Shape3Graph shape3Graph;

		public bool IsSet
		{
			get
			{
				switch (gridType)
				{
					case GridType.Grid1:
						return shape1Graph != null;
					case GridType.Grid2:
						return shape2Graph != null;
					case GridType.Grid3:
						return shape3Graph != null;
					default:
						throw new NotSupportedException("Does not support " + gridType);
				}
			}
		}
	}
}