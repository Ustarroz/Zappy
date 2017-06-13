using System;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// A type that can be used for inputs of nodes that don't have inputs.
	/// </summary>
	/// <remarks>This is a dummy type, used for inputs of primitive nodes
	/// (nodes that generate objects without input).</remarks>
	[Version(1, 1)]
	[Serializable]
	public class None { }
}