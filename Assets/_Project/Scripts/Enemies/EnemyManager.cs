using System;
using _Project.Scripts.Services.RemoteConfigs;

namespace _Project.Scripts.Enemies
{
    public class EnemyManager : IEnemyDeathListener
    {
        public event Action<IRemoteConfigs> OnEnemyKilled;

        public void OnEnemyDeath(IRemoteConfigs config)
        {
            OnEnemyKilled?.Invoke(config);
        }
    }
}