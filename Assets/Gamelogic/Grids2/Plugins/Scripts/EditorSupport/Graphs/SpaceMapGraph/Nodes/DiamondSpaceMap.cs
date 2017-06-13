using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that applies a diamond transform on the input.
	/// </summary>
	[SpaceMapNode("Linear/Diamond Space Map", 3)]
	public class DiamondSpaceMap : ProjectSpaceMapNode<Vector3, Vector3>
	{
		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			var linearMap = Map.Linear(Matrixf33.RotateZ(45 * Mathf.Deg2Rad));

			return (input == null) ? linearMap : input.ComposeWith(linearMap);
		}
	}
}