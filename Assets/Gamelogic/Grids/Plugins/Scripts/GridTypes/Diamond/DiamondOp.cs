//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Documentation in Op.cs
	/// </summary>
	public partial class DiamondOp<TCell>
	{
		#region Shape Methods
		/// <summary>
		/// The bottom left corner is always the origin.
		/// </summary>
		[ShapeMethod]
		public DiamondShapeInfo<TCell> Diamond(int side) 
		{
			return Shape(side, side, x => IsInsideParallelogram(x, side, side));
		}

		/// <summary>
		/// The bottom left corner is always the origin.
		/// </summary>
		[Version(1,7)]
		[ShapeMethod]
		public DiamondShapeInfo<TCell> Parallelogram(int width, int height)
		{
			return Shape(width, height, x => IsInsideParallelogram(x, width, height));
		}

		/// <summary>
		/// A ragged rectangle.
		/// The bottom left corner is always the origin.
		/// </summary>
		[Version(1, 7)]
		[ShapeMethod]
		public DiamondShapeInfo<TCell> Rectangle(int width, int height)
		{
			//Note: this fit is not the tightest possible.
			int diamondSize = width + GLMathf.FloorDiv(height, 2);

			var storageBottomLeft = new DiamondPoint(0, 1 - width);

			return Shape(diamondSize, diamondSize, p => IsInsideRaggedRectangle(p, width, height), storageBottomLeft);
		}

		/// <summary>
		/// A thin rectangle.
		/// The bottom left corner is always the origin.
		/// </summary>
		[ShapeMethod]
		public DiamondShapeInfo<TCell> ThinRectangle(int width, int height)
		{
			int diamondSize = width + GLMathf.FloorDiv(height - 1, 2);
			var storageBottomLeft = new DiamondPoint(0, 1 - width);

			return Shape(diamondSize, diamondSize, p => IsInsideThinRectangle(p, width, height), storageBottomLeft);
		}

		/// <summary>
		/// A fat rectangle.
		/// The bottom left corner is always the origin.
		/// </summary>
		[ShapeMethod]
		public DiamondShapeInfo<TCell> FatRectangle(int width, int height)
		{
			int diamondSize = width + GLMathf.FloorDiv(height, 2);
			var storageBottomLeft = new DiamondPoint(0, 1 - width);

			return Shape(diamondSize, diamondSize, p => IsInsideFatRectangle(p, width, height), storageBottomLeft);
		}
		#endregion

		#region Helpers
		private static bool IsInsideRaggedRectangle(DiamondPoint point, int width, int height)
		{
			int x = GLMathf.FloorDiv(point.X - point.Y, 2);
			int y = point.X + point.Y;

			return
				x >= 0 &&
				x < width &&
				y >= 0 &&
				y < height;
		}

		private static bool IsInsideThinRectangle(DiamondPoint point, int width, int height)
		{
			int x = GLMathf.FloorDiv(point.X - point.Y, 2);
			int y = point.X + point.Y;

			return
				x >= 0 &&
				x < width - GLMathf.FloorMod(y, 2) &&
				y >= 0 &&
				y < height;
		}

		private static bool IsInsideFatRectangle(DiamondPoint point, int width, int height)
		{
			int x = GLMathf.FloorDiv(point.X - point.Y, 2);
			int y = point.X + point.Y;

			return
				x >= -GLMathf.FloorMod(y, 2) &&
				x < width &&
				y >= 0 &&
				y < height;
		}

		private static bool IsInsideParallelogram(DiamondPoint point, int width, int height)
		{
			return
				point.X >= 0 &&
				point.X < width &&
				point.Y >= 0 &&
				point.Y < height;

		}
		#endregion
	}
}