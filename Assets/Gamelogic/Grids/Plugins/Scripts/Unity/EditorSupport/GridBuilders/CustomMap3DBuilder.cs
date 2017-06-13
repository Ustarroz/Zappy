using System;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Base class for 3D grid builders using a custom map.
	/// </summary>
	/// <seealso cref="GLMonoBehaviour" />
	[Version(1, 14)]
	public class CustomMap3DBuilder : GLMonoBehaviour
	{
		/// <summary>
		/// Creates a new IMap3D for the given point type.
		/// </summary>
		public virtual IMap3D<TPoint> CreateMap<TPoint>()
			where TPoint : IGridPoint<TPoint>
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Creates a new IMap3D for the given point type.
		/// </summary>
		public virtual IMeshMap<TPoint> CreateMeshMap<TPoint>()
			where TPoint : IGridPoint<TPoint>
		{
			throw new NotImplementedException();
		}
	}
}