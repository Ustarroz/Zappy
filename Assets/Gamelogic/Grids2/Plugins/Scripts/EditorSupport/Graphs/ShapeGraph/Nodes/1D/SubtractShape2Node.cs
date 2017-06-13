using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// The output of this node is the inverse of the input (inside the bounding space).
	/// </summary>
	[ShapeNode("Operator/Subtract", 2)]
	public class SubtractShape2Node : ShapeNode<GridPoint2, IExplicitShape<GridPoint2>, IExplicitShape<GridPoint2>>
	{
		public override List<IExplicitShape<GridPoint2>> Execute(IEnumerable<IExplicitShape<GridPoint2>> input)
		{
			var inputList = input.ToList();

			var shape1 = inputList[0];
			var shape2 = inputList[1];

			var result = ImplicitShape.Func<GridPoint2>(p => shape1.Contains(p) && !shape2.Contains(p)).ToExplicit(shape1.Bounds);

			return new List<IExplicitShape<GridPoint2>> { result };
		}

		public override void Recompute()
		{
			//
		}
	}
}