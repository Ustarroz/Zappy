//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

using System;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// This class provides is a mutable class that can be used to construct
	/// VectorPoints.
	/// 
	/// It is provided for use in Unity's inspector.
	/// </summary>
	/// <example>
	/// <code>
	/// [Serializable]
	/// public MyClass
	/// {
	///		public InspectableVectorPoint playerStart;
	/// 
	///		private PointyHexPoint playerPosition;
	/// 
	///		public void Start()
	///		{
	///			playerPosition = playerStart.GetPointyHexPoint();
	///		}
	/// }
	/// </code>
	/// </example>
	[Version(1)]
	[Serializable]
	public partial class InspectableVectorPoint
	{
		public int x;
		public int y;

		public InspectableVectorPoint()
		{
			x = 0;
			y = 0;
		}

		[Version(1,8)]
		public InspectableVectorPoint(VectorPoint point)
		{
			x = point.X;
			y = point.Y;
		}

		[Version(1,8)]
		public static InspectableVectorPoint Create<TPoint>(IVectorPoint<TPoint> point)
			where TPoint : IGridPoint<TPoint>, IVectorPoint<TPoint>
		{
			return new InspectableVectorPoint
			{
				x = point.X,
				y = point.Y
			};
		}
	
		public VectorPoint GetVectorPoint()
		{
			return new VectorPoint(x, y);
		}

		[Version(1,8)]
		public override string ToString()
		{
			return "(" + x + ", " + y + ")";
		}
	}
}