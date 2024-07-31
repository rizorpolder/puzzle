using System.Collections.Generic;
using Configs.TextureRepository;
using Data;
using Field;
using Systems;
using UnityEngine;

namespace Managers
{
	public class GameFieldManager : MonoBehaviour
	{
		[SerializeField] private PuzzleCell prefab;
		[SerializeField] private Transform fieldRoot;
		[SerializeField] private List<PuzzleCell> pool;

		private PuzzleCell _emptyCell;

		private FieldData _data;

		[SerializeField] //TODO to configurable root call
		private ImageRepositoryConfig _config;

		private List<TextureUnit> _textureUnits;
		private void Awake()
		{
			//_data = SaveDataSystem.Instance.LoadData<FieldData>();
			_data = new FieldData();
			GenerateField();
			InitializeField();
			// доступ к саб спрайтам получают через загрузку ресурсов
			// заранее кэшировать данные в конфиг и дергать оттуда
		}

		private void Start()
		{
			InputSystem.Instance.OnSwipe += OnSwipeAction;
		}

		private void OnSwipeAction(SwipeDirection direction)
		{

		}

		private void DoSwipe()
		{

		}

		private void GenerateField()
		{
			_textureUnits = _config.GetSprites(_data.LastTextureName);
			for (var index = 0; index < _textureUnits.Count; index++)
			{
				var unit = _textureUnits[index];
				if (index >= pool.Count)
				{
					var cell = CreateCell();
					cell.Initialize(unit.coords);
					cell.name = $"{unit.coords.x}x{unit.coords.y}_cell";
					pool.Add(cell);
				}
			}
			//по сохраненным данным получить название текстуры
			// по ее размерам воссоздать поле
			//проинициализировать ячейки из сохраненных (новых) данных
		}

		private void InitializeField()
		{
			int index = 0;
			for (; index <_textureUnits.Count; index++)
			{
				var cell = index >= pool.Count ? CreateCell() : pool[index];
				var unit = _textureUnits[index];
				cell.gameObject.SetActive(true);
				cell.Initialize(unit.coords);
				cell.CreateChip(unit.sprite);
			}
			_emptyCell = pool[^1];
			_emptyCell.Clear();

			for (; index <pool.Count; index++)
			{
				var cell = pool[index];
				cell.gameObject.SetActive(false);
				cell.Clear();
			}
		}

		private PuzzleCell CreateCell()
		{
			var newCell = Instantiate(prefab, this.fieldRoot);
			return newCell;
		}



		private void LoadField()
		{
		}

		private void FillField()
		{
			//Fill elements from index to index
			//Check if disabled - skip
		}

		private void Shuffle()
		{

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