using _Project.Scripts.Configs;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Player.Weapons
{
    public class DirectionShot : MonoBehaviour
    {
        // [SerializeField] private float _speed = 5f;
        
        [FormerlySerializedAs("_shootsConfig")] [SerializeField] private ShootingConfig shootingConfig;

        private void Update()
        {
            DirectionMove();
        }

        private void DirectionMove()
        {
            transform.Translate(transform.up * (shootingConfig.Speed * Time.deltaTime), Space.World);
        }
        
        public void StopMovement()
        {
            enabled = false; 
        }
    }
}
