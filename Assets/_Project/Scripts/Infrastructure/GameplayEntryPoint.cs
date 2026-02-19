using System.Collections.Generic;
using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.Background;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [Header("Prefabs UI")]
        [SerializeField] private BackgroundView _background;
        [SerializeField] private PerformanceShipView _performanceShip;
        [SerializeField] private EndGameView _endGameScreen;

        [Header("Prefab Player")]
        [SerializeField] private Character _ship;

        [Header("Prefabs Projectiles")]
        [SerializeField] private List<Bullet> _bulletPrefabs;
        [SerializeField] private List<Laser> _laserPrefabs;

        [Header("Systems Pool")]
        [SerializeField] private List<Enemy> _enemyPrefabs;
        [SerializeField] private GeneratorEnemies _generatorEnemies;

        [Header("UI & Data")]
        [SerializeField] private EnemyDeathTracker _deathTracker;

        private IGameFactory _gameFactory;
        private IInstantiator _instantiator;

        private LosePresenter _losePresenter;

        private Character _player;
        private PlayerController _controller;
        private InputForShoot _shoot;
        private WeaponShooter _weapons;

        private ObjectPool<Enemy> _enemyPool;
        private ObjectPool<Bullet> _bulletPool;
        private ObjectPool<Laser> _laserPool;

        private CoordinateDisplay _coordinateDisplay;
        private ViewCurrentAmountLaser _viewCurrentAmountLaser;
        private HierarchyScanner _hierarchyScanner;

        private Game _game;
        private Camera _mainCamera;
        private RestartGame _restartGame;
        private ScoreData _scoreData;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _instantiator = GetComponent<IInstantiator>();
            _gameFactory = new GameFactory(_instantiator);
            _hierarchyScanner = new HierarchyScanner();
            _weapons = new WeaponShooter();
            
            _restartGame = new RestartGame();
            _scoreData = new ScoreData();
            
            SetupPools();

            _weapons.Initialize(_bulletPool, _laserPool); 
            
            CreateGameEntities();
            SetupDataAndUI();

            SetupSystems();

            _game = new Game(_generatorEnemies, _player, _controller, _shoot, _losePresenter, _restartGame,
                _scoreData);
        }

        private void Start()
        {
            _game.Initialize();
        }

        private void OnDestroy()
        {
            _game.Dispose();
            _enemyPool.ClearPool();
        }

        private void CreateGameEntities()
        {
            _gameFactory.CreateBackground(_background, _mainCamera);
            _gameFactory.CreatePlayer(_ship, out _player, out _controller, out _shoot);
            _gameFactory.CreatePerformanceShip(_performanceShip, _player, _controller, _shoot, _weapons, _hierarchyScanner);
            _gameFactory.CreateEndGameScreen(_endGameScreen, _hierarchyScanner, _scoreData, out _losePresenter);
        }

        private void SetupPools()
        {
            Transform rootPool = new GameObject("All_Objects_Pools").transform;
            
            Transform enemiesContainer = new GameObject("Enemies_Category").transform;
            enemiesContainer.parent = rootPool;
            
            Transform projectilesContainer = new GameObject("Weapon_Category").transform;
            projectilesContainer.parent = rootPool;

            _enemyPool = new ObjectPool<Enemy>(_enemyPrefabs, 5, "Enemies", enemiesContainer);
            _bulletPool = new ObjectPool<Bullet>(_bulletPrefabs, 5, "Shoot", projectilesContainer);
            _laserPool = new ObjectPool<Laser>(_laserPrefabs, 5, "Shoot", projectilesContainer);
        }
        
        
        private void SetupSystems()
        {
            _generatorEnemies.Initialize(_enemyPool, _player);
            _deathTracker.Initialize(_scoreData);
        }

        private void SetupDataAndUI()
        {
            _generatorEnemies.Initialize(_enemyPool, _player);
            _deathTracker.Initialize(_scoreData);
        }
    }
}