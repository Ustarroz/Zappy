using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A <see cref="FuncSpaceMapNode"/> that scales its input.
	/// </summary>
	/// <seealso cref="FuncSpaceMapNode" />
	[SpaceMapNode("Linear/Scale", 3)]
	class ScaleSpaceMapNode : ProjectSpaceMapNode<Vector3, Vector3>
	{
		public Vector3 scaleFactor = new Vector3(1, 1, 1);

		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			var matrix = Matrixf33.Scale(scaleFactor);
			var map = Map.Linear(matrix);

			return (input == null) ? map : input.ComposeWith(map);
		}
	}
}
