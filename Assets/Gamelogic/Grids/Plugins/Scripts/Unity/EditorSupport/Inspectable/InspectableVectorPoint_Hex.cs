//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

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
	/// Typical usage us this:
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
	public partial class InspectableVectorPoint
	{
		[Version(1,8)]
		public InspectableVectorPoint(PointyHexPoint point)
		{
			x = point.X;
			y = point.Y;
		}

		public PointyHexPoint GetPointyHexPoint()
		{
			return new PointyHexPoint(x, y);
		}
	
		[Version(1,8)]
		public InspectableVectorPoint(FlatHexPoint point)
		{
			x = point.X;
			y = point.Y;
		}

		public FlatHexPoint GetFlatHexPoint()
		{
			return new FlatHexPoint(x, y);
		}
	}
}