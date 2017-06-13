using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A <see cref="FuncSpaceMapNode"/> that uses the absolute value function.
	/// </summary>
	/// <seealso cref="Gamelogic.Grids2.Graph.FuncSpaceMapNode" />
	[SpaceMapNode("Func/Abs", 3)]
	class AbsSpaceMapNode : FuncSpaceMapNode
	{
		public override float Func(float x)
		{
			return Mathf.Abs(x);
		}
	}
}
