using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public interface IEnemyInitialize
    {
        public void SetupAsteroids(ObjectPool<Enemy> pool, IEnemyDeathListener manager);

        public void SetupUfos(ObjectPool<Enemy> pool, IEnemyDeathListener manager, Transform player);
    }
}