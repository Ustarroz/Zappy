using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A <see cref="FuncSpaceMapNode"/> that uses a logarithmic function as its given function.
	/// </summary>
	/// <seealso cref="FuncSpaceMapNode" />
	[SpaceMapNode("Func/Log", 3)]
	public class LogSpaceMapNode : FuncSpaceMapNode
	{
		public float powerBase;

		public override float Func(float x)
		{
			return Mathf.Log(x, powerBase);
		}
	}
}