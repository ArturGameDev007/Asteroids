using _Project.Scripts.Configs.Enemies;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemies
{
    public class UfoSpawner : GeneratorEnemies
    {
        private Transform _player;

        [Inject]
        public UfoSpawner(EnemyConfig config, [Inject(Id = "UfoPool")] ObjectPool<Enemy> pool,
            IEnemyDeathListener enemyManager, [Inject(Id = "Player")] Transform player, Camera camera)
            : base(config, pool, enemyManager, player, camera)
        {
            _player = player;
        }

        // public override void Initialize(ObjectPool<Enemy> pool, IEnemyDeathListener enemyManager, Transform player, Camera  camera)
        // {
        //     base.Initialize(pool, enemyManager, player,  camera);
        //     _player = player;
        // }

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