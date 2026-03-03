using System;
using _Project.Scripts.Configs.Enemies;

namespace _Project.Scripts.Enemies
{
    public class EnemyManager : IEnemyDeathListener
    {
        public event Action<EnemyConfig> OnEnemyKilled;

        public void OnEnemyDeath(EnemyConfig config)
        {
            OnEnemyKilled?.Invoke(config);
        }
    }
}