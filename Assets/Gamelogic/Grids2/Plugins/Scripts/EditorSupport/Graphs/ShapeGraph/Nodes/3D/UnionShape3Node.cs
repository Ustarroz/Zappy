using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A node that performs a union operation on the inputs to produce its output.
	/// </summary>
	/// <seealso cref="AggregateShapeNode{GridPoint3, IExplicitShape{GridPoint3}, IExplicitShape{GridPoint3}}" />
	[ShapeNode("Operator/Union", 3)]
	public class UnionShape3Node : AggregateShapeNode<GridPoint3, IExplicitShape<GridPoint3>, IExplicitShape<GridPoint3>>
	{
		public override IExplicitShape<GridPoint3> Aggregate(IEnumerable<IExplicitShape<GridPoint3>> input)
		{
			var shape = ImplicitShape.Union(input.Cast<IImplicitShape<GridPoint3>>());
			var storageRect = input.Select(s => s.Bounds).Aggregate(GridBounds.UnionBoundingBox);

			return shape.ToExplicit(storageRect);
		}

		public override void Recompute()
		{
			//NOP
		}
	}
}