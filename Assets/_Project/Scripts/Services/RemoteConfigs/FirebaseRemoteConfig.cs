using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Services.RemoteConfigs
{
    public class FirebaseRemoteConfig : IRemoteConfigs
    {
        private const string GAME_CONFIGS = "Game_Configs";

        public RemoteConfigsRoot RemoteConfig { get; }

        public FirebaseRemoteConfig(RemoteConfigsRoot remoteConfig)
        {
            RemoteConfig = remoteConfig;
        }

        public async UniTask Initialize()
        {
            var dataFirebase = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance;
            string cachedJson = dataFirebase.GetValue(GAME_CONFIGS).StringValue;
            
            if (!string.IsNullOrEmpty(cachedJson))
            {
                JsonUtility.FromJsonOverwrite(cachedJson, RemoteConfig);
            }
            
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.Log("Нет связи. Берем локальные настройки.");
                return;
            }

            try
            {
                await dataFirebase.FetchAndActivateAsync();

                string json = dataFirebase.GetValue(GAME_CONFIGS).StringValue;

                if (!string.IsNullOrEmpty(json))
                {
                    JsonUtility.FromJsonOverwrite(json, RemoteConfig);
                }
            }
            catch (Exception e)
            {
                Debug.Log($"Firebase не отвечает. Не удалось взять данные - {e.Message}");
            }
        }
    }
}