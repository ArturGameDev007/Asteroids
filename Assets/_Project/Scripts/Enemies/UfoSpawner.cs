using _Project.Scripts.Configs.Enemies;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemies
{
    public class UfoSpawner : GeneratorEnemies
    {
        public UfoSpawner(EnemyConfig config, [Inject(Id = "UfoPool")] ObjectPool<Enemy> pool,
            IEnemyDeathListener enemyManager, Camera camera)
            : base(config, pool, enemyManager, camera){}
        
        protected override void ConfigureSpawn(Enemy enemy, Vector2 _)
        {
            if (enemy.TryGetComponent(out FlyingSaucerController flyingSaucerController))
                flyingSaucerController.enabled = true;
        }
    }
}