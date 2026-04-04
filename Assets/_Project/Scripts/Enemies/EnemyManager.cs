using System;
using _Project.Scripts.Configs.Enemies;
using _Project.Scripts.Services.RemoteConfigs;

namespace _Project.Scripts.Enemies
{
    public class EnemyManager : IEnemyDeathListener
    {
        public event Action<RemoteConfigsData> OnEnemyKilled;

        public void OnEnemyDeath(RemoteConfigsData config)
        {
            OnEnemyKilled?.Invoke(config);
        }
    }
}