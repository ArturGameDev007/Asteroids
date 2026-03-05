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

        public void SetupAllSpawners(ObjectPool<Enemy> asteroidPool,  ObjectPool<Enemy> ufoPool, IEnemyDeathListener manager, Transform player)
        {
            foreach (var spawner in _generatorEnemies)
            {
                ObjectPool<Enemy> targetPool = spawner is UfoSpawner ? ufoPool : asteroidPool;
                spawner.Initialize(targetPool, manager, player);
            }
        }
    }
}