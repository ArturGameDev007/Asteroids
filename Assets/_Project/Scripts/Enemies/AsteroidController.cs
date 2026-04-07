using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemies
{
    public class AsteroidController : Enemy
    {
        private IRemoteConfigs _remoteConfigs;
        
        private Vector2 _direction;

        [Inject]
        public void Construct(IRemoteConfigs remoteConfigs)
        {
            _remoteConfigs = remoteConfigs;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;
        }

        protected override void Move()
        {
            Head2D.velocity = _direction * _remoteConfigs.RemoteConfig.EnemyConfigs.EnemySpeed;
        }
    }
}
