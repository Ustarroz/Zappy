using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// The output of this node is the inverse of the input (inside the bounding space).
	/// </summary>
	[ShapeNode("Operator/Subtract", 3)]
	public class SubtractShape3Node : ShapeNode<GridPoint3, IExplicitShape<GridPoint3>, IExplicitShape<GridPoint3>>
	{

		public override List<IExplicitShape<GridPoint3>> Execute(IEnumerable<IExplicitShape<GridPoint3>> input)
		{
			var inputList = input.ToList();

			var shape1 = inputList[0];
			var shape2 = inputList[1];

			var result = ImplicitShape.Func<GridPoint3>(p => shape1.Contains(p) && !shape2.Contains(p)).ToExplicit(shape1.Bounds);

			return new List<IExplicitShape<GridPoint3>> { result };
		}

		public override void Recompute()
		{
			//
		}
	}
}