using System.Collections.Generic;
using _Project.Scripts.Enemies;
using _Project.Scripts.Services.Analytics;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class WeaponShooter
    {
        private readonly List<TimedPoolObject> _activeProjectiles = new();

        private readonly ObjectPool<Bullet> _bulletPool;
        private readonly ObjectPool<Laser> _laserPool;
        
        private readonly IAnalyticsService _analyticsService;

        private float _laserCooldown = 0.5f;
        private float _nextBulletShootTime;
        
        public int ShotsCount { get; private set; }
        public int LaserUsed { get; private set; }

        public WeaponShooter(ObjectPool<Bullet> bulletPool, ObjectPool<Laser> laserPool, IAnalyticsService analyticsService)
        {
            _bulletPool = bulletPool;
            _laserPool = laserPool;
            _analyticsService = analyticsService;
        }

        public void ShootBullet(Transform spawnPoint)
        {
            if (_bulletPool == null || Time.time < _nextBulletShootTime)
                return;

            _nextBulletShootTime = Time.time + _laserCooldown;

            Bullet bullet = _bulletPool.GetObject();

            ShotsCount++;

            bullet.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            bullet.Initialize(_bulletPool);

            _activeProjectiles.Add(bullet);
        }

        public void ShootLaser(Transform spawnPoint)
        {
            if (_laserPool == null)
                return;

            Laser laser = _laserPool.GetObject();
            
            LaserUsed++;
            _analyticsService.LogLaserUsed();

            laser.transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
            laser.Initialize(_laserPool);

            _activeProjectiles.Add(laser);
        }

        public void StopAllShoots()
        {
            foreach (var projectile in _activeProjectiles)
            {
                projectile.StopLifeTimer();

                if (projectile.TryGetComponent(out DirectionShot directionShot))
                    directionShot.StopMovement();
            }

            _activeProjectiles.Clear();
        }
    }
}