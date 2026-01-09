using Scripts.EnemySpace;
using Scripts.GameScreen;
using Scripts.Player;
using Scripts.Player.Weapons;
using Scripts.UI.GameScreen;
using UnityEngine.SceneManagement;

namespace Scripts.EntryPointInGame
{
    public class Game
    {
        private ObjectPool _objectPool;
        private GeneratorEnemies _generatorEnemies;
        private Character _player;
        private InputForShoot _shoot;
        private WindowEndGame _windowEndGame;
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
            _player.Reset();
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
            _windowEndGame.OpenScreen(_scoreData);

            _shoot.enabled = false;
        }

        private void OnRestartButtonClick()
        {
            _windowEndGame.CloseScreen();

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
