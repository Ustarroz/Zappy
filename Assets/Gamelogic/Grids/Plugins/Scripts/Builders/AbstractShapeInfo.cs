//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//
using System;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// The base class of shape info classes
	/// </summary>
	[Version(1)]
	public abstract class AbstractShapeInfo<TShapeInfo, TGrid, TPoint, TVectorPoint, TShapeOp> : IShapeInfo<TShapeInfo, TGrid, TPoint, TVectorPoint, TShapeOp>
		where TShapeInfo : IShapeInfo<TShapeInfo, TGrid, TPoint, TVectorPoint, TShapeOp>
		where TGrid : IGridSpace<TPoint>
		where TPoint : ISplicedVectorPoint<TPoint, TVectorPoint>, IGridPoint<TPoint>
		where TVectorPoint : IVectorPoint<TVectorPoint>
	{
		#region Fields

		protected ShapeStorageInfo<TPoint> shapeStorageStorageInfo;

		#endregion

		#region Properties

		public ShapeStorageInfo<TPoint> ShapeStorageStorageInfo
		{
			get
			{
				return shapeStorageStorageInfo;
			}
		}

		#endregion

		#region Construction

		protected AbstractShapeInfo(ShapeStorageInfo<TPoint> info)
		{
			shapeStorageStorageInfo = info;
		}

		#endregion

		#region Interface

		public TShapeOp If(bool condition)
		{
			return MakeOp(shapeStorageStorageInfo, (x, y) => condition ? x : y);
		}

		public TShapeOp Intersection()
		{
			return MakeOp(shapeStorageStorageInfo, (x, y) => x.Intersection(y));
		}

		public TShapeOp Union()
		{
			return MakeOp(shapeStorageStorageInfo, (x, y) => x.Union(y));
		}

		public TShapeOp Difference()
		{
			return MakeOp(shapeStorageStorageInfo, (x, y) => x.Difference(y));
		}

		/// <summary>
		/// Calculates the symmetric difference of all storged shape info.
		/// </summary>
		/// <returns>The resulting shape is the union of the first and immediately following shape, minus
		/// the intersection of the two shapes.</returns>
		[Version(1,1)]
		public TShapeOp SymmetricDifference()
		{
			return MakeOp(shapeStorageStorageInfo, (x, y) => x.SymmetricDifference(y));
		}

		public TShapeInfo Translate(int x, int y)
		{
			return Translate(MakePoint(x, y));
		}

		public TShapeInfo IfTranslate(bool condition, int x, int y)
		{
			return condition ? Translate(MakePoint(x, y)) : MakeShapeInfo(shapeStorageStorageInfo);
		}

		/// <summary>
		/// Assumption:
		/// ArrayFromGridPoint(p1 + offset) == ArrayFromGridPoint(p1) + ArrayFromGridPoint(offset)
		/// </summary>
		/// <param name="offset"></param>
		public TShapeInfo Translate(TVectorPoint offset)
		{
			Func<TPoint, bool> newIsInside = x => shapeStorageStorageInfo.contains(x.Subtract(offset));

			var newStorageRect = shapeStorageStorageInfo.storageRect
				.Translate(ArrayPointFromGridPoint(offset));
													  
			return MakeShapeInfo(new ShapeStorageInfo<TPoint>(newStorageRect, newIsInside));
		}

		public TShapeInfo Filter(Func<TPoint, bool> filter)
		{
			Func<TPoint, bool> newIsInside = 
				x => shapeStorageStorageInfo.contains(x) && filter(x);

			var newStorageRect = shapeStorageStorageInfo.storageRect;

			return MakeShapeInfo(new ShapeStorageInfo<TPoint>(newStorageRect, newIsInside));
		}

		public TGrid EndShape()
		{
			var gridOffset = GridPointFromArrayPoint(shapeStorageStorageInfo.storageRect.offset);

			return MakeShape(
				shapeStorageStorageInfo.storageRect.dimensions.X,
				shapeStorageStorageInfo.storageRect.dimensions.Y,
				x => shapeStorageStorageInfo.contains(x.Translate(gridOffset)),
				gridOffset);
		}

		#endregion
		
		#region Abstract Implementation

		protected abstract ArrayPoint ArrayPointFromGridPoint(TVectorPoint point);
		protected abstract TVectorPoint GridPointFromArrayPoint(ArrayPoint point);
		protected abstract TVectorPoint MakePoint(int x, int y);

		protected abstract TShapeOp MakeOp(
			ShapeStorageInfo<TPoint> shapeInfo,
			Func<
				ShapeStorageInfo<TPoint>,
				ShapeStorageInfo<TPoint>,
				ShapeStorageInfo<TPoint>> combineInfo);

		protected abstract TShapeInfo MakeShapeInfo(
			ShapeStorageInfo<TPoint> shapeStorageInfo);

		protected abstract TGrid MakeShape(int x, int y, Func<TPoint, bool> isInside, TVectorPoint offset);

		#endregion
	}
}