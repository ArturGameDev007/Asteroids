using _Project.Scripts.Configs.Enemies;
using _Project.Scripts.Configs.PoolObjects;
using _Project.Scripts.Enemies;
using _Project.Scripts.Infrastructure;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.Background;
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
        
        [Header("Prefab UI-LoseScreen")]
        [SerializeField] private LoseView _endGameScreenPrefab;

        [Header("Container For Objects Pools")]
        [SerializeField] private Transform _containerForPools;

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
            Container.BindInstance(_endGameScreenPrefab).AsSingle();
            
            BindBackgroundUI();
            BindPerformanceUI();

            BindPlayer();
            BindPools();
            BindSpawners();

            BindGameplayInfrastructure();
        }

        private void BindGameplayInfrastructure()
        {
            Container.Bind<Game>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayEntryPoint>().AsSingle();
        }

        private void BindBackgroundUI()
        {
            Container.Bind<Canvas>().WithId("Background_UI").FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<BackgroundView>().AsSingle();
        }

        private void BindPlayer()
        {
            Container.Bind(typeof(PlayerController), typeof(InputForShoot), typeof(HandlerCrashWithEnemy))
                .FromComponentInNewPrefab(_shipPrefab).AsSingle();
            
            Container.Bind<Transform>().WithId("Player").FromResolveGetter<PlayerController>(player => player.transform);

            Container.Bind<ICollisionHandler>().To<HandlerCrashWithEnemy>().FromResolve();
            Container.Bind<IInputService>().To<InputController>().AsSingle();
            Container.Bind<IControllable>().To<PlayerControllerAdapter>().AsSingle();
            Container.Bind<IShootable>().To<PlayerShootProvider>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<Character>().AsSingle();
        }

        private void BindPerformanceUI()
        {
            Container.Bind(typeof(PerformanceShipView), typeof(CoordinateDisplay), 
                    typeof(ViewCurrentAmountLaser), typeof(GenerateLaser))
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }

        private void BindPools()
        {
            Transform enemiesContainer = new GameObject("Enemies_Category").transform;
            enemiesContainer.parent = _containerForPools;
            
            Container.Bind<ObjectPool<Enemy>>().WithId("AsteroidPool").AsCached()
                .WithArguments(_asteroidPrefabs, _poolConfig.AsteroidPoolSize, "Asteroid", enemiesContainer);
            
            Container.Bind<ObjectPool<Enemy>>().WithId("UfoPool").AsCached()
                .WithArguments(_ufoPrefabs, _poolConfig.UfoPoolSize, "UFO", enemiesContainer);
            
            Transform projectilesContainer = new GameObject("Weapons_Category").transform;
            projectilesContainer.parent = _containerForPools;
            
            Container.Bind<ObjectPool<Bullet>>().AsCached()
                .WithArguments(_bulletPrefabs, _poolConfig.BulletPoolSize, "Bullet", projectilesContainer);
            
            Container.Bind<ObjectPool<Laser>>().AsCached()
                .WithArguments(_laserPrefabs, _poolConfig.LaserPoolSize, "Laser", projectilesContainer);
        }
        
        private void BindSpawners()
        {
            Container.Bind<GeneratorEnemies>().To<AsteroidSpawner>().AsCached().WithArguments(_enemyConfigs[0]);
            Container.Bind<GeneratorEnemies>().To<UfoSpawner>().AsCached().WithArguments(_enemyConfigs[1]);
            
            Container.BindInterfacesAndSelfTo<EnemyDeathTracker>().AsSingle();
            Container.Bind<EnemySpawnController>().AsSingle();
        }
    }
}