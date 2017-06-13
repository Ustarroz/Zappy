using System;
using Gamelogic.Extensions.Internal;

namespace Gamelogic.Grids
{
	/// <summary>
	/// A class for storing properties to setup a polar grid.
	/// </summary>
	[Version(1,8)]
	[Serializable]
	public class PolarGridProperties
	{
		public float innerRadius = 50;
		public float outerRadius = 350;
		public float border = 0f;
		public float quadSize = 15f;
	}
}