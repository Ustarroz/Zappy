using System;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that generates a hex rectangle shape.
	/// </summary>
	[ShapeNode("Hex/Hex Rectangle", 2)]
	[Version(2, 2)]
	public class HexRectangleShapeNode : PrimitiveShapeNode<GridPoint2>
	{
		/// <summary>
		/// The type of rectangle.
		/// </summary>
		public enum RectangleType
		{
			/// <summary>
			/// Represents the default rectangle shape. All rows and columns have the same number of rows.
			/// </summary>
			Normal,

			/// <summary>
			/// Represents a fat rectangle - alternate rows or columns have one cell more than the rest.
			/// </summary>
			Fat,

			/// <summary>
			/// Represents a thin rectangle - alternative rows or columns have one cell fewer than the rest.
			/// </summary>
			Thin
		}

		/// <summary>
		/// Enum HexType
		/// </summary>
		public enum HexType
		{
			Pointy,
			Flat
		}

		/// <summary>
		/// The type of rectangle for this node.
		/// </summary>
		public RectangleType type = RectangleType.Fat;

		/// <summary>
		/// The hex grid type for this node.
		/// </summary>
		public HexType hexType = HexType.Pointy;

		public InspectableGridPoint2 dimensions = new InspectableGridPoint2 { x = 0, y = 0 };

		protected override IExplicitShape<GridPoint2> Generate()
		{
			var size = dimensions.GetGridPoint();

			switch (hexType)
			{
				case HexType.Pointy:
					switch (type)
					{
						case RectangleType.Normal:
							return ExplicitShape.Hex.PointyRectangle(size);
						case RectangleType.Fat:
							return ExplicitShape.Hex.PointyFatRectangle(size);
						case RectangleType.Thin:
							return ExplicitShape.Hex.PointyThinRectangle(size);
						default:
							throw new ArgumentOutOfRangeException();
					}
				case HexType.Flat:
					switch (type)
					{
						case RectangleType.Normal:
							return ExplicitShape.Hex.FlatRectangle(size);
						case RectangleType.Fat:
							return ExplicitShape.Hex.FlatFatRectangle(size);
						case RectangleType.Thin:
							return ExplicitShape.Hex.FlatThinRectangle(size);
						default:
							throw new ArgumentOutOfRangeException();
					}
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}