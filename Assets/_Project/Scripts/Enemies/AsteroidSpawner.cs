using _Project.Scripts.Configs.Enemies;
using UnityEngine;


namespace _Project.Scripts.Enemies
{
    public class AsteroidSpawner : GeneratorEnemies
    {
        public AsteroidSpawner(EnemyConfig config) : base(config){}

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