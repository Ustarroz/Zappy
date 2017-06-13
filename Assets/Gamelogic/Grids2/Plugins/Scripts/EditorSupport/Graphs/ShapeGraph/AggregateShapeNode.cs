using System;
using System.Collections.Generic;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A simple node is a node that aggregates all its inputs into a single output node.
	/// </summary>
	/// <typeparam name="TInput"></typeparam>
	/// <typeparam name="TOutput"></typeparam>
	/// <typeparam name="TPoint"></typeparam>
	[Version(1, 1)]
	[Abstract]
	public class AggregateShapeNode<TPoint, TInput, TOutput> : ShapeNode<TPoint, TInput, TOutput>
	{
		/// <summary>
		/// Calculates the single output for this node.
		/// </summary>
		/// <param name="input">The input of this node.</param>
		/// <returns>The output calculated based on the input.</returns>
		[Abstract]
		public virtual TOutput Aggregate(IEnumerable<TInput> input)
		{
			throw new NotImplementedException("Node of type " + GetType() + "should override this method");
		}

		public sealed override List<TOutput> Execute(IEnumerable<TInput> input)
		{
			return new List<TOutput> { Aggregate(input) };
		}
	}
}