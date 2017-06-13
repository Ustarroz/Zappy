using System;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2
{
	/*
		@version1_8
	*/

	/// <summary>
	/// This is an inspectable presentation of a grid coloring,
	/// using three parameters as explained here:
	///  http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/
	/// </summary>
	/// <remarks>The three values represent two corners of a parallelogram
	///(x0, 0) and(xq, y1) that describe the patch to repeat for
	///the coloring.</remarks>
	[Version(2, 0)]
	[Serializable]
	//TODO make 1D and 3D versions
	public class ColorFunction
	{
		//TODO put in constraints
		public int x0;
		public int x1;
		public int y1;
	}
}