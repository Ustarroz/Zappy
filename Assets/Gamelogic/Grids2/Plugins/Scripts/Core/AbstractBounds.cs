using System.Collections.Generic;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Represents a axis-aligned bounding space. Concrete implementations for 1D (GridInterval), 2D (GridRect)
	/// and 3D (GridBounds) is provided.
	/// </summary>
	/// <typeparam name="TPoint">The point type</typeparam>
	public abstract class AbstractBounds<TPoint> : IExplicitShape<TPoint>
	{
		protected AbstractBounds(TPoint point, TPoint size)
		{
			Point = point;
			Size = size;
		}

		public TPoint Point { get; private set; }
		public TPoint Size { get; private set; }

		public abstract TPoint Extreme { get; }
		public abstract bool Contains(TPoint point);
		public abstract IEnumerable<TPoint> Points { get; }

		public AbstractBounds<TPoint> Bounds
		{
			get { return this; }
		}
	}
}