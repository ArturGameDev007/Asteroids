using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemies
{
    public class EnemyInitializer : IEnemyInitialize
    {
        // private GeneratorEnemies[] _generatorEnemies;
        //
        // [Inject]
        // public EnemyInitializer(GeneratorEnemies[] generatorEnemies)
        // {
        //     _generatorEnemies = generatorEnemies;
        // }
        //
        // public void SetupAllSpawners(ObjectPool<Enemy> asteroidPool,  ObjectPool<Enemy> ufoPool, IEnemyDeathListener manager, Transform player, Camera camera)
        // {
        //     foreach (var spawner in _generatorEnemies)
        //     {
        //         // ObjectPool<Enemy> targetPool = spawner is UfoSpawner ? ufoPool : asteroidPool;
        //         // spawner.Initialize(targetPool, manager, player, camera);
        //         spawner.StartSpawning();
        //     }
        // }
    }
}