using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// The output of this node is the intersection of the inputs.
	/// </summary>
	/// <seealso cref="AggregateShapeNode{int, IExplicitShape{int}, IExplicitShape{int}}" />
	[ShapeNode("Operator/Intersection", 1)]
	public class IntersectionShape1Node : AggregateShapeNode<int, IExplicitShape<int>, IExplicitShape<int>>
	{
		public override IExplicitShape<int> Aggregate(IEnumerable<IExplicitShape<int>> input)
		{
			var shape = ImplicitShape.Intersection(input.Cast<IImplicitShape<int>>());
			var storageRect = input.Select(s => s.Bounds).Aggregate(GridInterval.Intersection);
			return shape.ToExplicit(storageRect);
		}

		public override void Recompute()
		{
			//NOP
		}
	}
}