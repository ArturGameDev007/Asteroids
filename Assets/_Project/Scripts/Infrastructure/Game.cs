using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Infrastructure
{
    public class Game
    {
        private readonly ObjectPool _objectPool;
        private readonly GeneratorEnemies _generatorEnemies;
        private readonly Character _player;
        private readonly PlayerController _controller;
        private readonly InputForShoot _shoot;
        private readonly LoseViewModel _loseViewModel;
        private readonly RestartGame _restartGame;
        private readonly Enemy _enemy;
        private ScoreData _scoreData;

        public Game(ObjectPool objectPool, GeneratorEnemies generatorEnemies, Character player, PlayerController controller, InputForShoot shoot, LoseViewModel loseViewModel, RestartGame restartGame, ScoreData scoreData, Enemy enemy)
        {
            _objectPool = objectPool;
            _generatorEnemies = generatorEnemies;
            _player = player;
            _controller = controller;
            _shoot = shoot;
            _loseViewModel = loseViewModel;
            _restartGame = restartGame;
            _scoreData = scoreData;
            _enemy = enemy;
        }

        public void Initialize()
        {
            _scoreData?.Reset();
            _player.ClearState();
            _objectPool.Initialize();
            Subscribe();

            _shoot.enabled = true;
            _generatorEnemies.StartSpawning();
        }

        public void Dispose()
        {
            Unsubscribe();
        }

        private void Subscribe()
        {
            _loseViewModel.OnRestartClick += OnRestartButtonClick;
            _player.OnGameOver += OnGameOver;
        }

        private void Unsubscribe()
        {
            _loseViewModel.OnRestartClick -= OnRestartButtonClick;
            _player.OnGameOver -= OnGameOver;
        }

        private void OnGameOver()
        {
            _generatorEnemies.StopSpawning();
            _loseViewModel.Open();
            
            int finalScore = _scoreData.GetScore;
            
            _controller.StopPhysics();
            _controller.enabled = false;
            
            _shoot.enabled = false;
        }

        private void OnRestartButtonClick()
        {
            _loseViewModel.Close();
            _restartGame.RestartScene();
        }
    }
}
