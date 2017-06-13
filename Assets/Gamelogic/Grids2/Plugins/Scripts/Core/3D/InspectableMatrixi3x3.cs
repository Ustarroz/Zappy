using System;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Matrices are immutable, and cannot be used in the inspector.
	/// This class can be used instead.
	/// </summary>
	[Serializable]
	public struct InspectableMatrixi3x3
	{
		public int a;
		public int b;
		public int c;

		public int d;
		public int e;
		public int f;

		public int g;
		public int h;
		public int i;

		public Matrixi33 GetMatrix()
		{
			return new Matrixi33(a, b, c, d, e, f, g, h, i);
		}
	}
}