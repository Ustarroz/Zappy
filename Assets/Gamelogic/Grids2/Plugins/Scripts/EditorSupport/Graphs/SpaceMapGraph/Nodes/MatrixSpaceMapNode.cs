using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Transforms input using a specified matrix.
	/// </summary>
	/// <seealso cref="ProjectSpaceMapNode{TInput,TOutput}" />
	[SpaceMapNode("Linear/General Matrix", 3)]
	class MatrixSpaceMapNode : ProjectSpaceMapNode<Vector3, Vector3>
	{
		public InspectableMatrixf3x3 matrix;

		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			var matrix33 = matrix.GetMatrix();
			var linearMap = Map.Linear(matrix33);

			return (input == null) ? linearMap : input.ComposeWith(linearMap);
		}
	}
}
