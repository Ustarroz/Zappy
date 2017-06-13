using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A <see cref="FuncSpaceMapNode"/> that uses a sine function as its given function.
	/// </summary>
	/// <seealso cref="FuncSpaceMapNode" />
	[SpaceMapNode("Func/Sin", 3)]
	class SinSpaceMapNode : FuncSpaceMapNode
	{
		public float amplitude = 1;
		public float frequency = 1;//In rad TODO change to degrees

		public override float Func(float x)
		{
			return amplitude * Mathf.Sin(frequency * 2 * Mathf.PI * x);
		}
	}
}
