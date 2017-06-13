using System;
using System.Collections;
using System.Collections.Generic;

namespace Gamelogic.Grids2
{
	/// <summary>
	/// Base class for arrays used to configure nested arrays in the inspector.
	/// 
	/// This class provides all the implementation needed. Base classes are provided
	/// only so that the generic parameter is specified so that the base classes
	/// can be used in the inspector.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class Array<T> : IList<T> //TODO what is a better name for this?
	{
		public List<T> data;

		public int IndexOf(T item)
		{
			return data.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			data.Insert(index, item);
		}

		public void RemoveAt(int index)
		{
			data.RemoveAt(index);
		}

		public T this[int index]
		{
			get { return data[index]; }
			set { data[index] = value; }
		}


		public IEnumerator<T> GetEnumerator()
		{
			return data.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(T item)
		{
			data.Add(item);
		}

		public void Clear()
		{
			data.Clear();
		}

		public bool Contains(T item)
		{
			return data.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			data.CopyTo(array, arrayIndex);
		}

		public bool Remove(T item)
		{
			return data.Remove(item);
		}

		public int Count
		{
			get { return data.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}
	}
}