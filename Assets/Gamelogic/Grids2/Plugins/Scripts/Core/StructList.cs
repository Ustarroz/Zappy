using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// An implementation of IList for grid points that is
	/// safe to use with the AOT compiler.
	/// </summary>
	//@version1_6
	//@ingroup Utilities
	public sealed class StructList<T> : IList<T>
	{
		private readonly List<T> points;

		public StructList()
		{
			points = new List<T>();
		}

		public StructList(IEnumerable<T> collection)
		{
			points = new List<T>(collection);
		}

		public StructList(int capacity)
		{
			points = new List<T>(capacity);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return points.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return points.GetEnumerator();
		}

		public void Add(T point)
		{
			points.Add(point);
		}

		public void Clear()
		{
			points.Clear();
		}

		public bool Contains(T point)
		{
			return points.Any(x => x.Equals(point));
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			points.CopyTo(array, arrayIndex);
		}

		public bool Remove(T point)
		{
			int index = points.FindIndex(x => x.Equals(point));

			if (index >= 0)
			{
				points.RemoveAt(index);
				return true;
			}

			return false;
		}

		public int Count
		{
			get { return points.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public int IndexOf(T point)
		{
			return points.FindIndex(x => x.Equals(point));
		}

		public int LastIndexOf(T point)
		{
			return points.FindLastIndex(x => x.Equals(point));
		}

		public void Insert(int index, T point)
		{
			points.Insert(index, point);
		}

		public void RemoveAt(int index)
		{
			points.RemoveAt(index);
		}

		public T this[int index]
		{
			get { return points[index]; }
			set { points[index] = value; }
		}

		/// <summary>
		/// Removes all the elements in the list that does not satisfy the predicate.
		/// </summary>
		/// <param name="match">The match.</param>
		public void RemoveAllBut(Predicate<T> match)
		{
			Predicate<T> nomatch = point => !match(point);

			points.RemoveAll(nomatch);
		}

		public void RemoveAll(Predicate<T> match)
		{
			for (int i = points.Count - 1; i >= 0; i--)
			{
				if (match(points[i]))
				{
					points.RemoveAt(i);
				}
			}
		}
	}



	/// <summary>
	/// Extensions for IEnumerable.
	/// </summary>
	public static class EnumerableExtensions
	{
		/// <summary>
		/// This method performs the same function as ToList, but returns a StructList instead.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <returns></returns>
		public static StructList<T> ToStructList<T>(this IEnumerable<T> list)
		{
			return new StructList<T>(list);
		}

		public static IEnumerable<TResult> Select<TPoint, TCell, TResult>(
			this IEnumerable<PointCellPair<TPoint, TCell>> grid,
			Func<TPoint, TCell, TResult> selector)
		{
			return grid.Select(pair => selector(pair.point, pair.cell));
		}
	}
}