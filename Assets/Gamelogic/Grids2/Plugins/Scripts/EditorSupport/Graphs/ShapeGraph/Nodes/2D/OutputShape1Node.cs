namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// The output node for 1D shape graphs.
	/// </summary>
	/// <seealso cref="ProjectShapeNode{int, IExplicitShape{int}, IExplicitShape{int}}" />
	[ShapeNode("Output/Output", 1)]
	public class OutputShape1Node : ProjectShapeNode<int, IExplicitShape<int>, IExplicitShape<int>>
	{
		public override IExplicitShape<int> Transform(IExplicitShape<int> input)
		{
			return input;
		}
	}
}