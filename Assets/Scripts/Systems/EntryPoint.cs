using System.Collections.Generic;
using Common;
using Systems.SaveSystem;
using Systems.SaveSystem.Serializers;
using UnityEngine;

namespace Systems
{
	public class EntryPoint : MonoBehaviour
	{
		private readonly List<ASystem> _systems = new List<ASystem>();

		private void Awake()
		{
			DontDestroyOnLoad(this);

			_systems.Add(new SaveDataSystem(new JsonDataSerializer()))
				;
		}

		public void Start()
		{
			foreach (var system in _systems)
			{
				system.Initialize();
			}
		}
	}
}