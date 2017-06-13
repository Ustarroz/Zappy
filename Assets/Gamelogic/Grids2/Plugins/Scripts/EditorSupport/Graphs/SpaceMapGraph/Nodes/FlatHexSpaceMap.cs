using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that applies a flat hex transform on the input.
	/// </summary>
	[SpaceMapNode("Linear/Flat Hex Space Map", 3)]
	public class FlatHexSpaceMap : ProjectSpaceMapNode<Vector3, Vector3>
	{
		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			var linearMap = Map.Linear(PointyHexPoint.SpaceMapTransform.Mul(Matrixf33.RotateZ(-30 * Mathf.Deg2Rad)));

			return (input == null) ? linearMap : input.ComposeWith(linearMap);
		}
	}
}