using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemyInitializer : IEnemyInitialize
    {
        private GeneratorEnemies[] _generatorEnemies;

        public EnemyInitializer(GeneratorEnemies[] generatorEnemies)
        {
            _generatorEnemies = generatorEnemies;
        }

        public void SetupAsteroid(ObjectPool<Enemy> pool, IEnemyDeathListener manager)
        {
            foreach (var enemy in _generatorEnemies)
            {
                if (enemy is AsteroidSpawner asteroid)
                    asteroid.Initialize(pool, manager);
            }
        }

        public void SetupUfo(ObjectPool<Enemy> pool, IEnemyDeathListener manager, Transform player)
        {
            foreach (var enemy in _generatorEnemies)
            {
                if (enemy is UfoSpawner ufoSpawner)
                {
                    ufoSpawner.Initialize(pool, manager);
                    ufoSpawner.Construct(player);
                }
            }
        }
    }
}