//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013-15 Gamelogic (Pty) Ltd    //
//----------------------------------------------//

// Auto-generated File

using System;
using System.Linq;
using System.Collections.Generic;
using Gamelogic.Extensions.Algorithms;
using UnityEngine;

namespace Gamelogic.Grids
{
	public partial class RectGrid<TCell>
	{
		#region Creation

		/// <summary>
		/// Construct a new grid in the default shape with the given width and height.
		/// No transformations are applied to the grid.
		/// 
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public RectGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The test function should only return true for points within the bounds of the default shape.
		///
		/// No transformations are applied to the grid.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public RectGrid(int width, int height, Func<RectPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The function should only return true for points within the bounds of the rectangle when 
		/// the given transforms are applied to them.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public RectGrid(int width, int height, Func<RectPoint, bool> isInside, RectPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset), RectPoint.MainDirections)
		{}	

		#endregion

		#region Properties
		protected override RectPoint GridOrigin
		{
			get
			{
				return PointTransform(RectPoint.Zero);
			}
		}
		#endregion

		#region Clone methods

		/// <summary>
		/// Returns a grid in the same shape, but with contents in the new type.
		/// </summary>
		public override IGrid<TNewCellType, RectPoint> CloneStructure<TNewCellType>()
		{
			return new RectGrid<TNewCellType>(width, height, contains, PointTransform, InversePointTransform, NeighborDirections);
		}

		#endregion

		#region Shape Fluents
		
		/// <summary>
		/// Use this method to begin a shape building sequence.
		/// </summary>
		public static RectOp<TCell> BeginShape()
		{
			return new RectOp<TCell>();
		}

		#endregion

		#region ToString
		public override string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<RectPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}

	public partial class DiamondGrid<TCell>
	{
		#region Creation

		/// <summary>
		/// Construct a new grid in the default shape with the given width and height.
		/// No transformations are applied to the grid.
		/// 
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public DiamondGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The test function should only return true for points within the bounds of the default shape.
		///
		/// No transformations are applied to the grid.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public DiamondGrid(int width, int height, Func<DiamondPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The function should only return true for points within the bounds of the rectangle when 
		/// the given transforms are applied to them.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public DiamondGrid(int width, int height, Func<DiamondPoint, bool> isInside, DiamondPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset), DiamondPoint.MainDirections)
		{}	

		#endregion

		#region Properties
		protected override DiamondPoint GridOrigin
		{
			get
			{
				return PointTransform(DiamondPoint.Zero);
			}
		}
		#endregion

		#region Clone methods

		/// <summary>
		/// Returns a grid in the same shape, but with contents in the new type.
		/// </summary>
		public override IGrid<TNewCellType, DiamondPoint> CloneStructure<TNewCellType>()
		{
			return new DiamondGrid<TNewCellType>(width, height, contains, PointTransform, InversePointTransform, NeighborDirections);
		}

		#endregion

		#region Shape Fluents
		
		/// <summary>
		/// Use this method to begin a shape building sequence.
		/// </summary>
		public static DiamondOp<TCell> BeginShape()
		{
			return new DiamondOp<TCell>();
		}

		#endregion

		#region ToString
		public override string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<DiamondPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}

	public partial class PointyHexGrid<TCell>
	{
		#region Creation

		/// <summary>
		/// Construct a new grid in the default shape with the given width and height.
		/// No transformations are applied to the grid.
		/// 
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public PointyHexGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The test function should only return true for points within the bounds of the default shape.
		///
		/// No transformations are applied to the grid.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public PointyHexGrid(int width, int height, Func<PointyHexPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The function should only return true for points within the bounds of the rectangle when 
		/// the given transforms are applied to them.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public PointyHexGrid(int width, int height, Func<PointyHexPoint, bool> isInside, PointyHexPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset), PointyHexPoint.MainDirections)
		{}	

		#endregion

		#region Properties
		protected override PointyHexPoint GridOrigin
		{
			get
			{
				return PointTransform(PointyHexPoint.Zero);
			}
		}
		#endregion

		#region Clone methods

		/// <summary>
		/// Returns a grid in the same shape, but with contents in the new type.
		/// </summary>
		public override IGrid<TNewCellType, PointyHexPoint> CloneStructure<TNewCellType>()
		{
			return new PointyHexGrid<TNewCellType>(width, height, contains, PointTransform, InversePointTransform, NeighborDirections);
		}

		#endregion

		#region Shape Fluents
		
		/// <summary>
		/// Use this method to begin a shape building sequence.
		/// </summary>
		public static PointyHexOp<TCell> BeginShape()
		{
			return new PointyHexOp<TCell>();
		}

		#endregion

		#region ToString
		public override string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<PointyHexPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}

	public partial class FlatHexGrid<TCell>
	{
		#region Creation

		/// <summary>
		/// Construct a new grid in the default shape with the given width and height.
		/// No transformations are applied to the grid.
		/// 
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public FlatHexGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The test function should only return true for points within the bounds of the default shape.
		///
		/// No transformations are applied to the grid.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public FlatHexGrid(int width, int height, Func<FlatHexPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The function should only return true for points within the bounds of the rectangle when 
		/// the given transforms are applied to them.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public FlatHexGrid(int width, int height, Func<FlatHexPoint, bool> isInside, FlatHexPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset), FlatHexPoint.MainDirections)
		{}	

		#endregion

		#region Properties
		protected override FlatHexPoint GridOrigin
		{
			get
			{
				return PointTransform(FlatHexPoint.Zero);
			}
		}
		#endregion

		#region Clone methods

		/// <summary>
		/// Returns a grid in the same shape, but with contents in the new type.
		/// </summary>
		public override IGrid<TNewCellType, FlatHexPoint> CloneStructure<TNewCellType>()
		{
			return new FlatHexGrid<TNewCellType>(width, height, contains, PointTransform, InversePointTransform, NeighborDirections);
		}

		#endregion

		#region Shape Fluents
		
		/// <summary>
		/// Use this method to begin a shape building sequence.
		/// </summary>
		public static FlatHexOp<TCell> BeginShape()
		{
			return new FlatHexOp<TCell>();
		}

		#endregion

		#region ToString
		public override string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<FlatHexPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}

	public partial class PointyTriGrid<TCell>
	{
		#region Creation

		/// <summary>
		/// Construct a new grid in the default shape with the given width and height.
		/// No transformations are applied to the grid.
		/// 
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public PointyTriGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The test function should only return true for points within the bounds of the default shape.
		///
		/// No transformations are applied to the grid.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public PointyTriGrid(int width, int height, Func<PointyTriPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The function should only return true for points within the bounds of the rectangle when 
		/// the given transforms are applied to them.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public PointyTriGrid(int width, int height, Func<PointyTriPoint, bool> isInside, PointyTriPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset), PointyTriPoint.MainDirections)
		{}	

		#endregion

		#region Properties
		protected override PointyTriPoint GridOrigin
		{
			get
			{
				return PointTransform(PointyTriPoint.Zero);
			}
		}
		#endregion

		#region Clone methods

		/// <summary>
		/// Returns a grid in the same shape, but with contents in the new type.
		/// </summary>
		public override IGrid<TNewCellType, PointyTriPoint> CloneStructure<TNewCellType>()
		{
			return new PointyTriGrid<TNewCellType>(width, height, contains, PointTransform, InversePointTransform, NeighborDirections);
		}

		#endregion

		#region Shape Fluents
		
		/// <summary>
		/// Use this method to begin a shape building sequence.
		/// </summary>
		public static PointyTriOp<TCell> BeginShape()
		{
			return new PointyTriOp<TCell>();
		}

		#endregion

		#region ToString
		public override string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<PointyTriPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}

	public partial class FlatTriGrid<TCell>
	{
		#region Creation

		/// <summary>
		/// Construct a new grid in the default shape with the given width and height.
		/// No transformations are applied to the grid.
		/// 
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public FlatTriGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The test function should only return true for points within the bounds of the default shape.
		///
		/// No transformations are applied to the grid.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public FlatTriGrid(int width, int height, Func<FlatTriPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The function should only return true for points within the bounds of the rectangle when 
		/// the given transforms are applied to them.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public FlatTriGrid(int width, int height, Func<FlatTriPoint, bool> isInside, FlatTriPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset), FlatTriPoint.MainDirections)
		{}	

		#endregion

		#region Properties
		protected override FlatTriPoint GridOrigin
		{
			get
			{
				return PointTransform(FlatTriPoint.Zero);
			}
		}
		#endregion

		#region Clone methods

		/// <summary>
		/// Returns a grid in the same shape, but with contents in the new type.
		/// </summary>
		public override IGrid<TNewCellType, FlatTriPoint> CloneStructure<TNewCellType>()
		{
			return new FlatTriGrid<TNewCellType>(width, height, contains, PointTransform, InversePointTransform, NeighborDirections);
		}

		#endregion

		#region Shape Fluents
		
		/// <summary>
		/// Use this method to begin a shape building sequence.
		/// </summary>
		public static FlatTriOp<TCell> BeginShape()
		{
			return new FlatTriOp<TCell>();
		}

		#endregion

		#region ToString
		public override string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<FlatTriPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}

	public partial class PointyRhombGrid<TCell>
	{
		#region Creation

		/// <summary>
		/// Construct a new grid in the default shape with the given width and height.
		/// No transformations are applied to the grid.
		/// 
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public PointyRhombGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The test function should only return true for points within the bounds of the default shape.
		///
		/// No transformations are applied to the grid.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public PointyRhombGrid(int width, int height, Func<PointyRhombPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The function should only return true for points within the bounds of the rectangle when 
		/// the given transforms are applied to them.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public PointyRhombGrid(int width, int height, Func<PointyRhombPoint, bool> isInside, PointyRhombPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset), PointyRhombPoint.MainDirections)
		{}	

		#endregion

		#region Properties
		protected override PointyRhombPoint GridOrigin
		{
			get
			{
				return PointTransform(PointyRhombPoint.Zero);
			}
		}
		#endregion

		#region Clone methods

		/// <summary>
		/// Returns a grid in the same shape, but with contents in the new type.
		/// </summary>
		public override IGrid<TNewCellType, PointyRhombPoint> CloneStructure<TNewCellType>()
		{
			return new PointyRhombGrid<TNewCellType>(width, height, contains, PointTransform, InversePointTransform, NeighborDirections);
		}

		#endregion

		#region Shape Fluents
		
		/// <summary>
		/// Use this method to begin a shape building sequence.
		/// </summary>
		public static PointyRhombOp<TCell> BeginShape()
		{
			return new PointyRhombOp<TCell>();
		}

		#endregion

		#region ToString
		public override string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<PointyRhombPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}

	public partial class FlatRhombGrid<TCell>
	{
		#region Creation

		/// <summary>
		/// Construct a new grid in the default shape with the given width and height.
		/// No transformations are applied to the grid.
		/// 
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public FlatRhombGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The test function should only return true for points within the bounds of the default shape.
		///
		/// No transformations are applied to the grid.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public FlatRhombGrid(int width, int height, Func<FlatRhombPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The function should only return true for points within the bounds of the rectangle when 
		/// the given transforms are applied to them.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public FlatRhombGrid(int width, int height, Func<FlatRhombPoint, bool> isInside, FlatRhombPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset), FlatRhombPoint.MainDirections)
		{}	

		#endregion

		#region Properties
		protected override FlatRhombPoint GridOrigin
		{
			get
			{
				return PointTransform(FlatRhombPoint.Zero);
			}
		}
		#endregion

		#region Clone methods

		/// <summary>
		/// Returns a grid in the same shape, but with contents in the new type.
		/// </summary>
		public override IGrid<TNewCellType, FlatRhombPoint> CloneStructure<TNewCellType>()
		{
			return new FlatRhombGrid<TNewCellType>(width, height, contains, PointTransform, InversePointTransform, NeighborDirections);
		}

		#endregion

		#region Shape Fluents
		
		/// <summary>
		/// Use this method to begin a shape building sequence.
		/// </summary>
		public static FlatRhombOp<TCell> BeginShape()
		{
			return new FlatRhombOp<TCell>();
		}

		#endregion

		#region ToString
		public override string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<FlatRhombPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}

	public partial class CairoGrid<TCell>
	{
		#region Creation

		/// <summary>
		/// Construct a new grid in the default shape with the given width and height.
		/// No transformations are applied to the grid.
		/// 
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public CairoGrid(int width, int height) :
			this(width, height, x => DefaultContains(x, width, height))
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The test function should only return true for points within the bounds of the default shape.
		///
		/// No transformations are applied to the grid.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public CairoGrid(int width, int height, Func<CairoPoint, bool> isInside) :
			this(width, height, isInside, x => x, x => x)
		{}

		/// <summary>
		/// Construct a new grid whose cells are determined by the given test function.
		///
		/// The function should only return true for points within the bounds of the rectangle when 
		/// the given transforms are applied to them.
		///
		/// Normally, the static factory methods or shape building methods should be used to create grids.
		/// These constructors are provided for advanced usage.
		/// </summary>
		public CairoGrid(int width, int height, Func<CairoPoint, bool> isInside, CairoPoint offset) :
			this(width, height, isInside, x => x.MoveBy(offset), x => x.MoveBackBy(offset), CairoPoint.MainDirections)
		{}	

		#endregion

		#region Properties
		protected override CairoPoint GridOrigin
		{
			get
			{
				return PointTransform(CairoPoint.Zero);
			}
		}
		#endregion

		#region Clone methods

		/// <summary>
		/// Returns a grid in the same shape, but with contents in the new type.
		/// </summary>
		public override IGrid<TNewCellType, CairoPoint> CloneStructure<TNewCellType>()
		{
			return new CairoGrid<TNewCellType>(width, height, contains, PointTransform, InversePointTransform, NeighborDirections);
		}

		#endregion

		#region Shape Fluents
		
		/// <summary>
		/// Use this method to begin a shape building sequence.
		/// </summary>
		public static CairoOp<TCell> BeginShape()
		{
			return new CairoOp<TCell>();
		}

		#endregion

		#region ToString
		public override string ToString()
		{
			return this.ListToString();
		}
		#endregion

		#region Storage
		public static IntRect CalculateStorage(IEnumerable<CairoPoint> points)
		{
			var firstPoint = points.First();
			var arrayPoint = ArrayPointFromGridPoint(firstPoint.BasePoint);

			var minX = arrayPoint.X;
			var maxX = arrayPoint.X;

			var minY = arrayPoint.Y;
			var maxY = arrayPoint.Y;

			foreach(var point in points)
			{
				arrayPoint = ArrayPointFromGridPoint(point.BasePoint);

				minX = Mathf.Min(minX, arrayPoint.X);
				maxX = Mathf.Max(maxX, arrayPoint.X);

				minY = Mathf.Min(minY, arrayPoint.Y);
				maxY = Mathf.Max(maxY, arrayPoint.Y);
			}

			return new IntRect(
				new ArrayPoint(minX, minY),
				new ArrayPoint(maxX - minX + 1, maxY - minY + 1));
		}
		#endregion
	}

}
