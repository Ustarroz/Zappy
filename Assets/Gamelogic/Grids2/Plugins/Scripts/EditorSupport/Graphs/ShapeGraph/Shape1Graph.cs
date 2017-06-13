using System.Linq;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A graph that represents a 1D shape.
	/// </summary>
	/// <seealso>
	///     <cref>Graph{ShapeNode{int}}</cref>
	/// </seealso>
	[CreateAssetMenu(fileName = "ShapeGraph", menuName = "Grids/Shape Graph 1D")]
	public class Shape1Graph : Graph<ShapeNode<int>>
	{
		/// <summary>
		/// Gets the shape that this graph represents. If no output node is present,
		/// an empty shape is returned.
		/// </summary>
		/// <returns>IExplicitShape&lt;System.Int32&gt;.</returns>
		public IExplicitShape<int> GetShape()
		{
			Recompute();

			foreach (var outputNode in Nodes.OfType<OutputShape1Node>())
			{
				return outputNode.Output[0];
			}

			return ExplicitShape.Empty1();
		}
	}
}