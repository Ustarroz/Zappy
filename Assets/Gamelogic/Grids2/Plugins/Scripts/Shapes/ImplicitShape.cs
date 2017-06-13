using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Provides methods for creating and manipulating implicit shapes.
	/// </summary>
	public static class ImplicitShape
	{
		#region  Types

		/// <summary>
		/// It represent a 1D simple implicit shape that consist only of a center point.
		/// </summary>
		private sealed class SingleShape1 : IImplicitShape<int>
		{
			#region Constants

			private readonly int center;

			#endregion

			#region Constructors

			public SingleShape1(int center)
			{
				this.center = center;
			}

			#endregion

			#region Public Methods

			public bool Contains(int point)
			{
				return point == center;
			}

			#endregion
		}

		/// <summary>
		/// It represent a 2D simple implicit shape that consist only of a center point.
		/// </summary>
		private sealed class SingleShape2 : IImplicitShape<GridPoint2>
		{
			#region Constants

			private readonly GridPoint2 center;

			#endregion

			#region Constructors

			public SingleShape2(GridPoint2 center)
			{
				this.center = center;
			}

			#endregion

			#region Public Methods

			public bool Contains(GridPoint2 point)
			{
				return point == center;
			}

			#endregion
		}

		/// <summary>
		/// It represent a 3D simple implicit shape that consist only of a center point.
		/// </summary>
		private sealed class SingleShape3 : IImplicitShape<GridPoint3>
		{
			#region Constants

			private readonly GridPoint3 center;

			#endregion

			#region Constructors

			public SingleShape3(GridPoint3 center)
			{
				this.center = center;
			}

			#endregion

			#region Public Methods

			public bool Contains(GridPoint3 point)
			{
				return point == center;
			}

			#endregion
		}

		/// <summary>
		/// It represent a 2D circle implicit shape.
		/// </summary>
		private sealed class CircleShape2 : IImplicitShape<GridPoint2>
		{
			#region Constants

			private readonly Func<Vector2, float> norm;
			private readonly Vector2 center;
			private readonly float radius;

			#endregion

			#region Constructors

			public CircleShape2(Vector2 center, float radius, Func<Vector2, float> norm)
			{
				this.center = center;
				this.radius = radius;
				this.norm = norm;
			}

			#endregion

			#region Public Methods

			public bool Contains(GridPoint2 point)
			{
				return norm(point - center) < radius;
			}

			#endregion
		}

		/// <summary>
		/// It represent a 2D convex polygon implicit shape.
		/// </summary>
		private sealed class ConvexPolygonShape2 : IImplicitShape<GridPoint2>
		{
			#region Constants

			private readonly IImplicitShape<GridPoint2> shape;

			#endregion

			#region Constructors

			public ConvexPolygonShape2(IEnumerable<GridPoint2> vertices)
			{
				var vertexList = vertices.ToList();
				var halfPlanes = new List<IImplicitShape<GridPoint2>>();

				for (int i = 0; i < vertexList.Count; i++)
				{
					int nextI = i + 1;
					if (nextI == vertexList.Count) nextI = 0;

					var halfPlane = new Halfplane(vertexList[i], vertexList[nextI]);

					halfPlanes.Add(halfPlane);
				}

				shape = Intersection(halfPlanes);
			}

			#endregion

			#region Public Methods

			public bool Contains(GridPoint2 point)
			{
				return shape.Contains(point);
			}

			#endregion
		}

		/// <summary>
		/// It represent a 1D translation shape that check for offseted points.
		/// </summary>
		private sealed class TranslationShape1 : IImplicitShape<int>
		{
			#region Constants

			private readonly int offset;
			private readonly IImplicitShape<int> baseShape;

			#endregion

			#region Constructors

			public TranslationShape1(IImplicitShape<int> baseShape, int offset)
			{
				this.offset = offset;
				this.baseShape = baseShape;
			}

			#endregion

			#region Public Methods

			public bool Contains(int point)
			{
				return baseShape.Contains(point - offset);
			}

			#endregion
		}

		/// <summary>
		/// It represent a 2D translation shape that check for offseted points.
		/// </summary>
		private sealed class TranslationShape2 : IImplicitShape<GridPoint2>
		{
			#region Constants

			private readonly GridPoint2 offset;
			private readonly IImplicitShape<GridPoint2> baseShape;

			#endregion

			#region Constructors

			public TranslationShape2(IImplicitShape<GridPoint2> baseShape, GridPoint2 offset)
			{
				this.offset = offset;
				this.baseShape = baseShape;
			}

			#endregion

			#region Public Methods

			public bool Contains(GridPoint2 point)
			{
				return baseShape.Contains(point - offset);
			}

			#endregion
		}

		/// <summary>
		/// It represent a 3D translation shape that check for offseted points.
		/// </summary>
		private sealed class TranslationShape3 : IImplicitShape<GridPoint3>
		{
			#region Constants

			private readonly GridPoint3 offset;
			private readonly IImplicitShape<GridPoint3> baseShape;

			#endregion

			#region Constructors

			public TranslationShape3(IImplicitShape<GridPoint3> baseShape, GridPoint3 offset)
			{
				this.offset = offset;
				this.baseShape = baseShape;
			}

			#endregion

			#region Public Methods

			public bool Contains(GridPoint3 point)
			{
				return baseShape.Contains(point - offset);
			}

			#endregion
		}

		/// <summary>
		/// It represent a generic union shape that check if a point
		/// is within any of the shapes.
		/// </summary>
		/// <typeparam name="TPoint">Type of the point used.</typeparam>
		private sealed class UnionShape<TPoint> : IImplicitShape<TPoint>
		{
			#region Constants

			private readonly List<IImplicitShape<TPoint>> shapes;

			#endregion

			#region Constructors

			public UnionShape(IEnumerable<IImplicitShape<TPoint>> shapes)
			{
				this.shapes = shapes.ToList();
			}

			#endregion

			#region Public Methods

			public bool Contains(TPoint point)
			{
				return shapes.Any(s => s.Contains(point));
			}

			#endregion
		}

		/// <summary>
		/// It represent a generic intersection shape that check if a point
		/// is within any of the shapes.
		/// </summary>
		private sealed class IntersectionShape : IImplicitShape<GridPoint2>
		{
			#region Constants

			private readonly List<IImplicitShape<GridPoint2>> shapes;

			#endregion

			#region Constructors

			public IntersectionShape(IEnumerable<IImplicitShape<GridPoint2>> shapes)
			{
				this.shapes = shapes.ToList();
			}

			#endregion

			#region Public Methods

			public bool Contains(GridPoint2 point)
			{
				return shapes.All(s => s.Contains(point));
			}

			#endregion
		}

		/// <summary>
		/// It represent a 1D product shape that makes a product of two shapes and saved them
		/// as a 1D UnionShape.
		/// </summary>
		private sealed class ProductShape1 : IImplicitShape<int>
		{
			#region Constants

			private readonly UnionShape<int> unionShape;

			#endregion

			#region Constructors

			public ProductShape1(IExplicitShape<int> shape1, IImplicitShape<int> shape2, int scaleFactor)
			{
				var translationShapes = shape1.Points.Select(point => shape2.Translate(point * scaleFactor));

				unionShape = new UnionShape<int>(translationShapes);
			}

			#endregion

			#region Public Methods

			public bool Contains(int point)
			{
				return unionShape.Contains(point);
			}

			#endregion
		}

		/// <summary>
		/// It represent a 2D product shape that makes a product of two shapes and saved them
		/// as a 2D UnionShape.
		/// </summary>
		private sealed class ProductShape2 : IImplicitShape<GridPoint2>
		{
			#region Constants

			private readonly UnionShape<GridPoint2> unionShape;

			#endregion

			#region Constructors

			public ProductShape2(IExplicitShape<GridPoint2> shape1, IImplicitShape<GridPoint2> shape2, GridPoint2 scaleFactor)
			{
				var translationShapes = shape1.Points
					.Select(point => shape2.Translate(point.Mul(scaleFactor)));

				unionShape = new UnionShape<GridPoint2>(translationShapes);
			}

			#endregion

			#region Public Methods

			public bool Contains(GridPoint2 point)
			{
				return unionShape.Contains(point);
			}

			#endregion
		}

		/// <summary>
		/// It represent a 3D product shape that makes a product of two shapes and saved them
		/// as a 3D UnionShape.
		/// </summary>
		private sealed class ProductShape3 : IImplicitShape<GridPoint3>
		{
			#region Constants

			private readonly UnionShape<GridPoint3> unionShape;

			#endregion

			#region Constructors

			public ProductShape3(IExplicitShape<GridPoint3> shape1, IImplicitShape<GridPoint3> shape2, GridPoint3 scaleFactor)
			{
				var translationShapes = shape1.Points.Select(point => shape2.Translate(
					point.Mul(scaleFactor)));

				unionShape = new UnionShape<GridPoint3>(translationShapes);
			}

			#endregion

			#region Public Methods

			public bool Contains(GridPoint3 point)
			{
				return unionShape.Contains(point);
			}

			#endregion
		}

		/// <summary>
		/// It represent a 2D Halfplane shape
		/// </summary>
		private sealed class Halfplane : IImplicitShape<GridPoint2>
		{
			#region Constants

			private readonly GridPoint2 p1;
			private readonly GridPoint2 p2;

			#endregion

			#region Constructors

			public Halfplane(GridPoint2 p1, GridPoint2 p2)
			{
				this.p1 = p1;
				this.p2 = p2;
			}

			#endregion

			#region Public Methods

			public bool Contains(GridPoint2 point)
			{
				return (point - p1).PerpDot(p2 - p1) < 0;
			}

			#endregion
		}

		/// <summary>
		/// It represent a 2D Parallelogram Shape
		/// </summary>
		private sealed class ParallelogramShape : IImplicitShape<GridPoint2>
		{
			#region Private Fields

			[SerializeField]
			private GridPoint2 dimensions;

			#endregion

			#region Constructors

			public ParallelogramShape(GridPoint2 dimensions)
			{
				this.dimensions = dimensions;
			}

			#endregion

			#region Public Methods

			public bool Contains(GridPoint2 point)
			{
				return point.X >= 0 && point.Y >= 0 && point.X < dimensions.X && point.Y < dimensions.Y;
			}

			#endregion
		}

		/// <summary>
		/// It represent a generic Point list shape that hold a HashSet of points.
		/// </summary>
		/// <typeparam name="TPoint">Type of the point used.</typeparam>
		private sealed class PointListShape<TPoint> : IImplicitShape<TPoint>
		{
			#region Constants

			private readonly HashSet<TPoint> points;

			#endregion

			#region Constructors

			public PointListShape(IEnumerable<TPoint> pointList)
			{
				points = new HashSet<TPoint>(new EqualsComparer<TPoint>());

				foreach (var point in pointList)
				{
					points.Add(point);
				}
			}

			#endregion

			#region Public Methods

			public bool Contains(TPoint point)
			{
				return points.Contains(point);
			}

			#endregion
		}

		/// <summary>
		/// It represent a 1D Segment Shape.
		/// </summary>
		private sealed class SegmentShape : IImplicitShape<int>
		{
			#region Constants

			private readonly GridInterval interval;

			#endregion

			#region Constructors

			public SegmentShape(GridInterval interval)
			{
				this.interval = interval;
			}

			#endregion

			#region Public Methods

			public bool Contains(int point)
			{
				return interval.Contains(point);
			}

			#endregion
		}

		/// <summary>
		/// It represent a 3D Parallelepiped Shape.
		/// </summary>
		private sealed class ParallelepipedShape : IImplicitShape<GridPoint3>
		{
			#region Private Fields

			[SerializeField]
			private GridPoint3 dimensions;

			#endregion

			#region Constructors

			public ParallelepipedShape(GridPoint3 dimensions)
			{
				this.dimensions = dimensions;
			}

			#endregion

			#region Public Methods

			public bool Contains(GridPoint3 point)
			{
				return point.X >= 0 &&
					   point.Y >= 0 &&
					   point.Z >= 0 &&
					   point.X < dimensions.X &&
					   point.Y < dimensions.Y &&
					   point.Z < dimensions.Z;
			}

			#endregion
		}

		/// <summary>
		/// It represent a 3D Sphere Shape.
		/// </summary>
		private sealed class SphereShape : IImplicitShape<GridPoint3>
		{
			#region Constants

			private readonly float radius;

			#endregion

			#region Constructors

			public SphereShape(float radius)
			{
				this.radius = radius;
			}

			#endregion

			#region Public Methods

			public bool Contains(GridPoint3 point)
			{
				var magnitude = BlockPoint.EuclideanNorm(point);

				return magnitude < radius;
			}

			#endregion
		}

		/// <summary>
		/// It represet a generic Function Shape, a shape that can hold a function to be applayed to the points of the shape.
		/// </summary>
		/// <typeparam name="TPoint">Type of the point used.</typeparam>
		private sealed class FunctionShape<TPoint> : IImplicitShape<TPoint>
		{
			#region Constants

			private readonly Func<TPoint, bool> contains;

			#endregion

			#region Constructors

			public FunctionShape(Func<TPoint, bool> contains)
			{
				this.contains = contains;
			}

			#endregion

			#region Public Methods

			public bool Contains(TPoint point)
			{
				return contains(point);
			}

			#endregion
		}

		private sealed class SimpleLayerShape : IImplicitShape<GridPoint3>
		{
			private readonly IImplicitShape<GridPoint2> baseShape;
			private readonly int layerCount;

			public SimpleLayerShape(IImplicitShape<GridPoint2> baseShape, int layerCount)
			{
				this.baseShape = baseShape;
				this.layerCount = layerCount;
			}

			public bool Contains(GridPoint3 point)
			{
				if (point.Z < 0) return false;
				if (point.Z >= layerCount) return false;

				return baseShape.Contains(point.To2DXY());
			}
		}

		private sealed class MultiLayeredShape : IImplicitShape<GridPoint3>
		{
			private readonly IList<IImplicitShape<GridPoint2>> shapes;

			public MultiLayeredShape(IEnumerable<IImplicitShape<GridPoint2>> shapes)
			{
				this.shapes = shapes.ToList();
			}

			public bool Contains(GridPoint3 point)
			{
				return shapes[point.Z].Contains(point.To2DXY());
			}
		}

		#endregion

		#region Public Static Methods

		/// <summary>
		/// Apply a Manhattan Norm to a given point.
		/// </summary>
		/// <param name="point">Point to apply the norm.</param>
		private static float ManhattanNorm(Vector2 point)
		{
			return Mathf.Abs(point.x) + Mathf.Abs(point.y);
		}

		/// <summary>
		/// Gets the Chebychev of a given point.
		/// </summary>
		/// <param name="point">Point to apply the operation.</param>
		private static float Chebychev(Vector2 point)
		{
			return Mathf.Max(Mathf.Abs(point.x), Mathf.Abs(point.y));
		}

		/// <summary>
		/// Gets the Down Triangle norm to a given point.
		/// </summary>
		/// <param name="point">Point to apply the operation.</param>
		private static float DownTriangleNorm(Vector2 point)
		{
			return Mathf.Max(point.x, point.y, -point.x - point.y);
		}

		/// <summary>
		/// Gets the Up Triangle norm to a given point.
		/// </summary>
		/// <param name="point">Point to apply the operation.</param>
		private static float UpTriangleNorm(Vector2 point)
		{
			return Mathf.Max(-point.x, -point.y, point.x + point.y);
		}

		/// <summary>
		/// Gets the Hex norm to a given point.
		/// </summary>
		/// <param name="point">Point to apply the operation.</param>
		private static float HexNorm(Vector2 point)
		{
			return Mathf.Max(Mathf.Abs(point.x), Math.Abs(point.y), Mathf.Abs(-point.x - point.y));
		}

		/// <summary>
		/// Gets the Star norm to a given point.
		/// </summary>
		/// <param name="point">Point to apply the operation.</param>
		private static float StarNorm(Vector2 point)
		{
			var x = point.x;
			var y = point.y;

			return Mathf.Min(Mathf.Max(x, y, -x - y), Mathf.Max(-x, -y, x + y));
		}

		#endregion

		#region Public Methods

		#region 1D Methods

		/// <summary>
		/// It creates a 1D Single Shape with center value 0.
		/// </summary>
		public static IImplicitShape<int> Single1()
		{
			return new SingleShape1(0);
		}

		/// <summary>
		/// It creates a 1D Single Shape with a given point value.
		/// </summary>
		/// <param name="point">Value of the center point.</param>
		public static IImplicitShape<int> Single1(int point)
		{
			return new SingleShape1(point);
		}

		/// <summary>
		/// It creates a 1D Segment of size 1 and a value point of n.
		/// </summary>
		/// <param name="n">Value of the point of the segment.</param>
		public static IImplicitShape<int> Single(int n)
		{
			return Segment(n, 1);
		}

		/// <summary>
		/// It creates a 1D Segment Shape given a GridInterval.
		/// </summary>
		/// <param name="interval">Interval to transform into a Segment Shape</param>
		public static IImplicitShape<int> Segment(GridInterval interval)
		{
			return new SegmentShape(interval);
		}

		/// <summary>
		/// It creates a 1D Segment Shape with a given start and size interval.
		/// </summary>
		/// <param name="start">Where the interval start.</param>
		/// <param name="size">Size of the interval.</param>
		public static IImplicitShape<int> Segment(int start, int size)
		{
			return new SegmentShape(new GridInterval(start, size));
		}

		/// <summary>
		/// It creates a 1D Segment Shape with a given start and end point.
		/// </summary>
		/// <param name="start">Where the interval start.</param>
		/// <param name="end">Where the interval ends.</param>
		public static IImplicitShape<int> SegmentFromEndpoints(int start, int end)
		{
			return new SegmentShape(new GridInterval(start, end - start + 1));
		}

		/// <summary>
		/// It creates a 1D Translation Shape with a given offset.
		/// </summary>
		/// <param name="shape">Base shape used to make the translation.</param>
		/// <param name="offset">Offset to perform in the shape.</param>
		public static IImplicitShape<int> Translate(this IImplicitShape<int> shape, int offset)
		{
			return new TranslationShape1(shape, offset);
		}

		/// <summary>
		/// It creates a 1D Product Shape using another shape and a scale factor.
		/// </summary>
		/// <param name="shape1">Base shape to be process.</param>
		/// <param name="shape2">Shape used to process the shape1.</param>
		/// <param name="scale">Scale factor apply to the shape2 before making the product operation.</param>
		/// <returns></returns>
		public static IImplicitShape<int> Product(this IExplicitShape<int> shape1,
												  IImplicitShape<int> shape2, int scale)
		{
			return new ProductShape1(shape1, shape2, scale);
		}

		#endregion

		#region 2D Methods

		/// <summary>
		/// Creates a 2D Single Shape with a center value of (0,0)
		/// </summary>
		public static IImplicitShape<GridPoint2> Single2()
		{
			return new SingleShape2(GridPoint2.Zero);
		}

		/// <summary>
		/// Creates a 2D Single Shape with a given center value.
		/// </summary>
		/// <param name="point">Value for the center of the single shape.</param>
		public static IImplicitShape<GridPoint2> Single2(GridPoint2 point)
		{
			return new SingleShape2(point);
		}
		/*
		/// <summary>
		/// Creates a 2D Circle Shape.
		/// </summary>
		/// <param name="center">Center point of the shape.</param>
		/// <param name="radius">Radius for the sphere.</param>
		/// <param name="norm">This function is used to norm the points of the sphere.</param>		
		[Obsolete("Use the overload that uses Vector2 instead")]
		public static IImplicitShape<GridPoint2> Circle(GridPoint2 center, float radius, Func<GridPoint2, float> norm)
		{
			var roundMap = Map.RectRound();
			Func<Vector2, float> newNorm = (v) => norm(roundMap.Forward(v.To3DXY()));
			return new CircleShape2(center, radius, newNorm);
		}
		*/
		/// <summary>
		/// Creates a 2D Circle Shape.
		/// </summary>
		/// <param name="center">Center point of the shape.</param>
		/// <param name="radius">Radius for the sphere.</param>
		/// <param name="norm">This function is used to norm the points of the sphere.</param>
		public static IImplicitShape<GridPoint2> Circle(Vector2 center, float radius, Func<Vector2, float> norm)
		{
			return new CircleShape2(center, radius, norm);
		}
		/*
		/// <summary>
		/// Creates a 2D Circle Shape with center in (0,0)
		/// </summary>
		/// <param name="radius">Radius for the sphere.</param>
		/// <param name="norm">This function is used to norm the points of the sphere.</param>
		[Obsolete]
		public static IImplicitShape<GridPoint2> Circle(float radius, Func<GridPoint2, float> norm)
		{
			var roundMap = Map.RectRound();
			Func<Vector2, float> newNorm = (v) => norm(roundMap.Forward(v.To3DXY()));

			return new CircleShape2(Vector2.zero, radius, newNorm);
		}
		*/
		/// <summary>
		/// Creates a 2D Circle Shape with center in (0,0)
		/// </summary>
		/// <param name="radius">Radius for the sphere.</param>
		/// <param name="norm">This function is used to norm the points of the sphere.</param>
		public static IImplicitShape<GridPoint2> Circle(float radius, Func<Vector2, float> norm)
		{
			return new CircleShape2(Vector2.zero, radius, norm);
		}

		/// <summary>
		/// Creates a 2D Convex Polygon given a IEnumerable of vertices.
		/// </summary>
		/// <param name="vertices">Vertices that describe the polygon.</param>
		public static IImplicitShape<GridPoint2> ConvexPolygon(IEnumerable<GridPoint2> vertices)
		{
			return new ConvexPolygonShape2(vertices);
		}

		/// <summary>
		/// Creates a 2D Rect Diamond Shape using a Circle Shape with a Manhattan Norm.
		/// </summary>
		/// <param name="center">Center point of the shape.</param>
		/// <param name="radius">Radius for the sphere.</param>
		public static IImplicitShape<GridPoint2> RectDiamond(Vector2 center, float radius)
		{
			return Circle(center, radius, ManhattanNorm);
		}

		/// <summary>
		/// Creates a 2D Square Shape using a Circle Shape with a Chebychev norm.
		/// </summary>
		/// <param name="center">Center point of the shape.</param>
		/// <param name="radius">Radius for the sphere.</param>
		public static IImplicitShape<GridPoint2> Square(Vector2 center, float radius)
		{
			return Circle(center, radius, Chebychev);
		}

		/// <summary>
		/// Creates a 2D Square Shape using a Circle Shape with a Chebychev norm.
		/// </summary>
		/// <param name="radius">Radius for the sphere.</param>
		public static IImplicitShape<GridPoint2> Square(float radius)
		{
			return Circle(radius, Chebychev);
		}

		/// <summary>
		/// Creates a 2D RectDiamond Shape using a Circle Shape with a Manhattan Norm.
		/// </summary>
		/// <param name="radius">Radius for the sphere.</param>
		public static IImplicitShape<GridPoint2> RectDiamond(float radius)
		{
			return Circle(radius, ManhattanNorm);
		}

		/// <summary>
		/// Creates a 2D Down Triangle Shape using a Circle Shape with a DownTriangle Norm.
		/// </summary>
		/// <param name="center">Center point of the shape.</param>
		/// <param name="radius">Radius for the sphere.</param>
		public static IImplicitShape<GridPoint2> DownTriangle(Vector2 center, float radius)
		{
			return Circle(center, radius, DownTriangleNorm);
		}

		/// <summary>
		/// Creates a 2D Down Triangle Shape using a Down Triangle Norm.
		/// </summary>
		/// <param name="radius">Radius for the sphere.</param>
		public static IImplicitShape<GridPoint2> DownTriangle(float radius)
		{
			return Circle(radius, DownTriangleNorm);
		}

		/// <summary>
		/// Creates a 2D Up Triangle Shape using a Circle Shape with a UpTriangle Norm.
		/// </summary>
		/// <param name="center">Center point of the shape.</param>
		/// <param name="radius">Radius for the sphere.</param>
		public static IImplicitShape<GridPoint2> UpTriangle(Vector2 center, float radius)
		{
			return Circle(center, radius, UpTriangleNorm);
		}

		/// <summary>
		/// Creates a 2D UpTriangle shape.
		/// </summary>
		/// <param name="radius">Radius for the sphere.</param>
		public static IImplicitShape<GridPoint2> UpTriangle(float radius)
		{
			return Circle(radius, UpTriangleNorm);
		}

		[Version(2, 2)]
		public static IImplicitShape<GridPoint2> HexFatRectangle(GridPoint2 dimensions)
		{
			return new FunctionShape<GridPoint2>(point =>
				point.X >= -GLMathf.FloorDiv(point.Y, 2) - GLMathf.FloorMod(point.Y, 2) &&
				point.X < -GLMathf.FloorDiv(point.Y, 2) + dimensions.X &&
				point.Y >= 0 &&
				point.Y < dimensions.Y);
		}

		[Version(2, 2)]
		public static IImplicitShape<GridPoint2> HexRectangle(GridPoint2 dimensions)
		{
			return new FunctionShape<GridPoint2>(point =>
				point.Y >= 0 &&
				point.Y < dimensions.Y &&
				point.X >= -GLMathf.FloorDiv(point.Y, 2) &&
				point.X < -GLMathf.FloorDiv(point.Y, 2) + dimensions.X);
		}

		[Version(2, 2)]
		public static IImplicitShape<GridPoint2> HexThinRectangle(GridPoint2 dimensions)
		{
			return new FunctionShape<GridPoint2>(point =>
				point.X >= -GLMathf.FloorDiv(point.Y, 2) &&
				point.X + GLMathf.FloorMod(point.Y, 2) < -GLMathf.FloorDiv(point.Y, 2) + dimensions.X &&
				point.Y >= 0 &&
				point.Y < dimensions.Y);
		}

		[Version(2, 2)]
		public static IImplicitShape<GridPoint2> FlatHexFatRectangle(GridPoint2 dimensions)
		{
			return new FunctionShape<GridPoint2>(point =>
				point.Y >= -(GLMathf.FloorDiv(point.X, 2)) - GLMathf.FloorMod(point.X, 2) &&
				point.Y < -(GLMathf.FloorDiv(point.X, 2)) + dimensions.Y &&
				point.X >= 0 &&
				point.X < dimensions.X);
		}

		[Version(2, 2)]
		public static IImplicitShape<GridPoint2> FlatHexRectangle(GridPoint2 dimensions)
		{
			return new FunctionShape<GridPoint2>(point =>
				point.Y >= -(GLMathf.FloorDiv(point.X, 2)) &&
				point.Y < -(GLMathf.FloorDiv(point.X, 2)) + dimensions.Y &&
				point.X >= 0 &&
				point.X < dimensions.X);
		}

		[Version(2, 2)]
		public static IImplicitShape<GridPoint2> FlatHexThinRectangle(GridPoint2 dimensions)
		{
			return new FunctionShape<GridPoint2>(point =>
				point.Y >= -(GLMathf.FloorDiv(point.X, 2)) &&
				point.Y + GLMathf.FloorMod(point.X, 2) < -(GLMathf.FloorDiv(point.X, 2)) + dimensions.Y &&
				point.X >= 0 &&
				point.X < dimensions.X);
		}

		/// <summary>
		/// Creates a 2D Translate Shape.
		/// </summary>
		/// <param name="shape">Base Shape to translate.</param>
		/// <param name="offset">Offset of the translation.</param>
		public static IImplicitShape<GridPoint2> Translate(this IImplicitShape<GridPoint2> shape, GridPoint2 offset)
		{
			return new TranslationShape2(shape, offset);
		}

		/// <summary>
		/// It creates a 2D Intersect Shape.
		/// </summary>
		/// <param name="shapes">List of shapes that intersect each other.</param>
		public static IImplicitShape<GridPoint2> Intersect(IEnumerable<IImplicitShape<GridPoint2>> shapes)
		{
			return new IntersectionShape(shapes);
		}

		/// <summary>
		/// It creates a 2D Union Shape.
		/// </summary>
		/// <param name="shapes">List of shapes that can be united.</param>
		public static IImplicitShape<GridPoint2> Union(IEnumerable<IImplicitShape<GridPoint2>> shapes)
		{
			return new UnionShape<GridPoint2>(shapes);
		}

		/// <summary>
		/// It creates a 2D Product Shape.
		/// </summary>
		/// <param name="shape1">Base shape to make the product operation.</param>
		/// <param name="shape2">Shape used for the product operation.</param>
		/// <param name="scale">Scale factor applied to the shape2.</param>
		public static IImplicitShape<GridPoint2> Product(this IExplicitShape<GridPoint2> shape1,
			IImplicitShape<GridPoint2> shape2, GridPoint2 scale)
		{
			return new ProductShape2(shape1, shape2, scale);
		}

		/// <summary>
		/// It creates a 2D HalfPlane Shape given two points.
		/// </summary>
		/// <param name="point1">Base point 1 of the Plane.</param>
		/// <param name="point2">Base point 2 of the Plane.</param>
		public static IImplicitShape<GridPoint2> HalfPlane(GridPoint2 point1, GridPoint2 point2)
		{
			return new Halfplane(point1, point2);
		}

		/// <summary>
		/// It creates a 2D Hexagon Shape using a Circle Shape with a Hex Norm.
		/// </summary>
		/// <param name="center">Center point of the Hexagon.</param>
		/// <param name="radius">Radius of the shape.</param>
		public static IImplicitShape<GridPoint2> Hexagon(Vector2 center, float radius)
		{
			return Circle(center, radius, HexNorm);
		}

		/// <summary>
		/// It creates a 2D Hexagon Shape using a Circle Shape with a Hex Norm.
		/// </summary>
		/// <param name="radius">Radius of the shape.</param>
		public static IImplicitShape<GridPoint2> Hexagon(float radius)
		{
			return Circle(radius, HexNorm);
		}

		/// <summary>
		/// It creates a 2D Parallelogram Shape.
		/// </summary>
		/// <param name="dimensions">Dimensions of the shape.</param>
		public static IImplicitShape<GridPoint2> Parallelogram(GridPoint2 dimensions)
		{
			return new ParallelogramShape(dimensions);
		}

		/// <summary>
		/// It creates a 2D PointListShape with a list of given points.
		/// </summary>
		/// <param name="points">List of points that conform the shape.</param>
		public static IImplicitShape<GridPoint2> List(IEnumerable<GridPoint2> points)
		{
			return new PointListShape<GridPoint2>(points);
		}

		/// <summary>
		/// It creates a 2D Star Shape using a Circle Shape with a Star Norm.
		/// </summary>
		/// <param name="center">Center point of the shape.</param>
		/// <param name="radius">Radius of the shape.</param>
		public static IImplicitShape<GridPoint2> Star(Vector2 center, float radius)
		{
			return Circle(center, radius, StarNorm);
		}

		/// <summary>
		/// It creates a 2D Star Shape using a Circle Shape with a Star Norm.
		/// </summary>
		/// <param name="radius">Radius of the shape.</param>
		public static IImplicitShape<GridPoint2> Star(float radius)
		{
			return Circle(radius, StarNorm);
		}

		#endregion

		#region 3D Methods

		/// <summary>
		/// It creates a 3D Single Shape with a center point of (0, 0, 0)
		/// </summary>
		public static IImplicitShape<GridPoint3> Single3()
		{
			return new SingleShape3(GridPoint3.Zero);
		}

		/// <summary>
		/// It creates a 3D Single Shape.
		/// </summary>
		/// <param name="point">Point value of the center.</param>
		public static IImplicitShape<GridPoint3> Single1(GridPoint3 point)
		{
			return new SingleShape3(point);
		}

		/// <summary>
		/// It creates a 3D Parallelepiped.
		/// </summary>
		/// <param name="dimensions">Dimensions of the shape.</param>
		public static IImplicitShape<GridPoint3> Parallelepiped(GridPoint3 dimensions)
		{
			return new ParallelepipedShape(dimensions);
		}

		/// <summary>
		/// It creates a 3D Sphere.
		/// </summary>
		/// <param name="radius">Radius of shape.</param>
		public static IImplicitShape<GridPoint3> Sphere(float radius)
		{
			return new SphereShape(radius);
		}

		/// <summary>
		/// It creates a 3D Translate Shape.
		/// </summary>
		/// <param name="shape">Base shape to translate.</param>
		/// <param name="offset">Offset of the movement of the shape.</param>
		public static IImplicitShape<GridPoint3> Translate(this IImplicitShape<GridPoint3> shape, GridPoint3 offset)
		{
			return new TranslationShape3(shape, offset);
		}

		public static IImplicitShape<GridPoint3> Product(this IExplicitShape<GridPoint3> shape1,
												  IImplicitShape<GridPoint3> shape2, GridPoint3 scale)
		{
			return new ProductShape3(shape1, shape2, scale);
		}

		[Version(2, 2)]
		public static IImplicitShape<GridPoint3> Layer(this IImplicitShape<GridPoint2> shape, int layerCount)
		{
			return new SimpleLayerShape(shape, layerCount);
		}

		[Version(2, 2)]
		public static IImplicitShape<GridPoint3> Layer(IEnumerable<IImplicitShape<GridPoint2>> shapes)
		{
			return new MultiLayeredShape(shapes);
		}

		public static IImplicitShape<GridPoint2> SwapXY(this IImplicitShape<GridPoint2> shape)
		{
			return new FunctionShape<GridPoint2>(p => shape.Contains(new GridPoint2(p.Y, p.X)));
		}

		[Experimental]
		public static IImplicitShape<GridPoint3> SwapYZ(this IImplicitShape<GridPoint3> shape)
		{
			return new FunctionShape<GridPoint3>(p => shape.Contains(new GridPoint3(p.X, p.Z, p.Y)));
		}

		[Experimental]
		public static IImplicitShape<GridPoint3> SwapToXZY(this IImplicitShape<GridPoint3> shape)
		{
			return new FunctionShape<GridPoint3>(p => shape.Contains(new GridPoint3(p.X, p.Z, p.Y)));
		}

		[Experimental]
		public static IImplicitShape<GridPoint3> SwapToYXZ(this IImplicitShape<GridPoint3> shape)
		{
			return new FunctionShape<GridPoint3>(p => shape.Contains(new GridPoint3(p.Y, p.X, p.Z)));
		}

		[Experimental]
		public static IImplicitShape<GridPoint3> SwapToYZX(this IImplicitShape<GridPoint3> shape)
		{
			return new FunctionShape<GridPoint3>(p => shape.Contains(new GridPoint3(p.Y, p.Z, p.X)));
		}

		[Experimental]
		public static IImplicitShape<GridPoint3> SwapToZXY(this IImplicitShape<GridPoint3> shape)
		{
			return new FunctionShape<GridPoint3>(p => shape.Contains(new GridPoint3(p.Z, p.X, p.Y)));
		}

		[Experimental]
		public static IImplicitShape<GridPoint3> SwapToZYX(this IImplicitShape<GridPoint3> shape)
		{
			return new FunctionShape<GridPoint3>(p => shape.Contains(new GridPoint3(p.Z, p.Y, p.X)));
		}

		#endregion

		#region Generic Methods

		/// <summary>
		/// Creates a shape from a predicate.
		/// </summary>
		/// <typeparam name="TPoint">Type of the point.</typeparam>
		/// <param name="predicate">Function to store in the shape.</param>
		public static IImplicitShape<TPoint> Func<TPoint>(Func<TPoint, bool> predicate)
		{
			return new FunctionShape<TPoint>(predicate);
		}

		[Version(2, 3)]
		public static IImplicitShape<TPoint> ReverseSelect<TPoint>(this IImplicitShape<TPoint> shape, Func<TPoint, TPoint> projection)
		{
			return Func<TPoint>(p => shape.Contains(projection(p)));
		}

		/// <summary>
		/// It creates a generic Function Shape that works as a Generic Union.
		/// </summary>
		/// <typeparam name="TPoint">Type of the point.</typeparam>
		/// <param name="shape1">First shape.</param>
		/// <param name="shape2">Second shape.</param>
		public static IImplicitShape<TPoint> Union<TPoint>(IImplicitShape<TPoint> shape1, IImplicitShape<TPoint> shape2)
		{
			return new FunctionShape<TPoint>(p => shape1.Contains(p) || shape2.Contains(p));
		}

		/// <summary>
		/// It creates a generic Function Shape that works as a Generic Union.
		/// </summary>
		/// <typeparam name="TPoint">Type of the point.</typeparam>
		/// <param name="shapes">IEnumerable list of shapes.</param>
		public static IImplicitShape<TPoint> Union<TPoint>(IEnumerable<IImplicitShape<TPoint>> shapes)
		{
			return new FunctionShape<TPoint>(p => shapes.Any(s => s.Contains(p)));
		}

		/// <summary>
		/// It creates a generic Function Shape that works as a Generic Intersection.
		/// </summary>
		/// <typeparam name="TPoint">The type of the point.</typeparam>
		/// <param name="shapes">IEnumerable list of shapes to intersect.</param>
		public static IImplicitShape<TPoint> Intersection<TPoint>(IEnumerable<IImplicitShape<TPoint>> shapes)
		{
			return new FunctionShape<TPoint>(p => shapes.All(s => s.Contains(p)));
		}

		/// <summary>
		/// It creates a generic Function Shape that works as a Generic Intersection.
		/// </summary>
		/// <typeparam name="TPoint">The type of the point.</typeparam>
		/// <param name="shape1">First Shape.</param>
		/// <param name="shape2">Second Shape.</param>
		public static IImplicitShape<TPoint> Intersection<TPoint>(IImplicitShape<TPoint> shape1, IImplicitShape<TPoint> shape2)
		{
			return new FunctionShape<TPoint>(p => shape1.Contains(p) && shape2.Contains(p));
		}

		/// <summary>
		/// It creates a generic Function Shape that works as a Generic Transform.
		/// </summary>
		/// <typeparam name="TPoint">The type of the point.</typeparam>
		/// <param name="shape">Shape to transform.</param>
		/// <param name="transform">Reverse map to apply into the shape.</param>
		/// <returns></returns>
		public static IImplicitShape<TPoint> Transform<TPoint>(
			IImplicitShape<TPoint> shape,
			IReverseMap<TPoint, TPoint> transform)
		{
			return new FunctionShape<TPoint>(p => shape.Contains(transform.Reverse(p)));
		}

		/// <summary>
		/// It create a generic Function Shape that works as a Generic Inverse.
		/// </summary>
		/// <typeparam name="TPoint">Type of the point.</typeparam>
		/// <param name="shape">Shape to apply the inverse operation.</param>
		public static IImplicitShape<TPoint> Inverse<TPoint>(IImplicitShape<TPoint> shape)
		{
			return new FunctionShape<TPoint>(p => !shape.Contains(p));
		}

		/// <summary>
		/// It creates a generic Function Shape that works as a Generic Where.
		/// </summary>
		/// <typeparam name="TPoint">Type of the point.</typeparam>
		/// <param name="shape">Shape to apply the Where operation.</param>
		/// <param name="predicate">Predicate that indicate the where condition.</param>
		public static IImplicitShape<TPoint> Where<TPoint>(IImplicitShape<TPoint> shape, Func<TPoint, bool> predicate)
		{
			return new FunctionShape<TPoint>(p => shape.Contains(p) && predicate(p));
		}

		#endregion

		#endregion
	}
}