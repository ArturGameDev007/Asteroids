using _Project.Scripts.Enemies;
using _Project.Scripts.Infrastructure;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Services.AsyncLoader;
using _Project.Scripts.Services.Audio.Background;
using _Project.Scripts.Services.Audio.SFX;
using _Project.Scripts.Services.Effects;
using _Project.Scripts.UI.Background;
using _Project.Scripts.UI.BackToStartMenu;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Installers.Gameplay
{
    public class GameplaySceneInstaller : MonoInstaller
    {
        [SerializeField] private Camera _mainCamera;
        
        [Header("Prefabs UI")]
        [SerializeField] private AssetReference _endGameScreenPrefabReference;
        [SerializeField] private AssetReference _shipPerformancePrefabReference;
        [SerializeField] private AssetReference _backToStartMenu;

        [Header("Container For Objects Pools")]
        [SerializeField] private Transform _containerForPools;

        [Header("Load Async: Player & Shots")]
        [SerializeField] private AssetReference _shipPrefabReference;
        [SerializeField] private AssetReference _bulletPrefabReference;
        [SerializeField] private AssetReference _laserPrefabReference;

        [Header("Load Prefabs Async")]
        [SerializeField] private AssetReference[] _enemyRefences;

        [Header("Effects")]
        [SerializeField] private AssetReference _effectExplosionKill;
        [SerializeField] private AssetReference _effectPlayShoot;
        
        [Header("Audio SFX")]
        [SerializeField] private AssetReference _explosionClip;
        [SerializeField] private AssetReference _shootClip;
        [SerializeField] private AssetReference _backgroundMusic;
        
        public override void InstallBindings()
        {
            Container.Bind<IResourceLoader>().To<AddressableResourceLoader>().AsSingle();

            Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IWeaponShooter>().To<WeaponShooter>().AsSingle();
            Container.Bind<IEnemyDeathListener>().To<EnemyManager>().AsSingle();
            Container.Bind<RestartGame>().AsSingle();
            Container.Bind<ILoseModel>().To<ScoreData>().AsSingle();

            Container.Bind<IMusicBackgroundResourceManager>().To<MusicBackgroundResourceManager>().AsSingle().WithArguments(_backgroundMusic);
            Container.Bind<LoseResourceManager>().AsSingle().WithArguments(_endGameScreenPrefabReference);
            
            Container.Bind<IGameplayView>().To<GameplayView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<GameplayPresenter>().AsSingle();
            Container.Bind<IBackToMenuManager>().To<PanelBackToMenuResourceManager>().AsSingle().WithArguments(_backToStartMenu);
            
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
                .WithArguments(default(Enemy), "Asteroid", enemiesContainer);
            
            Container.Bind<ObjectPool<Enemy>>().WithId("UfoPool").AsCached()
                .WithArguments(default(Enemy), "UFO", enemiesContainer);

            Container.Bind<EnemyResourceManager>().AsSingle().WithArguments(_enemyRefences);

            Transform projectilesContainer = new GameObject("Weapons_Category").transform;
            projectilesContainer.parent = _containerForPools;

            Container.Bind<ObjectPool<Bullet>>().AsCached()
                .WithArguments(default(Bullet), "Bullet", projectilesContainer);

            Container.Bind<ObjectPool<Laser>>().AsCached()
                .WithArguments(default(Laser),  "Laser", projectilesContainer);
            
            Container.Bind<ProjectileResourceManager>().AsSingle()
                .WithArguments(_bulletPrefabReference, _laserPrefabReference);

            BindPoolEffects(enemiesContainer, projectilesContainer);
            BindPoolSFX(projectilesContainer, enemiesContainer);
        }

        private void BindPoolEffects(Transform projectilesContainer, Transform enemiesContainer)
        {
            Container.Bind<IEffectResourceManager>().To<EffectResourceManager>().AsSingle()
                .WithArguments(_effectExplosionKill, _effectPlayShoot);
            
            Container.Bind<ObjectPool<ExplosionEffect>>().AsCached()
                .WithArguments(default(ExplosionEffect), "ExplosionKills", enemiesContainer);
            
            Container.Bind<ObjectPool<ShootEffect>>().AsCached()
                .WithArguments(default(ShootEffect), "Shoots", projectilesContainer);
            
            Container.Bind<IEffectService>().To<EffectSystem>().AsSingle();
        }

        private void BindPoolSFX(Transform projectilesContainer, Transform enemiesContainer)
        {
            Container.Bind<IAudioResourceManager>().To<AudioResourceManager>().AsSingle().WithArguments(_explosionClip, _shootClip);

            Container.Bind<ObjectPool<ExplosionClip>>().AsCached()
                .WithArguments(default(ExplosionClip), "ExplosionSFX", enemiesContainer);
            
            Container.Bind<ObjectPool<ShootClip>>().AsCached()
                .WithArguments(default(ShootClip), "ShootSFX", projectilesContainer);
            
            Container.Bind<IAudioSystem>().To<AudioSystem>().AsSingle();
        }

        private void BindSpawners()
        {
            Container.Bind<GeneratorEnemies>().To<AsteroidSpawner>().AsCached();
            Container.Bind<GeneratorEnemies>().To<UfoSpawner>().AsCached();

            Container.BindInterfacesAndSelfTo<EnemyDeathTracker>().AsSingle();
            Container.Bind<EnemySpawnController>().AsSingle();
        }
    }
}