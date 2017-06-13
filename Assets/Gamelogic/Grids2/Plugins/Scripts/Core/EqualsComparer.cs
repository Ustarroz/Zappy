using System.Collections.Generic;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Use this class in constructors of HashSets and Dictionaries that
	/// take point types(such as PointyHexPoint) as keys.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class EqualsComparer<T> : IEqualityComparer<T>
	{
		public bool Equals(T p1, T p2)
		{
			return p1.Equals(p2);
		}

		public int GetHashCode(T p)
		{
			return p.GetHashCode();
		}
	}
}