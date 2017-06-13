using System;
using System.Collections;
using System.Collections.Generic;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Class that represents a grid shape.
	/// </summary>
	/// <typeparam name="TPoint">The type of the point.</typeparam>
	/// <seealso cref="Gamelogic.Grids.IShape{TPoint}" />
	public class Shape<TPoint> : IShape<TPoint> where TPoint : IGridPoint<TPoint>
	{
		private ArrayPoint bottomLeft;
		private ArrayPoint topRight;

		private Func<TPoint> contains; 
		public bool Contains(TPoint point)
		{
			return false;
		}

		public IEnumerator<TPoint> GetEnumerator()
		{
			//for(int i = )

			yield break;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
