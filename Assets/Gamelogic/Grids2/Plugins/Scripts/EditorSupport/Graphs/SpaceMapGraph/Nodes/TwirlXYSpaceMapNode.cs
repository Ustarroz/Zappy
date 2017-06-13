using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node for making a transformation that rotates points by an amount proportional to the origin.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.ProjectSpaceMapNode{UnityEngine.Vector3, UnityEngine.Vector3}" />
	[SpaceMapNode("Polar/Twirl XY", 3)]
	public class TwirlXYSpaceMapNode : ProjectSpaceMapNode<Vector3, Vector3>
	{
		public float anglePerRadius;

		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			var map = Map.Twirl(anglePerRadius);

			if (input == null) return map;

			return input.ComposeWith(map);
		}
	}
}