using _Project.Scripts.Configs.Enemies;
using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemies
{
    public class AsteroidController : Enemy
    {
        // [SerializeField] private AsteroidConfig _asteroidConfig;
        
        private RemoteConfigsData _remoteConfigs;
        
        private Vector2 _direction;

        [Inject]
        public void Construct(RemoteConfigsData remoteConfigs)
        {
            _remoteConfigs = remoteConfigs;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;
        }

        protected override void Move()
        {
            // Head2D.velocity = _direction * _asteroidConfig.Speed;
            Head2D.velocity = _direction * _remoteConfigs.EnemySpeed;
        }
    }
}
