using System.Collections.Generic;
using _Project.Scripts.Enemies;
using _Project.Scripts.Services.Analytics;
using _Project.Scripts.Services.Effects;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class WeaponShooter : IWeaponShooter
    {
        private readonly List<TimedPoolObject> _activeProjectiles = new();

        private readonly ObjectPool<Bullet> _bulletPool;
        private readonly ObjectPool<Laser> _laserPool;

        private readonly IAnalyticsService _analyticsService;
        
        private readonly IEffectService _effectService;

        private float _laserCooldown = 0.5f;
        private float _nextBulletShootTime;

        public int ShotsCount { get; private set; }
        public int LaserUsed { get; private set; }

        public WeaponShooter(ObjectPool<Bullet> bulletPool, ObjectPool<Laser> laserPool,
            IAnalyticsService analyticsService, IEffectService effectService)
        {
            _bulletPool = bulletPool;
            _laserPool = laserPool;
            _analyticsService = analyticsService;
            _effectService = effectService;
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
            
            _effectService.PlayShoot(spawnPoint.position, spawnPoint.rotation);

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
            
            _effectService.PlayShoot(spawnPoint.position, spawnPoint.rotation);

            _activeProjectiles.Add(laser);
        }

        public void StopAllShoots()
        {
            for (int i = _activeProjectiles.Count - 1; i >= 0; i--)
            {
                var projectile = _activeProjectiles[i];

                if (projectile != null)
                {
                    if (projectile.TryGetComponent(out DirectionShot directionShot))
                        directionShot.StopMovement();

                    projectile.ForceReturn();
                }
            }

            _activeProjectiles.Clear();
        }
    }
}