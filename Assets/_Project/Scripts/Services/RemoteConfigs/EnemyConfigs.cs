using System;
using UnityEngine;

namespace _Project.Scripts.Services.RemoteConfigs
{
    [Serializable]
    public class EnemyConfigs
    {
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
    }
}