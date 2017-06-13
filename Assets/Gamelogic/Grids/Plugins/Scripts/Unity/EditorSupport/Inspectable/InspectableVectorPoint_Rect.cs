//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	public partial class InspectableVectorPoint
	{
		[Version(1,8)]
		public InspectableVectorPoint(RectPoint point)
		{
			x = point.X;
			y = point.Y;
		}

		[Version(1,11)]
		public InspectableVectorPoint(DiamondPoint point)
		{
			x = point.X;
			y = point.Y;
		}

		public RectPoint GetRectPoint()
		{
			return new RectPoint(x, y);
		}
	
		public DiamondPoint GetDiamondPoint()
		{
			return new DiamondPoint(x, y);
		}
	}
}