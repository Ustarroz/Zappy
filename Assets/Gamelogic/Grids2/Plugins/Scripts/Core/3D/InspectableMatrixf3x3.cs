using System;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Matrices are immutable, and cannot be used in the inspector.
	/// This class can be used instead.
	/// </summary>
	[Serializable]
	public struct InspectableMatrixf3x3 //TODO make 2D version, make int version. Also for respective proeprty drawers.
	{
		public float a;
		public float b;
		public float c;

		public float d;
		public float e;
		public float f;

		public float g;
		public float h;
		public float i;

		public Matrixf33 GetMatrix()
		{
			return new Matrixf33(a, b, c, d, e, f, g, h, i);
		}
	}
}