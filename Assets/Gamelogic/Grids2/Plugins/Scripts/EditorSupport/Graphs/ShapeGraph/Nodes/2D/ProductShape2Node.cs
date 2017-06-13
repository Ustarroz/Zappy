using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A node that takes two shapes as inputs,
	/// and put a copy of the second in each cell of a scaled copy of the first.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.ShapeNode{Gamelogic.Grids2.GridPoint2, Gamelogic.Grids2.IExplicitShape{Gamelogic.Grids2.GridPoint2}, Gamelogic.Grids2.IExplicitShape{Gamelogic.Grids2.GridPoint2}}" />
	[ShapeNode("Operator/Product", 2)]
	public class ProductShape2Node : ShapeNode<GridPoint2, IExplicitShape<GridPoint2>, IExplicitShape<GridPoint2>>
	{
		public InspectableGridPoint2 scaleFactor;

		public override void Recompute()
		{
			//
		}

		public override List<IExplicitShape<GridPoint2>> Execute(IEnumerable<IExplicitShape<GridPoint2>> input)
		{
			var inputList = input.ToList();

			var shape1 = inputList[0];
			var shape2 = inputList[1];
			var scale = scaleFactor.GetGridPoint();
			var shape = shape1.Product(shape2, scale);

			var storageRect = new GridRect(
				shape1.Bounds.Point.Mul(scale) - shape2.Bounds.Size,
				shape1.Bounds.Size.Mul(scale) + shape2.Bounds.Size * 2);

			return new List<IExplicitShape<GridPoint2>>
			{
				shape.ToExplicit(storageRect)
			};
		}
	}
}