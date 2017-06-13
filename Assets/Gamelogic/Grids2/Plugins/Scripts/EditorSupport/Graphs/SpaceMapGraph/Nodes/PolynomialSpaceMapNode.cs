using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A <see cref="FuncSpaceMapNode"/> that uses a polynomial function as its given function.
	/// </summary>
	/// <seealso cref="FuncSpaceMapNode" />
	[SpaceMapNode("Func/Polynomial", 3)]
	class PolynomialSpaceMapNode : FuncSpaceMapNode
	{
		public float[] coefficients = new[] { 1f, 1 };

		public override float Func(float baseNumber)
		{
			var result = 0.0f;

			for (var i = coefficients.Length - 1; i >= 0; i--)
			{
				result += coefficients[i] * Mathf.Pow(baseNumber, i);
			}

			return result;
		}
	}
}
