using System;
using UnityEngine;

namespace _Project.Scripts.Services.RemoteConfigs
{
    [Serializable]
    public class RemoteConfigsRoot
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private EnemyConfigs _enemyConfigs;
        [SerializeField] private ObjectPoolConfigs _poolConfigs;
        
        public PlayerConfig PlayerConfig => _playerConfig;
        public EnemyConfigs EnemyConfigs => _enemyConfigs;
        public ObjectPoolConfigs PoolConfigs => _poolConfigs;
    }
}