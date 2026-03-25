using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Services.Analytics;
using _Project.Scripts.Services.AsyncLoader;
using _Project.Scripts.UI.GameScreen;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace _Project.Scripts.Infrastructure
{
    public class Game
    {
        private readonly IGameFactory _gameFactory;
        private readonly EnemySpawnController _enemySpawnController;
        private readonly Character _player;
        private readonly IControllable _controller;
        private readonly IShootable _shoot;
        private readonly WeaponShooter _weaponShooter;
        private readonly RestartGame _restartGame;
        private readonly ILoseModel _scoreData;
        private readonly EnemyDeathTracker _deathTracker;
        private readonly IAnalyticsService _analyticsService;

        private LosePresenter _losePresenter;

        private bool _isGameOver;

        public Game(IGameFactory gameFactory, EnemySpawnController enemySpawnController,
            Character player, IControllable controller, IShootable shoot, WeaponShooter weaponShooter,
            RestartGame restartGame, ILoseModel scoreData, EnemyDeathTracker deathTracker,
            IAnalyticsService analyticsService)
        {
            _gameFactory = gameFactory;
            _enemySpawnController = enemySpawnController;
            _player = player;
            _controller = controller;
            _shoot = shoot;
            _weaponShooter = weaponShooter;
            _restartGame = restartGame;
            _scoreData = scoreData;
            _deathTracker = deathTracker;
            _analyticsService = analyticsService;
        }

        public void Initialize()
        {
            _scoreData?.Reset();
            _player.ClearState();
            _player.OnGameOver += OnGameOver;

            _controller?.EnableControl();
            _shoot.EnableControl();

            _enemySpawnController.StartAll();
        }

        public void Tick()
        {
            _enemySpawnController.Process(Time.deltaTime);
        }

        public void Dispose()
        {
            _player.OnGameOver -= OnGameOver;

            if (_losePresenter != null)
            {
                _losePresenter.OnRestartClick -= OnRestartButtonClick;
                _losePresenter?.Dispose();
            }

            _player?.Dispose();
            _deathTracker?.Dispose();
        }

        private async void OnGameOver()
        {
            if (_isGameOver)
                return;
            
            _isGameOver = true;
            
            _controller?.StopPhysics();
            _controller?.DisableControl();

            _shoot.DisableControl();
            _shoot.StopAllShoots();

            _enemySpawnController.StopAndClearAll();

            _analyticsService.LogGameEnd(_weaponShooter.ShotsCount, _weaponShooter.LaserUsed, _deathTracker.KillCount);

            _losePresenter = await _gameFactory.CreateLoseScreenAsync();

            if (_losePresenter != null)
            {
                _losePresenter.OnRestartClick += OnRestartButtonClick;
                _losePresenter.Open(_scoreData.Score);
            }
        }

        private void OnRestartButtonClick()
        {
            _losePresenter.Close();
            _restartGame.RestartScene();
        }
    }
}