using _Project.Scripts.Configs.Enemies;
using _Project.Scripts.Configs.PoolObjects;
using _Project.Scripts.Enemies;
using _Project.Scripts.Infrastructure;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Services.AsyncLoader;
using _Project.Scripts.UI.Background;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Installers.Gameplay
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [Header("Camera")]
        [SerializeField] private Camera _mainCamera;

        [Header("Prefab UI")]
        [SerializeField] private AssetReference _endGameScreenPrefabReference;
        [SerializeField] private AssetReference _shipPerfomancePrefabReference;

        [Header("Container For Objects Pools")]
        [SerializeField] private Transform _containerForPools;

        [Header("Pool Config")]
        [SerializeField] private PoolConfig _poolConfig;

        [Header("Enemy Configs")]
        [SerializeField] private EnemyConfig[] _enemyConfigs;

        // [Header("Prefabs")]
        [SerializeField] private PlayerController _shipPrefab;
        [Header("Load Async: Player & Shots")]
        // [SerializeField] private AssetReference _shipPrefabReference;
        [SerializeField] private AssetReference _bulletPrefabReference;
        [SerializeField] private AssetReference _laserPrefabReference;

        [Header("Load Prefabs Async")]
        [SerializeField] private AssetReference[] _enemyRefences;

        public override void InstallBindings()
        {
            Container.Bind<IResourceLoader>().To<AddressableResourceLoader>().AsSingle();

            Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<WeaponShooter>().AsSingle();
            Container.Bind<IEnemyDeathListener>().To<EnemyManager>().AsSingle();
            Container.Bind<RestartGame>().AsSingle();
            Container.Bind<ILoseModel>().To<ScoreData>().AsSingle();
            Container.BindInstance(_endGameScreenPrefabReference).AsSingle();

            BindBackgroundUI();
            
            BindPlayer();
            
            BindPerformanceUI();

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
            // Container.Bind<Transform>().WithId("Player")
            //     .FromResolveGetter<PlayerController>(player => player.transform);
            // Container.Bind(typeof(PlayerController), typeof(InputForShoot), typeof(HandlerCrashWithEnemy),
            //     typeof(GenerateLaser)).FromMethod(provider => provider.Container.Resolve<PlayerProvider>().Player).AsCached();
            // Container.Bind<PlayerResourceManager>().AsSingle().WithArguments(_shipPrefabReference);
            //
            // Container.Bind<PlayerProvider>().AsSingle();
            //
            // Container.Bind<PlayerController>()
            //     .FromMethod(ctx => ctx.Container.Resolve<PlayerProvider>().Player)
            //     .AsCached();
            
            //
            // Container.Bind<InputForShoot>()
            //     .FromResolveGetter<PlayerController>(pc => pc?.GetComponent<InputForShoot>())
            //     .AsCached();
            //
            // Container.Bind<HandlerCrashWithEnemy>()
            //     .FromResolveGetter<PlayerController>(pc => pc?.GetComponent<HandlerCrashWithEnemy>())
            //     .AsCached();
            //
            // Container.Bind<GenerateLaser>()
            //     .FromResolveGetter<PlayerController>(pc => pc?.GetComponent<GenerateLaser>())
            //     .AsCached();
            
            // Container.Bind(typeof(PlayerController), typeof(InputForShoot), typeof(HandlerCrashWithEnemy),
            //     typeof(GenerateLaser)).FromComponentInNewPrefab(_shipPrefab).AsSingle();

            // Container.Bind<PlayerResourceManager>().AsSingle().WithArguments(_shipPrefab);
            //
            // // Container.Bind<IPlayerProvider>().To<PlayerProvider>().AsSingle();
            // Container.BindInterfacesAndSelfTo<PlayerProvider>().AsSingle();
            //
            // Container.Bind<PlayerController>().FromResolveGetter<IPlayerProvider>(player => player.Player);
            // Container.Bind<InputForShoot>().FromResolveGetter<IPlayerProvider>(pc => pc.Player?.GetComponent<InputForShoot>());
            // Container.Bind<HandlerCrashWithEnemy>().FromResolveGetter<IPlayerProvider>(pc => pc.Player?.GetComponent<HandlerCrashWithEnemy>());
            // Container.Bind<GenerateLaser>().FromResolveGetter<IPlayerProvider>(pc => pc.Player?.GetComponent<GenerateLaser>());
            // Container.BindInstance(_shipPrefab).AsSingle();
            // Container.Bind<PlayerResourceManager>().AsSingle();

            // Container.Bind<PlayerResourceManager>().AsSingle().WithArguments(_shipPrefab);
            //
            // Container.Bind<IPlayerProvider>().To<PlayerProvider>().AsSingle();
            // Container.Bind<ILaserState>().To<GenerateLaser>().FromResolve();
            // Container.Bind<ICollisionHandler>().To<HandlerCrashWithEnemy>().FromResolve();
            // Container.Bind<Transform>().WithId("Player").FromResolveGetter<IPlayerProvider>(p => p.Player?.transform);
            // Container.Bind<IMovableEntity>().To<PlayerController>().FromResolve();
            // Container.Bind<IInputPauseHandler>().To<InputForShoot>().FromResolve();
            // Container.Bind<ICollisionHandler>().To<HandlerCrashWithEnemy>().FromResolve();
            // Container.Bind<ILaserState>().To<GenerateLaser>().FromResolve();
            
            Container.Bind(typeof(IMovableEntity), typeof(IInputPauseHandler), typeof(ICollisionHandler),
                     typeof(ILaserState)).FromComponentInNewPrefab(_shipPrefab).AsSingle();
            
            // Container.BindInstance(_shipPrefabReference).AsSingle();
            
            // Container.Bind<PlayerResourceManager>().AsSingle().WithArguments(_shipPrefabReference);
            
            // Container.Bind<IPlayerProvider>().To<PlayerProvider>().AsSingle();

            // Container.Bind<IMovableEntity>().FromResolveGetter<IPlayerProvider>(player => player.Player);

            Container.Bind<Transform>().FromResolveGetter<IMovableEntity>(player => player.PlayerTransform);
            
            Container.Bind<IInputService>().To<InputController>().AsSingle();
            Container.Bind<IControllable>().To<PlayerControllerAdapter>().AsSingle();
            Container.Bind<IShootable>().To<PlayerShootProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<Character>().AsSingle();
        }

        private void BindPerformanceUI()
        {
            Container.Bind<CoordinateResourceManager>().AsSingle().WithArguments(_shipPerfomancePrefabReference);
            
            // Container.Bind<ICoordinateView>().FromComponentInHierarchy().AsSingle();
            // Container.Bind<ILaserView>().FromComponentInHierarchy().AsSingle();

            // Container.Bind<ILaserView>().To<ViewCurrentAmountLaser>().AsSingle();

            Container.BindInterfacesAndSelfTo<PerformancePresenter>().AsSingle();
        }

        private void BindPools()
        {
            Transform enemiesContainer = new GameObject("Enemies_Category").transform;
            enemiesContainer.parent = _containerForPools;

            Container.Bind<ObjectPool<Enemy>>().WithId("AsteroidPool").AsCached()
                .WithArguments(default(Enemy), _poolConfig.AsteroidPoolSize, "Asteroid", enemiesContainer);

            Container.Bind<ObjectPool<Enemy>>().WithId("UfoPool").AsCached()
                .WithArguments(default(Enemy), _poolConfig.UfoPoolSize, "UFO", enemiesContainer);

            Container.Bind<EnemyResourceManager>().AsSingle().WithArguments(_enemyRefences);

            Transform projectilesContainer = new GameObject("Weapons_Category").transform;
            projectilesContainer.parent = _containerForPools;

            Container.Bind<ObjectPool<Bullet>>().AsCached()
                .WithArguments(default(Bullet), _poolConfig.BulletPoolSize, "Bullet", projectilesContainer);

            Container.Bind<ObjectPool<Laser>>().AsCached()
                .WithArguments(default(Laser), _poolConfig.LaserPoolSize, "Laser", projectilesContainer);

            Container.Bind<ProjectileResourceManager>().AsSingle()
                .WithArguments(_bulletPrefabReference, _laserPrefabReference);
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