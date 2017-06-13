using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A node that takes two shapes as inputs,
	/// and put a copy of the second in each cell of a scaled copy of the first.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.ShapeNode{Gamelogic.Grids2.int, Gamelogic.Grids2.IExplicitShape{Gamelogic.Grids2.int}, Gamelogic.Grids2.IExplicitShape{Gamelogic.Grids2.int}}" />
	[ShapeNode("Operator/Product", 1)]
	public class ProductShape1Node : ShapeNode<int, IExplicitShape<int>, IExplicitShape<int>>
	{
		public int scaleFactor;

		public override void Recompute()
		{
			//
		}

		public override List<IExplicitShape<int>> Execute(IEnumerable<IExplicitShape<int>> input)
		{
			var inputList = input.ToList();

			var shape1 = inputList[0];
			var shape2 = inputList[1];
			var shape = shape1.Product(shape2, scaleFactor);

			var storageRect = new GridInterval(
				shape1.Bounds.Point * scaleFactor - shape2.Bounds.Size,
				shape1.Bounds.Size * scaleFactor + shape2.Bounds.Size * 2);

			return new List<IExplicitShape<int>>
			{
				shape.ToExplicit(storageRect)
			};
		}
	}
}