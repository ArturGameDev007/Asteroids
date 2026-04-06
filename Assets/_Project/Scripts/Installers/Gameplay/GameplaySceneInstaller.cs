using _Project.Scripts.Enemies;
using _Project.Scripts.Infrastructure;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Services.AsyncLoader;
using _Project.Scripts.Services.RemoteConfigs;
using _Project.Scripts.UI.Background;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using Zenject;

namespace _Project.Scripts.Installers.Gameplay
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [Header("Camera")]
        [SerializeField] private Camera _mainCamera;

        [Header("Prefabs UI")]
        [SerializeField] private AssetReference _endGameScreenPrefabReference;
        [SerializeField] private AssetReference _shipPerformancePrefabReference;

        [Header("Container For Objects Pools")]
        [SerializeField] private Transform _containerForPools;

        [Header("Load Async: Player & Shots")]
        [SerializeField] private AssetReference _shipPrefabReference;
        [SerializeField] private AssetReference _bulletPrefabReference;
        [SerializeField] private AssetReference _laserPrefabReference;

        [Header("Load Prefabs Async")]
        [SerializeField] private AssetReference[] _enemyRefences;


        [SerializeField] private RemoteConfigsData _remoteConfigsData;

        public override void InstallBindings()
        {
            Container.Bind<IRemoteConfigs>().To<FirebaseRemoteConfig>().AsSingle();
            Container.BindInstance(_remoteConfigsData).AsSingle();
            
            Container.Bind<IResourceLoader>().To<AddressableResourceLoader>().AsSingle();

            Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IWeaponShooter>().To<WeaponShooter>().AsSingle();
            Container.Bind<IEnemyDeathListener>().To<EnemyManager>().AsSingle();
            Container.Bind<RestartGame>().AsSingle();
            Container.Bind<ILoseModel>().To<ScoreData>().AsSingle();
            
            Container.Bind<LoseResourceManager>().AsSingle().WithArguments(_endGameScreenPrefabReference);
            
            BindBackgroundUI();

            BindPlayer();

            BindPerformanceUI();

            BindPools();
            BindSpawners();

            BindGameplayInfrastructure();
        }

        private void BindGameplayInfrastructure()
        {
            Container.Bind<GameLoader>().AsSingle();
            Container.Bind<GameplayController>().AsSingle();
            
            Container.Bind<Game>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayEntryPoint>().AsSingle();

            Container.BindInterfacesAndSelfTo<LoseManager>().AsSingle();
        }

        private void BindBackgroundUI()
        {
            Container.Bind<Canvas>().WithId("Background_UI").FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesTo<BackgroundView>().AsSingle();
        }

        private void BindPlayer()
        {
            Container.Bind<PlayerResourceManager>().AsSingle().WithArguments(_shipPrefabReference);

            Container.Bind<IPlayerProvider>().To<PlayerProvider>().AsSingle();

            Container.Bind<IInputService>().To<InputController>().AsSingle();
            Container.Bind<IControllable>().To<PlayerControllerAdapter>().AsSingle();
            Container.Bind<IShootable>().To<PlayerShootProvider>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<Character>().AsSingle();
        }

        private void BindPerformanceUI()
        {
            Container.Bind<CoordinateResourceManager>().AsSingle().WithArguments(_shipPerformancePrefabReference);
            Container.BindInterfacesAndSelfTo<PerformancePresenter>().AsSingle();
        }

        private void BindPools()
        {
            Transform enemiesContainer = new GameObject("Enemies_Category").transform;
            enemiesContainer.parent = _containerForPools;

            Container.Bind<ObjectPool<Enemy>>().WithId("AsteroidPool").AsCached()
                .WithArguments(default(Enemy), _remoteConfigsData.AsteroidPoolSize, "Asteroid", enemiesContainer);

            Container.Bind<ObjectPool<Enemy>>().WithId("UfoPool").AsCached()
                .WithArguments(default(Enemy), _remoteConfigsData.UfoPoolSize, "UFO", enemiesContainer);

            Container.Bind<EnemyResourceManager>().AsSingle().WithArguments(_enemyRefences);

            Transform projectilesContainer = new GameObject("Weapons_Category").transform;
            projectilesContainer.parent = _containerForPools;

            Container.Bind<ObjectPool<Bullet>>().AsCached()
                .WithArguments(default(Bullet), _remoteConfigsData.BulletPoolSize, "Bullet", projectilesContainer);

            Container.Bind<ObjectPool<Laser>>().AsCached()
                .WithArguments(default(Laser), _remoteConfigsData.LaserPoolSize, "Laser", projectilesContainer);

            Container.Bind<ProjectileResourceManager>().AsSingle()
                .WithArguments(_bulletPrefabReference, _laserPrefabReference);
        }

        private void BindSpawners()
        {
            Container.Bind<GeneratorEnemies>().To<AsteroidSpawner>().AsCached().WithArguments(_remoteConfigsData);
            Container.Bind<GeneratorEnemies>().To<UfoSpawner>().AsCached().WithArguments(_remoteConfigsData);

            Container.BindInterfacesAndSelfTo<EnemyDeathTracker>().AsSingle();
            Container.Bind<EnemySpawnController>().AsSingle();
        }
    }
}