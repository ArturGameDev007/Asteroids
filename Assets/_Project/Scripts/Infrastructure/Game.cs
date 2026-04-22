using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Services.Analytics;
using _Project.Scripts.Services.RemoteConfigs;
using _Project.Scripts.UI.GameScreen;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class Game
    {
        private readonly IRemoteConfigs _remoteConfigs;
        private readonly GameLoader _gameLoader;
        private readonly GameplayController _gameplayController;
        private readonly LoseManager _loseManager;
        private readonly Character _player;
        private readonly IWeaponShooter _weaponShooter;
        private readonly ILoseModel _scoreData;
        private readonly EnemyDeathTracker _deathTracker;
        private readonly IAnalyticsService _analyticsService;

        private LosePresenter _losePresenter;

        private bool _isInitialized;

        public Game(IRemoteConfigs remoteConfigs, GameLoader gameLoader, GameplayController gameplayController,
            LoseManager loseManager,
            Character player, IWeaponShooter weaponShooter,
            ILoseModel scoreData, EnemyDeathTracker deathTracker,
            IAnalyticsService analyticsService)
        {
            _remoteConfigs = remoteConfigs;
            _gameLoader = gameLoader;
            _gameplayController = gameplayController;
            _loseManager = loseManager;
            _player = player;
            _weaponShooter = weaponShooter;
            _scoreData = scoreData;
            _deathTracker = deathTracker;
            _analyticsService = analyticsService;
        }

        public async UniTask InitializeAsync()
        {
            await UniTask.WhenAll(_gameLoader.LoadAllAsync(),
                _remoteConfigs.Initialize());
            
            _scoreData?.Reset();

            _player.OnGameOver += OnGameOver;
            _loseManager.OnReviveRequested += OnRevive;

            _gameplayController.ContinueGame();

            _isInitialized = true;
        }
        
        public void Tick()
        {
            if (!_isInitialized || _loseManager.IsGameOver)
                return;

            _gameplayController.Update(Time.deltaTime);
        }

        public void Dispose()
        {
            _player.OnGameOver -= OnGameOver;
            _loseManager.OnReviveRequested -= OnRevive;

            _gameLoader.UnloadAll();
            _loseManager.Unload();

            _player?.Dispose();
            _deathTracker?.Dispose();
        }

        private void OnGameOver()
        {
            if (_loseManager.IsGameOver)
                return;

            _gameplayController.StopGameplay();
            _analyticsService.LogGameEnd(_weaponShooter.ShotsCount, _weaponShooter.LaserUsed, _deathTracker.KillCount);
            _loseManager.HandleGameOver().Forget();
        }

        private void OnRevive()
        {
            _gameplayController.ContinueGame();
        }
    }
}