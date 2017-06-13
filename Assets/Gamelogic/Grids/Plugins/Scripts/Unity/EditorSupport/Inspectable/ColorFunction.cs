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
	/// This is an inspectable presentation of a grid coloring, 
	/// using three parameters as explained here:
	/// 
	/// http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/
	/// 
	/// The three values represent two corners of a parallelogram
	/// (x0, 0) and(xq, y1) that describe the patch to repeat for 
	/// the coloring.
	/// </summary>
	[Version(1,8)]
	[Serializable]
	public class ColorFunction
	{
		public int x0;
		public int x1;
		public int y1;
	}
}