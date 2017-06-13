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
	/// Maps between RectPoints grid points and Vector2 world points. 
	/// </summary>
	[Version(1)]
	public class RectMap : AbstractMap<RectPoint>
	{
		public RectMap(Vector2 cellDimensions) :
			base(cellDimensions)
		{ }

		public override Vector2 GridToWorld(RectPoint point)
		{
			return new Vector2(point.X * cellDimensions.x, point.Y * cellDimensions.y);
		}

		public override RectPoint RawWorldToGrid(Vector2 point)
		{
			return new RectPoint(
				Mathf.FloorToInt((point.x + cellDimensions.x / 2) / cellDimensions.x),
				Mathf.FloorToInt((point.y + cellDimensions.y / 2) / cellDimensions.y));
		}
	}
}