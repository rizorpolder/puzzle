using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Systems.SaveSystem.CloudSaveSystem
{
    public class YandexCloudManager : ACloudManager
    {
        public override void LoadFromCloud(Action<byte[]> onComplete)
        {
            _onComplete = onComplete;

            if (Application.isEditor)
            {
                OnPlayerDataLoaded(null);
                return;
            }

#if UNITY_WEBGL
            LoadPlayerData();
#endif
        }

        public override void TryInitFirstData(byte[] data)
        {
            Debug.Log("try init first data");
            base.TryInitFirstData(data);

            if (data == null || data.Length == 0)
                return;

            SaveData remoteData;
            try
            {
                // data damaged
                remoteData = _dataSerializer.ToData<SaveData>(data);
            }
            catch (Exception ex)
            {
                Debug.Log($"remote data is damaged\n{ex.Message}");
                return;
            }

            Debug.Log("try init first data step1");
            if (!remoteData.SaveDictionary.ContainsKey(SaveDateTimeKey))
                return;

            Debug.Log("try init first data step2");

            if (_actualData.SaveDictionary.TryGetValue(SaveDateTimeKey, out var currentDataTimeSyncString))
            {
                Debug.Log("try init first data step3");
                var remoteDataSyncTime = _dataSerializer.ToData<ShortDataTime>(remoteData.SaveDictionary[SaveDateTimeKey]);
                var currentDataSyncTime = _dataSerializer.ToData<ShortDataTime>(currentDataTimeSyncString);

                Debug.Log($"try init first data current {currentDataSyncTime.ToDateTime()} remote {remoteDataSyncTime.ToDateTime()}");

                if (currentDataSyncTime.ToDateTime() >= remoteDataSyncTime.ToDateTime())
                    return;

                GC.Collect();
            }

            Debug.Log("try init first data step 4");
            _actualData = remoteData;
            SaveToFile(false);
        }

        public override void ForceSaveToCloud()
        {
            Debug.Log($"save data to yandex cloud");

            _lastSaveData = DateTime.Now;

            if (Application.isEditor)
                return;

#if UNITY_WEBGL
            SavePlayerData(Convert.ToBase64String(_dataSerializer.FromData(_actualData)));
#endif
        }

#if UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void LoadPlayerData();

        [DllImport("__Internal")]
        private static extern void SavePlayerData(string data);
#endif
    }
}