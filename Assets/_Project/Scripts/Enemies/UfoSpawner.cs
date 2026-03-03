using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class UfoSpawner : GeneratorEnemies
    {
        private Transform _player;

        public void Construct(Transform player)
        {
            _player = player;
        }

        public override void Initialize(ObjectPool<Enemy> pool, IEnemyDeathListener enemyManager)
        {
            base.Initialize(pool, enemyManager);
            CheckPLayer();
        }

        protected override void ConfigureSpawn(Enemy enemy, Vector2 _)
        {
            if (enemy.TryGetComponent(out FlyingSaucerController flyingSaucerController))
            {
                flyingSaucerController.Construct(_player);
                flyingSaucerController.enabled = true;
            }
        }
        
        private void CheckPLayer()
        {
            if (_player == null)
                Debug.Log($"{nameof(UfoSpawner)} - Player is not constructed");
            else
                Debug.Log("UFO Spawner инициализирован синхронно");
        }
    }
}