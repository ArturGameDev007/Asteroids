using System;
using _Project.Scripts.Configs.Enemies;
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
        
        [Header("Enemy Configs")]
        [SerializeField] private EnemyConfig _asteroidConfig;
        [SerializeField] private EnemyConfig _ufoConfig;

        [Header("Prefabs")]
        [SerializeField] private PlayerController _shipPrefab;
        [SerializeField] private Bullet _bulletPrefabs;
        [SerializeField] private Laser _laserPrefabs;
        [SerializeField] private Enemy _asteroidPrefabs;
        [SerializeField] private Enemy _ufoPrefabs;
        
        private GeneratorEnemies[] _generatorEnemies;

        private IGameFactory _gameFactory;
        private IControllable _controllable;
        private IShootable _shootable;
        private IEnemyDeathListener _enemyManager;
        private ICollisionHandler _collisionHandler;

        private PlayerController _controller; 
        private Character _player;
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

        public Game Compose()
        {
            _mainCamera = Camera.main;
            _gameFactory = new GameFactory();
            _weaponShooter = new WeaponShooter();
            _restartGame = new RestartGame();
            _enemyManager = new EnemyManager();
            _scoreData = new ScoreData();
            _deathTracker = new EnemyDeathTracker();

            CreateGameEntities();
            SetupPools();

            CreateSpawners();
            _spawnController = new EnemySpawnController(_generatorEnemies);

            _weaponShooter.Initialize(_bulletPool, _laserPool);

            SetupSystems();

            return new Game(_gameFactory, _endGameScreen, _spawnController, _player, _controllable, _shootable,
                _restartGame, _scoreData, _deathTracker);
        }

        private void CreateGameEntities()
        {
            _gameFactory.CreateBackground(_backgroundCanvas, _mainCamera);
            _gameFactory.CreatePlayer(_shipPrefab, out  _controller,  out _shoot, out _collisionHandler);
            InitializePlayer(_collisionHandler);
            
            _gameFactory.CreatePerformanceShip(_performanceShip, _controller, _shoot, _weaponShooter);
        }

        private void InitializePlayer(ICollisionHandler collisionHandler)
        {
            IInputService inputService = new InputController();
            _controller.Construct(inputService);

            _controllable = new PlayerControllerAdapter(_controller);
            _player = new Character(_controllable, collisionHandler);
 
            _shootable = new PlayerShootProvider(_shoot);
        }

        private void SetupPools()
        {
            Transform rootPool = new GameObject("All_Objects_Pools").transform;

            Transform enemiesContainer = new GameObject("Enemies_Category").transform;
            enemiesContainer.parent = rootPool;

            Transform projectilesContainer = new GameObject("Weapon_Category").transform;
            projectilesContainer.parent = rootPool;

            _asteroidPool = new ObjectPool<Enemy>(_asteroidPrefabs, _poolConfig.AsteroidPoolSize, "Asteroid", enemiesContainer);
            _ufoPool = new ObjectPool<Enemy>(_ufoPrefabs, _poolConfig.UfoPoolSize, "UFO", enemiesContainer);
            _bulletPool = new ObjectPool<Bullet>(_bulletPrefabs, _poolConfig.BulletPoolSize, "Shoot", projectilesContainer);
            _laserPool = new ObjectPool<Laser>(_laserPrefabs, _poolConfig.LaserPoolSize, "Shoot", projectilesContainer);
        }

        private void CreateSpawners()
        {
            _generatorEnemies = new GeneratorEnemies[] { new AsteroidSpawner(_asteroidConfig), new UfoSpawner(_ufoConfig) };
        }

        private void SetupSystems()
        {
            IEnemyInitialize initializer = new EnemyInitializer(_generatorEnemies);
            initializer.SetupAllSpawners(_asteroidPool, _ufoPool, _enemyManager, _controller.transform);

            _deathTracker.Initialize(_scoreData, _enemyManager);
        }
    }
}