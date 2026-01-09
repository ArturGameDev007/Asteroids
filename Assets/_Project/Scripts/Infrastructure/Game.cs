using Scripts.Enemies;
using Scripts.GameScreen;
using Scripts.Player;
using Scripts.Player.Weapons;
using Scripts.UI.GameScreen;
using UnityEngine.SceneManagement;

namespace Scripts.Infrastructure
{
    public class Game
    {
        private readonly ObjectPool _objectPool;
        private readonly GeneratorEnemies _generatorEnemies;
        private readonly Character _player;
        private readonly InputForShoot _shoot;
        private readonly WindowEndGame _windowEndGame;
        private ScoreData _scoreData;

        public Game(ObjectPool objectPool, GeneratorEnemies generatorEnemies, Character player, InputForShoot shoot, WindowEndGame windowEndGame, ScoreData scoreData)
        {
            _objectPool = objectPool;
            _generatorEnemies = generatorEnemies;
            _player = player;
            _shoot = shoot;
            _windowEndGame = windowEndGame;
            _scoreData = scoreData;
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
            _windowEndGame.OnRestartClick += OnRestartButtonClick;
            _player.OnGameOver += OnGameOver;
        }

        private void Unsubscribe()
        {
            _windowEndGame.OnRestartClick -= OnRestartButtonClick;
            _player.OnGameOver -= OnGameOver;
        }

        private void OnGameOver()
        {
            _generatorEnemies.StopSpawning();
            _windowEndGame.OpenScreen();

            int finalScore = _scoreData.GetScore;

            _shoot.enabled = false;
        }

        private void OnRestartButtonClick()
        {
            _windowEndGame.CloseScreen();

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
