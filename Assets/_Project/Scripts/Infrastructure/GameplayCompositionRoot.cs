using System;
using _Project.Scripts.Configs.PoolObjects;
using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    [Serializable]
    public class GameplayCompositionRoot
    {
        [Header("Prefabs UI-Background")]
        [SerializeField] private Canvas _backgroundCanvas;
        [SerializeField] private PerformanceShipView _performanceShip;
        [SerializeField] private EndGameView _endGameScreen;

        [Header("Pool Config")]
        [SerializeField] private PoolConfig _poolConfig;

        [Header("Prefabs")]
        [SerializeField] private Character _ship;
        [SerializeField] private Bullet _bulletPrefabs;
        [SerializeField] private Laser _laserPrefabs;
        [SerializeField] private Enemy _asteroidPrefabs;
        [SerializeField] private Enemy _ufoPrefabs;
        [SerializeField] private GeneratorEnemies[] _generatorEnemies;

        private IGameFactory _gameFactory;
        private IControllable _controllable;
        private IShootable _shootable;
        private IEnemyDeathListener _enemyManager;
        private ICollisionHandler _collisionHandler;

        private Character _player;
        private PlayerController _controller;
        private InputForShoot _shoot;
        private WeaponShooter _weaponShooter;

        private ObjectPool<Enemy> _asteroidPool;
        private ObjectPool<Enemy> _ufoPool;
        private ObjectPool<Bullet> _bulletPool;
        private ObjectPool<Laser> _laserPool;

        private CoordinateDisplay _coordinateDisplay;
        private ViewCurrentAmountLaser _viewCurrentAmountLaser;

        private Camera _mainCamera;
        private RestartGame _restartGame;
        private ScoreData _scoreData;

        private LosePresenter _losePresenter;
        private EnemyDeathTracker _deathTracker;
        private EnemySpawnController _spawnController;

        public Game Game { get; private set; }

        public void Compose()
        {
            _mainCamera = Camera.main;
            _gameFactory = new GameFactory();
            _weaponShooter = new WeaponShooter();
            _restartGame = new RestartGame();
            _enemyManager = new EnemyManager();
            _spawnController = new EnemySpawnController(_generatorEnemies);
            _scoreData = new ScoreData();
            _deathTracker = new EnemyDeathTracker();

            CreateGameEntities();
            SetupPools();

            _weaponShooter.Initialize(_bulletPool, _laserPool);

            SetupSystems();

            Game = new Game(_gameFactory, _endGameScreen, _spawnController, _player, _controllable, _shootable,
                _restartGame, _scoreData, _deathTracker);
        }

        private void CreateGameEntities()
        {
            _gameFactory.CreateBackground(_backgroundCanvas, _mainCamera);
            _gameFactory.CreatePlayer(_ship, out _player, out _controller, out _shoot, out _collisionHandler);
            InitializePlayer(_collisionHandler);

            _gameFactory.CreatePerformanceShip(_performanceShip, _player, _controller, _shoot, _weaponShooter);
        }

        private void InitializePlayer(ICollisionHandler collisionHandler)
        {
            IInputService inputService = new InputController();
            _controller.Construct(inputService);

            _controllable = new PlayerControllerAdapter(_controller);
            _player.Construct(_controllable, collisionHandler);

            _shootable = new PlayerShootProvider(_shoot);
        }

        private void SetupPools()
        {
            Transform rootPool = new GameObject("All_Objects_Pools").transform;

            Transform enemiesContainer = new GameObject("Enemies_Category").transform;
            enemiesContainer.parent = rootPool;

            Transform projectilesContainer = new GameObject("Weapon_Category").transform;
            projectilesContainer.parent = rootPool;

            _asteroidPool = new ObjectPool<Enemy>(_asteroidPrefabs, _poolConfig.AsteroidPoolSize, "Asteroid",
                enemiesContainer);
            _ufoPool = new ObjectPool<Enemy>(_ufoPrefabs, _poolConfig.UfoPoolSize, "UFO", enemiesContainer);
            _bulletPool =
                new ObjectPool<Bullet>(_bulletPrefabs, _poolConfig.BulletPoolSize, "Shoot", projectilesContainer);
            _laserPool = new ObjectPool<Laser>(_laserPrefabs, _poolConfig.LaserPoolSize, "Shoot", projectilesContainer);
        }

        private void SetupSystems()
        {
            IEnemyInitialize initializer = new EnemyInitializer(_generatorEnemies);

            initializer.SetupAsteroid(_asteroidPool, _enemyManager);
            initializer.SetupUfo(_ufoPool, _enemyManager, _player.transform);

            _deathTracker.Initialize(_scoreData, _enemyManager);
        }
    }
}