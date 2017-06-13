//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//


using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids
{
	/// <summary>
	/// The default Map to be used with a PointyHexGrid.
	/// </summary>
	[Version(1)]
	public class PointyHexMap : AbstractMap<PointyHexPoint>
	{
		private readonly float hexHeight;
		
		public PointyHexMap(Vector2 cellDimensions) :
			base(cellDimensions)
		{
			hexHeight = 1.5f*cellDimensions.y/2.0f;
		}

		public override Vector2 GridToWorld(PointyHexPoint point)
		{
			float sX = (point.X + point.Y / 2.0f) * cellDimensions.x;
			float sY = point.Y * hexHeight;

			return new Vector2(sX, sY);
		}

		public override PointyHexPoint RawWorldToGrid(Vector2 point)
		{
			float hexSize = hexHeight / 1.5f;

			//TODO why is this plus?
			float y = (point.y) / hexSize;
			float x = (point.x) / cellDimensions.x;

			/* 
			This is equivalent, rewritten with Ceil instead of Mathf.FloorToInt.

			float ti = Mathf.Ceil(x - y);
			float tj = Mathf.Ceil(-x - y);
			float tk = Mathf.Ceil(2 * x);

			float hi0 = Mathf.Ceil((ti + tk - 2)/3);
			float hj0 = Mathf.Ceil((tj - tk - 1)/3);
			float hk0 = -hi0 - hj0;
			*/

			float ti = Mathf.FloorToInt(-x + y);
			float tj = Mathf.FloorToInt(x + y);
			float tk = Mathf.FloorToInt(-2 * x);

			float hi0 = Mathf.FloorToInt((+ti + tk + 2) / 3);
			float hj0 = Mathf.FloorToInt((+tj - tk + 1) / 3);
			float hk0 = hi0 + hj0;

			float hi = -hi0;
			float hj = hk0;

			return new PointyHexPoint(Mathf.FloorToInt(hi), Mathf.FloorToInt(hj));
		}
	}
}