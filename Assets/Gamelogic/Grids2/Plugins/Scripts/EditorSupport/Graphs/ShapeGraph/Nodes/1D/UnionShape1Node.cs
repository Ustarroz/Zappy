using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A node that performs a union operation on the inputs to produce its output.
	/// </summary>
	/// <seealso cref="AggregateShapeNode{int, IExplicitShape{int}, IExplicitShape{int}}" />
	[ShapeNode("Operator/Union", 1)]
	public class UnionShape1Node : AggregateShapeNode<int, IExplicitShape<int>, IExplicitShape<int>>
	{
		public override IExplicitShape<int> Aggregate(IEnumerable<IExplicitShape<int>> input)
		{
			var shape = ImplicitShape.Union(input.Cast<IImplicitShape<int>>());
			var storageRect = input.Select(s => s.Bounds).Aggregate(GridInterval.UnionBoundingBox);

			return shape.ToExplicit(storageRect);
		}

		public override void Recompute()
		{
			//NOP
		}
	}
}