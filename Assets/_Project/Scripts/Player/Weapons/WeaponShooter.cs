using System;
using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class WeaponShooter
    {
        private ObjectPool<Bullet> _bulletPool;
        private ObjectPool<Laser> _laserPool;
        
        private float _laserCooldown = 0.5f;
        private float _nextLaserShootTime; 
        
        public void Initialize(ObjectPool<Bullet> bulletPool, ObjectPool<Laser> laserPool)
        {
            _bulletPool = bulletPool;
            _laserPool = laserPool;
        }
        
        public void ShootBullet(Transform spawnPoint)
        {
            if (_bulletPool == null) return;

            Bullet bullet = _bulletPool.GetObject();
            
            bullet.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            bullet.Initialize(_bulletPool);
        }

        public void ShootLaser(Transform spawnPoint)
        {
            if (_laserPool == null || Time.time < _nextLaserShootTime) 
                return;

            _nextLaserShootTime = Time.time + _laserCooldown;

            Laser laser = _laserPool.GetObject();
            
            laser.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            laser.Initialize(_laserPool);
        }
    }
}