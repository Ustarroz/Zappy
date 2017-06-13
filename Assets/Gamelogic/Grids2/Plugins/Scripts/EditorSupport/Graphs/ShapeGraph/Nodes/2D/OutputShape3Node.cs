namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Output node for 3D shape graphs.
	/// </summary>
	/// <seealso cref="ProjectShapeNode{TPoint,TInput,TOutput}" />
	[ShapeNode("Output/Output", 3)]
	public class OutputShape3Node : ProjectShapeNode<GridPoint3, IExplicitShape<GridPoint3>, IExplicitShape<GridPoint3>>
	{
		public override IExplicitShape<GridPoint3> Transform(IExplicitShape<GridPoint3> input)
		{
			return input;
		}
	}
}