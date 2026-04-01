using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public interface IWeaponShooter
    {
        public int ShotsCount { get; }
        public int LaserUsed { get; }
        
        public void ShootBullet(Transform spawnPoint);

        public void ShootLaser(Transform spawnPoint);

        public void StopAllShoots();
    }
}