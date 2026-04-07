using System;
using UnityEngine;

namespace _Project.Scripts.Services.RemoteConfigs
{
    [Serializable]
    public class ObjectPoolConfigs
    {
        [Header("Remote Configs Pools ")]
        [SerializeField] private int _asteroidPoolSize;
        [SerializeField] private int _ufoPoolSize;
        [SerializeField] private int _bulletPoolSize;
        [SerializeField] private int _laserPoolSize;
        
        public int AsteroidPoolSize => _asteroidPoolSize;
        public int UfoPoolSize => _ufoPoolSize;
        public int BulletPoolSize => _bulletPoolSize;
        public int LaserPoolSize => _laserPoolSize;
    }
}