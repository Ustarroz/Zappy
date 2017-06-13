using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A node that builds a 3D shape by stacking a 2D shape in a number of layers.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.PrimitiveShapeNode{GridPoint3}" />
	[ShapeNode("Layer Shapes/Simple Layer Shapes", 3)]
	[Version(2, 2)]
	public class SimpleLayerShapeNode : PrimitiveShapeNode<GridPoint3>
	{
		public Shape2Graph baseShape;
		public int layerCount;

		protected override IExplicitShape<GridPoint3> Generate()
		{
			var shape = baseShape.GetShape().Layer(layerCount);

			return shape;
		}
	}
}