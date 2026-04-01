using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Services.Ads;
using _Project.Scripts.Services.Analytics;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class Game
    {
        private readonly PlayerResourceManager _playerResourceManager;
        private readonly CoordinateResourceManager _coordinateResourceManager;
        private readonly EnemyResourceManager _enemyResourceManager;
        private readonly ProjectileResourceManager _projectileResourceManager;
        private readonly IGameFactory _gameFactory;
        private readonly LoseResourceManager _loseResourceManager;
        private readonly EnemySpawnController _enemySpawnController;
        private readonly Character _player;
        private readonly IControllable _controller;
        private readonly IShootable _shoot;
        private readonly IWeaponShooter _weaponShooter;
        private readonly RestartGame _restartGame;
        private readonly ILoseModel _scoreData;
        private readonly EnemyDeathTracker _deathTracker;
        private readonly IAnalyticsService _analyticsService;
        private readonly IAdsService _adsService;

        private LosePresenter _losePresenter;

        private bool _isInitialized;
        private bool _isGameOver;
        private bool _canRevive;

        public Game(PlayerResourceManager playerResourceManager, CoordinateResourceManager coordinateResourceManager,
            EnemyResourceManager enemyResourceManager,
            ProjectileResourceManager projectileResourceManager,
            IGameFactory gameFactory, LoseResourceManager loseResourceManager,
            EnemySpawnController enemySpawnController,
            Character player, IControllable controller, IShootable shoot, IWeaponShooter weaponShooter,
            RestartGame restartGame, ILoseModel scoreData, EnemyDeathTracker deathTracker,
            IAnalyticsService analyticsService, IAdsService adsService)
        {
            _playerResourceManager = playerResourceManager;
            _coordinateResourceManager = coordinateResourceManager;
            _enemyResourceManager = enemyResourceManager;
            _projectileResourceManager = projectileResourceManager;
            _gameFactory = gameFactory;
            _loseResourceManager = loseResourceManager;
            _enemySpawnController = enemySpawnController;
            _player = player;
            _controller = controller;
            _shoot = shoot;
            _weaponShooter = weaponShooter;
            _restartGame = restartGame;
            _scoreData = scoreData;
            _deathTracker = deathTracker;
            _analyticsService = analyticsService;
            _adsService = adsService;
        }

        public async UniTask InitializeAsync()
        {
            var loadPlayerAsync = _playerResourceManager.LoadAsync();
            var loadCoordinateAsync = _coordinateResourceManager.LoadAsyncPerformanceShip();
            var loadEnemiesAsync = _enemyResourceManager.LoadEnemiesAsync();
            var loadShotsAsync = _projectileResourceManager.LoadShotsAsync();

            await UniTask.WhenAll(loadPlayerAsync, loadCoordinateAsync, loadEnemiesAsync, loadShotsAsync);

            _scoreData?.Reset();
            
            _player.OnGameOver += OnGameOver;
            _adsService.OnAdsFinished += OnAdsClosed;

            _canRevive = true;
            _isGameOver = false;

            ContinueGame();

            _isInitialized = true;
        }

        public void Tick()
        {
            if (!_isInitialized)
                return;

            _enemySpawnController.Process(Time.deltaTime);
        }

        public void Dispose()
        {
            _player.OnGameOver -= OnGameOver;
            _adsService.OnAdsFinished -= OnAdsClosed;
            
            if (_losePresenter != null)
            {
                _losePresenter.OnRestartClick -= OnRestartButtonClick;
                _losePresenter?.Dispose();
            }

            _playerResourceManager.Unload();
            _coordinateResourceManager.Unload();
            _enemyResourceManager.UnloadEnemies();
            _projectileResourceManager.UnloadShots();
            _loseResourceManager.Unload();

            _player?.Dispose();
            _deathTracker?.Dispose();
        }

        private void OnGameOver()
        {
            if (_isGameOver)
                return;

            StopGameplay();

            if (_canRevive)
            {
                _canRevive = false;
                _adsService.ShowAdsReward();
            }
            else
            {
                _isGameOver = true;
                _adsService.ShowAdsInterstitial();

                ShowLoseScreen();
            }
        }

        private void ContinueGame()
        {
            _player.Revive();
            _controller?.ResetState();
            _controller?.EnableControl();
            _shoot.EnableControl();
            _enemySpawnController.StartAll();
        }

        private void StopGameplay()
        {
            _controller?.StopPhysics();
            _controller?.DisableControl();

            _shoot.DisableControl();
            _shoot.StopAllShoots();

            _enemySpawnController.StopAndClearAll();

            _analyticsService.LogGameEnd(_weaponShooter.ShotsCount, _weaponShooter.LaserUsed, _deathTracker.KillCount);
        }

        private async void ShowLoseScreen()
        {
            var prefab = await _loseResourceManager.LoseScreenLoadAsync();

            _losePresenter = _gameFactory.CreateLoseScreen(prefab);

            if (_losePresenter != null)
            {
                _losePresenter.OnRestartClick += OnRestartButtonClick;
                _losePresenter.Open(_scoreData.Score);
            }
        }

        private void OnAdsClosed()
        {
            if (_isGameOver)
                return;
            
            ContinueGame();
        }

        private void OnRestartButtonClick()
        {
            _losePresenter.Close();
            _restartGame.RestartScene();
        }
    }
}