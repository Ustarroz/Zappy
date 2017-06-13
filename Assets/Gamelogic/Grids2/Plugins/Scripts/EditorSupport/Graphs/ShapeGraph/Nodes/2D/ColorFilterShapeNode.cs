using System.Collections.Generic;

namespace Gamelogic.Grids2.Graph
{

	/// <summary>
	/// Applies a color-function to all cells in the input shapes, and only make those that are have indices in the provided list
	/// part of the output shapes.
	/// </summary>
	/// <seealso cref="ProjectShapeNode{GridPoint2, IExplicitShape{GridPoint2}, IExplicitShape{GridPoint2}}" />

	[ShapeNode("Shape Filters/Color Function Filter", 2)]
	public class ColorFilterShapeNode : ProjectShapeNode<GridPoint2, IExplicitShape<GridPoint2>, IExplicitShape<GridPoint2>>
	{
		public ColorFunction colorFunction;
		public List<int> legalValues;

		public override IExplicitShape<GridPoint2> Transform(IExplicitShape<GridPoint2> input)
		{
			return input.Where(x => legalValues.Contains(x.GetColor(colorFunction)));
		}

		public override void Recompute()
		{
			//NOP
		}
	}
}