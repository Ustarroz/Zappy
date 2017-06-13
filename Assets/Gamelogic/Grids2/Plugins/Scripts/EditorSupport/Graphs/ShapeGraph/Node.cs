using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// The base class for all nodes in a <see cref="Graph"/>.
	/// </summary>
	[Version(1, 1)]
	[Abstract]
	[Serializable]
	public class ShapeNode<TPoint> : BaseNode { }

	/// <summary>
	/// A node with typed output.
	/// </summary>
	/// <typeparam name="TOutput">The type of the output for this node.</typeparam>
	/// <typeparam name="TPoint"></typeparam>
	[Abstract]
	public class ShapeNode<TPoint, TOutput> : ShapeNode<TPoint>
	{
		[HideInInspector]
		[SerializeField]
		private List<TOutput> output;

		/// <summary>
		/// A list of outputs for this node.
		/// </summary>
		public List<TOutput> Output
		{
			get { return output; }
			protected set { output = value; }
		}
	}

	/// <summary>
	/// A Node with typed inputs and outputs.
	/// </summary>
	/// <typeparam name="TInput"></typeparam>
	/// <typeparam name="TOutput"></typeparam>
	/// <typeparam name="TPoint"></typeparam>
	[Abstract]
	public class ShapeNode<TPoint, TInput, TOutput> : ShapeNode<TPoint, TOutput>
	{
		[HideInInspector]
		public List<BaseNode> inputNodes = new List<BaseNode>();

		/// <summary>
		/// Total number of items coming into the node.
		/// </summary>
		protected int InputItemCount
		{
			get { return inputNodes.Cast<ShapeNode<TPoint, TOutput>>().Sum(n => n.Output.Count); }
		}

		/// <summary>
		/// Calculates a list of output from a given list of input.
		/// </summary>
		/// <param name="input">The input values to base the computation on.</param>
		/// <returns>The list of output values.</returns>
		[Abstract]
		public virtual List<TOutput> Execute(IEnumerable<TInput> input)
		{
			throw new NotImplementedException();
		}

		public sealed override void UpdateStatic()
		{
			foreach (var node in inputNodes)
			{
				node.UpdateStatic();
			}

			if (enable)
			{
				var inputs = inputNodes.Cast<ShapeNode<TPoint, TInput>>().Select(node => node.Output).SelectMany(x => x);
				Output = Execute(inputs);
			}
			else
			{
				var inputs = inputNodes.Cast<ShapeNode<TPoint, TOutput>>().Select(node => node.Output).SelectMany(x => x);
				Output = inputs.ToList();
			}
		}

		public sealed override IEnumerable<BaseNode> Inputs
		{
			get { return inputNodes; }
		}

		public sealed override void AddNodeInput(BaseNode input)
		{
			var inputNode = input as ShapeNode<TPoint, TInput>;

			if (inputNode != null)
			{
				inputNodes.Add(inputNode);
			}
		}

		public sealed override void RemoveNodeInput(BaseNode input)
		{
			inputNodes.Remove(input);
		}
	}
}