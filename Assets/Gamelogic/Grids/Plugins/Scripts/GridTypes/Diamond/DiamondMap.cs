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
	/// Maps between grid points and world points.
	/// </summary>
	[Version(1)]
	public class DiamondMap : AbstractMap<DiamondPoint>
	{
		public DiamondMap(Vector2 cellDimensions) :
			base(cellDimensions)
		{}

		public override Vector2 GridToWorld(DiamondPoint point)
		{
			float x = (point.X - point.Y) * cellDimensions.x / 2;
			float y = (point.X + point.Y) * cellDimensions.y / 2;

			return new Vector2(x, y);
		}

		public override DiamondPoint RawWorldToGrid(Vector2 point)
		{
			int x =
				 Mathf.FloorToInt((point.x + 0 * cellDimensions.x / 2) / cellDimensions.x +
				 /*GLMathf.FloorToInt(*/(point.y + cellDimensions.y / 2) / cellDimensions.y);

			int y =
				 Mathf.FloorToInt((point.y + cellDimensions.y / 2) / cellDimensions.y -
				 (point.x + 0*cellDimensions.x / 2) / cellDimensions.x);

			return new DiamondPoint(x, y);
		}
	}
}