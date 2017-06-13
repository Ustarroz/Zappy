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
	/// This wrapper wraps pointy hex points horizontally. 
	/// </summary>
	[Version(1,7)]
	[Experimental]
	public class PointyHexHorizontalRectangleWrapper : IPointWrapper<PointyHexPoint>
	{
		readonly int width;

		public PointyHexHorizontalRectangleWrapper(int width)
		{
			this.width = width;
		}

		public PointyHexPoint Wrap(PointyHexPoint point)
		{
			return new PointyHexPoint(GLMathf.FloorMod(point.X + GLMathf.FloorDiv(point.Y, 2), width) - GLMathf.FloorDiv(point.Y, 2), point.Y);
		}
	}
}