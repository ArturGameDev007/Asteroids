using _Project.Scripts.Configs;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class DirectionShot : MonoBehaviour
    {
        [SerializeField] private ShootingConfig _shootingConfig;

        private void Update()
        {
            DirectionMove();
        }

        private void DirectionMove()
        {
            transform.Translate(transform.up * (_shootingConfig.Speed * Time.deltaTime), Space.World);
        }
        
        public void StopMovement()
        {
            enabled = false; 
        }
    }
}
