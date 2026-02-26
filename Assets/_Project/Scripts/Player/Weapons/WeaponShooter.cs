using System.Collections.Generic;
using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class WeaponShooter
    {
        private List<TimedPoolObject> _activeProjectiles = new();

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
            if (_bulletPool == null || Time.time < _nextLaserShootTime)
                return;

            _nextLaserShootTime = Time.time + _laserCooldown;

            Bullet bullet = _bulletPool.GetObject();
            
            _activeProjectiles.Add(bullet);
            
            bullet.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            bullet.Initialize(_bulletPool);
        }

        public void ShootLaser(Transform spawnPoint)
        {
            if (_laserPool == null)
                return;

            Laser laser = _laserPool.GetObject();
            
            _activeProjectiles.Add(laser);

            laser.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            laser.Initialize(_laserPool);
        }


        public void StopAllShoots()
        {
            foreach (var projectile in _activeProjectiles)
            {
                projectile.StopLifeTimer();

                if (projectile.TryGetComponent(out DirectionShot  directionShot))
                    directionShot.StopMovement();
            }

            _activeProjectiles.Clear();
        }
    }
}