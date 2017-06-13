using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// The output node for space map graphs.
	/// </summary>
	/// <seealso cref="ProjectSpaceMapNode{TInput,TOutput}" />
	[SpaceMapNode("Output/Output", 3)]
	public class OutputSpaceMapNode : ProjectSpaceMapNode<Vector3, Vector3>
	{
		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			return input;
		}
	}
}