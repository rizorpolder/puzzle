using System.Collections.Generic;
using Data;
using Field;
using UnityEngine;
using UnityEngine.UI;

public class GameFieldManager : MonoBehaviour
{
	[SerializeField] private Sprite[] _sprites; //todo TO SO
	[SerializeField] private RectTransform fieldRoot;
	[SerializeField] private Image prefab;
	[SerializeField] private List<Image> pool;

	private void Awake()
	{
		GenerateField();
		// доступ к саб спрайтам получают через загрузку ресурсов
		// заранее кэшировать данные в конфиг и дергать оттуда
	}

	private void GenerateField()
	{
		var loadedData = new FieldData();

		var activeField = new GameField();
		activeField.CreateNewGameField(loadedData);
	}

	private void LoadField()
	{

	}

	private void FillField()
	{
		//Fill elements from index to index
		//Check if disabled - skip
	}

	private void ClearPool()
	{
		foreach (var image in pool)
		{
			DestroyImmediate(image.gameObject);
		}
		pool.Clear();
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
				_target.ClearPool();
				_target.GenerateField();
			}
			if (GUILayout.Button("Clear", style, GUILayout.Width(180), GUILayout.Height(20)))
			{
				_target.ClearPool();
			}
			base.OnInspectorGUI();
		}
	}
}