using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// Extend from this class to hook in your own grid initialisation code. This is also a 
	/// useful place for other logic that interacts with the grid(typically, your game logic). 
	/// It has properties to access the grid and map.
	/// 
	/// You cannot use this to customize the shape or map of the grid (instead, use
	/// CustomGridBuilder and CustomMapBuilder).
	/// </summary>
	[Version(1,8)]
	public class GridBehaviour<TPoint> : GLMonoBehaviour
		where TPoint : IGridPoint<TPoint>
	{
		private TileGridBuilder<TPoint> gridBuilder = null;

		/// <summary>
		/// Get's the mouse position in Grid coordinates.
		/// 
		/// Currently it is only useful for 2D grids, rendered with
		/// orthographic cameras.
		/// </summary>
		public TPoint MousePosition
		{
			get { return GridBuilder.MousePosition; }
		}

		/// <summary>
		/// Returns the grid builder attached to the same game object as this
		/// grid behaviour.
		/// 
		/// (It's provided, but you will mostly need only the Grid and Map.)
		/// </summary>
		public TileGridBuilder<TPoint> GridBuilder
		{
			get
			{
				if (gridBuilder == null)
				{
					gridBuilder = GetComponent<TileGridBuilder<TPoint>>();
				}

				return gridBuilder;
			}
		}

		/// <summary>
		/// The map used to build the grid.
		/// </summary>
		public IMap3D<TPoint> Map
		{
			get { return GridBuilder.Map; }
		}

		/// <summary>
		/// The grid data structure, containing cells as elements.
		/// 
		/// (It's likely that this will return a grid of a different 
		/// (more general) cell type in the future).
		/// </summary>
		public IGrid<TileCell, TPoint> Grid
		{
			get { return GridBuilder.Grid; }
		}

		/// <summary>
		/// When this behaviour is attached to a grid builder, this method is called
		/// once the grid is created, and all cells(tiles) have been instantiated.
		/// 
		/// Override this to implement custom initialisation code. (You can access the
		/// grid through the Grid property).
		/// </summary>
		public virtual void InitGrid()
		{
			//NOP
		}
	}
}
