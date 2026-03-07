using _Project.Scripts.Configs.Enemies;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemies
{
    public class AsteroidSpawner : GeneratorEnemies
    {
        [Inject]
        public AsteroidSpawner(EnemyConfig config, [Inject(Id = "AsteroidPool")] ObjectPool<Enemy> pool, IEnemyDeathListener enemyManager, Transform player, Camera camera)
            : base(config, pool, enemyManager, player, camera){}

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