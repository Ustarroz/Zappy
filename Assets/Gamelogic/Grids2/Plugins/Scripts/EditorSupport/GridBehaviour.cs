using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Base class of grid behaviours that is not generic in the point type.
	/// </summary>
	/// <typeparam name="TCell">The type of the cell.</typeparam>
	/// <seealso cref="GLMonoBehaviour" />
	public abstract class GridBehaviour<TCell> : GLMonoBehaviour
	{
		/// <summary>
		/// When this behaviour is attached to a grid builder, this method is called
		/// once the grid is created, and all cells(tiles) have been instantiated.
		/// Override this to implement custom initialization code. (You can access the
		/// grid through the Grid property).
		/// </summary>
		public virtual void InitGrid()
		{
			//NOP
		}

		[EditorInternal]
		public abstract void __InitPrivateFields<TPoint>(
			GridBuilder<TCell> gridBuilder,
			IGrid<TPoint, TCell> grid,
			GridMap<TPoint> map);

	}

	/// <summary>
	/// A grid behaviour is a <see cref="GLMonoBehaviour"/> that can be used next to a <see cref="GridBuilder"/>
	/// and provides easy access to the grid and map of the grid builder.
	/// </summary>
	/// <typeparam name="TPoint">The point type.</typeparam>
	/// <typeparam name="TCell">The cell type.</typeparam>
	/// <seealso cref="Gamelogic.Grids2.GridBehaviour{TCell}" />
	public class GridBehaviour<TPoint, TCell> : GridBehaviour<TCell>
	{
		private GridMap<TPoint> map;
		private IGrid<TPoint, TCell> grid;
		private GridBuilder<TCell> gridBuilder = null;

		[EditorInternal]
		public override void __InitPrivateFields<TPoint1>(
			GridBuilder<TCell> gridBuilder,
			IGrid<TPoint1, TCell> grid,
			GridMap<TPoint1> map)
		{
			this.gridBuilder = gridBuilder;
			this.map = (GridMap<TPoint>)(object)map;
			this.grid = (IGrid<TPoint, TCell>)grid;
		}

		/// <summary>
		/// Returns the grid builder attached to the same game object as this
		/// grid behaviour.
		///(It's provided, but you will mostly need only the Grid and Map.)
		/// </summary>
		public GridBuilder<TCell> GridBuilder
		{
			get
			{
				//if (gridBuilder == null) GLDebug.Log("Impossible " +GetInstanceID());
				return gridBuilder;
			}
		}

		/// <summary>
		/// The map used to build the grid.
		/// </summary>
		public GridMap<TPoint> GridMap
		{
			get { return map; }
		}

		/// <summary>
		/// The grid data structure, containing cells as elements.
		/// </summary>
		public IGrid<TPoint, TCell> Grid
		{
			get { return grid; }
		}
	}
}