using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that applies a pointy hex transform on the input.
	/// </summary>
	[SpaceMapNode("Linear/Rect Map", 3)]
	public class RectSpaceMap : ProjectSpaceMapNode<Vector3, Vector3>
	{
		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			var linearMap = Map.Identity<Vector3>();

			return (input == null) ? linearMap : input.ComposeWith(linearMap);
		}
	}
}