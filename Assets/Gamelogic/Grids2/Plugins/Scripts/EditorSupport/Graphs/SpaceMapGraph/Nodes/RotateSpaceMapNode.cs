using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A <see cref="FuncSpaceMapNode"/> that rotates its input about the origin.
	/// </summary>
	/// <seealso cref="FuncSpaceMapNode" />
	[SpaceMapNode("Linear/Rotate", 3)]
	class RotateSpaceMapNode : ProjectSpaceMapNode<Vector3, Vector3>
	{
		[Tooltip("Euler Angles in degrees.")]
		public Vector3 rotation = Vector3.zero; //Degrees

		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			var matrix = Matrixf33.RotateXYZ(rotation * Mathf.Deg2Rad);
			var map = Map.Linear(matrix);

			return (input == null) ? map : input.ComposeWith(map);
		}
	}
}
