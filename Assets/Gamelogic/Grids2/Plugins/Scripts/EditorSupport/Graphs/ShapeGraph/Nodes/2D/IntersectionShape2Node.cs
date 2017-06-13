using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// The output of this node is the intersection of the inputs.
	/// </summary>
	/// <seealso cref="AggregateShapeNode{GridPoint2, IExplicitShape{GridPoint2}, IExplicitShape{GridPoint2}}" />
	[ShapeNode("Operator/Intersection", 2)]
	public class IntersectionShape2Node : AggregateShapeNode<GridPoint2, IExplicitShape<GridPoint2>, IExplicitShape<GridPoint2>>
	{
		public override IExplicitShape<GridPoint2> Aggregate(IEnumerable<IExplicitShape<GridPoint2>> input)
		{
			var shape = ImplicitShape.Intersect(input.Cast<IImplicitShape<GridPoint2>>());
			var storageRect = input.Select(s => s.Bounds).Aggregate(GridRect.Intersection);
			return shape.ToExplicit(storageRect);
		}

		public override void Recompute()
		{
			//NOP
		}
	}
}