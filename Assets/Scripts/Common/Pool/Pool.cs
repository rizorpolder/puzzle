using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.Pool
{
	[Serializable]

	//TODO fix
	public class Pool<T> where T : PoolObject, new()
	{
		private List<T> _busyElements = new();
		private List<T> _freeElements = new();
		public int Count => _freeElements.Count;

		public T this[int i] => _busyElements[i];

		public T GetObject()
		{
			T element;
			if (_freeElements.Count <= 0)
			{
				element = CreateObject();
			}
			else
			{
				element = _freeElements[^1];
				_freeElements.Remove(element);
			}

			_busyElements.Add(element);
			return element;
		}

		public void ReturnObject(T element)
		{
			_busyElements.Remove(element);
			_freeElements.Add(element);
		}

		private T CreateObject()
		{
			var po = Object.Instantiate(new T());
			return po;
		}
	}

	[Serializable]
	public class PoolObject : MonoBehaviour
	{
	}
}