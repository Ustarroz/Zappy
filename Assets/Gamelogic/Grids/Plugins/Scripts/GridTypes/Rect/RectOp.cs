//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Documentation in Op.cs
	/// </summary>
	public partial class RectOp<TCell> : AbstractOp<ShapeStorageInfo<RectPoint>>
	{
		/// <summary>
		/// XXXXX
		/// XXXXX
		/// XXX
		/// </summary>
		[ShapeMethod]
		public RectShapeInfo<TCell> FixedWidth(int width, int cellCount)
		{
			var rawInfo = MakeShapeStorageInfo<RectPoint>(
				width,
				Mathf.CeilToInt(cellCount / (float)width),
				x => IsInsideFixedWidth(x, width, cellCount));

			return new RectShapeInfo<TCell>(rawInfo);
		}

		[ShapeMethod]
		public RectShapeInfo<TCell> FixedHeight(int height, int cellCount)
		{
			var rawInfo = MakeShapeStorageInfo<RectPoint>(
				Mathf.CeilToInt(cellCount / (float)height),
				height,
				x => IsInsideFixedHeight(x, height, cellCount));

			return new RectShapeInfo<TCell>(rawInfo);
		}

		/// <summary>
		/// Generates a grid in a rectangular shape.
		/// </summary>
		/// <param name="width">The width of the grid.</param>
		/// <param name="height">The height of the grid.</param>
		[ShapeMethod]
		public RectShapeInfo<TCell> Rectangle(int width, int height)
		{
			var rawInfow = MakeShapeStorageInfo<RectPoint>(
				width,
				height,
				x => IsInsideRect(x, width, height));

			return new RectShapeInfo<TCell>(rawInfow);
		}

		/// <summary>
		/// A rectangle centered at the given center, and with the given side length.
		/// </summary>
		[Version(1,9)]
		[ShapeMethod]
		public RectShapeInfo<TCell> Circle(int radius)
		{
			int storageHeight = 2*radius + 1;
			int storageWidth = 2*radius + 1;
			var storageBottomLeft = new RectPoint(1 - radius, 1 - radius);

			return Shape(storageWidth, storageHeight, p => IsInsideCircle(p, radius), storageBottomLeft);
		}

		[Version(1, 9)]
		private static bool IsInsideCircle(RectPoint point, int radius)
		{
			return Mathf.Max(point.X, point.Y) < radius;
		}

		/// <summary>
		/// A synonym for Rectangle. Provided to support wrapped grids uniformly.
		/// </summary>
		[Version(1, 7)]
		[ShapeMethod]
		public RectShapeInfo<TCell> Parallelogram(int width, int height)
		{
			return Rectangle(width, height);
		}

		[ShapeMethod]
		public RectShapeInfo<TCell> CheckerBoard(int width, int height)
		{
			return CheckerBoard(width, height, true);
		}

		[ShapeMethod]
		public RectShapeInfo<TCell> CheckerBoard(int width, int height, bool includesOrigin)
		{
			var rawInfo = MakeShapeStorageInfo<RectPoint>(
				width, height,
				x => IsInsideCheckerBoard(x, width, height, includesOrigin));

			return new RectShapeInfo<TCell>(rawInfo);
		}

		public static bool IsInsideFixedWidth(RectPoint point, int width, int cellCount)
		{
			return point.X >= 0 && point.X < width && point.Y * width + point.X < cellCount;
		}

		public static bool IsInsideFixedHeight(RectPoint point, int height, int cellCount)
		{
			return point.Y >= 0 && point.Y < height && point.X * height + point.Y < cellCount;
		}

		public static bool IsInsideRect(RectPoint point, int width, int height)
		{
			return point.X >= 0 && point.X < width && point.Y >= 0 && point.Y < height;
		}

		public static bool IsInsideCheckerBoard(RectPoint point, int width, int height, bool includesOrigin)
		{
			return
				IsInsideRect(point, width, height) &&
				(GLMathf.FloorMod(point.X + point.Y, 2) == (includesOrigin ? 0 : 1));
		}
	}
}
