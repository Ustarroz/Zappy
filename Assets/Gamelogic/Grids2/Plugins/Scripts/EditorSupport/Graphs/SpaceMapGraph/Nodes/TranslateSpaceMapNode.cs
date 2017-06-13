using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A <see cref="FuncSpaceMapNode"/> that translates its input.
	/// </summary>
	/// <seealso cref="FuncSpaceMapNode" />
	[SpaceMapNode("Linear/Translate", 3)]
	public class TranslateSpaceMapNode : ProjectSpaceMapNode<Vector3, Vector3>
	{
		public Vector3 offset;

		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			var map = Map.Translate(offset);

			if (input == null) return map;

			return input.ComposeWith(map);
		}
	}
}