using _Project.Scripts.Configs.Enemies;
using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemies
{
    public class AsteroidSpawner : GeneratorEnemies
    {
        public AsteroidSpawner(RemoteConfigsData remoteConfig, [Inject(Id = "AsteroidPool")] ObjectPool<Enemy> pool,
            IEnemyDeathListener enemyManager, Camera camera)
            : base(remoteConfig, pool, enemyManager, camera)
        {
        }

        protected override void ConfigureSpawn(Enemy enemy, Vector2 spawnPosition)
        {
            if (enemy.TryGetComponent(out AsteroidController asteroid))
            {
                asteroid.SetDirection(spawnPosition);
                asteroid.enabled = true;
            }
        }
    }
}