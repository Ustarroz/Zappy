using System;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Permutates the coordinates of the Shape.
	/// </summary>
	/// <seealso cref="ProjectShapeNode{TPoint,TInput,TOutput}"/>
	[ShapeNode("Operator/Permutate Coordinates", 3)]
	class PermutateCoordinatesShape3Node : ProjectShapeNode<GridPoint3, IExplicitShape<GridPoint3>, IExplicitShape<GridPoint3>>
	{
		public CoordinatePermutation newPermutation = CoordinatePermutation.XYZ;

		public override IExplicitShape<GridPoint3> Transform(IExplicitShape<GridPoint3> input)
		{
			switch (newPermutation)
			{
				case CoordinatePermutation.XYZ:
					break;
				case CoordinatePermutation.XZY:
					return input.SwapToXZY();
				case CoordinatePermutation.YXZ:
					return input.SwapToYXZ();
				case CoordinatePermutation.YZX:
					return input.SwapToYZX();
				case CoordinatePermutation.ZXY:
					return input.SwapToZXY();
				case CoordinatePermutation.ZYX:
					return input.SwapToZYX();
				default:
					throw new ArgumentOutOfRangeException();
			}

			return input;
		}
	}
}
