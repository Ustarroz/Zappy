using System.Linq;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A graph that represents a 3D shape.
	/// </summary>
	/// <seealso>
	///     <cref>Graph{ShapeNode{GridPoint3}}</cref>
	/// </seealso>
	[CreateAssetMenu(fileName = "ShapeGraph", menuName = "Grids/Shape Graph 3D")]
	public class Shape3Graph : Graph<ShapeNode<GridPoint3>>
	{
		/// <summary>
		/// Gets the shape that this graph represents. If no output node is present,
		/// an empty shape is returned.
		/// </summary>
		/// <returns>IExplicitShape&lt;GridPoint3&gt;.</returns>
		public IExplicitShape<GridPoint3> GetShape()
		{
			Recompute();

			foreach (var outputNode in Nodes.OfType<OutputShape3Node>())
			{
				return outputNode.Output[0];
			}

			return ExplicitShape.Empty3();
		}
	}
}