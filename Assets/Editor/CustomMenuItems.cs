using System.IO;
using UnityEditor;
using UnityEngine;


public class CustomMenuItems
{
	[MenuItem("Project/Clear all")]
	public static void ClearAll()
	{
		var files = Directory.GetFiles(Application.persistentDataPath);
		foreach (var file in files)
		{
			File.Delete(file);
		}
		PlayerPrefs.DeleteAll();
	}

}