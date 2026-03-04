using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class AsteroidSpawner : GeneratorEnemies
    {
        public override void Initialize(ObjectPool<Enemy> pool, IEnemyDeathListener enemyManager)
        {
            base.Initialize(pool, enemyManager);
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