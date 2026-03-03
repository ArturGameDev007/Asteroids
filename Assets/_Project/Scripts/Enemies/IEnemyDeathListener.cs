using System;
using _Project.Scripts.Configs;

namespace _Project.Scripts.Enemies
{
    public interface IEnemyDeathListener
    {
        public event Action<EnemyConfig> OnEnemyKilled;
        
        public void OnEnemyDeath(EnemyConfig config);
    }
}