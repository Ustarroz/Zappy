using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that applies a isometric transform on the input.
	/// </summary>
	[SpaceMapNode("Linear/Isometric Space Map", 3)]
	public class IsometricSpaceMap : ProjectSpaceMapNode<Vector3, Vector3>
	{
		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			var linearMap = Map.Linear(
				Matrixf33
					.RotateZ(45 * Mathf.Deg2Rad)
					.Mul(Matrixf33.Scale(new Vector3(PointyHexPoint.Sqrt3, 1, 1))));

			return (input == null) ? linearMap : input.ComposeWith(linearMap);
		}
	}
}