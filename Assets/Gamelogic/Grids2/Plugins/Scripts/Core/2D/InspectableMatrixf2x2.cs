using System;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Matrices are immutable, and cannot be used in the inspector.
	/// This class can be used instead.
	/// </summary>
	[Serializable]
	public struct InspectableMatrixf2x2
	{
		public float a;
		public float b;

		public float c;
		public float d;

		public Matrixf22 GetMatrix()
		{
			return new Matrixf22(a, b, c, d);
		}
	}
}