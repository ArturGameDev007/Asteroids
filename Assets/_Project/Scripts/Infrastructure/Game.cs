using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;

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
        private readonly ScoreData _scoreData;

        public Game(ObjectPool objectPool, GeneratorEnemies generatorEnemies, Character player, PlayerController controller, InputForShoot shoot, LoseViewModel loseViewModel, RestartGame restartGame, ScoreData scoreData)
        {
            _objectPool = objectPool;
            _generatorEnemies = generatorEnemies;
            _player = player;
            _controller = controller;
            _shoot = shoot;
            _loseViewModel = loseViewModel;
            _restartGame = restartGame;
            _scoreData = scoreData;
        }

        public void Initialize()
        {
            _scoreData?.Reset();
            _objectPool.Initialize(_player);
            _player?.ClearState();
            
            Subscribe();

            _controller.EnableControl();
            _shoot.EnableControl();
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
            _controller.StopPhysics();
            _controller.DisableControl();
            _shoot.DisableControl();
            
            _generatorEnemies.StopSpawning();
            _generatorEnemies.StopAllEnemies();
            
            int finalScore = _scoreData.GetScore;
            
            _loseViewModel.Open();
        }

        private void OnRestartButtonClick()
        {
            _loseViewModel.Close();
            _restartGame.RestartScene();
        }
    }
}
