using System.Collections.Generic;
using Gamelogic.Extensions;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A node that generates a single explicit shape.
	/// </summary>
	/// <typeparam name="TPoint">The point type.</typeparam>
	/// <seealso cref="ShapeNode{TPoint, None, IExplicitShape{TPoint}}" />
	public class PrimitiveShapeNode<TPoint> : ShapeNode<TPoint, None, IExplicitShape<TPoint>>
	{
		public sealed override List<IExplicitShape<TPoint>> Execute(IEnumerable<None> input)
		{
			return new List<IExplicitShape<TPoint>> { Generate() };
		}

		[Abstract]
		protected virtual IExplicitShape<TPoint> Generate()
		{
			throw new NotImplementedByException(GetType());
		}

		public override void Recompute()
		{
			//NOP
		}
	}
}