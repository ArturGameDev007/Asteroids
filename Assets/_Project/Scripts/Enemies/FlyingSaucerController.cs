using _Project.Scripts.Player;
using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemies
{
    public class FlyingSaucerController : Enemy
    {
        private IRemoteConfigs _remoteConfigs;
        private IPlayerProvider _playerProvider;
        
        [Inject]
        public void Construct(IRemoteConfigs remoteConfigs, IPlayerProvider target)
        {
            _remoteConfigs = remoteConfigs;
            _playerProvider = target;
        }

        protected override void Move()
        {
            if (_playerProvider.Player == null || _playerProvider.PlayerTransform == null)
                return;
            
            Vector2 currentPosition = Head2D.position;
            Vector2 targetPosition = _playerProvider.PlayerTransform.position;
            
            Vector2 directionToPlayer = (targetPosition - currentPosition).normalized;
            RotateTowardPlayer(directionToPlayer);
            
            Vector2 directionMove = Vector2.MoveTowards(currentPosition, targetPosition, _remoteConfigs.RemoteConfig.EnemySpeed * Time.deltaTime);
            Head2D.MovePosition(directionMove);
        }
        
        private void RotateTowardPlayer(Vector2 directionToPlayer)
        {
            float rotateX = 0;
            float rotateY = 0;
        
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        
            Quaternion targetRotation = Quaternion.Euler(new Vector3(rotateX, rotateY, angle));
        
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, targetRotation, _remoteConfigs.RemoteConfig.RotationSpeed * Time.deltaTime);
        }
    }
}