using System;
using _Project.Scripts.Services.RemoteConfigs;

namespace _Project.Scripts.Enemies
{
    public interface IEnemyDeathListener
    {
        public event Action<RemoteConfigsData> OnEnemyKilled;
        
        public void OnEnemyDeath(RemoteConfigsData config);
    }
}