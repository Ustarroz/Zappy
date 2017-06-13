using System.Collections.Generic;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A class the represents a computational graph.
	/// </summary>
	/// <remarks>This is the non-generic base class, see <see cref="Graph{TNode}"/> for details.</remarks>
	public class Graph : ScriptableObject
	{ }

	/// <summary>
	/// A class the represents a computational graph.
	/// </summary>
	/// <typeparam name="TNode">The node type for this graph.</typeparam>
	/// <seealso cref="Gamelogic.Grids2.Graph.Graph" />
	/// <remarks>Each node in this graph takes some inputs, and calculates outputs,
	/// that can in turn be fed as inputs into other nodes.
	/// All nodes produces outputs as lists. When a node has multiple inputs,
	/// these are all combined into a single list for the node to operate on.
	/// The output of one node can only be connected to the input of another node if the types match.</remarks>
	[Version(1, 1)]
	public class Graph<TNode> : Graph
		where TNode : BaseNode
	{
		#region Private Fields

		[HideInInspector]
		[SerializeField]
		private int idCounter;

		[HideInInspector]
		[SerializeField]
		// ReSharper disable once FieldCanBeMadeReadOnly.Local
		// Cannot be readonly because it is serialized.
		private List<BaseNode> nodes = new List<BaseNode>();

		#endregion

		#region Properties

		/// <summary>
		/// Returns all the nodes in this graph.
		/// </summary>
		public List<BaseNode> Nodes
		{
			get { return nodes; }
		}

		/// <summary>
		/// For Editor internal routines only.
		/// Only use if you are completely sure of what you are doing.
		/// </summary>
		[EditorInternal]
		public int __IdCounter
		{
			get { return idCounter; }
			set { idCounter = value; }
		}

		/// <summary>
		/// For Editor internal routines only. Use <see cref="Nodes"/> instead.
		/// </summary>
		[EditorInternal]
		public List<BaseNode> __Nodes
		{
			get { return nodes; }
			set { nodes = value; }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Calls recompute on all nodes in the graph.
		/// </summary>
		public void Recompute()
		{
			foreach (var node in nodes)
			{
				node.Recompute();
			}

			foreach (var node in nodes)
			{
				node.UpdateStatic();
			}
		}

		#endregion
	}
}
