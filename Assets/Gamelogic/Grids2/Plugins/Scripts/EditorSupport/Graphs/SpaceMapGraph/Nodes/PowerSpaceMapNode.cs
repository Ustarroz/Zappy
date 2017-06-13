using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A <see cref="FuncSpaceMapNode"/> that uses a power function as its given function.
	/// </summary>
	/// <seealso cref="FuncSpaceMapNode" />
	[SpaceMapNode("Func/Power", 3)]
	class PowerSpaceMapNode : FuncSpaceMapNode
	{
		public float exponent = 2;

		public override float Func(float powerBase)
		{
			return Mathf.Pow(powerBase, exponent);
		}
	}
}
