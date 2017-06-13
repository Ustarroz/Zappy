using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A node that builds a 3D shape from stacking 2D shapes in layers.
	/// </summary>
	[ShapeNode("Layer Shapes/Multi Layer Shape", 3)]
	[Version(2, 2)]
	public class MultiLayerShapeNode : PrimitiveShapeNode<GridPoint3>
	{
		public List<Shape2Graph> baseShape;

		protected override IExplicitShape<GridPoint3> Generate()
		{
			var shapes = baseShape.Select(g => g.GetShape());
			var boundingPlane = shapes.Select(s => (GridRect)s.Bounds).Aggregate(GridRect.UnionBoundingBox);
			var shape = ImplicitShape.Layer(shapes.Select(p => (IImplicitShape<GridPoint2>)p));
			var boundingShape = new GridBounds(boundingPlane.Point.To3DXY(0), boundingPlane.Size.To3DXY(baseShape.Count));

			return shape.ToExplicit(boundingShape);
		}
	}
}