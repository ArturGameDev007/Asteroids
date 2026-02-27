using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public interface IEnemyInitialize
    {
        public void SetupAsteroid(ObjectPool<Enemy> pool, IEnemyDeathListener manager);

        public void SetupUfo(ObjectPool<Enemy> pool, IEnemyDeathListener manager, Transform player);
    }
}