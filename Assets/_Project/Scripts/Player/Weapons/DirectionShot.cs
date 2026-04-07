using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player.Weapons
{
    public class DirectionShot : MonoBehaviour
    {
        private IRemoteConfigs _remoteConfigs;

        [Inject]
        public void Construct(IRemoteConfigs remoteConfigs)
        {
            _remoteConfigs = remoteConfigs;
        }

        private void Update()
        {
            DirectionMove();
        }

        private void DirectionMove()
        {
            transform.Translate(transform.up * (_remoteConfigs.RemoteConfig.PlayerConfig.SpeedShoot * Time.deltaTime), Space.World);
        }
        
        public void StopMovement()
        {
            enabled = false; 
        }
    }
}
