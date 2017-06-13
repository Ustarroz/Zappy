using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A node that performs a union operation on the inputs to produce its output.
	/// </summary>
	/// <seealso cref="AggregateShapeNode{GridPoint2, IExplicitShape{GridPoint2}, IExplicitShape{GridPoint2}}" />
	[ShapeNode("Operator/Union", 2)]
	public class UnionShape2Node : AggregateShapeNode<GridPoint2, IExplicitShape<GridPoint2>, IExplicitShape<GridPoint2>>
	{
		public override IExplicitShape<GridPoint2> Aggregate(IEnumerable<IExplicitShape<GridPoint2>> input)
		{
			var shape = ImplicitShape.Union(input.Cast<IImplicitShape<GridPoint2>>());
			var storageRect = input.Select(s => s.Bounds).Aggregate(GridRect.UnionBoundingBox);

			return shape.ToExplicit(storageRect);
		}

		public override void Recompute()
		{
			//NOP
		}
	}
}