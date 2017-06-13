using System;
using System.Collections.Generic;
using System.Linq;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Node that applies a transform function to project input onto the output.
	/// </summary>
	/// <typeparam name="TPoint">The point type.</typeparam>
	/// <typeparam name="TInput">The input type.</typeparam>
	/// <typeparam name="TOutput">The output type.</typeparam>
	/// <seealso cref="Gamelogic.Grids2.Graph.ShapeNode{TPoint, TInput, TOutput}" />
	/// <remarks>Each node is processed independently. Derived types must override the
	///  <c>Transform</c> method to change this nodes behaviour.</remarks>
	public class ProjectShapeNode<TPoint, TInput, TOutput> : ShapeNode<TPoint, TInput, TOutput>
	{
		public sealed override List<TOutput> Execute(IEnumerable<TInput> input)
		{
			return input.Select<TInput, TOutput>(Transform).ToList();
		}

		/// <summary>
		/// Transforms the specified input into the output.
		/// </summary>
		/// <param name="input">The input.</param>
		/// <returns>TOutput.</returns>
		/// <exception cref="NotImplementedException">Not implemented by the derived type.</exception>
		/// <remarks>Override this method to modify this node's behaviour.</remarks>
		[Abstract]
		public virtual TOutput Transform(TInput input)
		{
			throw new NotImplementedException("Should be implemented by nodes of type: " + GetType());
		}

		public override void Recompute()
		{
			//nothing to compute
		}
	}
}