using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// The base class for all nodes in a map graph/>.
	/// </summary>
	[Version(1, 1)]
	[Abstract]
	public class SpaceMapNode : BaseNode
	{ }

	/// <summary>
	/// A node with typed output.
	/// </summary>
	/// <typeparam name="TOutput">The type of the output for this node.</typeparam>
	[Abstract]
	public class SpaceMapNode<TOutput> : SpaceMapNode
	{
		[HideInInspector]
		[SerializeField]
		private List<IMap<Vector3, TOutput>> output;

		/// <summary>
		/// A list of outputs for this node.
		/// </summary>
		public List<IMap<Vector3, TOutput>> Output
		{
			get { return output; }
			protected set { output = value; }
		}

		public override void Recompute() { }
	}

	/// <summary>
	/// A Node with typed inputs and outputs.
	/// </summary>
	/// <typeparam name="TInput"></typeparam>
	/// <typeparam name="TOutput"></typeparam>
	/// <typeparam name="TPoint"></typeparam>
	[Abstract]
	public class SpaceMapNode<TInput, TOutput> : SpaceMapNode<TOutput>
	{
		[HideInInspector]
		public List<BaseNode> inputNodes = new List<BaseNode>();

		/// <summary>
		/// Total number of items coming into the node.
		/// </summary>
		protected int InputItemCount
		{
			get { return inputNodes.Cast<SpaceMapNode<TOutput>>().Sum(n => n.Output.Count); }
		}

		public sealed override void Recompute() { }

		/// <summary>
		/// Calculates a list of output from a given list of input.
		/// </summary>
		/// <param name="input">The input values to base the computation on.</param>
		/// <returns>The list of output values.</returns>
		[Abstract]
		public virtual List<IMap<Vector3, TOutput>> Execute(IEnumerable<IMap<Vector3, TInput>> input)
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
				var inputs = inputNodes.Cast<SpaceMapNode<TInput>>().Select(node => node.Output).SelectMany(x => x);
				Output = Execute(inputs);
			}
			else
			{
				var inputs = inputNodes.Cast<SpaceMapNode<TOutput>>().Select(node => node.Output).SelectMany(x => x);
				Output = inputs.ToList();
			}
		}

		public sealed override IEnumerable<BaseNode> Inputs
		{
			get { return inputNodes; }
		}

		public sealed override void AddNodeInput(BaseNode input)
		{
			var inputNode = input as SpaceMapNode<TInput>;

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

	/// <summary>
	/// A node that has no input and creates a single object of the given type.
	/// </summary>
	/// <typeparam name="T">The type of objects to create.</typeparam>
	/// <seealso cref="SpaceMapNode{None, T}" />
	public class PrimitiveSpaceMapNode<T> : SpaceMapNode<None, T>
	{
		public sealed override List<IMap<Vector3, T>> Execute(IEnumerable<IMap<Vector3, None>> input)
		{
			return new List<IMap<Vector3, T>> { Generate() };
		}

		/// <summary>
		/// Generates an object of the given type.
		/// </summary>
		/// <returns>IMap&lt;Vector3, T&gt;.</returns>
		/// <exception cref="NotImplementedByException"></exception>
		[Abstract]
		protected virtual IMap<Vector3, T> Generate()
		{
			throw new NotImplementedByException(GetType());
		}
	}

	/// <summary>
	/// A node where each input item is processed ("projected to output") independently.
	/// </summary>
	/// <typeparam name="TInput">The type of the t input.</typeparam>
	/// <typeparam name="TOutput">The type of the t output.</typeparam>
	/// <seealso cref="Gamelogic.Grids2.Graph.SpaceMapNode{TInput, TOutput}" />
	/// <remarks>Derived nodes should override the <c>Transform</c> method to specify how to process inputs.
	/// The output is the list of inputs transformed using this function.</remarks>
	public class ProjectSpaceMapNode<TInput, TOutput> : SpaceMapNode<TInput, TOutput>
	{
		/// <summary>
		/// Calculates a list of output from a given list of input.
		/// </summary>
		/// <param name="input">The input values to base the computation on.</param>
		/// <returns>The list of output values.</returns>
		public sealed override List<IMap<Vector3, TOutput>> Execute(IEnumerable<IMap<Vector3, TInput>> input)
		{
			if (input == null)
			{
				return new List<IMap<Vector3, TOutput>>() { Transform(null) };
			}

			if (!input.Any())
			{
				return new List<IMap<Vector3, TOutput>>() { Transform(null) };
			}

			return input.Select<IMap<Vector3, TInput>, IMap<Vector3, TOutput>>(Transform).ToList();
		}

		/// <summary>
		/// Transforms the specified input to a corresponding output item.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>IMap&lt;Vector3, TOutput&gt;.</returns>
		/// <exception cref="NotImplementedException">Not overridden by the derived type</exception>
		/// <remarks>Override this method to change the node's behaviour.</remarks>
		[Abstract]
		protected virtual IMap<Vector3, TOutput> Transform(IMap<Vector3, TInput> input)
		{
			throw new NotImplementedException("Should be implemented by nodes of type: " + GetType());
		}
	}
}