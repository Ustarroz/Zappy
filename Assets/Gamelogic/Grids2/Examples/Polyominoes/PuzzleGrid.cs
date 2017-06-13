using System;
using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2
{
	public class PuzzleGrid<T>
	{
		private readonly IGrid<GridPoint2, T> grid;
		private readonly IGrid<GridPoint2, IExplicitShape<GridPoint2>> shapes;

		public T this[GridPoint2 point]
		{
			get { return grid[point]; }
		}

		public IExplicitShape<GridPoint2> GetShape(GridPoint2 point)
		{
			return shapes[point];
		}

		public IEnumerable<GridPoint2> Points
		{
			get { return grid.Points; }
		}

		public IEnumerable<GridPoint2> EmptyPoints
		{
			get { return grid.Points.Where(IsEmpty); }
		}

		public IEnumerable<GridPoint2> FilledPoints
		{
			get { return grid.Points.Where(p => !IsEmpty(p)); }
		}

		public PuzzleGrid(IExplicitShape<GridPoint2> gridShape)
		{
			grid = gridShape.ToGrid<T>();
			grid.Fill(default(T));

			shapes = gridShape.ToGrid<IExplicitShape<GridPoint2>>();
			shapes.Fill((IExplicitShape<GridPoint2>)null);
		}

		public bool Contains(IExplicitShape<GridPoint2> shape, GridPoint2 point)
		{
			var offsetShape = shape.Translate(point);

			return offsetShape.Points.All(p => grid.Contains(p));
		}

		public bool IsEmpty(GridPoint2 point)
		{
			return shapes[point] == null;
		}

		public bool IsEmpty(IExplicitShape<GridPoint2> shape, GridPoint2 point)
		{
			var offsetShape = shape.Translate(point);

			return offsetShape.Points.All(p => shapes[p] == null);
		}

		public IExplicitShape<GridPoint2> Place(IExplicitShape<GridPoint2> shape, GridPoint2 point, T item)
		{
			if (!Contains(shape, point)) throw new InvalidOperationException("Shape is not completely in grid.");
			if (!IsEmpty(shape, point)) throw new InvalidOperationException("Shape is not completely empty.");

			var offsetShape = shape.Translate(point);

			foreach (var shapePoint in offsetShape.Points)
			{
				grid[shapePoint] = item;
				shapes[shapePoint] = offsetShape;
			}

			return offsetShape;
		}

		public void Remove(GridPoint2 point)
		{
			if (grid.Contains(point))
			{
				var shape = shapes[point];

				if (shape != null)
				{
					foreach (var shapePoint in shape.Points)
					{
						grid[shapePoint] = default(T);
						shapes[shapePoint] = null;
					}
				}
			}
		}
	}

	public class TightShape2 : IExplicitShape<GridPoint2>
	{
		private IExplicitShape<GridPoint2> shape;

		public TightShape2(IExplicitShape<GridPoint2> shape)
		{
			InitTightShape(shape);
		}

		private void InitTightShape(IExplicitShape<GridPoint2> looseShape)
		{
			var newBounds = ExplicitShape.GetBounds(looseShape.Points);

			shape = looseShape.ToExplicit(newBounds);
		}

		private TightShape2(IExplicitShape<GridPoint2> shape, bool alreadyTight)
		{
			if (alreadyTight)
			{
				this.shape = shape;
			}
			else
			{
				InitTightShape(shape);
			}
		}

		public bool Contains(GridPoint2 point)
		{
			return shape.Contains(point);
		}

		public IEnumerable<GridPoint2> Points
		{
			get { return shape.Points; }
		}

		public AbstractBounds<GridPoint2> Bounds
		{
			get { return shape.Bounds; }
		}

		public bool IsEqualUpToTranslation(TightShape2 other)
		{
			var shape1 = ToCanonicalPosition();
			var shape2 = other.ToCanonicalPosition();

			return shape1.Points.All(shape2.Contains) && shape2.Points.All(shape1.Contains);
		}

		public TightShape2 Transform(IMap<GridPoint2, GridPoint2> transformation)
		{
			var newPoints = Points.Select<GridPoint2, GridPoint2>(transformation.Forward);
			var newBounds = ExplicitShape.GetBounds(newPoints);

			var newShape = shape.ReverseSelect(transformation.Reverse).ToExplicit(newBounds);

			return new TightShape2(newShape, true);
		}

		public bool IsEqualUpToTransformations(TightShape2 otherShape, IEnumerable<IMap<GridPoint2, GridPoint2>> transformations)
		{
			return transformations
				.Select<IMap<GridPoint2, GridPoint2>, TightShape2>(otherShape.Transform)
				.Any(IsEqualUpToTranslation);
		}

		public TightShape2 ToCanonicalPosition()
		{
			var newShape = shape.Translate(-shape.Bounds.Point);

			return new TightShape2(newShape, true);
		}
	}
}