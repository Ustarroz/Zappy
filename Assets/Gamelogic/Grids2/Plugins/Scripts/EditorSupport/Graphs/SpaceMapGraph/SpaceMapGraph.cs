using System;
using System.Linq;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A graph for making a space map, that is, a map from <see cref="Vector3"/> to Vector3.
	/// </summary>
	/// <seealso cref="Graph{MapNode{Vector3}}" />
	[CreateAssetMenu(fileName = "SpaceMapGraph", menuName = "Grids/Space Map Graph")]
	public class SpaceMapGraph : Graph<SpaceMapNode<Vector3>>
	{
		/// <summary>
		/// Gets built from this graph.
		/// </summary>
		/// <returns>IMap&lt;Vector3, Vector3&gt;.</returns>
		/// <exception cref="InvalidOperationException">Graph has no output node.</exception>
		public IMap<Vector3, Vector3> GetMap()
		{
			Recompute();

			var node = (OutputSpaceMapNode)Nodes.FirstOrDefault(n => n is OutputSpaceMapNode);

			if (node == null)
			{
				throw new InvalidOperationException("Graph has no output node.");
			}

			if (node.Output.Count <= 0)
			{
				throw new InvalidOperationException("Output node has no output. Does it have input?");
			}



			return node.Output[0];
		}
	}
}