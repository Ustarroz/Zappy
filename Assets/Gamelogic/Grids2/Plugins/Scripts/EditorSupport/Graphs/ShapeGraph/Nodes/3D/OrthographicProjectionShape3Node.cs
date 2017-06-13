using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Constructs a new 3D shape from its 2D orhtographic projections on the XY, XZ and YZ planes.
	/// </summary>
	/// <seealso cref="PrimitiveShapeNode{TPoint}"/>
	[ShapeNode("Operator/Orthographic Projection", 3)]
	[Version(2, 2)]
	class OrthographicProjectionShape3Node : PrimitiveShapeNode<GridPoint3>
	{
		public Shape2Graph shape2Graph1 = null;
		public Shape2Graph shape2Graph2 = null;
		public Shape2Graph shape2Graph3 = null;

		protected override IExplicitShape<GridPoint3> Generate()
		{
			var shape1 = shape2Graph1.GetShape();
			var shape2 = shape2Graph2.GetShape();
			var shape3 = shape2Graph3.GetShape();

			var bounds12 = GridRect.UnionBoundingBox(shape1.Bounds, shape2.Bounds);
			var bounds13 = GridRect.UnionBoundingBox(shape1.Bounds, shape3.Bounds);
			var bounds23 = GridRect.UnionBoundingBox(shape2.Bounds, shape3.Bounds);

			var layer1 = shape1.Layer(bounds23.Size.Y);
			var layer2 = shape2.Layer(bounds13.Size.X);
			var layer3 = shape3.Layer(bounds12.Size.Y);

			layer2 = layer2.SwapToXZY();
			layer3 = layer3.SwapToZYX();

			var layer1Translation = new GridPoint3(0, 0, bounds23.Point.Y);
			var layer2Translation = new GridPoint3(0, 0, bounds13.Point.X);
			var layer3Translation = new GridPoint3(0, 0, bounds12.Point.Y);

			var bound1 = new GridBounds(layer1.Bounds.Point + layer1Translation, layer1.Bounds.Size);
			var bound2 = new GridBounds(layer2.Bounds.Point + layer2Translation, layer2.Bounds.Size);
			var bound3 = new GridBounds(layer3.Bounds.Point + layer3Translation, layer3.Bounds.Size);

			var fixedLayer1 = layer1.Translate(layer1Translation);
			var fixedLayer2 = layer2.Translate(layer2Translation);
			var fixedLayer3 = layer3.Translate(layer3Translation);

			var fixedLayer12 = ImplicitShape.Intersection(fixedLayer1, fixedLayer2);
			var fixedLayer123 = ImplicitShape.Intersection(fixedLayer12, fixedLayer3);

			var bound12 = GridBounds.Intersection(bound1, bound2);
			var bound123 = GridBounds.Intersection(bound12, bound3);

			return fixedLayer123.ToExplicit(bound123);
		}
	}
}