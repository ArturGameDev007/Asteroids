using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class AsteroidSpawner : GeneratorEnemies
    {
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