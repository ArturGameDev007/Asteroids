using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Services.RemoteConfigs
{
    public class FirebaseRemoteConfig : IRemoteConfigs
    {
        private const string GAME_CONFIGS = "Game_Configs";
        
        // public RemoteConfigsData RemoteConfig { get; }
        private RemoteConfigsData _remoteConfig;

        public FirebaseRemoteConfig(RemoteConfigsData remoteConfig)
        {
            _remoteConfig = remoteConfig;
        }

        public async Task Initialize()
        {
            var dataFirebase = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance;

            var settings = new Firebase.RemoteConfig.ConfigSettings { MinimumFetchIntervalInMilliseconds = 0 };
            
            await dataFirebase.SetConfigSettingsAsync(settings);

            await dataFirebase.FetchAndActivateAsync();

            string json = dataFirebase.GetValue(GAME_CONFIGS).StringValue;

            if (!string.IsNullOrEmpty(json))
            {
                // RemoteConfig = JsonUtility.FromJson<RemoteConfigsData>(json);
                JsonUtility.FromJsonOverwrite(json, _remoteConfig);
            }
            
            Debug.Log(json);
        }
    }
}