using System.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Services.RemoteConfigs
{
    public class FirebaseRemoteConfig : IRemoteConfigs
    {
        private const string GAME_CONFIGS = "Game_Configs";
        
        public RemoteConfigsData RemoteConfig { get; private set; }

        public FirebaseRemoteConfig(RemoteConfigsData remoteConfig)
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
                RemoteConfig = JsonUtility.FromJson<RemoteConfigsData>(json);
            }
            
        }
    }
}