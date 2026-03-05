using _Project.Scripts.Configs.Enemies;
using _Project.Scripts.Configs.PoolObjects;
using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using Zenject;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [Header("Prefabs UI-Background")]
        [SerializeField] private Canvas _backgroundCanvas;
        [SerializeField] private PerformanceShipView _performanceShip;
        [SerializeField] private EndGameView _endGameScreen;

        [Header("Pool Config")]
        [SerializeField] private PoolConfig _poolConfig;
        
        [Header("Enemy Configs")]
        [SerializeField] private EnemyConfig[] _enemyConfigs;

        [Header("Prefabs")]
        [SerializeField] private PlayerController _shipPrefab;
        [SerializeField] private Bullet _bulletPrefabs;
        [SerializeField] private Laser _laserPrefabs;
        [SerializeField] private Enemy _asteroidPrefabs;
        [SerializeField] private Enemy _ufoPrefabs;


        public override void InstallBindings()
        {
 
        }

        private void BindPools()
        {

        }
        
        private void BindPlayer()
        {
            throw new System.NotImplementedException();
        }
        
        private void BindSpawners()
        {
            throw new System.NotImplementedException();
        }
    }
}