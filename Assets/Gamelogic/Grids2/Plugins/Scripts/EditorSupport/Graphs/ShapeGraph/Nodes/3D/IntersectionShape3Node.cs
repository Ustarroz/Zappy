using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// The output of this node is the intersection of the inputs.
	/// </summary>
	/// <seealso cref="AggregateShapeNode{GridPoint3, IExplicitShape{GridPoint3}, IExplicitShape{GridPoint3}}" />
	[ShapeNode("Operator/Intersection", 3)]
	public class IntersectionShape3Node : AggregateShapeNode<GridPoint3, IExplicitShape<GridPoint3>, IExplicitShape<GridPoint3>>
	{
		public override IExplicitShape<GridPoint3> Aggregate(IEnumerable<IExplicitShape<GridPoint3>> input)
		{
			var shape = ImplicitShape.Intersection(input.Cast<IImplicitShape<GridPoint3>>());
			var storageRect = input.Select(s => s.Bounds).Aggregate(GridBounds.Intersection);
			return shape.ToExplicit(storageRect);
		}

		public override void Recompute()
		{
			//NOP
		}
	}
}