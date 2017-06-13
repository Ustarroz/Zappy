//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

// Auto-generated File

using System;

namespace Gamelogic.Grids
{
	
	/// <summary>
	/// Class for making RectGrids in different shapes.
	/// Copyright Gamelogic.
	///	Author Herman Tulleken
	///	Since version 1.0
	/// See also AbstractOp
	/// </summary>
	public partial class RectOp<TCell> : AbstractOp<ShapeStorageInfo<RectPoint>>
	{
		public RectOp(){}

		public RectOp(
			ShapeStorageInfo<RectPoint> leftShapeInfo,
			Func<ShapeStorageInfo<RectPoint>, ShapeStorageInfo<RectPoint>, ShapeStorageInfo<RectPoint>> combineShapeInfo) :
			base(leftShapeInfo, combineShapeInfo)
		{}

		/// <summary>
		/// Use this function to create shapes to ensure they fit into memory.
		/// 
		/// The test function can test shapes anywhere in space.If you specify the bottom corner
		/// (in terms of the storage rectangle), the shape is automatically translated in memory
		/// to fit, assuming memory width and height is big enough.
		/// 
		/// Strategy for implementing new shapes:
		///		- First, determine the test function.
		///		- Next, draw a storage rectangle that contains the shape.
		///		- Determine the storgae rectangle width and height.
		///		- Finally, determine the grid-space coordinate of the left bottom corner of the storage rectangle.
		/// 
		/// Then define your function as follows:
		/// 
		/// <code>
		/// public RectShapeInfo&lt;TCell&gt; MyShape()
		/// {
		///		Shape(stargeRectangleWidth, storageRectangleHeight, isInsideMyShape, storageRectangleBottomleft);
		/// }
		/// </code>
		/// </summary>
		/// <param name="width">The widh of the storage rectangle</param>
		/// <param name="height">The height of the storage rectangle</param>
		/// <param name="isInside">A function that returns true if a passed point lies inside the shape being defined</param>
		/// <param name="bottomLeftCorner">The grid-space coordinate of the bottom left corner of the storage rect.</param>
		public RectShapeInfo<TCell> Shape(int width, int height, Func<RectPoint, bool> isInside, RectPoint bottomLeftCorner)
		{
			var shapeInfo = MakeShapeStorageInfo<RectPoint>(width, height, x=>isInside(x + bottomLeftCorner));
			return new RectShapeInfo<TCell>(shapeInfo).Translate(bottomLeftCorner);
		}

		/// <summary>
		/// The same as Shape with all parameters, but with bottomLeft Point set to  RectPoint.Zero.
		/// </summary>
		public RectShapeInfo<TCell> Shape(int width, int height, Func<RectPoint, bool> isInside)
		{
			return Shape(width, height, isInside, RectPoint.Zero);
		}

		/// <summary>
		/// Creates the grid in a shape that spans 
		///	the entire storage rectangle of the given width and height.
		/// </summary>
		[ShapeMethod]
		public RectShapeInfo<TCell> Default(int width, int height)
		{
			var rawInfow = MakeShapeStorageInfo<RectPoint>(
				width, 
				height,
				x => RectGrid<TCell>.DefaultContains(x, width, height));

			return new RectShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Makes a grid with a single cell that corresponds to the origin.
		/// </summary>
		[ShapeMethod]
		public RectShapeInfo<TCell> Single()
		{
			var rawInfow = MakeShapeStorageInfo<RectPoint>(
				1, 
				1,
				x => x == RectPoint.Zero);

			return new RectShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Starts a compound shape operation.
        ///
		///	Any shape that is defined in terms of other shape operations must use this method, and use Endgroup() to end the definition.
        ///<code>
		///		public static RectShapeInfo&lt;TCell&gt; MyCustomShape(this RectOp&lt;TCell&gt; op)
		///		{
		///			return 
		///				BeginGroup()
		///					.Shape1()
		///					.Union()
		///					.Shape2()
		///				.EndGroup(op);
		///		}
        /// </code>
		///	Since version 1.1
		/// </summary>
		public RectOp<TCell> BeginGroup()
		{
			return RectGrid<TCell>.BeginShape();
		}
	}
	
	/// <summary>
	/// Class for making DiamondGrids in different shapes.
	/// Copyright Gamelogic.
	///	Author Herman Tulleken
	///	Since version 1.0
	/// See also AbstractOp
	/// </summary>
	public partial class DiamondOp<TCell> : AbstractOp<ShapeStorageInfo<DiamondPoint>>
	{
		public DiamondOp(){}

		public DiamondOp(
			ShapeStorageInfo<DiamondPoint> leftShapeInfo,
			Func<ShapeStorageInfo<DiamondPoint>, ShapeStorageInfo<DiamondPoint>, ShapeStorageInfo<DiamondPoint>> combineShapeInfo) :
			base(leftShapeInfo, combineShapeInfo)
		{}

		/// <summary>
		/// Use this function to create shapes to ensure they fit into memory.
		/// 
		/// The test function can test shapes anywhere in space.If you specify the bottom corner
		/// (in terms of the storage rectangle), the shape is automatically translated in memory
		/// to fit, assuming memory width and height is big enough.
		/// 
		/// Strategy for implementing new shapes:
		///		- First, determine the test function.
		///		- Next, draw a storage rectangle that contains the shape.
		///		- Determine the storgae rectangle width and height.
		///		- Finally, determine the grid-space coordinate of the left bottom corner of the storage rectangle.
		/// 
		/// Then define your function as follows:
		/// 
		/// <code>
		/// public DiamondShapeInfo&lt;TCell&gt; MyShape()
		/// {
		///		Shape(stargeRectangleWidth, storageRectangleHeight, isInsideMyShape, storageRectangleBottomleft);
		/// }
		/// </code>
		/// </summary>
		/// <param name="width">The widh of the storage rectangle</param>
		/// <param name="height">The height of the storage rectangle</param>
		/// <param name="isInside">A function that returns true if a passed point lies inside the shape being defined</param>
		/// <param name="bottomLeftCorner">The grid-space coordinate of the bottom left corner of the storage rect.</param>
		public DiamondShapeInfo<TCell> Shape(int width, int height, Func<DiamondPoint, bool> isInside, DiamondPoint bottomLeftCorner)
		{
			var shapeInfo = MakeShapeStorageInfo<DiamondPoint>(width, height, x=>isInside(x + bottomLeftCorner));
			return new DiamondShapeInfo<TCell>(shapeInfo).Translate(bottomLeftCorner);
		}

		/// <summary>
		/// The same as Shape with all parameters, but with bottomLeft Point set to  DiamondPoint.Zero.
		/// </summary>
		public DiamondShapeInfo<TCell> Shape(int width, int height, Func<DiamondPoint, bool> isInside)
		{
			return Shape(width, height, isInside, DiamondPoint.Zero);
		}

		/// <summary>
		/// Creates the grid in a shape that spans 
		///	the entire storage rectangle of the given width and height.
		/// </summary>
		[ShapeMethod]
		public DiamondShapeInfo<TCell> Default(int width, int height)
		{
			var rawInfow = MakeShapeStorageInfo<DiamondPoint>(
				width, 
				height,
				x => DiamondGrid<TCell>.DefaultContains(x, width, height));

			return new DiamondShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Makes a grid with a single cell that corresponds to the origin.
		/// </summary>
		[ShapeMethod]
		public DiamondShapeInfo<TCell> Single()
		{
			var rawInfow = MakeShapeStorageInfo<DiamondPoint>(
				1, 
				1,
				x => x == DiamondPoint.Zero);

			return new DiamondShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Starts a compound shape operation.
        ///
		///	Any shape that is defined in terms of other shape operations must use this method, and use Endgroup() to end the definition.
        ///<code>
		///		public static DiamondShapeInfo&lt;TCell&gt; MyCustomShape(this DiamondOp&lt;TCell&gt; op)
		///		{
		///			return 
		///				BeginGroup()
		///					.Shape1()
		///					.Union()
		///					.Shape2()
		///				.EndGroup(op);
		///		}
        /// </code>
		///	Since version 1.1
		/// </summary>
		public DiamondOp<TCell> BeginGroup()
		{
			return DiamondGrid<TCell>.BeginShape();
		}
	}
	
	/// <summary>
	/// Class for making PointyHexGrids in different shapes.
	/// Copyright Gamelogic.
	///	Author Herman Tulleken
	///	Since version 1.0
	/// See also AbstractOp
	/// </summary>
	public partial class PointyHexOp<TCell> : AbstractOp<ShapeStorageInfo<PointyHexPoint>>
	{
		public PointyHexOp(){}

		public PointyHexOp(
			ShapeStorageInfo<PointyHexPoint> leftShapeInfo,
			Func<ShapeStorageInfo<PointyHexPoint>, ShapeStorageInfo<PointyHexPoint>, ShapeStorageInfo<PointyHexPoint>> combineShapeInfo) :
			base(leftShapeInfo, combineShapeInfo)
		{}

		/// <summary>
		/// Use this function to create shapes to ensure they fit into memory.
		/// 
		/// The test function can test shapes anywhere in space.If you specify the bottom corner
		/// (in terms of the storage rectangle), the shape is automatically translated in memory
		/// to fit, assuming memory width and height is big enough.
		/// 
		/// Strategy for implementing new shapes:
		///		- First, determine the test function.
		///		- Next, draw a storage rectangle that contains the shape.
		///		- Determine the storgae rectangle width and height.
		///		- Finally, determine the grid-space coordinate of the left bottom corner of the storage rectangle.
		/// 
		/// Then define your function as follows:
		/// 
		/// <code>
		/// public PointyHexShapeInfo&lt;TCell&gt; MyShape()
		/// {
		///		Shape(stargeRectangleWidth, storageRectangleHeight, isInsideMyShape, storageRectangleBottomleft);
		/// }
		/// </code>
		/// </summary>
		/// <param name="width">The widh of the storage rectangle</param>
		/// <param name="height">The height of the storage rectangle</param>
		/// <param name="isInside">A function that returns true if a passed point lies inside the shape being defined</param>
		/// <param name="bottomLeftCorner">The grid-space coordinate of the bottom left corner of the storage rect.</param>
		public PointyHexShapeInfo<TCell> Shape(int width, int height, Func<PointyHexPoint, bool> isInside, PointyHexPoint bottomLeftCorner)
		{
			var shapeInfo = MakeShapeStorageInfo<PointyHexPoint>(width, height, x=>isInside(x + bottomLeftCorner));
			return new PointyHexShapeInfo<TCell>(shapeInfo).Translate(bottomLeftCorner);
		}

		/// <summary>
		/// The same as Shape with all parameters, but with bottomLeft Point set to  PointyHexPoint.Zero.
		/// </summary>
		public PointyHexShapeInfo<TCell> Shape(int width, int height, Func<PointyHexPoint, bool> isInside)
		{
			return Shape(width, height, isInside, PointyHexPoint.Zero);
		}

		/// <summary>
		/// Creates the grid in a shape that spans 
		///	the entire storage rectangle of the given width and height.
		/// </summary>
		[ShapeMethod]
		public PointyHexShapeInfo<TCell> Default(int width, int height)
		{
			var rawInfow = MakeShapeStorageInfo<PointyHexPoint>(
				width, 
				height,
				x => PointyHexGrid<TCell>.DefaultContains(x, width, height));

			return new PointyHexShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Makes a grid with a single cell that corresponds to the origin.
		/// </summary>
		[ShapeMethod]
		public PointyHexShapeInfo<TCell> Single()
		{
			var rawInfow = MakeShapeStorageInfo<PointyHexPoint>(
				1, 
				1,
				x => x == PointyHexPoint.Zero);

			return new PointyHexShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Starts a compound shape operation.
        ///
		///	Any shape that is defined in terms of other shape operations must use this method, and use Endgroup() to end the definition.
        ///<code>
		///		public static PointyHexShapeInfo&lt;TCell&gt; MyCustomShape(this PointyHexOp&lt;TCell&gt; op)
		///		{
		///			return 
		///				BeginGroup()
		///					.Shape1()
		///					.Union()
		///					.Shape2()
		///				.EndGroup(op);
		///		}
        /// </code>
		///	Since version 1.1
		/// </summary>
		public PointyHexOp<TCell> BeginGroup()
		{
			return PointyHexGrid<TCell>.BeginShape();
		}
	}
	
	/// <summary>
	/// Class for making FlatHexGrids in different shapes.
	/// Copyright Gamelogic.
	///	Author Herman Tulleken
	///	Since version 1.0
	/// See also AbstractOp
	/// </summary>
	public partial class FlatHexOp<TCell> : AbstractOp<ShapeStorageInfo<FlatHexPoint>>
	{
		public FlatHexOp(){}

		public FlatHexOp(
			ShapeStorageInfo<FlatHexPoint> leftShapeInfo,
			Func<ShapeStorageInfo<FlatHexPoint>, ShapeStorageInfo<FlatHexPoint>, ShapeStorageInfo<FlatHexPoint>> combineShapeInfo) :
			base(leftShapeInfo, combineShapeInfo)
		{}

		/// <summary>
		/// Use this function to create shapes to ensure they fit into memory.
		/// 
		/// The test function can test shapes anywhere in space.If you specify the bottom corner
		/// (in terms of the storage rectangle), the shape is automatically translated in memory
		/// to fit, assuming memory width and height is big enough.
		/// 
		/// Strategy for implementing new shapes:
		///		- First, determine the test function.
		///		- Next, draw a storage rectangle that contains the shape.
		///		- Determine the storgae rectangle width and height.
		///		- Finally, determine the grid-space coordinate of the left bottom corner of the storage rectangle.
		/// 
		/// Then define your function as follows:
		/// 
		/// <code>
		/// public FlatHexShapeInfo&lt;TCell&gt; MyShape()
		/// {
		///		Shape(stargeRectangleWidth, storageRectangleHeight, isInsideMyShape, storageRectangleBottomleft);
		/// }
		/// </code>
		/// </summary>
		/// <param name="width">The widh of the storage rectangle</param>
		/// <param name="height">The height of the storage rectangle</param>
		/// <param name="isInside">A function that returns true if a passed point lies inside the shape being defined</param>
		/// <param name="bottomLeftCorner">The grid-space coordinate of the bottom left corner of the storage rect.</param>
		public FlatHexShapeInfo<TCell> Shape(int width, int height, Func<FlatHexPoint, bool> isInside, FlatHexPoint bottomLeftCorner)
		{
			var shapeInfo = MakeShapeStorageInfo<FlatHexPoint>(width, height, x=>isInside(x + bottomLeftCorner));
			return new FlatHexShapeInfo<TCell>(shapeInfo).Translate(bottomLeftCorner);
		}

		/// <summary>
		/// The same as Shape with all parameters, but with bottomLeft Point set to  FlatHexPoint.Zero.
		/// </summary>
		public FlatHexShapeInfo<TCell> Shape(int width, int height, Func<FlatHexPoint, bool> isInside)
		{
			return Shape(width, height, isInside, FlatHexPoint.Zero);
		}

		/// <summary>
		/// Creates the grid in a shape that spans 
		///	the entire storage rectangle of the given width and height.
		/// </summary>
		[ShapeMethod]
		public FlatHexShapeInfo<TCell> Default(int width, int height)
		{
			var rawInfow = MakeShapeStorageInfo<FlatHexPoint>(
				width, 
				height,
				x => FlatHexGrid<TCell>.DefaultContains(x, width, height));

			return new FlatHexShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Makes a grid with a single cell that corresponds to the origin.
		/// </summary>
		[ShapeMethod]
		public FlatHexShapeInfo<TCell> Single()
		{
			var rawInfow = MakeShapeStorageInfo<FlatHexPoint>(
				1, 
				1,
				x => x == FlatHexPoint.Zero);

			return new FlatHexShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Starts a compound shape operation.
        ///
		///	Any shape that is defined in terms of other shape operations must use this method, and use Endgroup() to end the definition.
        ///<code>
		///		public static FlatHexShapeInfo&lt;TCell&gt; MyCustomShape(this FlatHexOp&lt;TCell&gt; op)
		///		{
		///			return 
		///				BeginGroup()
		///					.Shape1()
		///					.Union()
		///					.Shape2()
		///				.EndGroup(op);
		///		}
        /// </code>
		///	Since version 1.1
		/// </summary>
		public FlatHexOp<TCell> BeginGroup()
		{
			return FlatHexGrid<TCell>.BeginShape();
		}
	}
	
	/// <summary>
	/// Class for making PointyTriGrids in different shapes.
	/// Copyright Gamelogic.
	///	Author Herman Tulleken
	///	Since version 1.0
	/// See also AbstractOp
	/// </summary>
	public partial class PointyTriOp<TCell> : AbstractOp<ShapeStorageInfo<PointyTriPoint>>
	{
		public PointyTriOp(){}

		public PointyTriOp(
			ShapeStorageInfo<PointyTriPoint> leftShapeInfo,
			Func<ShapeStorageInfo<PointyTriPoint>, ShapeStorageInfo<PointyTriPoint>, ShapeStorageInfo<PointyTriPoint>> combineShapeInfo) :
			base(leftShapeInfo, combineShapeInfo)
		{}

		/// <summary>
		/// Use this function to create shapes to ensure they fit into memory.
		/// 
		/// The test function can test shapes anywhere in space.If you specify the bottom corner
		/// (in terms of the storage rectangle), the shape is automatically translated in memory
		/// to fit, assuming memory width and height is big enough.
		/// 
		/// Strategy for implementing new shapes:
		///		- First, determine the test function.
		///		- Next, draw a storage rectangle that contains the shape.
		///		- Determine the storgae rectangle width and height.
		///		- Finally, determine the grid-space coordinate of the left bottom corner of the storage rectangle.
		/// 
		/// Then define your function as follows:
		/// 
		/// <code>
		/// public PointyTriShapeInfo&lt;TCell&gt; MyShape()
		/// {
		///		Shape(stargeRectangleWidth, storageRectangleHeight, isInsideMyShape, storageRectangleBottomleft);
		/// }
		/// </code>
		/// </summary>
		/// <param name="width">The widh of the storage rectangle</param>
		/// <param name="height">The height of the storage rectangle</param>
		/// <param name="isInside">A function that returns true if a passed point lies inside the shape being defined</param>
		/// <param name="bottomLeftCorner">The grid-space coordinate of the bottom left corner of the storage rect.</param>
		public PointyTriShapeInfo<TCell> Shape(int width, int height, Func<PointyTriPoint, bool> isInside, FlatHexPoint bottomLeftCorner)
		{
			var shapeInfo = MakeShapeStorageInfo<PointyTriPoint>(width, height, x=>isInside(x + bottomLeftCorner));
			return new PointyTriShapeInfo<TCell>(shapeInfo).Translate(bottomLeftCorner);
		}

		/// <summary>
		/// The same as Shape with all parameters, but with bottomLeft Point set to  PointyTriPoint.Zero.
		/// </summary>
		public PointyTriShapeInfo<TCell> Shape(int width, int height, Func<PointyTriPoint, bool> isInside)
		{
			return Shape(width, height, isInside, FlatHexPoint.Zero);
		}

		/// <summary>
		/// Creates the grid in a shape that spans 
		///	the entire storage rectangle of the given width and height.
		/// </summary>
		[ShapeMethod]
		public PointyTriShapeInfo<TCell> Default(int width, int height)
		{
			var rawInfow = MakeShapeStorageInfo<PointyTriPoint>(
				width, 
				height,
				x => PointyTriGrid<TCell>.DefaultContains(x, width, height));

			return new PointyTriShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Makes a grid with a single cell that corresponds to the origin.
		/// </summary>
		[ShapeMethod]
		public PointyTriShapeInfo<TCell> Single()
		{
			var rawInfow = MakeShapeStorageInfo<PointyTriPoint>(
				1, 
				1,
				x => x == PointyTriPoint.Zero);

			return new PointyTriShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Starts a compound shape operation.
        ///
		///	Any shape that is defined in terms of other shape operations must use this method, and use Endgroup() to end the definition.
        ///<code>
		///		public static PointyTriShapeInfo&lt;TCell&gt; MyCustomShape(this PointyTriOp&lt;TCell&gt; op)
		///		{
		///			return 
		///				BeginGroup()
		///					.Shape1()
		///					.Union()
		///					.Shape2()
		///				.EndGroup(op);
		///		}
        /// </code>
		///	Since version 1.1
		/// </summary>
		public PointyTriOp<TCell> BeginGroup()
		{
			return PointyTriGrid<TCell>.BeginShape();
		}
	}
	
	/// <summary>
	/// Class for making FlatTriGrids in different shapes.
	/// Copyright Gamelogic.
	///	Author Herman Tulleken
	///	Since version 1.0
	/// See also AbstractOp
	/// </summary>
	public partial class FlatTriOp<TCell> : AbstractOp<ShapeStorageInfo<FlatTriPoint>>
	{
		public FlatTriOp(){}

		public FlatTriOp(
			ShapeStorageInfo<FlatTriPoint> leftShapeInfo,
			Func<ShapeStorageInfo<FlatTriPoint>, ShapeStorageInfo<FlatTriPoint>, ShapeStorageInfo<FlatTriPoint>> combineShapeInfo) :
			base(leftShapeInfo, combineShapeInfo)
		{}

		/// <summary>
		/// Use this function to create shapes to ensure they fit into memory.
		/// 
		/// The test function can test shapes anywhere in space.If you specify the bottom corner
		/// (in terms of the storage rectangle), the shape is automatically translated in memory
		/// to fit, assuming memory width and height is big enough.
		/// 
		/// Strategy for implementing new shapes:
		///		- First, determine the test function.
		///		- Next, draw a storage rectangle that contains the shape.
		///		- Determine the storgae rectangle width and height.
		///		- Finally, determine the grid-space coordinate of the left bottom corner of the storage rectangle.
		/// 
		/// Then define your function as follows:
		/// 
		/// <code>
		/// public FlatTriShapeInfo&lt;TCell&gt; MyShape()
		/// {
		///		Shape(stargeRectangleWidth, storageRectangleHeight, isInsideMyShape, storageRectangleBottomleft);
		/// }
		/// </code>
		/// </summary>
		/// <param name="width">The widh of the storage rectangle</param>
		/// <param name="height">The height of the storage rectangle</param>
		/// <param name="isInside">A function that returns true if a passed point lies inside the shape being defined</param>
		/// <param name="bottomLeftCorner">The grid-space coordinate of the bottom left corner of the storage rect.</param>
		public FlatTriShapeInfo<TCell> Shape(int width, int height, Func<FlatTriPoint, bool> isInside, PointyHexPoint bottomLeftCorner)
		{
			var shapeInfo = MakeShapeStorageInfo<FlatTriPoint>(width, height, x=>isInside(x + bottomLeftCorner));
			return new FlatTriShapeInfo<TCell>(shapeInfo).Translate(bottomLeftCorner);
		}

		/// <summary>
		/// The same as Shape with all parameters, but with bottomLeft Point set to  FlatTriPoint.Zero.
		/// </summary>
		public FlatTriShapeInfo<TCell> Shape(int width, int height, Func<FlatTriPoint, bool> isInside)
		{
			return Shape(width, height, isInside, PointyHexPoint.Zero);
		}

		/// <summary>
		/// Creates the grid in a shape that spans 
		///	the entire storage rectangle of the given width and height.
		/// </summary>
		[ShapeMethod]
		public FlatTriShapeInfo<TCell> Default(int width, int height)
		{
			var rawInfow = MakeShapeStorageInfo<FlatTriPoint>(
				width, 
				height,
				x => FlatTriGrid<TCell>.DefaultContains(x, width, height));

			return new FlatTriShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Makes a grid with a single cell that corresponds to the origin.
		/// </summary>
		[ShapeMethod]
		public FlatTriShapeInfo<TCell> Single()
		{
			var rawInfow = MakeShapeStorageInfo<FlatTriPoint>(
				1, 
				1,
				x => x == FlatTriPoint.Zero);

			return new FlatTriShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Starts a compound shape operation.
        ///
		///	Any shape that is defined in terms of other shape operations must use this method, and use Endgroup() to end the definition.
        ///<code>
		///		public static FlatTriShapeInfo&lt;TCell&gt; MyCustomShape(this FlatTriOp&lt;TCell&gt; op)
		///		{
		///			return 
		///				BeginGroup()
		///					.Shape1()
		///					.Union()
		///					.Shape2()
		///				.EndGroup(op);
		///		}
        /// </code>
		///	Since version 1.1
		/// </summary>
		public FlatTriOp<TCell> BeginGroup()
		{
			return FlatTriGrid<TCell>.BeginShape();
		}
	}
	
	/// <summary>
	/// Class for making PointyRhombGrids in different shapes.
	/// Copyright Gamelogic.
	///	Author Herman Tulleken
	///	Since version 1.0
	/// See also AbstractOp
	/// </summary>
	public partial class PointyRhombOp<TCell> : AbstractOp<ShapeStorageInfo<PointyRhombPoint>>
	{
		public PointyRhombOp(){}

		public PointyRhombOp(
			ShapeStorageInfo<PointyRhombPoint> leftShapeInfo,
			Func<ShapeStorageInfo<PointyRhombPoint>, ShapeStorageInfo<PointyRhombPoint>, ShapeStorageInfo<PointyRhombPoint>> combineShapeInfo) :
			base(leftShapeInfo, combineShapeInfo)
		{}

		/// <summary>
		/// Use this function to create shapes to ensure they fit into memory.
		/// 
		/// The test function can test shapes anywhere in space.If you specify the bottom corner
		/// (in terms of the storage rectangle), the shape is automatically translated in memory
		/// to fit, assuming memory width and height is big enough.
		/// 
		/// Strategy for implementing new shapes:
		///		- First, determine the test function.
		///		- Next, draw a storage rectangle that contains the shape.
		///		- Determine the storgae rectangle width and height.
		///		- Finally, determine the grid-space coordinate of the left bottom corner of the storage rectangle.
		/// 
		/// Then define your function as follows:
		/// 
		/// <code>
		/// public PointyRhombShapeInfo&lt;TCell&gt; MyShape()
		/// {
		///		Shape(stargeRectangleWidth, storageRectangleHeight, isInsideMyShape, storageRectangleBottomleft);
		/// }
		/// </code>
		/// </summary>
		/// <param name="width">The widh of the storage rectangle</param>
		/// <param name="height">The height of the storage rectangle</param>
		/// <param name="isInside">A function that returns true if a passed point lies inside the shape being defined</param>
		/// <param name="bottomLeftCorner">The grid-space coordinate of the bottom left corner of the storage rect.</param>
		public PointyRhombShapeInfo<TCell> Shape(int width, int height, Func<PointyRhombPoint, bool> isInside, PointyHexPoint bottomLeftCorner)
		{
			var shapeInfo = MakeShapeStorageInfo<PointyRhombPoint>(width, height, x=>isInside(x + bottomLeftCorner));
			return new PointyRhombShapeInfo<TCell>(shapeInfo).Translate(bottomLeftCorner);
		}

		/// <summary>
		/// The same as Shape with all parameters, but with bottomLeft Point set to  PointyRhombPoint.Zero.
		/// </summary>
		public PointyRhombShapeInfo<TCell> Shape(int width, int height, Func<PointyRhombPoint, bool> isInside)
		{
			return Shape(width, height, isInside, PointyHexPoint.Zero);
		}

		/// <summary>
		/// Creates the grid in a shape that spans 
		///	the entire storage rectangle of the given width and height.
		/// </summary>
		[ShapeMethod]
		public PointyRhombShapeInfo<TCell> Default(int width, int height)
		{
			var rawInfow = MakeShapeStorageInfo<PointyRhombPoint>(
				width, 
				height,
				x => PointyRhombGrid<TCell>.DefaultContains(x, width, height));

			return new PointyRhombShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Makes a grid with a single cell that corresponds to the origin.
		/// </summary>
		[ShapeMethod]
		public PointyRhombShapeInfo<TCell> Single()
		{
			var rawInfow = MakeShapeStorageInfo<PointyRhombPoint>(
				1, 
				1,
				x => x == PointyRhombPoint.Zero);

			return new PointyRhombShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Starts a compound shape operation.
        ///
		///	Any shape that is defined in terms of other shape operations must use this method, and use Endgroup() to end the definition.
        ///<code>
		///		public static PointyRhombShapeInfo&lt;TCell&gt; MyCustomShape(this PointyRhombOp&lt;TCell&gt; op)
		///		{
		///			return 
		///				BeginGroup()
		///					.Shape1()
		///					.Union()
		///					.Shape2()
		///				.EndGroup(op);
		///		}
        /// </code>
		///	Since version 1.1
		/// </summary>
		public PointyRhombOp<TCell> BeginGroup()
		{
			return PointyRhombGrid<TCell>.BeginShape();
		}
	}
	
	/// <summary>
	/// Class for making FlatRhombGrids in different shapes.
	/// Copyright Gamelogic.
	///	Author Herman Tulleken
	///	Since version 1.0
	/// See also AbstractOp
	/// </summary>
	public partial class FlatRhombOp<TCell> : AbstractOp<ShapeStorageInfo<FlatRhombPoint>>
	{
		public FlatRhombOp(){}

		public FlatRhombOp(
			ShapeStorageInfo<FlatRhombPoint> leftShapeInfo,
			Func<ShapeStorageInfo<FlatRhombPoint>, ShapeStorageInfo<FlatRhombPoint>, ShapeStorageInfo<FlatRhombPoint>> combineShapeInfo) :
			base(leftShapeInfo, combineShapeInfo)
		{}

		/// <summary>
		/// Use this function to create shapes to ensure they fit into memory.
		/// 
		/// The test function can test shapes anywhere in space.If you specify the bottom corner
		/// (in terms of the storage rectangle), the shape is automatically translated in memory
		/// to fit, assuming memory width and height is big enough.
		/// 
		/// Strategy for implementing new shapes:
		///		- First, determine the test function.
		///		- Next, draw a storage rectangle that contains the shape.
		///		- Determine the storgae rectangle width and height.
		///		- Finally, determine the grid-space coordinate of the left bottom corner of the storage rectangle.
		/// 
		/// Then define your function as follows:
		/// 
		/// <code>
		/// public FlatRhombShapeInfo&lt;TCell&gt; MyShape()
		/// {
		///		Shape(stargeRectangleWidth, storageRectangleHeight, isInsideMyShape, storageRectangleBottomleft);
		/// }
		/// </code>
		/// </summary>
		/// <param name="width">The widh of the storage rectangle</param>
		/// <param name="height">The height of the storage rectangle</param>
		/// <param name="isInside">A function that returns true if a passed point lies inside the shape being defined</param>
		/// <param name="bottomLeftCorner">The grid-space coordinate of the bottom left corner of the storage rect.</param>
		public FlatRhombShapeInfo<TCell> Shape(int width, int height, Func<FlatRhombPoint, bool> isInside, FlatHexPoint bottomLeftCorner)
		{
			var shapeInfo = MakeShapeStorageInfo<FlatRhombPoint>(width, height, x=>isInside(x + bottomLeftCorner));
			return new FlatRhombShapeInfo<TCell>(shapeInfo).Translate(bottomLeftCorner);
		}

		/// <summary>
		/// The same as Shape with all parameters, but with bottomLeft Point set to  FlatRhombPoint.Zero.
		/// </summary>
		public FlatRhombShapeInfo<TCell> Shape(int width, int height, Func<FlatRhombPoint, bool> isInside)
		{
			return Shape(width, height, isInside, FlatHexPoint.Zero);
		}

		/// <summary>
		/// Creates the grid in a shape that spans 
		///	the entire storage rectangle of the given width and height.
		/// </summary>
		[ShapeMethod]
		public FlatRhombShapeInfo<TCell> Default(int width, int height)
		{
			var rawInfow = MakeShapeStorageInfo<FlatRhombPoint>(
				width, 
				height,
				x => FlatRhombGrid<TCell>.DefaultContains(x, width, height));

			return new FlatRhombShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Makes a grid with a single cell that corresponds to the origin.
		/// </summary>
		[ShapeMethod]
		public FlatRhombShapeInfo<TCell> Single()
		{
			var rawInfow = MakeShapeStorageInfo<FlatRhombPoint>(
				1, 
				1,
				x => x == FlatRhombPoint.Zero);

			return new FlatRhombShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Starts a compound shape operation.
        ///
		///	Any shape that is defined in terms of other shape operations must use this method, and use Endgroup() to end the definition.
        ///<code>
		///		public static FlatRhombShapeInfo&lt;TCell&gt; MyCustomShape(this FlatRhombOp&lt;TCell&gt; op)
		///		{
		///			return 
		///				BeginGroup()
		///					.Shape1()
		///					.Union()
		///					.Shape2()
		///				.EndGroup(op);
		///		}
        /// </code>
		///	Since version 1.1
		/// </summary>
		public FlatRhombOp<TCell> BeginGroup()
		{
			return FlatRhombGrid<TCell>.BeginShape();
		}
	}
	
	/// <summary>
	/// Class for making CairoGrids in different shapes.
	/// Copyright Gamelogic.
	///	Author Herman Tulleken
	///	Since version 1.0
	/// See also AbstractOp
	/// </summary>
	public partial class CairoOp<TCell> : AbstractOp<ShapeStorageInfo<CairoPoint>>
	{
		public CairoOp(){}

		public CairoOp(
			ShapeStorageInfo<CairoPoint> leftShapeInfo,
			Func<ShapeStorageInfo<CairoPoint>, ShapeStorageInfo<CairoPoint>, ShapeStorageInfo<CairoPoint>> combineShapeInfo) :
			base(leftShapeInfo, combineShapeInfo)
		{}

		/// <summary>
		/// Use this function to create shapes to ensure they fit into memory.
		/// 
		/// The test function can test shapes anywhere in space.If you specify the bottom corner
		/// (in terms of the storage rectangle), the shape is automatically translated in memory
		/// to fit, assuming memory width and height is big enough.
		/// 
		/// Strategy for implementing new shapes:
		///		- First, determine the test function.
		///		- Next, draw a storage rectangle that contains the shape.
		///		- Determine the storgae rectangle width and height.
		///		- Finally, determine the grid-space coordinate of the left bottom corner of the storage rectangle.
		/// 
		/// Then define your function as follows:
		/// 
		/// <code>
		/// public CairoShapeInfo&lt;TCell&gt; MyShape()
		/// {
		///		Shape(stargeRectangleWidth, storageRectangleHeight, isInsideMyShape, storageRectangleBottomleft);
		/// }
		/// </code>
		/// </summary>
		/// <param name="width">The widh of the storage rectangle</param>
		/// <param name="height">The height of the storage rectangle</param>
		/// <param name="isInside">A function that returns true if a passed point lies inside the shape being defined</param>
		/// <param name="bottomLeftCorner">The grid-space coordinate of the bottom left corner of the storage rect.</param>
		public CairoShapeInfo<TCell> Shape(int width, int height, Func<CairoPoint, bool> isInside, PointyHexPoint bottomLeftCorner)
		{
			var shapeInfo = MakeShapeStorageInfo<CairoPoint>(width, height, x=>isInside(x + bottomLeftCorner));
			return new CairoShapeInfo<TCell>(shapeInfo).Translate(bottomLeftCorner);
		}

		/// <summary>
		/// The same as Shape with all parameters, but with bottomLeft Point set to  CairoPoint.Zero.
		/// </summary>
		public CairoShapeInfo<TCell> Shape(int width, int height, Func<CairoPoint, bool> isInside)
		{
			return Shape(width, height, isInside, PointyHexPoint.Zero);
		}

		/// <summary>
		/// Creates the grid in a shape that spans 
		///	the entire storage rectangle of the given width and height.
		/// </summary>
		[ShapeMethod]
		public CairoShapeInfo<TCell> Default(int width, int height)
		{
			var rawInfow = MakeShapeStorageInfo<CairoPoint>(
				width, 
				height,
				x => CairoGrid<TCell>.DefaultContains(x, width, height));

			return new CairoShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Makes a grid with a single cell that corresponds to the origin.
		/// </summary>
		[ShapeMethod]
		public CairoShapeInfo<TCell> Single()
		{
			var rawInfow = MakeShapeStorageInfo<CairoPoint>(
				1, 
				1,
				x => x == CairoPoint.Zero);

			return new CairoShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// Starts a compound shape operation.
        ///
		///	Any shape that is defined in terms of other shape operations must use this method, and use Endgroup() to end the definition.
        ///<code>
		///		public static CairoShapeInfo&lt;TCell&gt; MyCustomShape(this CairoOp&lt;TCell&gt; op)
		///		{
		///			return 
		///				BeginGroup()
		///					.Shape1()
		///					.Union()
		///					.Shape2()
		///				.EndGroup(op);
		///		}
        /// </code>
		///	Since version 1.1
		/// </summary>
		public CairoOp<TCell> BeginGroup()
		{
			return CairoGrid<TCell>.BeginShape();
		}
	}
}

