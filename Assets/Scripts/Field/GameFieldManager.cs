using System;
using System.Collections.Generic;
using Data;
using Field;
using Global;
using UnityEngine;
using UnityEngine.UI;

public class GameFieldManager : MonoBehaviour
{
	[SerializeField] private Vector2 FieldSize;
	[SerializeField] private List<PuzzleCell> pool;


	private FieldData _data;

	private void Awake()
	{
		GenerateField();
		InitializeField();
		// доступ к саб спрайтам получают через загрузку ресурсов
		// заранее кэшировать данные в конфиг и дергать оттуда
	}
	
	private void GenerateField()
	{
		//заранее загруженные пользовательские данные  или созданные с нуля
		_data = new FieldData();
	}

	private void InitializeField()
	{
		for (int i = 0;  i < FieldSize.x; i++)
		{
			for (int j = 0; j < FieldSize.y; j++)
			{
				pool[i+j].Initialize(i,j);
				ConfigurableRoot.Instance.ImageRepositoryConfig.GetSprites();
			}
		}
	}

	private void LoadField()
	{

	}

	private void FillField()
	{
		//Fill elements from index to index
		//Check if disabled - skip
	}

	private void RenameElements()
	{
		int currentIndex = 0;
		for (int i = 0;  i < FieldSize.x; i++)
		{
			for (int j = 0; j < FieldSize.y; j++)
			{
				var element = pool[currentIndex];
				element.gameObject.name = $"{i}x{j}_cell";
				currentIndex++;
			}
		}
	}

	[UnityEditor.CustomEditor(typeof(GameFieldManager))]
	public class GameFieldManagerEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var _target = target as GameFieldManager;
			var style = new GUIStyle(GUI.skin.button);
			style.normal.textColor = new Color(0, 150f / 255f, 90f / 255f);
			if (GUILayout.Button("Create field", style, GUILayout.Width(180), GUILayout.Height(20)))
			{
				_target.RenameElements();
			}

			base.OnInspectorGUI();
		}
	}
}