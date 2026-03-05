using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public interface IEnemyInitialize
    {
        public void SetupAllSpawners(ObjectPool<Enemy> asteroidPool,  ObjectPool<Enemy> ufoPool, IEnemyDeathListener manager, Transform player);
    }
}