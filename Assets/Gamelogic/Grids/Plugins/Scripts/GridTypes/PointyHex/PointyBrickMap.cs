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
	/// A map that can be used with a PointyHexGrid to get a brick-wall pattern. The cells are rectangular.
	/// </summary>
	[Version(1)]
	public class PointyBrickMap : AbstractMap<PointyHexPoint>
	{
		public PointyBrickMap(Vector2 cellDimensions) :
			base(cellDimensions)
		{ }

		public override Vector2 GridToWorld(PointyHexPoint point)
		{
			float sX = (point.X + point.Y / 2.0f) * cellDimensions.x;
			float sY = point.Y * cellDimensions.y;

			return new Vector2(sX, sY);
		}

		public override PointyHexPoint RawWorldToGrid(Vector2 point)
		{
			int y = Mathf.FloorToInt((point.y + cellDimensions.y / 2) / cellDimensions.y);
			int x = Mathf.FloorToInt((point.x - y * cellDimensions.x / 2 + cellDimensions.x / 2) / cellDimensions.x);

			return new PointyHexPoint(x, y);
		}
	}
}