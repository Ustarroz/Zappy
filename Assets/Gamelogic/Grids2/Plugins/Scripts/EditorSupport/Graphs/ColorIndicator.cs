using System;
using UnityEngine;

namespace Gamelogic.Grids2.Graph
{
	/// <summary>
	/// Wrapper to set the input and output colors that
	/// represent the type parameters that the node use
	/// for input and output.
	/// </summary>
	[Serializable]
	public class ColorIndicator
	{
		public Color inputColor;
		public Color outputColor;
	}
}