namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Attribute for marking space map nodes that should appear in the graph editor.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.GridGraphEditorAttribute" />
	public class SpaceMapNodeAttribute : GridGraphEditorAttribute
	{
		public SpaceMapNodeAttribute(string name, int dimension) : base(name, dimension)
		{
		}
	}

	/// <summary>
	/// Attribute for marking shape nodes that should appear in the graph editor.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.GridGraphEditorAttribute" />
	public class ShapeNodeAttribute : GridGraphEditorAttribute
	{
		public ShapeNodeAttribute(string name, int dimension) : base(name, dimension)
		{
		}
	}
}