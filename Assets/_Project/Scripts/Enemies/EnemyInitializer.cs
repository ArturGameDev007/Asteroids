using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemyInitializer : IEnemyInitialize
    {
        private AsteroidSpawner _asteroidSpawners;
        private UfoSpawner _ufoSpawner;

        public EnemyInitializer(GeneratorEnemies[] generatorEnemies)
        {
            _asteroidSpawners = generatorEnemies.OfType<AsteroidSpawner>().FirstOrDefault();
            _ufoSpawner = generatorEnemies.OfType<UfoSpawner>().FirstOrDefault();
        }

        public void SetupAsteroid(ObjectPool<Enemy> pool, IEnemyDeathListener manager)
        {
            _asteroidSpawners?.Initialize(pool, manager);
        }

        public void SetupUfo(ObjectPool<Enemy> pool, IEnemyDeathListener manager, Transform player)
        {
            _ufoSpawner?.Initialize(pool, manager);
            _ufoSpawner?.Construct(player);
        }
    }
}