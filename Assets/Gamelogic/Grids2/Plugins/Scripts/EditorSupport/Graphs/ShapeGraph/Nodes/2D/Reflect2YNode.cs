namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node for reflecting a shape around a horizontal axis.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.ProjectShapeNode{Gamelogic.Grids2.GridPoint2, Gamelogic.Grids2.IExplicitShape{Gamelogic.Grids2.GridPoint2}, Gamelogic.Grids2.IExplicitShape{Gamelogic.Grids2.GridPoint2}}" />
	[ShapeNode("Operator/Reflect Y in bounds", 2)]
	public class Reflect2YNode : ProjectShapeNode<GridPoint2, IExplicitShape<GridPoint2>, IExplicitShape<GridPoint2>>
	{
		public override IExplicitShape<GridPoint2> Transform(IExplicitShape<GridPoint2> input)
		{
			return input.ReflectYInBounds();
		}
	}
}