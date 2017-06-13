using System;
using Gamelogic.Extensions;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A node that applies a given
	/// function to the X-coordinate and either sets the result on X, or adds it to Y.
	/// </summary>
	/// <seealso cref="ProjectSpaceMapNode{TInput,TOutput}" />
	public class FuncSpaceMapNode : ProjectSpaceMapNode<Vector3, Vector3>
	{
		/// <summary>
		/// How the results should be applied to the output.
		/// </summary>
		public enum Type
		{
			/// <summary>
			/// The result is fed to the X coordinate of the output.
			/// </summary>
			TransformX,

			/// <summary>
			/// The result is added to the Y coordinate of the output.
			/// </summary>
			AddToY
		}

		/// <summary>
		/// How the results should be applied to the output.
		/// </summary>
		public Type transformType;

		public virtual float Func(float x)
		{
			throw new NotImplementedByException(GetType());
		}

		protected override IMap<Vector3, Vector3> Transform(IMap<Vector3, Vector3> input)
		{
			IMap<Vector3, Vector3> map;
			switch (transformType)
			{
				case Type.TransformX:
					map = Map.FuncXOnX(Func);
					break;
				case Type.AddToY:
					map = Map.FuncXOnY(Func);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return input.ComposeWith(map);
		}
	}
}