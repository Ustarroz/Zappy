//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2014 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Class for making LineGrids in different shapes.
	/// <see cref="AbstractOp{TShapeInfo}"/>
	/// </summary>
	[Version(1,8)]
	public partial class LineOp<TCell> : AbstractOp<ShapeStorageInfo<LinePoint>>
	{
		public LineOp()
		{}

		public LineOp(
			ShapeStorageInfo<LinePoint> leftShapeInfo,
			Func<ShapeStorageInfo<LinePoint>, ShapeStorageInfo<LinePoint>, ShapeStorageInfo<LinePoint>> combineShapeInfo) :
			base(leftShapeInfo, combineShapeInfo)
		{}

		[ShapeMethod]
		public LineShapeInfo<TCell> Segment(int length)
		{
			var shapeStorageInfo = MakeShapeStorageInfo<LinePoint>(length, 1, p => IsInsideSegment(p, length));
			return new LineShapeInfo<TCell>(shapeStorageInfo);
		}

		private static bool IsInsideSegment(LinePoint point, int length)
		{
			return 0 <= point && point < length;
		}
	}
}