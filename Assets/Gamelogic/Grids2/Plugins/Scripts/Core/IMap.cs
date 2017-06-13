using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Algorithms;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// How an object (such as a grid) is aligned horizontally within a rectangle.
	/// </summary>
	public enum HorizontalAlignment
	{
		None,
		Left,
		Center,
		Right
	}

	/// <summary>
	/// How an object (such as a grid) is aligned vertically within a rectangle.
	/// </summary>
	public enum VerticalAlignment
	{
		None,
		Top,
		Center,
		Bottom
	}

	/// <summary>
	/// Represents a function.
	/// </summary>
	/// <typeparam name="TInput">The input type.</typeparam>
	/// <typeparam name="TOutput">The output type.</typeparam>
	public interface IForwardMap<in TInput, out TOutput>
	{
		#region Public Methods

		/// <summary>
		/// 
		/// </summary>
		/// <param name="input">The point.</param>
		/// <returns>TOutput.</returns>
		TOutput Forward(TInput input);

		#endregion
	}

	/// <summary>
	/// Represents the inverse of a function.
	/// </summary>
	/// <typeparam name="TInput">The input type of the function.</typeparam>
	/// <typeparam name="TOutput">The output type of the function.</typeparam>
	public interface IReverseMap<out TInput, in TOutput>
	{
		#region Public Methods

		/// <summary>
		/// Gives the input given the output.
		/// </summary>
		/// <param name="output">The point.</param>
		/// <returns>TInput.</returns>
		TInput Reverse(TOutput output);

		#endregion
	}

	/// <summary>
	/// Represents a function and its inverse.
	/// </summary>
	/// <typeparam name="TInput">The input type.</typeparam>
	/// <typeparam name="TOutput">The output type.</typeparam>
	/// <seealso cref="Gamelogic.Grids2.IForwardMap{TInput, TOutput}" />
	/// <seealso cref="Gamelogic.Grids2.IReverseMap{TInput, TOutput}" />
	public interface IMap<TInput, TOutput> : IForwardMap<TInput, TOutput>, IReverseMap<TInput, TOutput>
	{ }

	/// <summary>
	/// Provides methods for creating and manipulating maps.
	/// </summary>
	/// <seealso cref="IMap{TInput,TOutput}"/>
	public static class Map
	{
		#region  Types

		private sealed class IdentityMap<TInput> : IMap<TInput, TInput>
		{
			#region Public Methods

			public TInput Forward(TInput input)
			{
				return input;
			}

			public TInput Reverse(TInput output)
			{
				return output;
			}

			#endregion
		}

		private sealed class InverseMap<TInput, TOuput> : IMap<TInput, TOuput>
		{
			#region Constants

			private readonly IMap<TOuput, TInput> map;

			#endregion

			#region Constructors

			public InverseMap(IMap<TOuput, TInput> map)
			{
				this.map = map;
			}

			#endregion

			#region Public Methods

			public TOuput Forward(TInput input)
			{
				return map.Reverse(input);
			}

			public TInput Reverse(TOuput output)
			{
				return map.Forward(output);
			}

			#endregion
		}

		private sealed class CombinedMap<TInput, TOutput> : IMap<TInput, TOutput>
		{
			#region Constants

			private readonly IForwardMap<TInput, TOutput> forwardMap;
			private readonly IReverseMap<TInput, TOutput> reverseMap;

			#endregion

			#region Constructors

			public CombinedMap(
				IForwardMap<TInput, TOutput> forwardMap,
				IReverseMap<TInput, TOutput> reverseMap)
			{
				this.forwardMap = forwardMap;
				this.reverseMap = reverseMap;
			}

			#endregion

			#region Public Methods

			public TOutput Forward(TInput input)
			{
				return forwardMap.Forward(input);
			}

			public TInput Reverse(TOutput output)
			{
				return reverseMap.Reverse(output);
			}

			#endregion
		}

		private sealed class CompositionMap<TInput, TInter, TOutput> : IMap<TInput, TOutput>
		{
			#region Constants

			private readonly IMap<TInput, TInter> map1;
			private readonly IMap<TInter, TOutput> map2;

			#endregion

			#region Constructors

			public CompositionMap(IMap<TInput, TInter> map1, IMap<TInter, TOutput> map2)
			{
				this.map1 = map1;
				this.map2 = map2;
			}

			#endregion

			#region Public Methods

			public TOutput Forward(TInput input)
			{
				return map2.Forward(map1.Forward(input));
			}

			public TInput Reverse(TOutput output)
			{
				return map1.Reverse(map2.Reverse(output));
			}

			#endregion
		}

		private sealed class CompositionForwardMap<TInput, TInter, TOutput> : IForwardMap<TInput, TOutput>
		{
			#region Constants

			private readonly IForwardMap<TInput, TInter> map1;
			private readonly IForwardMap<TInter, TOutput> map2;

			#endregion

			#region Constructors

			public CompositionForwardMap(IForwardMap<TInput, TInter> map1, IForwardMap<TInter, TOutput> map2)
			{
				this.map1 = map1;
				this.map2 = map2;
			}

			#endregion

			#region Public Methods

			public TOutput Forward(TInput input)
			{
				return map2.Forward(map1.Forward(input));
			}

			#endregion
		}

		private sealed class CompositionReverseMap<TInput, TInter, TOutput> : IReverseMap<TInput, TOutput>
		{
			#region Constants

			private readonly IReverseMap<TInput, TInter> map1;
			private readonly IReverseMap<TInter, TOutput> map2;

			#endregion

			#region Constructors

			public CompositionReverseMap(IReverseMap<TInput, TInter> map1, IReverseMap<TInter, TOutput> map2)
			{
				this.map1 = map1;
				this.map2 = map2;
			}

			#endregion

			#region Public Methods

			public TInput Reverse(TOutput output)
			{
				return map1.Reverse(map2.Reverse(output));
			}

			#endregion
		}

		private sealed class FuncForwardMap<TInput, TOuput> : IForwardMap<TInput, TOuput>
		{
			#region Constants

			private readonly Func<TInput, TOuput> forward;

			#endregion

			#region Constructors

			public FuncForwardMap(Func<TInput, TOuput> forward)
			{
				this.forward = forward;
			}

			#endregion

			#region Public Methods

			public TOuput Forward(TInput input)
			{
				return forward(input);
			}

			#endregion
		}

		private sealed class InverseForwardMap<TInput, TOuput> : IReverseMap<TInput, TOuput>
		{
			#region Constants

			private readonly IForwardMap<TOuput, TInput> map;

			#endregion

			#region Constructors

			public InverseForwardMap(IForwardMap<TOuput, TInput> map)
			{
				this.map = map;
			}

			#endregion

			#region Public Methods

			public TInput Reverse(TOuput output)
			{
				return map.Forward(output);
			}

			#endregion
		}

		private sealed class InverseReverseMap<TInput, TOuput> : IForwardMap<TInput, TOuput>
		{
			#region Constants

			private readonly IReverseMap<TOuput, TInput> map;

			#endregion

			#region Constructors

			public InverseReverseMap(IReverseMap<TOuput, TInput> map)
			{
				this.map = map;
			}

			#endregion

			#region Public Methods

			public TOuput Forward(TInput input)
			{
				return map.Reverse(input);
			}

			#endregion
		}

		private sealed class DiscreteTranslateMap1 : IMap<int, int>
		{
			private readonly int offset;

			public DiscreteTranslateMap1(int offset)
			{
				this.offset = offset;
			}

			public int Forward(int input)
			{
				return input + offset;
			}

			public int Reverse(int output)
			{
				return output - offset;
			}
		}

		private sealed class DiscreteTranslateMap2 : IMap<GridPoint2, GridPoint2>
		{
			private readonly GridPoint2 offset;

			public DiscreteTranslateMap2(GridPoint2 offset)
			{
				this.offset = offset;
			}

			public GridPoint2 Forward(GridPoint2 input)
			{
				return input + offset;
			}

			public GridPoint2 Reverse(GridPoint2 output)
			{
				return output - offset;
			}
		}

		private sealed class DiscreteTranslateMap3 : IMap<GridPoint3, GridPoint3>
		{
			private readonly GridPoint3 offset;

			public DiscreteTranslateMap3(GridPoint3 offset)
			{
				this.offset = offset;
			}

			public GridPoint3 Forward(GridPoint3 input)
			{
				return input + offset;
			}

			public GridPoint3 Reverse(GridPoint3 output)
			{
				return output - offset;
			}
		}

		[Serializable]
		private sealed class Translate1Map : IMap<float, float>
		{
			private readonly float offset;

			public Translate1Map(float offset)
			{
				this.offset = offset;
			}

			public float Forward(float input)
			{
				return input + offset;
			}

			public float Reverse(float output)
			{
				return output - offset;
			}
		}

		[Serializable]
		private sealed class Translate2Map : IMap<Vector2, Vector2>
		{
			private readonly Vector2 offset;

			public Translate2Map(Vector2 offset)
			{
				this.offset = offset;
			}

			public Vector2 Forward(Vector2 input)
			{
				return input + offset;
			}

			public Vector2 Reverse(Vector2 output)
			{
				return output - offset;
			}
		}

		[Serializable]
		private sealed class Translate3Map : IMap<Vector3, Vector3>
		{
			private readonly Vector3 offset;

			public Translate3Map(Vector3 offset)
			{
				this.offset = offset;
			}

			public Vector3 Forward(Vector3 input)
			{
				return input + offset;
			}

			public Vector3 Reverse(Vector3 output)
			{
				return output - offset;
			}
		}

		private sealed class ScaleMap1 : IMap<float, float>
		{
			private readonly float scale;

			public ScaleMap1(float scale)
			{
				this.scale = scale;
			}

			public float Forward(float input)
			{
				return input * scale;
			}

			public float Reverse(float output)
			{
				return output * scale;
			}
		}

		private sealed class ScaleMap2 : IMap<Vector2, Vector2>
		{
			private readonly Vector2 scale;

			public ScaleMap2(Vector2 scale)
			{
				this.scale = scale;
			}

			public Vector2 Forward(Vector2 input)
			{
				return input.HadamardMul(scale);
			}

			public Vector2 Reverse(Vector2 output)
			{
				return output.HadamardDiv(scale);
			}
		}

		private sealed class ScaleMap3 : IMap<Vector3, Vector3>
		{
			private readonly Vector3 scale;

			public ScaleMap3(Vector3 scale)
			{
				this.scale = scale;
			}

			public Vector3 Forward(Vector3 input)
			{
				return input.HadamardMul(scale);
			}

			public Vector3 Reverse(Vector3 output)
			{
				return output.HadamardDiv(scale);
			}
		}

		/// <summary>
		/// Map that does a polar transform on the X and Y axes.
		/// </summary>
		/// <seealso cref="IMap{Vector3, Vector3}" />
		private sealed class PolarMap : IMap<Vector3, Vector3>
		{
			#region Constants

			private readonly float radius;
			private readonly float theta;

			#endregion

			#region Constructors

			public PolarMap(float radius, float theta)
			{
				this.radius = radius;
				this.theta = theta;
			}

			#endregion

			#region Public Methods

			public Vector3 Forward(Vector3 input)
			{
				var newX = radius * input.y * Mathf.Cos(input.x * theta * 2 * Mathf.PI);
				var newY = radius * input.y * Mathf.Sin(input.x * theta * 2 * Mathf.PI);

				return new Vector3(newX, newY, input.z);
			}

			//of course not completely invertible
			public Vector3 Reverse(Vector3 output)
			{
				var y = Mathf.Sqrt(output.x * output.x + output.y * output.y) / radius;
				var angle = Mathf.Atan2(output.y, output.x);

				if (angle < 0)
				{
					angle += 2 * Mathf.PI;
				}

				var x = angle / (theta * 2 * Mathf.PI);

				return new Vector3(x, y, output.z);
			}

			#endregion
		}

		private sealed class HeightMap : IMap<Vector3, Vector3>
		{
			private readonly Grid2<float> heights;
			private readonly IMap<Vector3, GridPoint2> roundMap;
			private readonly float heightScale;

			public HeightMap(Grid2<float> heights, float heightScale, IMap<Vector3, GridPoint2> roundMap)
			{
				this.heights = heights;
				this.heightScale = heightScale;
				this.roundMap = roundMap;

			}

			public Vector3 Forward(Vector3 input)
			{
				var pointXY = input.To2DXY();
				var newPoint = roundMap.Forward(pointXY);
				var height = heights[newPoint];

				return input + Vector3.forward * height * heightScale;
			}

			public Vector3 Reverse(Vector3 output)
			{
				var pointXY = output.To2DXY();
				var newPoint = roundMap.Forward(pointXY);
				var height = heights[newPoint];

				return output - Vector3.forward * height * heightScale;
			}
		}

		private sealed class LeftShiftAxesMap : IMap<Vector3, Vector3>
		{
			public Vector3 Forward(Vector3 input)
			{
				return new Vector3(input.y, input.z, input.x);
			}

			public Vector3 Reverse(Vector3 output)
			{
				return new Vector3(output.z, output.x, output.y);
			}
		}

		private sealed class SwapXYMap : IMap<Vector3, Vector3>
		{
			public Vector3 Forward(Vector3 input)
			{
				return new Vector3(input.y, input.x, input.z);
			}

			public Vector3 Reverse(Vector3 output)
			{
				return new Vector3(output.y, output.x, output.z);
			}
		}

		private sealed class SwapYZMap : IMap<Vector3, Vector3>
		{
			public Vector3 Forward(Vector3 input)
			{
				return new Vector3(input.x, input.z, input.y);
			}

			public Vector3 Reverse(Vector3 output)
			{
				return new Vector3(output.x, output.z, output.y);
			}
		}

		private sealed class LinearMap : IMap<Vector3, Vector3>
		{
			private readonly Matrixf33 matrix;
			private readonly Matrixf33 inverse;

			public LinearMap(Matrixf33 matrix)
			{
				this.matrix = matrix;
				inverse = matrix.Inv();
			}

			public Vector3 Forward(Vector3 input)
			{
				return input.Mul(matrix);
			}

			public Vector3 Reverse(Vector3 output)
			{
				return output.Mul(inverse);
			}
		}

		private sealed class FuncXonYMap : IMap<Vector3, Vector3>
		{
			private readonly Func<float, float> func;

			public FuncXonYMap(Func<float, float> func)
			{
				this.func = func;
			}

			public Vector3 Forward(Vector3 input)
			{
				return new Vector3(
					input.x,
					input.y + func(input.x),
					input.z);
			}

			public Vector3 Reverse(Vector3 output)
			{
				return new Vector3(
					output.x,
					output.y - func(output.x),
					output.z);
			}
		}

		//TODO this is not invertible!
		private sealed class FuncXonXMap : IMap<Vector3, Vector3>
		{
			private readonly Func<float, float> func;

			public FuncXonXMap(Func<float, float> func)
			{
				this.func = func;
			}

			public Vector3 Forward(Vector3 input)
			{
				return new Vector3(
					func(input.x),
					input.y,
					input.z);
			}

			public Vector3 Reverse(Vector3 output)
			{
				return new Vector3(
					func(output.x),
					output.y,
					output.z);
			}
		}

		/// <summary>
		/// A map that maps Vector3 to GridPoint3 by rounding each coordinate individually.
		/// </summary>
		/// <seealso cref="Gamelogic.Grids2.IMap{Vector3, GridPoint3}" />
		private sealed class BlockRoundMap : IMap<Vector3, GridPoint3>
		{
			public GridPoint3 Forward(Vector3 input)
			{
				return BlockPoint.RoundToGridPoint(input);
			}

			public Vector3 Reverse(GridPoint3 output)
			{
				return output.ToVector3();
			}
		}

		private sealed class HexBlockRoundMap : IMap<Vector3, GridPoint3>
		{
			public GridPoint3 Forward(Vector3 input)
			{
				return HexBlockPoint.RoundToGridPoint(input);
			}

			public Vector3 Reverse(GridPoint3 output)
			{
				return output.ToVector3();
			}
		}

		/// <summary>
		/// A round map that rounds points to the nearest hex center.
		/// </summary>
		/// <seealso cref="Gamelogic.Grids2.IMap{Vector3, GridPoint2}" />
		/// TODO Take out the projection.
		private sealed class HexRoundMap : IMap<Vector3, GridPoint2>
		{
			public Vector3 Reverse(GridPoint2 output)
			{
				return output.ToVector2().To3DXY();
			}

			public GridPoint2 Forward(Vector3 input)
			{
				return PointyHexPoint.RoundToGridPoint(input.To2DXY());
			}
		}



		private sealed class ParallelogramWrapXMap : IMap<Vector3, Vector3>
		{
			private readonly float width;

			public ParallelogramWrapXMap(float width)
			{
				this.width = width;
			}

			public Vector3 Forward(Vector3 input)
			{
				return new Vector3(GLMathf.FloorMod(input.x, width), input.y, input.z);
			}

			public Vector3 Reverse(Vector3 output)
			{
				return output; //TODO: Is this correct?
			}
		}

		private sealed class ParallelogramWrapXYMap : IMap<Vector3, Vector3>
		{
			private readonly float width;
			private readonly float height;

			public ParallelogramWrapXYMap(float width, float height)
			{
				this.width = width;
				this.height = height;
			}

			public Vector3 Forward(Vector3 input)
			{
				return new Vector3(GLMathf.FloorMod(input.x, width), GLMathf.FloorMod(input.y, height), input.z);
			}

			public Vector3 Reverse(Vector3 output)
			{
				return output; //TODO: Is this correct?
			}
		}

		/// <summary>
		/// Map that rounds floats to integers.
		/// </summary>
		/// <seealso cref="Gamelogic.Grids2.IMap{Vector3, Int32}" />
		private sealed class PointRoundMap : IMap<Vector3, int>
		{
			public Vector3 Reverse(int output)
			{
				return new Vector3(output, 0, 0);
			}

			public int Forward(Vector3 input)
			{
				return Mathf.RoundToInt(input.x);
			}
		}

		/// <summary>
		/// Map that rounds vectors to GridPoint2 by flooring the coordinates.
		/// </summary>
		/// <seealso cref="Gamelogic.Grids2.IMap{Vector3, GridPoint2}" />
		/// TODO Remove the projection
		private sealed class RectFloorMap : IMap<Vector3, GridPoint2>
		{
			public Vector3 Reverse(GridPoint2 output)
			{
				return output.ToVector2().To3DXY();
			}

			public GridPoint2 Forward(Vector3 input)
			{
				//TODO - make this a RectPoint method
				//TODO - make a block point version
				return new GridPoint2(
					Mathf.FloorToInt(input.x),
					Mathf.FloorToInt(input.y));
			}
		}

		/// <summary>
		/// Map that rounds vectors to GridPoint2 by rounding the coordinates.
		/// </summary>
		/// <seealso cref="Gamelogic.Grids2.IMap{Vector3, GridPoint2}" />
		/// TODO Remove the projection
		private sealed class RectRoundMap : IMap<Vector3, GridPoint2>
		{
			public Vector3 Reverse(GridPoint2 output)
			{
				return output.ToVector2().To3DXY();
			}

			public GridPoint2 Forward(Vector3 input)
			{
				return RectPoint.RoundToGridPoint(input.To2DXY());
			}
		}

		private sealed class PolygonPartitionOffsetMap : IMap<Vector3, GridPoint2>
		{
			private readonly IPointPartition2 partition;
			private readonly IMap<Vector3, GridPoint2> map;
			private readonly IList<GridPoint2> offsets;

			public PolygonPartitionOffsetMap(
				IMap<Vector3, GridPoint2> map,
				IPointPartition2 partition,
				IList<GridPoint2> offsets)
			{
				this.partition = partition;
				this.offsets = offsets;
				this.map = map;
			}

			public Vector3 Reverse(GridPoint2 output)
			{
				return map.Reverse(output);
			}

			public GridPoint2 Forward(Vector3 input)
			{
				var gridPointBase = map.Forward(input);
				var center = map.Reverse(gridPointBase);
				var offset = input - center;

				return offsets[partition.GetPartition(offset)] + gridPointBase;
			}
		}

		[Serializable]
		private sealed class XYMap : IMap<Vector2, Vector3>
		{
			private readonly float height;

			public XYMap(float height)
			{
				this.height = height;
			}

			public Vector3 Forward(Vector2 input)
			{
				return new Vector3(input.x, input.y, height);
			}

			public Vector2 Reverse(Vector3 output)
			{
				return new Vector2(output.x, output.y);
			}
		}

		[Serializable]
		private sealed class XZMap : IMap<Vector2, Vector3>
		{
			private readonly float height;

			public XZMap(float height)
			{
				this.height = height;
			}

			public Vector3 Forward(Vector2 input)
			{
				return new Vector3(input.x, height, input.x);
			}

			public Vector2 Reverse(Vector3 output)
			{
				return new Vector2(output.x, output.y);
			}
		}
		#endregion

		#region Private Static Methods

		private static float AlignGridHorizontal(HorizontalAlignment alignment, Bounds bounds, Bounds gridBounds)
		{
			float toZeroOffset = bounds.center.x - gridBounds.center.x;
			float offset = 0.0f;

			switch (alignment)
			{
				case HorizontalAlignment.Left:
					offset = -(bounds.size.x - gridBounds.size.x) / 2.0f;

					break;
				case HorizontalAlignment.Center:
					offset = 0.0f;

					break;
				case HorizontalAlignment.Right:
					offset = (bounds.size.x - gridBounds.size.x) / 2.0f;

					break;
				case HorizontalAlignment.None:
					return 0;
			}

			return toZeroOffset + offset;
		}

		private static float AlignGridVertical(VerticalAlignment alignment, Bounds bounds, Bounds gridBounds)
		{
			float toZeroOffset = bounds.center.y - gridBounds.center.y;
			float offset = 0.0f;

			switch (alignment)
			{
				case VerticalAlignment.Top:
					offset = (bounds.size.y - gridBounds.size.y) / 2.0f;

					break;
				case VerticalAlignment.Center:
					offset = 0.0f;

					break;
				case VerticalAlignment.Bottom:
					offset = -(bounds.size.y - gridBounds.size.y) / 2.0f;

					break;
				case VerticalAlignment.None:
					return 0;
			}

			return toZeroOffset + offset;
		}

		private static float AnchorHorizontalPivot(HorizontalAlignment alignment, Vector2 cellSize)
		{
			float pivotOffset = 0.0f;

			switch (alignment)
			{
				case HorizontalAlignment.Left:
					pivotOffset = 0.0f;

					break;
				case HorizontalAlignment.Center:
					pivotOffset = cellSize.x / 2;

					break;
				case HorizontalAlignment.Right:
					pivotOffset = cellSize.x;

					break;
				case HorizontalAlignment.None:
					return 0;
			}

			return pivotOffset;
		}

		private static float AnchorVerticalPivot(VerticalAlignment alignment, Vector2 cellSize)
		{
			float pivotOffset = 0.0f;

			switch (alignment)
			{
				case VerticalAlignment.Top:
					pivotOffset = cellSize.y;

					break;
				case VerticalAlignment.Center:
					pivotOffset = cellSize.y / 2;

					break;
				case VerticalAlignment.Bottom:
					pivotOffset = 0.0f;

					break;
				case VerticalAlignment.None:
					return 0;
			}

			return pivotOffset;
		}

		#endregion

		#region Public Static Methods

		/// <summary>
		/// Creates a Reverse Map that swap the calls of Forward and Reverse, meaning that now calling Forward
		/// will do the Reverse call and vice versa.
		/// </summary>
		/// <typeparam name="TInput">The type of the Input.</typeparam>
		/// <typeparam name="TOutput">The type of the Output.</typeparam>
		/// <param name="map">Base map where you apply this call.</param>
		public static IMap<TOutput, TInput> Reverse<TInput, TOutput>(this IMap<TInput, TOutput> map)
		{
			return new InverseMap<TOutput, TInput>(map);
		}

		/// <summary>
		/// Creates a Reverse Map based only on the InverseForwardMap. In other words only the Reverse is swaped,
		/// meaning that calling Reverse will do the Forward operation.
		/// </summary>
		/// <typeparam name="TInput">The type of the Input.</typeparam>
		/// <typeparam name="TOutput">The type of the Output.</typeparam>
		/// <param name="map">Base map where you apply this call.</param>
		public static IReverseMap<TOutput, TInput> Reverse<TInput, TOutput>(this IForwardMap<TInput, TOutput> map)
		{
			return new InverseForwardMap<TOutput, TInput>(map);
		}

		/// <summary>
		/// Creates a Reverse Map based only on the InverseReverseMap. In other words only the Forward is swapped,
		/// meaning that calling Forward will do the Reverse operation.
		/// </summary>
		/// <typeparam name="TInput">The type of the Input.</typeparam>
		/// <typeparam name="TOutput">The type of the Output.</typeparam>
		/// <param name="map">Base map where you apply this call.</param>
		public static IForwardMap<TOutput, TInput> Reverse<TInput, TOutput>(this IReverseMap<TInput, TOutput> map)
		{
			return new InverseReverseMap<TOutput, TInput>(map);
		}

		/// <summary>
		/// Creates a Composition Map. This map pass the map1 Forward call as a parameter of the map2 Forward call making
		/// a composition of such methods. For the Reverse the process is the other way around, calling the Reverse of map2 passing
		/// the return value to the Reverse of the map1.
		/// </summary>
		/// <typeparam name="TInput">The type of the Input.</typeparam>
		/// <typeparam name="TInter">The type of the Intermediate value that allows communication between map1 and map2.</typeparam>
		/// <typeparam name="TOutput">The type of the Output.</typeparam>
		/// <param name="map1">Base map where you apply this call.</param>
		/// <param name="map2">Map used for the composition.</param>
		public static IMap<TInput, TOutput> ComposeWith<TInput, TInter, TOutput>(this IMap<TInput, TInter> map1, IMap<TInter, TOutput> map2)
		{
			return new CompositionMap<TInput, TInter, TOutput>(map1, map2);
		}

		/// <summary>
		/// Creates a Composition Map based only on the CompositionForwardMap. This map pass the map1 Forward call as a parameter of the map2 Forward call making
		/// a composition of such methods. There is no Reverse call from this Map.
		/// </summary>
		/// <typeparam name="TInput">The type of the Input.</typeparam>
		/// <typeparam name="TInter">The type of the Intermediate value that allows comunication between map1 and map2.</typeparam>
		/// <typeparam name="TOutput">The type of the Output.</typeparam>
		/// <param name="map1">Base map where you apply this call.</param>
		/// <param name="map2">Map used for the composition.</param>
		public static IForwardMap<TInput, TOutput> ComposeWith<TInput, TInter, TOutput>(this IForwardMap<TInput, TInter> map1, IForwardMap<TInter, TOutput> map2)
		{
			return new CompositionForwardMap<TInput, TInter, TOutput>(map1, map2);
		}

		/// <summary>
		/// Creates a Composition Map based only on the CompositionReverseMap. This map pass the map2 Reverse call as a parameter of the map1 Reverse call making
		/// a composition of such methods. There is no Forward call from this Map.
		/// </summary>
		/// <typeparam name="TInput">The type of the Input.</typeparam>
		/// <typeparam name="TInter">The type of the Intermediate value that allows comunication between map1 and map2.</typeparam>
		/// <typeparam name="TOutput">The type of the Output.</typeparam>
		/// <param name="map1">Base map where you apply this call.</param>
		/// <param name="map2">Map used for the composition.</param>
		public static IReverseMap<TInput, TOutput> ComposeWith<TInput, TInter, TOutput>(this IReverseMap<TInput, TInter> map1, IReverseMap<TInter, TOutput> map2)
		{
			return new CompositionReverseMap<TInput, TInter, TOutput>(map1, map2);
		}

		/// <summary>
		/// Creates a Composed map with a 3D XYMap.
		/// </summary>
		/// <typeparam name="TInput">Type of the input.</typeparam>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="height">Height expected in the XYMap.</param>
		public static IMap<TInput, Vector3> To3DXY<TInput>(this IMap<TInput, Vector2> map, float height = 0)
		{
			return map.ComposeWith(new XYMap(height));
		}

		/// <summary>
		/// Creates a Composed map with a 3D XZMap.
		/// </summary>
		/// <typeparam name="TInput">Type of the input.</typeparam>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="height">Height expected in the XZMap.</param>
		public static IMap<TInput, Vector3> To3DXZ<TInput>(this IMap<TInput, Vector2> map, float height = 0)
		{
			return map.ComposeWith(new XZMap(height));
		}

		/// <summary>
		/// Creates a Composed map with a 2D XYMap.
		/// </summary>
		/// <typeparam name="TInput">Type of the input.</typeparam>
		/// <param name="map">Base map where you apply this call.</param>
		public static IMap<TInput, Vector2> To2DXY<TInput>(this IMap<TInput, Vector3> map)
		{
			return map.ComposeWith(new XYMap(0).Reverse());
		}

		/// <summary>
		/// Creates a Composed map with a 2D XZMap.
		/// </summary>
		/// <typeparam name="TInput">Type of the input.</typeparam>
		/// <param name="map">Base map where you apply this call.</param>
		public static IMap<TInput, Vector2> To2DXZ<TInput>(this IMap<TInput, Vector3> map)
		{
			return map.ComposeWith(new XZMap(0).Reverse());
		}

		/// <summary>
		/// It returns a Map that translate in such a way that it is aligned horizontally
		/// according to the given alignment.
		/// </summary>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="shape">Shape used in the map.</param>
		/// <param name="cellSize">This function is used to calculate the size of a cell.</param>
		/// <param name="bounds">Bounds of the Map.</param>
		/// <param name="alignment">Type of alignment to apply.</param>
		public static IMap<Vector3, Vector3> AlignGridHorizontal(this IMap<Vector3, Vector3> map, IExplicitShape<GridPoint2> shape, Func<GridPoint2, Vector3> cellSize, Bounds bounds, HorizontalAlignment alignment)
		{
			var gridBounds = map.CalculateBounds(shape, cellSize);
			float xOffset = AlignGridHorizontal(alignment, bounds, gridBounds);

			return map.PreTranslate(Vector3.right * xOffset);
		}

		/// <summary>
		/// It returns a Map that translate in such a way that it is aligned vertically
		/// according to the given alignment.
		/// </summary>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="shape">Shape used in the map.</param>
		/// <param name="cellSize">This function is used to calculate the size of a cell.</param>
		/// <param name="bounds">Bounds of the Map.</param>
		/// <param name="alignment">Type of alignment to apply.</param>
		public static IMap<Vector3, Vector3> AlignGridVertical(this IMap<Vector3, Vector3> map, IExplicitShape<GridPoint2> shape, Func<GridPoint2, Vector3> cellSize, Bounds bounds, VerticalAlignment alignment)
		{
			var gridBounds = map.CalculateBounds(shape, cellSize);
			float yOffset = AlignGridVertical(alignment, bounds, gridBounds);

			return map.PreTranslate(Vector3.up * yOffset);
		}

		/// <summary>
		/// It returns a Map that translate in such a way that it is aligned horizontally and vertically
		/// according to the given alignment.
		/// </summary>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="shape">Shape used in the map.</param>
		/// <param name="cellSize">This function is used to calculate the size of a cell.</param>
		/// <param name="bounds">Bounds of the Map.</param>
		/// <param name="horizontalAlignment">Type of horizontal alignment to apply.</param>
		/// <param name="verticalAlignment">Type of vertical alignment to apply.</param>
		public static IMap<Vector3, Vector3> AlignGridInRect(
			this IMap<Vector3, Vector3> map,
			IExplicitShape<GridPoint2> shape,
			Func<GridPoint2, Vector3> cellSize,
			Bounds bounds,
			HorizontalAlignment horizontalAlignment,
			VerticalAlignment verticalAlignment)
		{
			var gridBounds = map.CalculateBounds(shape, cellSize);

			var xOffset = AlignGridHorizontal(horizontalAlignment, bounds, gridBounds);
			var yOffset = AlignGridVertical(verticalAlignment, bounds, gridBounds);

			var translateMap = Translate(new Vector3(xOffset, yOffset, 0));
			return map.ComposeWith(translateMap);
		}

		public static Bounds CalculateBounds(this IMap<Vector3, Vector3> map, IExplicitShape<GridPoint2> shape, Func<GridPoint2, Vector3> cellSize)
		{
			if (!shape.Points.Any())
				return new Bounds(Vector3.zero, Vector3.zero);

			var firstPoint = shape.Points.First();
			var cellDimensions = cellSize(firstPoint);
			var worldPoint = map.Forward(firstPoint.ToVector2());

			var minX = worldPoint.x;
			var maxX = worldPoint.x + cellDimensions.x;
			var minY = worldPoint.y;
			var maxY = worldPoint.y + cellDimensions.y;
			var minZ = worldPoint.z;
			var maxZ = worldPoint.z + cellDimensions.z;

			foreach (var point in shape.Points.ButFirst())
			{
				cellDimensions = cellSize(point);
				worldPoint = map.Forward(point.ToVector2());

				minX = Mathf.Min(minX, worldPoint.x);
				maxX = Mathf.Max(maxX, worldPoint.x + cellDimensions.x);
				minY = Mathf.Min(minY, worldPoint.y);
				maxY = Mathf.Max(maxY, worldPoint.y + cellDimensions.y);
				minZ = Mathf.Min(minZ, worldPoint.y);
				maxZ = Mathf.Max(maxZ, worldPoint.y + cellDimensions.y);
			}

			var width = maxX - minX;
			var height = maxY - minY;
			var length = maxZ - minZ;

			var center = new Vector3((maxX + minX) / 2, (maxY + minY) / 2, (maxZ + minZ) / 2);
			var size = new Vector3(width, height, length);

			return new Bounds(center, size);
		}

		/// <summary>
		/// It returns a Map that translate in such a way that it is aligned horizontally
		/// according to the given alignment. This is different to the normal AlignGrid methods, since this
		/// one take in account the pivot used in the cell.
		/// </summary>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="shape">Shape used in the map.</param>
		/// <param name="cellSize">This function is used to calculate the size of a cell.</param>
		/// <param name="alignment">Type of alignment to apply.</param>
		public static IMap<Vector3, GridPoint3> AnchorPivotHorizontal(this IMap<Vector3, GridPoint3> map, IExplicitShape<GridPoint3> shape, Func<GridPoint3, Vector3> cellSize, HorizontalAlignment alignment)
		{
			var cellSizeResult = cellSize(shape.Points.First());

			float xOffset = AnchorHorizontalPivot(alignment, cellSizeResult);

			return map.PreTranslate(Vector3.right * xOffset);
		}

		/// <summary>
		/// It returns a Map that translate in such a way that it is aligned vertically
		/// according to the given alignment. This is different to the normal AlignGrid methods, since this
		/// one take in account the pivot used in the cell.
		/// </summary>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="shape">Shape used in the map.</param>
		/// <param name="cellSize">This function is used to calculate the size of a cell.</param>
		/// <param name="alignment">Type of alignment to apply.</param>
		public static IMap<Vector3, GridPoint3> AnchorPivotVertical(this IMap<Vector3, GridPoint3> map, IExplicitShape<GridPoint3> shape, Func<GridPoint3, Vector3> cellSize, VerticalAlignment alignment)
		{
			var cellSizeResult = cellSize(shape.Points.First());

			float yOffset = AnchorVerticalPivot(alignment, cellSizeResult);

			return map.PreTranslate(Vector3.up * yOffset);
		}

		/// <summary>
		/// It returns a Map that translate in such a way that it is aligned horizontally and vertically
		/// according to the given alignment. This is different to the normal AlignGrid methods, since this
		/// one take in account the pivot used in the cell.
		/// </summary>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="shape">Shape used in the map.</param>
		/// <param name="cellSize">This function is used to calculate the size of a cell.</param>
		/// <param name="horizontalAlignment">Type of horizontal alignment to apply.</param>
		/// <param name="verticalAlignment">Type of vertical alignment to apply.</param>
		public static IMap<Vector3, GridPoint3> AnchorPivotInRect(this IMap<Vector3, GridPoint3> map, IExplicitShape<GridPoint3> shape, Func<GridPoint3, Vector3> cellSize, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
		{
			var cellSizeResult = cellSize(shape.Points.First());

			var xOffset = AnchorHorizontalPivot(horizontalAlignment, cellSizeResult);
			var yOffset = AnchorVerticalPivot(verticalAlignment, cellSizeResult);

			return map.PreTranslate(new Vector3(xOffset, yOffset, 0));
		}

		/// <summary>
		/// It returns a Map that translate in such a way that it is aligned horizontally and vertically
		/// according to the given alignment. This is different to the normal AlignGrid methods, since this
		/// one take in account the pivot used in the cell.
		/// </summary>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="shape">Shape used in the map.</param>
		/// <param name="cellSize">This function is used to calculate the size of a cell.</param>
		/// <param name="horizontalAlignment">Type of horizontal alignment to apply.</param>
		/// <param name="verticalAlignment">Type of vertical alignment to apply.</param>
		public static IMap<Vector3, Vector3> AnchorPivotInRect<TPoint>(this IMap<Vector3, Vector3> map, IExplicitShape<TPoint> shape, Func<TPoint, Vector3> cellSize, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
		{
			var cellSizeResult = cellSize(shape.Points.First());

			var xOffset = AnchorHorizontalPivot(horizontalAlignment, cellSizeResult);
			var yOffset = AnchorVerticalPivot(verticalAlignment, cellSizeResult);

			var translateMap = Translate(new Vector3(xOffset, yOffset, 0));
			return map.ComposeWith(translateMap);

			//return map.PreTranslate(new Vector3(xOffset, yOffset));
		}

		/// <summary>
		/// Creates a map that Scale its width in a Proportional way.
		/// </summary>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="shape">Shape used in the map.</param>
		/// <param name="cellSize">This function is used to calculate the size of a cell.</param>
		/// <param name="rect">Bounds of the screen where the grid is.</param>
		public static IMap<Vector3, Vector3> StretchInRectWidthProportional(this IMap<Vector3, Vector3> map, IExplicitShape<GridPoint2> shape, Func<GridPoint2, Vector3> cellSize, Bounds rect)
		{
			var gridBounds = map.CalculateBounds(shape, cellSize);

			//TODO check this formula
			return map.PreScale(Vector2.one * (rect.size.x / gridBounds.size.y));
		}

		/// <summary>
		/// Creates a map that Scale its height in a Proportional way.
		/// </summary>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="shape">Shape used in the map.</param>
		/// <param name="cellSize">This function is used to calculate the size of a cell.</param>
		/// <param name="bounds">Bounds of the screen where the grid is.</param>
		public static IMap<Vector3, Vector3> StretchInRectHeightProportional(this IMap<Vector3, Vector3> map, IExplicitShape<GridPoint2> shape, Func<GridPoint2, Vector3> cellSize, Bounds bounds)
		{
			var gridBounds = map.CalculateBounds(shape, cellSize);
			//TODO check
			return map.PreScale(Vector2.one * (bounds.size.y / gridBounds.size.y));
		}

		/// <summary>
		/// Creates a map that Scale its height and weight in a Proportional way.
		/// </summary>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="shape">Shape used in the map.</param>
		/// <param name="cellSize">This function is used to calculate the size of a cell.</param>
		/// <param name="bounds">Bounds of the screen where the grid is.</param>
		public static IMap<Vector3, Vector3> StretchInRect(this IMap<Vector3, Vector3> map, IExplicitShape<GridPoint2> shape, Func<GridPoint2, Vector3> cellSize, Bounds bounds)
		{
			var gridBounds = map.CalculateBounds(shape, cellSize);

			return map.PreScale(bounds.size.HadamardDiv(gridBounds.size));
		}

		/// <summary>
		/// It stretch the grid to fill the rect where it is.
		/// </summary>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="shape">Shape used in the map.</param>
		/// <param name="cellSize">This function is used to calculate the size of a cell.</param>
		/// <param name="bounds">Bounds of the screen where the grid is.</param>
		public static IMap<Vector3, Vector3> StretchInRectToFit(this IMap<Vector3, Vector3> map, IExplicitShape<GridPoint2> shape, Func<GridPoint2, Vector3> cellSize, Bounds bounds)
		{
			var gridBounds = map.CalculateBounds(shape, cellSize);
			var ratios = bounds.size.HadamardDiv(gridBounds.size);

			if (ratios.x < ratios.y && ratios.x < ratios.z)
			{
				return map.StretchInRectWidthProportional(shape, cellSize, bounds);
			}

			return map.StretchInRectHeightProportional(shape, cellSize, bounds);
		}

		/// <summary>
		/// It stretch the grid to cover the rect where it is.
		/// </summary>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="shape">Shape used in the map.</param>
		/// <param name="cellSize">This function is used to calculate the size of a cell.</param>
		/// <param name="bounds">Bounds of the screen where the grid is.</param>
		public static IMap<Vector3, Vector3> StretchInRectToCover(this IMap<Vector3, Vector3> map, IExplicitShape<GridPoint2> shape, Func<GridPoint2, Vector3> cellSize, Bounds bounds)
		{
			var gridBounds = map.CalculateBounds(shape, cellSize);
			var ratios = bounds.size.HadamardDiv(gridBounds.size);

			if (ratios.x < ratios.y)
			{
				return map.StretchInRectHeightProportional(shape, cellSize, bounds);
			}

			return map.StretchInRectWidthProportional(shape, cellSize, bounds);
		}

		/// <summary>
		/// Creates an Identity Map where the TInput and TOutput are the same.
		/// </summary>
		/// <typeparam name="TInput">Type of the Input</typeparam>
		public static IMap<TInput, TInput> Identity<TInput>()
		{
			return new IdentityMap<TInput>();
		}

		/// <summary>
		/// Creates a Combined Map. This map use the forward map to call the Forward method and the base map to call the Reverse Map.
		/// </summary>
		/// <typeparam name="TInput">Type of the Input.</typeparam>
		/// <typeparam name="TOutput">Type of the Output.</typeparam>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="forward">Forward map to combined with the base map.</param>
		public static IMap<TInput, TOutput> AddForward<TInput, TOutput>(this IReverseMap<TInput, TOutput> map, IForwardMap<TInput, TOutput> forward)
		{
			return new CombinedMap<TInput, TOutput>(forward, map);
		}

		/// <summary>
		/// Creates a Combined Map. This map use the reverse map to call the Reverse method and the base map to call the Forward Map.
		/// </summary>
		/// <typeparam name="TInput">Type of the Input.</typeparam>
		/// <typeparam name="TOutput">Type of the Output.</typeparam>
		/// <param name="map">Base map where you apply this call.</param>
		/// <param name="reverse">Reverse map to combined with the base map.</param>
		public static IMap<TInput, TOutput> AddReverse<TInput, TOutput>(this IForwardMap<TInput, TOutput> map, IReverseMap<TInput, TOutput> reverse)
		{
			return new CombinedMap<TInput, TOutput>(map, reverse);
		}

		/// <summary>
		/// Creates a FuncForwardMap. This map saves a function to be used in a Forward call.
		/// </summary>
		/// <typeparam name="TInput">Type of the Input.</typeparam>
		/// <typeparam name="TOutput">Type of the Output.</typeparam>
		/// <param name="forward">This function is used when the Forward method of the map is called.</param>
		public static IForwardMap<TInput, TOutput> Func<TInput, TOutput>(Func<TInput, TOutput> forward)
		{
			return new FuncForwardMap<TInput, TOutput>(forward);
		}

		/// <summary>
		/// Creates a Func Map that calls a forward and reverse function in is Forward and Reverse calls.
		/// </summary>
		/// <typeparam name="TInput">Type of the Input.</typeparam>
		/// <typeparam name="TOutput">Type of the Output.</typeparam>
		/// <param name="forward">This function will be used when calling the Forward map.</param>
		/// <param name="reverse">This function will be used when calling the Reverse map.</param>
		public static IMap<TInput, TOutput> Func<TInput, TOutput>(Func<TInput, TOutput> forward, Func<TOutput, TInput> reverse)
		{
			return Func(forward).AddReverse(Func(reverse).Reverse());
		}

		/// <summary>
		/// Creates a 3D Vector Translate Map.
		/// </summary>
		/// <param name="offset">Offset of the translation.</param>
		public static IMap<Vector3, Vector3> Translate(Vector3 offset)
		{
			return new Translate3Map(offset);
		}

		/// <summary>
		/// Creates a 2D Vector Translate Map.
		/// </summary>
		/// <param name="offset">Offset of the translation.</param>
		public static IMap<Vector2, Vector2> Translate(Vector2 offset)
		{
			return new Translate2Map(offset);
		}

		/// <summary>
		/// Creates a 1D Translate Map.
		/// </summary>
		/// <param name="offset">Offset of the translation.</param>
		public static IMap<float, float> Translate(float offset)
		{
			return new Translate1Map(offset);
		}

		/// <summary>
		/// Creates a 3D Discrete Translate Map.
		/// </summary>
		/// <param name="offset">Offset of the translation.</param>
		public static IMap<GridPoint3, GridPoint3> Translate(GridPoint3 offset)
		{
			return new DiscreteTranslateMap3(offset);
		}

		/// <summary>
		/// Creates a 2D Discrete Translate Map.
		/// </summary>
		/// <param name="offset">Offset of the translation.</param>
		public static IMap<GridPoint2, GridPoint2> Translate(GridPoint2 offset)
		{
			return new DiscreteTranslateMap2(offset);
		}

		/// <summary>
		/// Creates a 1D Discrete Translate Map.
		/// </summary>
		/// <param name="offset">Offset of the translation.</param>
		public static IMap<int, int> Translate(int offset)
		{
			return new DiscreteTranslateMap1(offset);
		}

		/// <summary>
		/// Creates a PolarMap. This map transform a cartesian coordinate into a polar coordinate.
		/// </summary>
		/// <param name="radius">Radius magnitude of the polar map.</param>
		/// <param name="theta">Angle magnitude of the polar map.</param>
		public static IMap<Vector3, Vector3> Polar(float radius, float theta)
		{
			return new PolarMap(radius, theta);
		}

		//TODO: @herman, check this to add more details.
		/// <summary>
		/// Creates a HeightMap.
		/// </summary>
		/// <param name="heights"></param>
		/// <param name="heightScale"></param>
		/// <param name="roundMap"></param>
		public static IMap<Vector3, Vector3> Height(Grid2<float> heights, float heightScale, IMap<Vector3, GridPoint2> roundMap)
		{
			return new HeightMap(heights, heightScale, roundMap);
		}

		/// <summary>
		/// Creates a map that shift the axes of a map from XYZ to XZY.
		/// </summary>
		/// <param name="map">Base map that get its axes shift.</param>
		public static IMap<Vector3, Vector3> XYZToXZY(this IMap<Vector3, Vector3> map)
		{
			return map.ComposeWith(new SwapYZMap());
		}

		/// <summary>
		/// Creates a map that shift the axes of a map from XYZ to YZX.
		/// </summary>
		/// <param name="map">Base map that get its axes shift.</param>
		public static IMap<Vector3, Vector3> XYZToYZX(this IMap<Vector3, Vector3> map)
		{
			return map.ComposeWith(new LeftShiftAxesMap());
		}

		/// <summary>
		/// Creates a map that shift the axes of a map from XYZ to YXZ.
		/// </summary>
		/// <param name="map">Base map that get its axes shift.</param>
		public static IMap<Vector3, Vector3> XYZToYXZ(this IMap<Vector3, Vector3> map)
		{
			return map.ComposeWith(new SwapXYMap());
		}

		/// <summary>
		/// Creates a Linear Map that takes a matrix and multiply it with the given position in a linear way.
		/// </summary>
		/// <param name="matrix">Matrix you want to operate.</param>
		public static IMap<Vector3, Vector3> Linear(Matrixf33 matrix)
		{
			return new LinearMap(matrix);
		}

		/// <summary>
		/// Creates a FuncX on X Map that given a function it alter the X value of the coordinates of the map.
		/// </summary>
		/// <param name="func">This function is used to alter the X value of the coordinates of the map.</param>
		public static IMap<Vector3, Vector3> FuncXOnX(Func<float, float> func)
		{
			return new FuncXonXMap(func);
		}

		/// Creates a FuncX on Y Map that given a function it alter the Y value of the coordinates of the map.
		/// </summary>
		/// <param name="func">This function is used to alter the Y value of the coordinates of the map.</param>
		public static IMap<Vector3, Vector3> FuncXOnY(Func<float, float> func)
		{
			return new FuncXonYMap(func);
		}

		/// <summary>
		/// Equivalent to a scale map.
		/// </summary>
		/// <param name="cellDimensions">New dimensions for the cells.</param>
		public static IMap<Vector3, Vector3> Block(Vector3 cellDimensions)
		{
			return Linear(Matrixf33.Scale(cellDimensions));
		}

		/// <summary>
		/// Creates a map that block rounds from Vector3 to GridPoint3
		/// </summary>
		public static IMap<Vector3, GridPoint3> BlockRound()
		{
			return new BlockRoundMap();
		}

		/// <summary>
		/// Creates a map that hex-block rounds from Vector3 to GridPoint3
		/// </summary>
		[Version(2, 2)]
		public static IMap<Vector3, GridPoint3> HexBlockRound()
		{
			return new HexBlockRoundMap();
		}

		/// <summary>
		/// Creates a Hex Round map that hex round from Vector3 to GridPoint2
		/// </summary>
		public static IMap<Vector3, GridPoint2> HexRound()
		{
			return new HexRoundMap();
		}

		//TODO: @herman
		public static IMap<Vector3, Vector3> ParallelogramWrapX(float width)
		{
			return new ParallelogramWrapXMap(width);
		}

		//TODO: @herman
		public static IMap<Vector3, Vector3> ParallelogramWrapXY(float width, float height)
		{
			return new ParallelogramWrapXYMap(width, height);
		}

		/// <summary>
		/// Creates a map that rounds Vector3 to Gridpoint3 by rounding the x coordinate.
		/// </summary>
		/// <returns></returns>
		public static IMap<Vector3, int> PointRound()
		{
			return new PointRoundMap();
		}

		/// <summary>
		/// Creates a map that rounds vectors to GridPoint2 by rounding the coordinates.
		/// </summary>
		public static IMap<Vector3, GridPoint2> RectRound()
		{
			return new RectRoundMap();
		}

		/// <summary>
		/// Creates a map that rounds vectors to GridPoint2 by flooring the coordinates.
		/// </summary>
		public static IMap<Vector3, GridPoint2> RectFloor()
		{
			return new RectFloorMap();
		}

		//TODO Make 2D / 3D versions?
		/// <summary>
		/// Creates a map that translate before applying the given map.
		/// </summary>
		/// <typeparam name="TPoint">Type of the point</typeparam>
		/// <param name="map">Base map used in the operations.</param>
		/// <param name="offset">Offset to translate the map.</param>
		public static IMap<Vector3, TPoint> PreTranslate<TPoint>(this IMap<Vector3, TPoint> map, Vector3 offset)
		{
			return Translate(offset).ComposeWith(map);
		}

		/// <summary>
		/// Creates a map that scales a vector before applying the given map.
		/// </summary>
		/// <typeparam name="TPoint">Type of the point</typeparam>
		/// <param name="map">Base map used in the operations.</param>
		/// <param name="scale">Scale factor used in the map.</param>
		public static IMap<Vector3, TPoint> PreScale<TPoint>(this IMap<Vector3, TPoint> map, Vector3 scale)
		{
			return new ScaleMap3(scale).ComposeWith(map);
		}

		//TODO: @herman
		public static IMap<Vector3, GridPoint2> PolygonOffset(this IMap<Vector3, GridPoint2> roundMap, IPointPartition2 partition, IList<GridPoint2> offsets)
		{
			return new PolygonPartitionOffsetMap(roundMap, partition, offsets);
		}

		public static IMap<Vector3, Vector3> Twirl(float anglePerRadius)
		{
			return Func<Vector3, Vector3>(x => Twirl(x, anglePerRadius), x => TwirlReverse(x, anglePerRadius));
		}

		#endregion

		#region Private methods

		public static Vector3 Twirl(Vector3 input, float anglePerRadius)
		{
			float radius = Mathf.Sqrt(input.x * input.x + input.y * input.y);

			if (radius == 0)
				return input;

			float angle = Mathf.Atan2(input.y, input.x);
			float angleOffset = radius * anglePerRadius;
			float newAngle = angle + angleOffset;

			float newX = radius * Mathf.Cos(newAngle);
			float newY = radius * Mathf.Sin(newAngle);

			return new Vector3(newX, newY, input.z);
		}

		public static Vector3 TwirlReverse(Vector3 output, float anglePerRadius)
		{
			float radius = Mathf.Sqrt(output.x * output.x + output.y * output.y);

			if (radius == 0)
				return output;

			float angle = Mathf.Atan2(output.y, output.x);
			float angleOffset = radius * anglePerRadius;
			float newAngle = angle - angleOffset;

			float newX = radius * Mathf.Cos(newAngle);
			float newY = radius * Mathf.Sin(newAngle);

			return new Vector3(newX, newY, output.z);
		}

		#endregion
	}
}