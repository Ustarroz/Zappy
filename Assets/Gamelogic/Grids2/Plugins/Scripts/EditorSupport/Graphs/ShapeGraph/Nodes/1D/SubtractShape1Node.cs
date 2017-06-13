using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// The output of this node is the inverse of the input (inside the bounding space).
	/// </summary>
	[ShapeNode("Operator/Subtract", 1)]
	public class SubtractShape1Node : ShapeNode<int, IExplicitShape<int>, IExplicitShape<int>>
	{
		public override List<IExplicitShape<int>> Execute(IEnumerable<IExplicitShape<int>> input)
		{
			var inputList = input.ToList();

			var shape1 = inputList[0];
			var shape2 = inputList[1];

			var result = ImplicitShape.Func<int>(p => shape1.Contains(p) && !shape2.Contains(p)).ToExplicit(shape1.Bounds);

			return new List<IExplicitShape<int>> { result };
		}

		public override void Recompute()
		{
			//
		}
	}
}