using System.Collections.Generic;
using Data;
using Field;
using Systems.SaveSystem;
using UnityEngine;

namespace Managers
{
	public class GameFieldManager : MonoBehaviour
	{
		[SerializeField] private PuzzleCell prefab;
		[SerializeField] private Transform fieldRoot;
		[SerializeField] private List<PuzzleCell> pool;

		private FieldData _data;

		private void Awake()
		{
			//_data = SaveDataSystem.Instance.LoadData<FieldData>();
			
			GenerateField();
			InitializeField();
			// доступ к саб спрайтам получают через загрузку ресурсов
			// заранее кэшировать данные в конфиг и дергать оттуда
		}

		private void GenerateField()
		{
			//по сохраненным данным получить название текстуры
			// по ее размерам воссоздать поле
			//проинициализировать ячейки из сохраненных (новых) данных
		}

		private void InitializeField()
		{
		}

		private void LoadField()
		{
		}

		private void FillField()
		{
			//Fill elements from index to index
			//Check if disabled - skip
		}

#if UNITY_EDITOR

		// [UnityEditor.CustomEditor(typeof(GameFieldManager))]
		// public class GameFieldManagerEditor : UnityEditor.Editor
		// {
		// 	private GameFieldManager _target;
		//
		// 	public override void OnInspectorGUI()
		// 	{
		// 		_target = target as GameFieldManager;
		// 		var style = new GUIStyle(GUI.skin.button);
		// 		style.normal.textColor = new Color(0, 150f / 255f, 90f / 255f);
		// 		if (GUILayout.Button("Create field", style, GUILayout.Width(180), GUILayout.Height(20)))
		// 		{
		// 			RenameElements();
		// 		}
		//
		// 		base.OnInspectorGUI();
		// 	}
		//
		// 	private void RenameElements()
		// 	{
		// 		int currentIndex = 0;
		// 		for (int i = 0; i < FieldSize.x; i++)
		// 		{
		// 			for (int j = 0; j < FieldSize.y; j++)
		// 			{
		// 				var element = _target.pool[currentIndex];
		// 				element.gameObject.name = $"{i}x{j}_cell";
		// 				currentIndex++;
		// 			}
		// 		}
		// 	}
		// }
#endif
	}
}