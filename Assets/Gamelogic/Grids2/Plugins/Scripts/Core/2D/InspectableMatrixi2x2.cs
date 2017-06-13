using System;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Matrices are immutable, and cannot be used in the inspector.
	/// This class can be used instead.
	/// </summary>
	[Serializable]
	public struct InspectableMatrixi2x2
	{
		public int a;
		public int b;

		public int c;
		public int d;

		public Matrixi22 GetMatrix()
		{
			return new Matrixi22(a, b, c, d);
		}
	}
}