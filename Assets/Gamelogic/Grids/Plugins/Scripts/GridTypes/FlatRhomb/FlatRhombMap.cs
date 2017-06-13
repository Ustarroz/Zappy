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
	/// The default map to be used with FlatRhombGrid.
	/// </summary>
	[Version(1)]
	public class FlatRhombMap : AbstractMap<FlatRhombPoint>
	{
		readonly IMap<FlatHexPoint> baseMap;
		private readonly float rhombWidth;
		private readonly float rhombHeight;

		//TODO: Make this constructor take more intuitive parms
		public FlatRhombMap(Vector2 cellDimensions) :
			base(cellDimensions)
		{
			Vector2 hexDimensions = cellDimensions / 2;
			hexDimensions.x = 2 * hexDimensions.x / 1.5f;

			baseMap = new FlatHexMap(hexDimensions).AnchorCellMiddleCenter();
			rhombWidth = cellDimensions.y / Mathf.Sqrt(3);
			rhombHeight = cellDimensions.y;
		}

		public override Vector2 GetCellDimensions(FlatRhombPoint point)
		{
			return point.I == 2 ? new Vector2(rhombWidth, rhombHeight) : new Vector2(cellDimensions.x, cellDimensions.y / 2);
		}

		public override Vector2 GridToWorld(FlatRhombPoint point)
		{
			Vector2 basePoint = baseMap[point.BasePoint] * 2;

			switch (point.I)
			{
				case 0:
					basePoint += new Vector2(-cellDimensions.x, -cellDimensions.y / 2) / 2;
					break;
				case 1:
					basePoint += new Vector2(-cellDimensions.x, cellDimensions.y / 2) / 2;
					break;
			}

			basePoint += new Vector2(cellDimensions.x / 2, cellDimensions.y / 4);

			return basePoint;
		}

		//TODO: Fix
		public override FlatRhombPoint RawWorldToGrid(Vector2 point)
		{
			float hexSize = cellDimensions.y;

			//basePoint += new Vector2(cellDimensions.x/2, cellDimensions.y / 4);

			float y = (point.y - cellDimensions.y / 4) / hexSize;
			float x = (point.x - cellDimensions.y / 4 / Mathf.Sqrt(3)) / hexSize * Mathf.Sqrt(3);

			int ti = Mathf.FloorToInt(x - y);
			int tj = Mathf.FloorToInt(x + y);
			int tk = Mathf.FloorToInt(-2 * y);

			float hi0 = Mathf.FloorToInt((+ti + tk + 2.0f) / 3);
			float hj0 = Mathf.FloorToInt((+tj - tk + 1.0f) / 3);
			float hk0 = hi0 + hj0;

			float hi = -hi0;
			float hj = hk0;

			int index;

			if (
				GLMathf.FloorMod(tj, 3) == 0 && GLMathf.FloorMod(tk, 3) == 1 ||
				GLMathf.FloorMod(tj, 3) == 1 && GLMathf.FloorMod(tk, 3) == 2 ||
				GLMathf.FloorMod(tj, 3) == 2 && GLMathf.FloorMod(tk, 3) == 0)
			{
				index = 0;
			}
			else if (
				GLMathf.FloorMod(ti, 3) == 0 && GLMathf.FloorMod(tk, 3) == 1 ||
				GLMathf.FloorMod(ti, 3) == 1 && GLMathf.FloorMod(tk, 3) == 0 ||
				GLMathf.FloorMod(ti, 3) == 2 && GLMathf.FloorMod(tk, 3) == 2
				)
			{
				index = 1;
			}
			else
			{
				index = 2;
			}

			return new FlatRhombPoint(Mathf.FloorToInt(hj), Mathf.FloorToInt(hi), index);
		}
	}
}