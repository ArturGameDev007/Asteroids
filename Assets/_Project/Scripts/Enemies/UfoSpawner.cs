using _Project.Scripts.Configs.Enemies;
using _Project.Scripts.Player;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemies
{
    public class UfoSpawner : GeneratorEnemies
    {
        // private Transform _player;
        // private readonly IPlayerProvider _playerProvider;
        private readonly IMovableEntity _movableEntity;

        public UfoSpawner(EnemyConfig config, [Inject(Id = "UfoPool")] ObjectPool<Enemy> pool,
            IEnemyDeathListener enemyManager, IMovableEntity player, Camera camera)
            : base(config, pool, enemyManager, camera)
        {
            _movableEntity = player;
        }
        
        protected override void ConfigureSpawn(Enemy enemy, Vector2 _)
        {
            if (enemy.TryGetComponent(out FlyingSaucerController flyingSaucerController))
            {
                flyingSaucerController.Construct(_movableEntity.PlayerTransform);
                flyingSaucerController.enabled = true;
            }
        }
    }
}