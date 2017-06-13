namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Base class for marking nodes of graphs in this library.
	/// </summary>
	/// <seealso cref="GraphEditorAttribute" />
	public class GridGraphEditorAttribute : GraphEditorAttribute
	{
		public int Dimension { get; private set; }

		public GridGraphEditorAttribute(string name, int dimension) : base(name)
		{
			Dimension = dimension;
		}


	}
}