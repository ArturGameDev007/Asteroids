using System.Threading.Tasks;
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

        public async Task Initialize()
        {
            var dataFirebase = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance;
            
            await dataFirebase.FetchAndActivateAsync();

            string json = dataFirebase.GetValue(GAME_CONFIGS).StringValue;

            if (!string.IsNullOrEmpty(json))
            {
                // RemoteConfig = JsonUtility.FromJson<RemoteConfigsRoot>(json);
                JsonUtility.FromJsonOverwrite(json, RemoteConfig);
            }
        }
    }
}