
namespace Gamelogic.Grids
{
	/// <summary>
	/// An interface that all tile grid builder implement. 
	/// This gives common hooks that are used by the grid editors.
	/// </summary>
	//TODO: this should be named ITileGridBuilder
	public interface ITileGrid<TPoint> : IGridBuilderBase
	{
		void __UpdatePresentation(bool forceUpdate);
	}
}
