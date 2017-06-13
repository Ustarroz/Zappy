using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Does a polar transform on the XY-coordinates.
	/// </summary>
	/// <seealso cref="ProjectSpaceMapNode{TInput,TOutput}" />
	[SpaceMapNode("Polar/Polar XY Map", 3)]
	public class PolarXYSpaceMapNode : ProjectSpaceMapNode<Vector3, Vector3>
	{
		public float radius;
		public float angularFrequency;

		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			var map = Map.Polar(radius, angularFrequency);

			if (input == null) return map;

			return input.ComposeWith(map);
		}
	}
}