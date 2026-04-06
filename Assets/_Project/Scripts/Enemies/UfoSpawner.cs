using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemies
{
    public class UfoSpawner : GeneratorEnemies
    {
        public UfoSpawner(RemoteConfigsData remoteConfig, [Inject(Id = "UfoPool")] ObjectPool<Enemy> pool,
            IEnemyDeathListener enemyManager, Camera camera)
            : base(remoteConfig, pool, enemyManager, camera){}
        
        protected override void ConfigureSpawn(Enemy enemy, Vector2 _)
        {
            if (enemy.TryGetComponent(out FlyingSaucerController flyingSaucerController))
                flyingSaucerController.enabled = true;
        }
    }
}