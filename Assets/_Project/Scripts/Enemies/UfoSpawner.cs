using _Project.Scripts.Configs.Enemies;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class UfoSpawner : GeneratorEnemies
    {
        private Transform _player;

        public UfoSpawner(EnemyConfig config) : base(config){}

        public override void Initialize(ObjectPool<Enemy> pool, IEnemyDeathListener enemyManager, Transform player)
        {
            base.Initialize(pool, enemyManager, player);
            _player = player;
        }

        protected override void ConfigureSpawn(Enemy enemy, Vector2 _)
        {
            if (enemy.TryGetComponent(out FlyingSaucerController flyingSaucerController))
            {
                flyingSaucerController.Construct(_player);
                flyingSaucerController.enabled = true;
            }
        }
    }
}