using _Project.Scripts.Configs.Player;
using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player.Weapons
{
    public class DirectionShot : MonoBehaviour
    {
        // [SerializeField] private ShootingConfig _shootingConfig;
        
        private RemoteConfigsData _remoteConfigs;

        [Inject]
        public void Construct(RemoteConfigsData remoteConfigs)
        {
            _remoteConfigs = remoteConfigs;
        }

        private void Update()
        {
            DirectionMove();
        }

        private void DirectionMove()
        {
            transform.Translate(transform.up * (_remoteConfigs.SpeedShoot * Time.deltaTime), Space.World);
        }
        
        public void StopMovement()
        {
            enabled = false; 
        }
    }
}
