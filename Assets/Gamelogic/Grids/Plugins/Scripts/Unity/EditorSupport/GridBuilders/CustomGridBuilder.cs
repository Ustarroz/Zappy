using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Inherit from this class to implement custom grid shapes.
	/// 
	/// This class should return a grid.To have the grid builder
	/// used this grid, you should set the Shape to Custom in the
	/// editor, and attached this script to the same game object
	/// as the grid builder.
	/// </summary>
	[Version(1,8)]
	public abstract class CustomGridBuilder : GLMonoBehaviour
	{
		/// <remarks>
		/// Check the type; if it is "your" type, return the grid.
		/// Otherwise return null.
		/// </remarks>
		public virtual IGrid<TCell, TPoint> MakeGrid<TCell, TPoint>()
			where TPoint : IGridPoint<TPoint>

		{
			return null;
		}
	}
}