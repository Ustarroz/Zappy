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
	/// The default Map to be used with a FlatHexGrid.
	/// </summary>
	[Version(1)]
	public class FlatHexMap : AbstractMap<FlatHexPoint>
	{
		private readonly float hexWidth;

		/// <summary>
		/// Calculates the FlatHexMap
		/// </summary>
		/// <param name="cellDimensions">
		/// The vertical and horizontal distance between cells 
		/// (and thus, in fact a bit smaller in width than the actual cell).
		/// </param>
		public FlatHexMap(Vector2 cellDimensions) :
			base(cellDimensions)
		{
			hexWidth = 1.5f * cellDimensions.x / 2.0f;
		}

		public override Vector2 GridToWorld(FlatHexPoint point)
		{
			float x = point.X * hexWidth;
			float y = (point.X / 2.0f + point.Y) * cellDimensions.y;

			return new Vector2(x, y);
		}

		public override FlatHexPoint RawWorldToGrid(Vector2 point)
		{
			float hexSize = hexWidth / 1.5f;

			float y = (point.y) / cellDimensions.y;
			float x = (point.x) / hexSize;

			//triangular cooords
			float ti = Mathf.FloorToInt(x - y);
			float tj = Mathf.FloorToInt(x + y);
			float tk = Mathf.FloorToInt(-2 * y);

			int hi0 = Mathf.FloorToInt((+ti + tk + 2) / 3);
			int hj0 = Mathf.FloorToInt((+tj - tk + 1) / 3);
			int hk0 = -hi0 - hj0;

			return new FlatHexPoint(-hk0, -hi0);
		}
	}
}