using System;
using UnityEngine;

namespace _Project.Scripts.Services.RemoteConfigs
{
    [Serializable]
    public class PlayerConfig
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
    }
}