using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A <see cref="FuncSpaceMapNode"/> that uses an exponential function as its given function.
	/// </summary>
	/// <seealso cref="FuncSpaceMapNode" />
	[SpaceMapNode("Func/Exponential", 3)]
	class ExponentialSpaceMapNode : FuncSpaceMapNode
	{
		public float powerBase = 2;

		public override float Func(float exponent)
		{
			return Mathf.Pow(powerBase, exponent);
		}
	}
}
