using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemyInitializer: IEnemyInitialize
    {
        private AsteroidSpawner[] _asteroidSpawners;
        private UfoSpawner[] _ufoSpawner;

        public EnemyInitializer(GeneratorEnemies[] generatorEnemies)
        {
            _asteroidSpawners = generatorEnemies.OfType<AsteroidSpawner>().ToArray();
            _ufoSpawner = generatorEnemies.OfType<UfoSpawner>().ToArray();;
        }

        public void SetupAsteroids(ObjectPool<Enemy> pool, IEnemyDeathListener manager)
        {
            foreach (var generator in _asteroidSpawners)
            {
                generator.Initialize(pool, manager);
            }
        }

        public void SetupUfos(ObjectPool<Enemy> pool, IEnemyDeathListener manager, Transform player)
        {
            foreach (var generator in _ufoSpawner)
            {
                generator.Initialize(pool, manager);
                generator.Construct(player);
            }
        }
    }
}