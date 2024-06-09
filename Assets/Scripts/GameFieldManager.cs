using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameFieldManager : MonoBehaviour
{
	[SerializeField] private Sprite[] _sprites; //todo TO SO
	[SerializeField] private RectTransform field;
	[SerializeField] private Image prefab;
	[SerializeField] private List<Image> pool;

	private void Awake()
	{
		CreateField();
		// доступ к саб спрайтам получают через загрузку ресурсов
		// заранее кэшировать данные в конфиг и дергать оттуда
	}

	private void CreateField()
	{
		//Create/enable/disable elements from pool or create
		var spriteSize = _sprites[0].rect;
		int rows = (int) (field.rect.width / spriteSize.width);
		var columns = (int) (field.rect.height / spriteSize.height);
		var offset = new Vector2(spriteSize.width / 2, spriteSize.height / 2);
		for (int i = 0; i < rows; i++)
		{
			for (int j = 0; j < columns; j++)
			{
				var cell = GameObject.Instantiate(prefab, field);
				var pos = new Vector2(spriteSize.width * i, spriteSize.height * j);
				cell.transform.localPosition = field.rect.min + pos;
				pool.Add(cell);
			}
		}
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
				_target.CreateField();
			}
			if (GUILayout.Button("Clear", style, GUILayout.Width(180), GUILayout.Height(20)))
			{
				_target.ClearPool();
			}
			base.OnInspectorGUI();
		}
	}
}