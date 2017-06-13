using System.Linq;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A graph that represents a 2D shape.
	/// </summary>
	/// <seealso>
	///     <cref>Graph{ShapeNode{GridPoint2}}</cref>
	/// </seealso>
	[CreateAssetMenu(fileName = "ShapeGraph", menuName = "Grids/Shape Graph 2D")]
	public class Shape2Graph : Graph<ShapeNode<GridPoint2>>
	{
		/// <summary>
		/// Gets the shape that this graph represents. If no output node is present,
		/// an empty shape is returned.
		/// </summary>
		/// <returns>IExplicitShape&lt;GridPoint2&gt;.</returns>
		public IExplicitShape<GridPoint2> GetShape()
		{
			Recompute();

			foreach (var outputNode in Nodes.OfType<OutputShape2Node>())
			{
				return outputNode.Output[0];
			}

			return ExplicitShape.Empty2();
		}
	}
}