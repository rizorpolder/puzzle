using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Systems.SaveSystem.Serializers;
using UnityEngine;

public abstract class ACloudManager : MonoBehaviour
{
	protected const string FILE_NAME = "save_data";
	protected const string SaveDateTimeKey = "LastSaveDateTime";

	public class SaveData
	{
		public Dictionary<string, byte[]> SaveDictionary = new Dictionary<string, byte[]>();
	}

	[SerializeField] protected float saveTimeout = 3f;

	protected Action<byte[]> _onComplete;
	protected DateTime _lastSaveData;

	protected SaveData _actualData;
	private IDataSerializer _dataSerializer;

	public static ACloudManager Instance { get; private set; }

	private void Awake()
	{
		_lastSaveData = DateTime.UnixEpoch;

		Instance = this;
	}

	public void SetSerializer(IDataSerializer dataSerializer)
	{
		_dataSerializer = dataSerializer;

		if (!_dataSerializer.IsInitialized)
			_dataSerializer.Initialize();
	}

	public virtual void LoadFromCloud(Action<byte[]> onComplete)
	{
		Debug.Log("Try load from cloud");

		_onComplete = onComplete;

		OnPlayerDataLoaded(null);
	}

	// from js events
	public void OnPlayerDataLoaded(string data)
	{
		byte[] result = null;
		if (!string.IsNullOrEmpty(data))
		{
			Debug.LogError($"data loaded internal {data}");
			Debug.LogError($"callback is empty {_onComplete == null}");

			try
			{
				result = Convert.FromBase64String(data);
			}
			catch (Exception ex)
			{
				Debug.LogError($"Failed to update player data.\n{ex.Message}");
				result = Array.Empty<byte>();
			}
		}

		TryInitFirstData(result);

		_onComplete?.Invoke(result);
		_onComplete = null;
	}

	public virtual void TryInitFirstData(byte[] data)
	{
		_actualData = LoadDataFromFile();
	}

	protected void SaveToFile(bool force)
	{
		RefreshSaveDateTime();
		var result = _dataSerializer.FromData(_actualData);
		//TODO
		//File.WriteAllBytes(DataStorageManager.SavePath, result);

#if UNITY_WEBGL
		if (!Application.isEditor)
			JS_FileSystem_Sync();
#endif

		if (force)
			ForceSaveToCloud();
		else
			TrySaveToCloud();
	}

#if UNITY_WEBGL
	[DllImport("__Internal")]
	private static extern void JS_FileSystem_Sync();
#endif

	private void RefreshSaveDateTime()
	{
		var now = DateTime.UtcNow;
		_actualData.SaveDictionary[SaveDateTimeKey] = _dataSerializer.FromData(now);
	}

	private SaveData LoadDataFromFile()
	{
		var data = new SaveData();
		// if (File.Exists(DataStorageManager.SavePath))
		// {
		// 	try
		// 	{
		// 		var bytes = File.ReadAllBytes(DataStorageManager.SavePath);
		// 		data = _dataSerializer.ToData<SaveData>(bytes);
		// 	}
		// 	catch (Exception e)
		// 	{
		// 		Debug.LogError($"LoadDataFromFile FAILED!\n{e.Message}");
		// 	}
		// }

		return data;
	}

	public string GetData(string key)
	{
		if (!HasData(key))
			return string.Empty;

		return Encoding.UTF8.GetString(_actualData.SaveDictionary[key]);
	}

	public T GetData<T>(string key, T defaultVal)
	{
		if (!HasData(key))
			return defaultVal;

		return _dataSerializer.ToData<T>(_actualData.SaveDictionary[key]);
	}

	public bool HasData(string key)
	{
		if (_actualData == null || _actualData.SaveDictionary == null || !_actualData.SaveDictionary.ContainsKey(key))
			return false;

		return true;
	}

	public void ClearData(string key)
	{
		if (!HasData(key))
			return;

		_actualData.SaveDictionary.Remove(key);
	}

	public void Save(string key, string dataStr)
	{
		var data = Encoding.UTF8.GetBytes(dataStr);

		if (!_actualData.SaveDictionary.ContainsKey(key))
			_actualData.SaveDictionary.Add(key, data);
		else
			_actualData.SaveDictionary[key] = data;

		SaveToFile(false);
	}

	public void Save<T>(string key, T dataMP, bool isForce = false)
	{
		var data = _dataSerializer.FromData(dataMP);

		if (!_actualData.SaveDictionary.ContainsKey(key))
			_actualData.SaveDictionary.Add(key, data);
		else
			_actualData.SaveDictionary[key] = data;

		SaveToFile(isForce);
	}

	protected void TrySaveToCloud()
	{
		if ((DateTime.Now - _lastSaveData).TotalSeconds < saveTimeout)
			return;

		ForceSaveToCloud();
	}

	public virtual void ForceSaveToCloud()
	{
		_lastSaveData = DateTime.Now;

		Debug.Log("Save Sent to cloud.");
	}

	public void ResetProgress()
	{
		_actualData.SaveDictionary.Clear();
		ForceSaveToCloud();
	}

	private void OnDestroy()
	{
		Instance = null;
	}
}