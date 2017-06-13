//----------------------------------------------//
// Gamelogic Grids                              //
// http://www.gamelogic.co.za                   //
// Copyright (c) 2013 Gamelogic (Pty) Ltd       //
//----------------------------------------------//

// Auto-generated File

using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids
{
	public partial struct RectPoint
	{
		#region Constants

		/// <summary>
		/// The zero point (0, 0).
		/// </summary>
		public static readonly RectPoint Zero = new RectPoint(0, 0);

		#endregion

		#region Fields

		//private readonly VectorPoint vector;
		private readonly int x;
		private readonly int y;

		#endregion

		#region Properties

		/// <summary>
		/// The x-coordinate of this point. This need to be in XML
		/// </summary>
		public int X
		{
			get
			{
				return x;
			}
		}

		/// <summary>
		/// The y-coordinate of this point.
		/// </summary>
		public int Y
		{
			get
			{
				return y;
			}
		}

		public int SpliceIndex
		{
			get 
			{
				return 0;
			}
		}

		public int SpliceCount
		{	
			get 
			{
				return 1; 
			}
		}

		/// <summary>
		/// A Uniform point's base point is simply the point itself.
		///	Makes it easier to implement generic algorithms.
		/// Since version 1.1
		/// </summary>
		public RectPoint BasePoint
		{
			get
			{
				return this;
			}
		}
		#endregion

		#region Construction

		/// <summary>
		/// Constructs a new RectPoint with the given coordinates.
		/// </summary>
		public RectPoint(int x, int y):
			this(new VectorPoint(x, y))
		{
		}

		/// <summary>
		/// Constructs a new RectPoint with the same coordinates as the given VectorPoint.
		/// </summary>
		private RectPoint(VectorPoint vector)
		{
			x = vector.X;
			y = vector.Y;
		}
		#endregion

		#region Distance

		/// <summary>
		/// The lattice distance from this point to the other.
		/// </summary>
		public int DistanceFrom(RectPoint other)
		{
			return Subtract(other).Magnitude();
		}

		#endregion

		#region Equality
		public bool Equals(RectPoint other)
		{
			bool areEqual = (x == other.X) && (y == other.Y);
			return areEqual;
		}

		public override bool Equals (object other)
		{
			if(other.GetType() != typeof(RectPoint))
			{
				return false;
			}

			var point = (RectPoint) other;
			return Equals(point);
		}
	
		public override int GetHashCode ()
		{
			return x ^ y;
		}	
		#endregion

		#region Arithmetic

		/// <summary>
		/// This is a norm defined on the point, such that `p1.Difference(p2).Abs()` is equal to 
		///	`p1.DistanceFrom(p2)`.
		/// </summary>
		public RectPoint Translate(RectPoint translation)
		{
			return new RectPoint(x + translation.X, y + translation.Y);
		}

		public RectPoint Negate()
		{
			return new RectPoint(-x, -y);
		}

		public RectPoint ScaleDown(int r)
		{
			return new RectPoint(GLMathf.FloorDiv(x, r), GLMathf.FloorDiv(y, r));
		}

		public RectPoint ScaleUp(int r)
		{
			return new RectPoint(x * r, y * r);
		}

		/// <summary>
		/// Subtracts the other point from this point, and returns the result.
		/// </summary>
		public RectPoint Subtract(RectPoint other)
		{
			return new RectPoint(x - other.X, y - other.Y);
		}

		public RectPoint MoveBy(RectPoint translation)
		{
			return Translate(translation);
		}

		public RectPoint MoveBackBy(RectPoint translation)
		{
			return Translate(translation.Negate());
		}

		[Version(1,7)]
		public int Dot(RectPoint other)
		{
			return x * other.X + y * other.Y;
		}

		[Version(1,7)]
		public int PerpDot(RectPoint other)
		{
			return x * other.Y - y * other.x;
		}

		[Version(1,10)]
		public RectPoint Perp()
		{
			return new RectPoint(-y, x);
		}

		/// <summary>
		/// Gives a new point that represents the 
		///	reminder when the first point is divided
		///	by the second point	component-wise. The
		///	division is integer division.
		///
		///	Since version 1.6 (Rect)
		///	Since version 1.7 (other)
		/// </summary>
		public RectPoint Mod(RectPoint otherPoint)
		{
			var x = GLMathf.FloorMod(X, otherPoint.X);
			var y = GLMathf.FloorMod(Y, otherPoint.Y);

			return new RectPoint(x, y);
		}

		/// <summary>
		/// Gives a new point that represents the 
		///	first point divided by the second point
		///	component-wise. The division is integer
		///	division.
		///
		///	Since version 1.6 (Rect)
		///	Since version 1.7 (other)
		/// </summary>
		public RectPoint Div(RectPoint otherPoint)
		{
			var x = GLMathf.FloorDiv(X, otherPoint.X);
			var y = GLMathf.FloorDiv(Y, otherPoint.Y);

			return new RectPoint(x, y);
		}

		/// <summary>
		/// Gives a new point that represents the 
		///	first point multiplied by the second point
		///	component-wise. 
		///
		///	Since version 1.6 (Rect)
		///	Since version 1.7 (other)
		/// </summary>
		public RectPoint Mul(RectPoint otherPoint)
		{
			var x = X * otherPoint.X;
			var y = Y * otherPoint.Y;

			return new RectPoint(x, y);
		}
		#endregion 

		#region Utility
		public override string ToString()
		{
			return "(" + x + ", " + y + ")";
		}
		#endregion

		#region Operators
		public static bool operator ==(RectPoint point1, RectPoint point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(RectPoint point1, RectPoint point2)
		{
			return !point1.Equals(point2);
		}

		public static RectPoint operator +(RectPoint point)
		{
			return point;
		}

		public static RectPoint operator -(RectPoint point)
		{
			return point.Negate();
		}

		public static RectPoint operator +(RectPoint point1, RectPoint point2)
		{
			return point1.Translate(point2);
		}

		public static RectPoint operator -(RectPoint point1, RectPoint point2)
		{
			return point1.Subtract(point2);
		}

		public static RectPoint operator *(RectPoint point, int n)
		{
			return point.ScaleUp(n);
		}

		public static RectPoint operator /(RectPoint point, int n)
		{
			return point.ScaleDown(n);
		}

		public static RectPoint operator *(RectPoint point1, RectPoint point2)
		{
			return point1.Mul(point2);
		}

		public static RectPoint operator /(RectPoint point1, RectPoint point2)
		{
			return point1.Div(point2);
		}

		public static RectPoint operator %(RectPoint point1, RectPoint point2)
		{
			return point1.Mod(point2);
		}

		#endregion

		#region Colorings

		/// <summary>
		/// Gives a coloring of the grid such that 
		///	if a point p has color k, then all points
		///	p + m[ux, 0] + n[vx, vy] have the same color
		///	for any integers a and b.
		///
		///	More information anout grid colorings:
		///	http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/
		///
		///	Since version 1.7
		/// </summary>
		public int __GetColor__ReferenceImplementation(int ux, int vx, int vy)
		{
			var u = new RectPoint(ux, 0);
			var v = new RectPoint(vx, vy);

			int colorCount = u.PerpDot(v);
			
			float a = PerpDot(v) / (float) colorCount;
			float b = -PerpDot(u) / (float) colorCount;

			int m = Mathf.FloorToInt(a);
			int n = Mathf.FloorToInt(b);

			int baseVectorX = m*u.X + n*v.X;
			int baseVectorY = n*u.Y + n*v.Y;
				
			int offsetX = GLMathf.FloorMod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathf.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}

		/// <summary>
		/// Gives a coloring of the grid such that 
		///	if a point p has color k, then all points
		///	p + m[ux, 0] + n[vx, vy] have the same color
		///	for any integers a and b.
		///
		///	More information anout grid colorings:
		///	http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/
		///
		///	Since version 1.7
		/// </summary>
		public int GetColor(int ux, int vx, int vy)
		{
			int colorCount = ux * vy;

			float a = (x * vy - y * vx) / (float)colorCount;
			float b = (y * ux) / (float)colorCount;

			int m = Mathf.FloorToInt(a);
			int n = Mathf.FloorToInt(b);

			int baseVectorX = m * ux + n * vx;
			int baseVectorY = n * vy;

			int offsetX = GLMathf.FloorMod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathf.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}

		#endregion
	}

	#region Wrappers

	/// <summary>
	/// Wraps points both horizontally and vertically.
	///
	///	Since version 1.7
	/// </summary>
	[Experimental]
	public class RectParallelogramWrapper : IPointWrapper<RectPoint>
	{
		readonly int width;
		readonly int height;

		public RectParallelogramWrapper(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		public RectPoint Wrap(RectPoint point)
		{
			return new RectPoint(GLMathf.FloorMod(point.X, width), GLMathf.FloorMod(point.Y, height));
		}
	}

	/// <summary>
	/// Wraps points horizontally.
	///
	///	Since version 1.7
	/// </summary>
	[Experimental]
	public class RectHorizontalWrapper : IPointWrapper<RectPoint>
	{
		readonly int width;

		public RectHorizontalWrapper(int width)
		{
			this.width = width;
		}

		public RectPoint Wrap(RectPoint point)
		{
			return new RectPoint(GLMathf.FloorMod(point.X, width), point.Y);
		}
	}

	/// <summary>
	/// Wraps points vertically.
	///
	///	Since version 1.7
	/// </summary>
	[Experimental]
	public class RectVerticalWrapper : IPointWrapper<RectPoint>
	{
		readonly int height;

		public RectVerticalWrapper(int height)
		{
			this.height = height;
		}

		public RectPoint Wrap(RectPoint point)
		{
			return new RectPoint(point.X, GLMathf.FloorMod(point.Y, height));
		}
	}

	#endregion 
	public partial struct DiamondPoint
	{
		#region Constants

		/// <summary>
		/// The zero point (0, 0).
		/// </summary>
		public static readonly DiamondPoint Zero = new DiamondPoint(0, 0);

		#endregion

		#region Fields

		//private readonly VectorPoint vector;
		private readonly int x;
		private readonly int y;

		#endregion

		#region Properties

		/// <summary>
		/// The x-coordinate of this point. This need to be in XML
		/// </summary>
		public int X
		{
			get
			{
				return x;
			}
		}

		/// <summary>
		/// The y-coordinate of this point.
		/// </summary>
		public int Y
		{
			get
			{
				return y;
			}
		}

		public int SpliceIndex
		{
			get 
			{
				return 0;
			}
		}

		public int SpliceCount
		{	
			get 
			{
				return 1; 
			}
		}

		/// <summary>
		/// A Uniform point's base point is simply the point itself.
		///	Makes it easier to implement generic algorithms.
		/// Since version 1.1
		/// </summary>
		public DiamondPoint BasePoint
		{
			get
			{
				return this;
			}
		}
		#endregion

		#region Construction

		/// <summary>
		/// Constructs a new DiamondPoint with the given coordinates.
		/// </summary>
		public DiamondPoint(int x, int y):
			this(new VectorPoint(x, y))
		{
		}

		/// <summary>
		/// Constructs a new DiamondPoint with the same coordinates as the given VectorPoint.
		/// </summary>
		private DiamondPoint(VectorPoint vector)
		{
			x = vector.X;
			y = vector.Y;
		}
		#endregion

		#region Distance

		/// <summary>
		/// The lattice distance from this point to the other.
		/// </summary>
		public int DistanceFrom(DiamondPoint other)
		{
			return Subtract(other).Magnitude();
		}

		#endregion

		#region Equality
		public bool Equals(DiamondPoint other)
		{
			bool areEqual = (x == other.X) && (y == other.Y);
			return areEqual;
		}

		public override bool Equals (object other)
		{
			if(other.GetType() != typeof(DiamondPoint))
			{
				return false;
			}

			var point = (DiamondPoint) other;
			return Equals(point);
		}
	
		public override int GetHashCode ()
		{
			return x ^ y;
		}	
		#endregion

		#region Arithmetic

		/// <summary>
		/// This is a norm defined on the point, such that `p1.Difference(p2).Abs()` is equal to 
		///	`p1.DistanceFrom(p2)`.
		/// </summary>
		public DiamondPoint Translate(DiamondPoint translation)
		{
			return new DiamondPoint(x + translation.X, y + translation.Y);
		}

		public DiamondPoint Negate()
		{
			return new DiamondPoint(-x, -y);
		}

		public DiamondPoint ScaleDown(int r)
		{
			return new DiamondPoint(GLMathf.FloorDiv(x, r), GLMathf.FloorDiv(y, r));
		}

		public DiamondPoint ScaleUp(int r)
		{
			return new DiamondPoint(x * r, y * r);
		}

		/// <summary>
		/// Subtracts the other point from this point, and returns the result.
		/// </summary>
		public DiamondPoint Subtract(DiamondPoint other)
		{
			return new DiamondPoint(x - other.X, y - other.Y);
		}

		public DiamondPoint MoveBy(DiamondPoint translation)
		{
			return Translate(translation);
		}

		public DiamondPoint MoveBackBy(DiamondPoint translation)
		{
			return Translate(translation.Negate());
		}

		[Version(1,7)]
		public int Dot(DiamondPoint other)
		{
			return x * other.X + y * other.Y;
		}

		[Version(1,7)]
		public int PerpDot(DiamondPoint other)
		{
			return x * other.Y - y * other.x;
		}

		[Version(1,10)]
		public DiamondPoint Perp()
		{
			return new DiamondPoint(-y, x);
		}

		/// <summary>
		/// Gives a new point that represents the 
		///	reminder when the first point is divided
		///	by the second point	component-wise. The
		///	division is integer division.
		///
		///	Since version 1.6 (Rect)
		///	Since version 1.7 (other)
		/// </summary>
		public DiamondPoint Mod(DiamondPoint otherPoint)
		{
			var x = GLMathf.FloorMod(X, otherPoint.X);
			var y = GLMathf.FloorMod(Y, otherPoint.Y);

			return new DiamondPoint(x, y);
		}

		/// <summary>
		/// Gives a new point that represents the 
		///	first point divided by the second point
		///	component-wise. The division is integer
		///	division.
		///
		///	Since version 1.6 (Rect)
		///	Since version 1.7 (other)
		/// </summary>
		public DiamondPoint Div(DiamondPoint otherPoint)
		{
			var x = GLMathf.FloorDiv(X, otherPoint.X);
			var y = GLMathf.FloorDiv(Y, otherPoint.Y);

			return new DiamondPoint(x, y);
		}

		/// <summary>
		/// Gives a new point that represents the 
		///	first point multiplied by the second point
		///	component-wise. 
		///
		///	Since version 1.6 (Rect)
		///	Since version 1.7 (other)
		/// </summary>
		public DiamondPoint Mul(DiamondPoint otherPoint)
		{
			var x = X * otherPoint.X;
			var y = Y * otherPoint.Y;

			return new DiamondPoint(x, y);
		}
		#endregion 

		#region Utility
		public override string ToString()
		{
			return "(" + x + ", " + y + ")";
		}
		#endregion

		#region Operators
		public static bool operator ==(DiamondPoint point1, DiamondPoint point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(DiamondPoint point1, DiamondPoint point2)
		{
			return !point1.Equals(point2);
		}

		public static DiamondPoint operator +(DiamondPoint point)
		{
			return point;
		}

		public static DiamondPoint operator -(DiamondPoint point)
		{
			return point.Negate();
		}

		public static DiamondPoint operator +(DiamondPoint point1, DiamondPoint point2)
		{
			return point1.Translate(point2);
		}

		public static DiamondPoint operator -(DiamondPoint point1, DiamondPoint point2)
		{
			return point1.Subtract(point2);
		}

		public static DiamondPoint operator *(DiamondPoint point, int n)
		{
			return point.ScaleUp(n);
		}

		public static DiamondPoint operator /(DiamondPoint point, int n)
		{
			return point.ScaleDown(n);
		}

		public static DiamondPoint operator *(DiamondPoint point1, DiamondPoint point2)
		{
			return point1.Mul(point2);
		}

		public static DiamondPoint operator /(DiamondPoint point1, DiamondPoint point2)
		{
			return point1.Div(point2);
		}

		public static DiamondPoint operator %(DiamondPoint point1, DiamondPoint point2)
		{
			return point1.Mod(point2);
		}

		#endregion

		#region Colorings

		/// <summary>
		/// Gives a coloring of the grid such that 
		///	if a point p has color k, then all points
		///	p + m[ux, 0] + n[vx, vy] have the same color
		///	for any integers a and b.
		///
		///	More information anout grid colorings:
		///	http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/
		///
		///	Since version 1.7
		/// </summary>
		public int __GetColor__ReferenceImplementation(int ux, int vx, int vy)
		{
			var u = new DiamondPoint(ux, 0);
			var v = new DiamondPoint(vx, vy);

			int colorCount = u.PerpDot(v);
			
			float a = PerpDot(v) / (float) colorCount;
			float b = -PerpDot(u) / (float) colorCount;

			int m = Mathf.FloorToInt(a);
			int n = Mathf.FloorToInt(b);

			int baseVectorX = m*u.X + n*v.X;
			int baseVectorY = n*u.Y + n*v.Y;
				
			int offsetX = GLMathf.FloorMod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathf.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}

		/// <summary>
		/// Gives a coloring of the grid such that 
		///	if a point p has color k, then all points
		///	p + m[ux, 0] + n[vx, vy] have the same color
		///	for any integers a and b.
		///
		///	More information anout grid colorings:
		///	http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/
		///
		///	Since version 1.7
		/// </summary>
		public int GetColor(int ux, int vx, int vy)
		{
			int colorCount = ux * vy;

			float a = (x * vy - y * vx) / (float)colorCount;
			float b = (y * ux) / (float)colorCount;

			int m = Mathf.FloorToInt(a);
			int n = Mathf.FloorToInt(b);

			int baseVectorX = m * ux + n * vx;
			int baseVectorY = n * vy;

			int offsetX = GLMathf.FloorMod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathf.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}

		#endregion
	}

	#region Wrappers

	/// <summary>
	/// Wraps points both horizontally and vertically.
	///
	///	Since version 1.7
	/// </summary>
	[Experimental]
	public class DiamondParallelogramWrapper : IPointWrapper<DiamondPoint>
	{
		readonly int width;
		readonly int height;

		public DiamondParallelogramWrapper(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		public DiamondPoint Wrap(DiamondPoint point)
		{
			return new DiamondPoint(GLMathf.FloorMod(point.X, width), GLMathf.FloorMod(point.Y, height));
		}
	}

	/// <summary>
	/// Wraps points horizontally.
	///
	///	Since version 1.7
	/// </summary>
	[Experimental]
	public class DiamondHorizontalWrapper : IPointWrapper<DiamondPoint>
	{
		readonly int width;

		public DiamondHorizontalWrapper(int width)
		{
			this.width = width;
		}

		public DiamondPoint Wrap(DiamondPoint point)
		{
			return new DiamondPoint(GLMathf.FloorMod(point.X, width), point.Y);
		}
	}

	/// <summary>
	/// Wraps points vertically.
	///
	///	Since version 1.7
	/// </summary>
	[Experimental]
	public class DiamondVerticalWrapper : IPointWrapper<DiamondPoint>
	{
		readonly int height;

		public DiamondVerticalWrapper(int height)
		{
			this.height = height;
		}

		public DiamondPoint Wrap(DiamondPoint point)
		{
			return new DiamondPoint(point.X, GLMathf.FloorMod(point.Y, height));
		}
	}

	#endregion 
	public partial struct PointyHexPoint
	{
		#region Constants

		/// <summary>
		/// The zero point (0, 0).
		/// </summary>
		public static readonly PointyHexPoint Zero = new PointyHexPoint(0, 0);

		#endregion

		#region Fields

		//private readonly VectorPoint vector;
		private readonly int x;
		private readonly int y;

		#endregion

		#region Properties

		/// <summary>
		/// The x-coordinate of this point. This need to be in XML
		/// </summary>
		public int X
		{
			get
			{
				return x;
			}
		}

		/// <summary>
		/// The y-coordinate of this point.
		/// </summary>
		public int Y
		{
			get
			{
				return y;
			}
		}

		public int SpliceIndex
		{
			get 
			{
				return 0;
			}
		}

		public int SpliceCount
		{	
			get 
			{
				return 1; 
			}
		}

		/// <summary>
		/// A Uniform point's base point is simply the point itself.
		///	Makes it easier to implement generic algorithms.
		/// Since version 1.1
		/// </summary>
		public PointyHexPoint BasePoint
		{
			get
			{
				return this;
			}
		}
		#endregion

		#region Construction

		/// <summary>
		/// Constructs a new PointyHexPoint with the given coordinates.
		/// </summary>
		public PointyHexPoint(int x, int y):
			this(new VectorPoint(x, y))
		{
		}

		/// <summary>
		/// Constructs a new PointyHexPoint with the same coordinates as the given VectorPoint.
		/// </summary>
		private PointyHexPoint(VectorPoint vector)
		{
			x = vector.X;
			y = vector.Y;
		}
		#endregion

		#region Distance

		/// <summary>
		/// The lattice distance from this point to the other.
		/// </summary>
		public int DistanceFrom(PointyHexPoint other)
		{
			return Subtract(other).Magnitude();
		}

		#endregion

		#region Equality
		public bool Equals(PointyHexPoint other)
		{
			bool areEqual = (x == other.X) && (y == other.Y);
			return areEqual;
		}

		public override bool Equals (object other)
		{
			if(other.GetType() != typeof(PointyHexPoint))
			{
				return false;
			}

			var point = (PointyHexPoint) other;
			return Equals(point);
		}
	
		public override int GetHashCode ()
		{
			return x ^ y;
		}	
		#endregion

		#region Arithmetic

		/// <summary>
		/// This is a norm defined on the point, such that `p1.Difference(p2).Abs()` is equal to 
		///	`p1.DistanceFrom(p2)`.
		/// </summary>
		public PointyHexPoint Translate(PointyHexPoint translation)
		{
			return new PointyHexPoint(x + translation.X, y + translation.Y);
		}

		public PointyHexPoint Negate()
		{
			return new PointyHexPoint(-x, -y);
		}

		public PointyHexPoint ScaleDown(int r)
		{
			return new PointyHexPoint(GLMathf.FloorDiv(x, r), GLMathf.FloorDiv(y, r));
		}

		public PointyHexPoint ScaleUp(int r)
		{
			return new PointyHexPoint(x * r, y * r);
		}

		/// <summary>
		/// Subtracts the other point from this point, and returns the result.
		/// </summary>
		public PointyHexPoint Subtract(PointyHexPoint other)
		{
			return new PointyHexPoint(x - other.X, y - other.Y);
		}

		public PointyHexPoint MoveBy(PointyHexPoint translation)
		{
			return Translate(translation);
		}

		public PointyHexPoint MoveBackBy(PointyHexPoint translation)
		{
			return Translate(translation.Negate());
		}

		[Version(1,7)]
		public int Dot(PointyHexPoint other)
		{
			return x * other.X + y * other.Y;
		}

		[Version(1,7)]
		public int PerpDot(PointyHexPoint other)
		{
			return x * other.Y - y * other.x;
		}

		[Version(1,10)]
		public PointyHexPoint Perp()
		{
			return new PointyHexPoint(-y, x);
		}

		/// <summary>
		/// Gives a new point that represents the 
		///	reminder when the first point is divided
		///	by the second point	component-wise. The
		///	division is integer division.
		///
		///	Since version 1.6 (Rect)
		///	Since version 1.7 (other)
		/// </summary>
		public PointyHexPoint Mod(PointyHexPoint otherPoint)
		{
			var x = GLMathf.FloorMod(X, otherPoint.X);
			var y = GLMathf.FloorMod(Y, otherPoint.Y);

			return new PointyHexPoint(x, y);
		}

		/// <summary>
		/// Gives a new point that represents the 
		///	first point divided by the second point
		///	component-wise. The division is integer
		///	division.
		///
		///	Since version 1.6 (Rect)
		///	Since version 1.7 (other)
		/// </summary>
		public PointyHexPoint Div(PointyHexPoint otherPoint)
		{
			var x = GLMathf.FloorDiv(X, otherPoint.X);
			var y = GLMathf.FloorDiv(Y, otherPoint.Y);

			return new PointyHexPoint(x, y);
		}

		/// <summary>
		/// Gives a new point that represents the 
		///	first point multiplied by the second point
		///	component-wise. 
		///
		///	Since version 1.6 (Rect)
		///	Since version 1.7 (other)
		/// </summary>
		public PointyHexPoint Mul(PointyHexPoint otherPoint)
		{
			var x = X * otherPoint.X;
			var y = Y * otherPoint.Y;

			return new PointyHexPoint(x, y);
		}
		#endregion 

		#region Utility
		public override string ToString()
		{
			return "(" + x + ", " + y + ")";
		}
		#endregion

		#region Operators
		public static bool operator ==(PointyHexPoint point1, PointyHexPoint point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(PointyHexPoint point1, PointyHexPoint point2)
		{
			return !point1.Equals(point2);
		}

		public static PointyHexPoint operator +(PointyHexPoint point)
		{
			return point;
		}

		public static PointyHexPoint operator -(PointyHexPoint point)
		{
			return point.Negate();
		}

		public static PointyHexPoint operator +(PointyHexPoint point1, PointyHexPoint point2)
		{
			return point1.Translate(point2);
		}

		public static PointyHexPoint operator -(PointyHexPoint point1, PointyHexPoint point2)
		{
			return point1.Subtract(point2);
		}

		public static PointyHexPoint operator *(PointyHexPoint point, int n)
		{
			return point.ScaleUp(n);
		}

		public static PointyHexPoint operator /(PointyHexPoint point, int n)
		{
			return point.ScaleDown(n);
		}

		public static PointyHexPoint operator *(PointyHexPoint point1, PointyHexPoint point2)
		{
			return point1.Mul(point2);
		}

		public static PointyHexPoint operator /(PointyHexPoint point1, PointyHexPoint point2)
		{
			return point1.Div(point2);
		}

		public static PointyHexPoint operator %(PointyHexPoint point1, PointyHexPoint point2)
		{
			return point1.Mod(point2);
		}

		#endregion

		#region Colorings

		/// <summary>
		/// Gives a coloring of the grid such that 
		///	if a point p has color k, then all points
		///	p + m[ux, 0] + n[vx, vy] have the same color
		///	for any integers a and b.
		///
		///	More information anout grid colorings:
		///	http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/
		///
		///	Since version 1.7
		/// </summary>
		public int __GetColor__ReferenceImplementation(int ux, int vx, int vy)
		{
			var u = new PointyHexPoint(ux, 0);
			var v = new PointyHexPoint(vx, vy);

			int colorCount = u.PerpDot(v);
			
			float a = PerpDot(v) / (float) colorCount;
			float b = -PerpDot(u) / (float) colorCount;

			int m = Mathf.FloorToInt(a);
			int n = Mathf.FloorToInt(b);

			int baseVectorX = m*u.X + n*v.X;
			int baseVectorY = n*u.Y + n*v.Y;
				
			int offsetX = GLMathf.FloorMod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathf.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}

		/// <summary>
		/// Gives a coloring of the grid such that 
		///	if a point p has color k, then all points
		///	p + m[ux, 0] + n[vx, vy] have the same color
		///	for any integers a and b.
		///
		///	More information anout grid colorings:
		///	http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/
		///
		///	Since version 1.7
		/// </summary>
		public int GetColor(int ux, int vx, int vy)
		{
			int colorCount = ux * vy;

			float a = (x * vy - y * vx) / (float)colorCount;
			float b = (y * ux) / (float)colorCount;

			int m = Mathf.FloorToInt(a);
			int n = Mathf.FloorToInt(b);

			int baseVectorX = m * ux + n * vx;
			int baseVectorY = n * vy;

			int offsetX = GLMathf.FloorMod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathf.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}

		#endregion
	}

	#region Wrappers

	/// <summary>
	/// Wraps points both horizontally and vertically.
	///
	///	Since version 1.7
	/// </summary>
	[Experimental]
	public class PointyHexParallelogramWrapper : IPointWrapper<PointyHexPoint>
	{
		readonly int width;
		readonly int height;

		public PointyHexParallelogramWrapper(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		public PointyHexPoint Wrap(PointyHexPoint point)
		{
			return new PointyHexPoint(GLMathf.FloorMod(point.X, width), GLMathf.FloorMod(point.Y, height));
		}
	}

	/// <summary>
	/// Wraps points horizontally.
	///
	///	Since version 1.7
	/// </summary>
	[Experimental]
	public class PointyHexHorizontalWrapper : IPointWrapper<PointyHexPoint>
	{
		readonly int width;

		public PointyHexHorizontalWrapper(int width)
		{
			this.width = width;
		}

		public PointyHexPoint Wrap(PointyHexPoint point)
		{
			return new PointyHexPoint(GLMathf.FloorMod(point.X, width), point.Y);
		}
	}

	/// <summary>
	/// Wraps points vertically.
	///
	///	Since version 1.7
	/// </summary>
	[Experimental]
	public class PointyHexVerticalWrapper : IPointWrapper<PointyHexPoint>
	{
		readonly int height;

		public PointyHexVerticalWrapper(int height)
		{
			this.height = height;
		}

		public PointyHexPoint Wrap(PointyHexPoint point)
		{
			return new PointyHexPoint(point.X, GLMathf.FloorMod(point.Y, height));
		}
	}

	#endregion 
	public partial struct FlatHexPoint
	{
		#region Constants

		/// <summary>
		/// The zero point (0, 0).
		/// </summary>
		public static readonly FlatHexPoint Zero = new FlatHexPoint(0, 0);

		#endregion

		#region Fields

		//private readonly VectorPoint vector;
		private readonly int x;
		private readonly int y;

		#endregion

		#region Properties

		/// <summary>
		/// The x-coordinate of this point. This need to be in XML
		/// </summary>
		public int X
		{
			get
			{
				return x;
			}
		}

		/// <summary>
		/// The y-coordinate of this point.
		/// </summary>
		public int Y
		{
			get
			{
				return y;
			}
		}

		public int SpliceIndex
		{
			get 
			{
				return 0;
			}
		}

		public int SpliceCount
		{	
			get 
			{
				return 1; 
			}
		}

		/// <summary>
		/// A Uniform point's base point is simply the point itself.
		///	Makes it easier to implement generic algorithms.
		/// Since version 1.1
		/// </summary>
		public FlatHexPoint BasePoint
		{
			get
			{
				return this;
			}
		}
		#endregion

		#region Construction

		/// <summary>
		/// Constructs a new FlatHexPoint with the given coordinates.
		/// </summary>
		public FlatHexPoint(int x, int y):
			this(new VectorPoint(x, y))
		{
		}

		/// <summary>
		/// Constructs a new FlatHexPoint with the same coordinates as the given VectorPoint.
		/// </summary>
		private FlatHexPoint(VectorPoint vector)
		{
			x = vector.X;
			y = vector.Y;
		}
		#endregion

		#region Distance

		/// <summary>
		/// The lattice distance from this point to the other.
		/// </summary>
		public int DistanceFrom(FlatHexPoint other)
		{
			return Subtract(other).Magnitude();
		}

		#endregion

		#region Equality
		public bool Equals(FlatHexPoint other)
		{
			bool areEqual = (x == other.X) && (y == other.Y);
			return areEqual;
		}

		public override bool Equals (object other)
		{
			if(other.GetType() != typeof(FlatHexPoint))
			{
				return false;
			}

			var point = (FlatHexPoint) other;
			return Equals(point);
		}
	
		public override int GetHashCode ()
		{
			return x ^ y;
		}	
		#endregion

		#region Arithmetic

		/// <summary>
		/// This is a norm defined on the point, such that `p1.Difference(p2).Abs()` is equal to 
		///	`p1.DistanceFrom(p2)`.
		/// </summary>
		public FlatHexPoint Translate(FlatHexPoint translation)
		{
			return new FlatHexPoint(x + translation.X, y + translation.Y);
		}

		public FlatHexPoint Negate()
		{
			return new FlatHexPoint(-x, -y);
		}

		public FlatHexPoint ScaleDown(int r)
		{
			return new FlatHexPoint(GLMathf.FloorDiv(x, r), GLMathf.FloorDiv(y, r));
		}

		public FlatHexPoint ScaleUp(int r)
		{
			return new FlatHexPoint(x * r, y * r);
		}

		/// <summary>
		/// Subtracts the other point from this point, and returns the result.
		/// </summary>
		public FlatHexPoint Subtract(FlatHexPoint other)
		{
			return new FlatHexPoint(x - other.X, y - other.Y);
		}

		public FlatHexPoint MoveBy(FlatHexPoint translation)
		{
			return Translate(translation);
		}

		public FlatHexPoint MoveBackBy(FlatHexPoint translation)
		{
			return Translate(translation.Negate());
		}

		[Version(1,7)]
		public int Dot(FlatHexPoint other)
		{
			return x * other.X + y * other.Y;
		}

		[Version(1,7)]
		public int PerpDot(FlatHexPoint other)
		{
			return x * other.Y - y * other.x;
		}

		[Version(1,10)]
		public FlatHexPoint Perp()
		{
			return new FlatHexPoint(-y, x);
		}

		/// <summary>
		/// Gives a new point that represents the 
		///	reminder when the first point is divided
		///	by the second point	component-wise. The
		///	division is integer division.
		///
		///	Since version 1.6 (Rect)
		///	Since version 1.7 (other)
		/// </summary>
		public FlatHexPoint Mod(FlatHexPoint otherPoint)
		{
			var x = GLMathf.FloorMod(X, otherPoint.X);
			var y = GLMathf.FloorMod(Y, otherPoint.Y);

			return new FlatHexPoint(x, y);
		}

		/// <summary>
		/// Gives a new point that represents the 
		///	first point divided by the second point
		///	component-wise. The division is integer
		///	division.
		///
		///	Since version 1.6 (Rect)
		///	Since version 1.7 (other)
		/// </summary>
		public FlatHexPoint Div(FlatHexPoint otherPoint)
		{
			var x = GLMathf.FloorDiv(X, otherPoint.X);
			var y = GLMathf.FloorDiv(Y, otherPoint.Y);

			return new FlatHexPoint(x, y);
		}

		/// <summary>
		/// Gives a new point that represents the 
		///	first point multiplied by the second point
		///	component-wise. 
		///
		///	Since version 1.6 (Rect)
		///	Since version 1.7 (other)
		/// </summary>
		public FlatHexPoint Mul(FlatHexPoint otherPoint)
		{
			var x = X * otherPoint.X;
			var y = Y * otherPoint.Y;

			return new FlatHexPoint(x, y);
		}
		#endregion 

		#region Utility
		public override string ToString()
		{
			return "(" + x + ", " + y + ")";
		}
		#endregion

		#region Operators
		public static bool operator ==(FlatHexPoint point1, FlatHexPoint point2)
		{
			return point1.Equals(point2);
		}

		public static bool operator !=(FlatHexPoint point1, FlatHexPoint point2)
		{
			return !point1.Equals(point2);
		}

		public static FlatHexPoint operator +(FlatHexPoint point)
		{
			return point;
		}

		public static FlatHexPoint operator -(FlatHexPoint point)
		{
			return point.Negate();
		}

		public static FlatHexPoint operator +(FlatHexPoint point1, FlatHexPoint point2)
		{
			return point1.Translate(point2);
		}

		public static FlatHexPoint operator -(FlatHexPoint point1, FlatHexPoint point2)
		{
			return point1.Subtract(point2);
		}

		public static FlatHexPoint operator *(FlatHexPoint point, int n)
		{
			return point.ScaleUp(n);
		}

		public static FlatHexPoint operator /(FlatHexPoint point, int n)
		{
			return point.ScaleDown(n);
		}

		public static FlatHexPoint operator *(FlatHexPoint point1, FlatHexPoint point2)
		{
			return point1.Mul(point2);
		}

		public static FlatHexPoint operator /(FlatHexPoint point1, FlatHexPoint point2)
		{
			return point1.Div(point2);
		}

		public static FlatHexPoint operator %(FlatHexPoint point1, FlatHexPoint point2)
		{
			return point1.Mod(point2);
		}

		#endregion

		#region Colorings

		/// <summary>
		/// Gives a coloring of the grid such that 
		///	if a point p has color k, then all points
		///	p + m[ux, 0] + n[vx, vy] have the same color
		///	for any integers a and b.
		///
		///	More information anout grid colorings:
		///	http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/
		///
		///	Since version 1.7
		/// </summary>
		public int __GetColor__ReferenceImplementation(int ux, int vx, int vy)
		{
			var u = new FlatHexPoint(ux, 0);
			var v = new FlatHexPoint(vx, vy);

			int colorCount = u.PerpDot(v);
			
			float a = PerpDot(v) / (float) colorCount;
			float b = -PerpDot(u) / (float) colorCount;

			int m = Mathf.FloorToInt(a);
			int n = Mathf.FloorToInt(b);

			int baseVectorX = m*u.X + n*v.X;
			int baseVectorY = n*u.Y + n*v.Y;
				
			int offsetX = GLMathf.FloorMod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathf.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}

		/// <summary>
		/// Gives a coloring of the grid such that 
		///	if a point p has color k, then all points
		///	p + m[ux, 0] + n[vx, vy] have the same color
		///	for any integers a and b.
		///
		///	More information anout grid colorings:
		///	http://gamelogic.co.za/2013/12/18/what-are-grid-colorings/
		///
		///	Since version 1.7
		/// </summary>
		public int GetColor(int ux, int vx, int vy)
		{
			int colorCount = ux * vy;

			float a = (x * vy - y * vx) / (float)colorCount;
			float b = (y * ux) / (float)colorCount;

			int m = Mathf.FloorToInt(a);
			int n = Mathf.FloorToInt(b);

			int baseVectorX = m * ux + n * vx;
			int baseVectorY = n * vy;

			int offsetX = GLMathf.FloorMod(X - baseVectorX, ux);
			int offsetY = Y - baseVectorY;

			int colorIndex = Mathf.FloorToInt(offsetX + offsetY * ux);

			return colorIndex;
		}

		#endregion
	}

	#region Wrappers

	/// <summary>
	/// Wraps points both horizontally and vertically.
	///
	///	Since version 1.7
	/// </summary>
	[Experimental]
	public class FlatHexParallelogramWrapper : IPointWrapper<FlatHexPoint>
	{
		readonly int width;
		readonly int height;

		public FlatHexParallelogramWrapper(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		public FlatHexPoint Wrap(FlatHexPoint point)
		{
			return new FlatHexPoint(GLMathf.FloorMod(point.X, width), GLMathf.FloorMod(point.Y, height));
		}
	}

	/// <summary>
	/// Wraps points horizontally.
	///
	///	Since version 1.7
	/// </summary>
	[Experimental]
	public class FlatHexHorizontalWrapper : IPointWrapper<FlatHexPoint>
	{
		readonly int width;

		public FlatHexHorizontalWrapper(int width)
		{
			this.width = width;
		}

		public FlatHexPoint Wrap(FlatHexPoint point)
		{
			return new FlatHexPoint(GLMathf.FloorMod(point.X, width), point.Y);
		}
	}

	/// <summary>
	/// Wraps points vertically.
	///
	///	Since version 1.7
	/// </summary>
	[Experimental]
	public class FlatHexVerticalWrapper : IPointWrapper<FlatHexPoint>
	{
		readonly int height;

		public FlatHexVerticalWrapper(int height)
		{
			this.height = height;
		}

		public FlatHexPoint Wrap(FlatHexPoint point)
		{
			return new FlatHexPoint(point.X, GLMathf.FloorMod(point.Y, height));
		}
	}

	#endregion 
}
