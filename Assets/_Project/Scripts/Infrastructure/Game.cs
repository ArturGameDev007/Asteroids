using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;

namespace _Project.Scripts.Infrastructure
{
    public class Game
    {
        private readonly IGameFactory _gameFactory;
        private readonly EndGameView _endGameScreen;
        private readonly GeneratorEnemies _generatorEnemies;
        private readonly Character _player;
        private readonly IControllable _controller;
        private readonly InputForShoot _shoot;
        private readonly RestartGame _restartGame;
        private readonly ScoreData _scoreData;
        
        private LosePresenter _losePresenter;

        public Game(IGameFactory gameFactory, EndGameView endGameScreen, GeneratorEnemies generatorEnemies,
            Character player, IControllable controller, InputForShoot shoot,
            RestartGame restartGame, ScoreData scoreData)
        {
            _gameFactory = gameFactory;
            _endGameScreen = endGameScreen;
            _generatorEnemies = generatorEnemies;
            _player = player;
            _controller = controller;
            _shoot = shoot;
            _restartGame = restartGame;
            _scoreData = scoreData;
        }

        public void Initialize()
        {
            _scoreData?.Reset();
            _player.ClearState();

            _player.OnGameOver += OnGameOver;

            _controller?.EnableControl();
            _shoot.EnableControl();
            _generatorEnemies.StartSpawning();
        }

        public void Dispose()
        {
            _player.OnGameOver -= OnGameOver;

            if (_losePresenter != null)
            {
                _losePresenter.OnRestartClick -= OnRestartButtonClick;
                _losePresenter?.Dispose();
            }
        }

        private void OnGameOver()
        {
            _controller?.StopPhysics();
            _controller?.DisableControl();

            _shoot.DisableControl();

            _generatorEnemies.StopSpawning();
            _generatorEnemies.StopAllEnemies();

            ShowLoseScreen();

            _losePresenter.OnRestartClick += OnRestartButtonClick;
            _losePresenter.Open(_scoreData.Score);
        }

        private void ShowLoseScreen()
        {
            _gameFactory.CreateEndGameScreen(_endGameScreen, _scoreData, out _losePresenter);
        }

        private void OnRestartButtonClick()
        {
            _losePresenter.Close();
            _restartGame.RestartScene();
        }
    }
}