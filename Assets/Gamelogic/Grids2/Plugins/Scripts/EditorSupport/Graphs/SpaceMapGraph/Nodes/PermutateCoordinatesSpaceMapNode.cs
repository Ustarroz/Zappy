using System;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Permutates the coordinates.
	/// </summary>
	/// <seealso cref="ProjectSpaceMapNode{TInput,TOutput}" />
	[SpaceMapNode("Linear/Permutate Coordinates", 3)]
	public class PermutateCoordinatesSpaceMapNode : ProjectSpaceMapNode<Vector3, Vector3>
	{
		public CoordinatePermutation newPermutation;

		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			var leftMap = input ?? Map.Linear(Matrixf33.Identity);

			switch (newPermutation)
			{
				case CoordinatePermutation.XYZ:
					return leftMap;
				case CoordinatePermutation.XZY:
					return leftMap.XYZToYXZ().XYZToYZX();
				case CoordinatePermutation.YXZ:
					return leftMap.XYZToYXZ();
				case CoordinatePermutation.YZX:
					return leftMap.XYZToYZX();
				case CoordinatePermutation.ZXY:
					return leftMap.XYZToYZX().XYZToYZX();
				case CoordinatePermutation.ZYX:
					return leftMap.XYZToYXZ().XYZToYZX().XYZToYZX();
				default:
					throw new ArgumentOutOfRangeException();

			}
		}
	}
}