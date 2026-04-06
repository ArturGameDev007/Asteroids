using System;
using UnityEngine;

namespace _Project.Scripts.Services.RemoteConfigs
{
    [Serializable]
    public class RemoteConfigsData
    {
        [Header("Remote Configs Ship & Shots Data")]
        [SerializeField] private float _forceInputShip;
        [SerializeField] private float _rotationSpeedShip;
        [SerializeField] private float _speedShoot;
        [SerializeField] private float _lifeTimeShoot;
        [SerializeField] private float _reloadTimeLaser;
        [SerializeField] private int _maxAmountLaser;
        
        public float ForceInputShip => _forceInputShip;
        public float RotationSpeedShip => _rotationSpeedShip;
        public float SpeedShoot => _speedShoot;
        public float LifeTimeShoot => _lifeTimeShoot;
        public float ReloadTimeLaser => _reloadTimeLaser;
        public int MaxAmountLaser => _maxAmountLaser;
        
        [Header("Remote Configs Enemy Data")]
        [SerializeField] private float _enemySpeed;
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _spawnOffset;
        [SerializeField] private float _delay;
        [SerializeField] private int _scoreForKill;
        
        public float EnemySpeed => _enemySpeed;
        public float RotationSpeed => _rotationSpeed;
        public float SpawnOffset => _spawnOffset;
        public float Delay => _delay;
        public int ScoreForKill => _scoreForKill;
        
        [Header("Remote Configs Enemies Data")]
        [SerializeField] private int _asteroidPoolSize;
        [SerializeField] private int _ufoPoolSize;
        
        public int AsteroidPoolSize => _asteroidPoolSize;
        public int UfoPoolSize => _ufoPoolSize;
        
        [Header("Remote Configs Weapons Data")]
        [SerializeField] private int _bulletPoolSize;
        [SerializeField] private int _laserPoolSize;
        
        public int BulletPoolSize => _bulletPoolSize;
        public int LaserPoolSize => _laserPoolSize;
    }
}