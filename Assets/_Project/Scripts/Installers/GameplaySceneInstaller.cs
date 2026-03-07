using _Project.Scripts.Configs.Enemies;
using _Project.Scripts.Configs.PoolObjects;
using _Project.Scripts.Enemies;
using _Project.Scripts.Infrastructure;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [Header("Camera")]
        [SerializeField] private Camera _mainCamera;
        
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
            Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<WeaponShooter>().AsSingle();
            Container.Bind<IEnemyDeathListener>().To<EnemyManager>().AsSingle();
            Container.Bind<RestartGame>().AsSingle();
            Container.Bind<ILoseModel>().To<ScoreData>().AsSingle();
            Container.Bind<EnemyDeathTracker>().AsSingle();

            Container.Bind<PerformanceShipView>().FromInstance(_performanceShip).AsSingle();
            Container.Bind<EndGameView>().FromInstance(_endGameScreen).AsSingle();

            BindPlayer();
            BindPools();
            BindSpawners();

            Container.BindInterfacesAndSelfTo<Game>().AsSingle().NonLazy();
        }

        private void BindPlayer()
        {
            Container.Bind<PlayerController>().FromComponentInNewPrefab(_shipPrefab).AsSingle();

            Container.Bind<IInputService>().To<InputController>().AsSingle();
            Container.Bind<InputForShoot>().FromComponentOnRoot().AsSingle();
            
            Container.Bind<ICollisionHandler>().To<HandlerCrashWithEnemy>().FromComponentOnRoot().AsSingle();
            
            Container.Bind<IControllable>().To<PlayerControllerAdapter>().AsSingle();
            Container.Bind<IShootable>().To<PlayerShootProvider>().AsSingle();
            
            Container.Bind<Character>().AsSingle();
        }
        
        private void BindPools()
        {
            Transform rootPool = new GameObject("All_Objects_Pools").transform;

            Transform enemiesContainer = new GameObject("Enemies_Category").transform;
            enemiesContainer.parent = rootPool;
            
            Container.Bind<ObjectPool<Enemy>>()
                .WithId("AsteroidPool")
                .FromInstance(new ObjectPool<Enemy>(_asteroidPrefabs, _poolConfig.AsteroidPoolSize, "Asteroid", enemiesContainer))
                .AsSingle();
            
            Container.Bind<ObjectPool<Enemy>>()
                .WithId("UfoPool")
                .FromInstance(new ObjectPool<Enemy>(_ufoPrefabs, _poolConfig.UfoPoolSize, "UFO", enemiesContainer))
                .AsSingle();
            
            Transform projectilesContainer = new GameObject("Weapons_Category").transform;
            projectilesContainer.parent = rootPool;
            
            Container.Bind<ObjectPool<Bullet>>()
                // .WithId("AsteroidPool")
                .FromInstance(new ObjectPool<Bullet>(_bulletPrefabs, _poolConfig.BulletPoolSize, "Shoot", projectilesContainer))
                .AsSingle();
            
            Container.Bind<ObjectPool<Laser>>()
                // .WithId("UfoPool")
                .FromInstance(new ObjectPool<Laser>(_laserPrefabs, _poolConfig.LaserPoolSize, "Shoot", projectilesContainer))
                .AsSingle();
        }
        
        private void BindSpawners()
        {
            Container.Bind<GeneratorEnemies>().To<AsteroidSpawner>().AsSingle().WithArguments(_enemyConfigs[0]);
            Container.Bind<GeneratorEnemies>().To<UfoSpawner>().AsSingle().WithArguments(_enemyConfigs[1]);
            
            Container.BindInterfacesAndSelfTo<EnemyDeathTracker>().AsSingle();

            Container.Bind<EnemySpawnController>().AsSingle();
            Container.Bind<IEnemyInitialize>().To<EnemyInitializer>().AsSingle();
        }
    }
}