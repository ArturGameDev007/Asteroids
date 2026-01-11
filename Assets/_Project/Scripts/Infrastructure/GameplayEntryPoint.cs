using Assets._Project.Scripts.UI.GameScreen;
using Scripts.Enemies;
using Scripts.GameScreen;
using Scripts.Player;
using Scripts.Player.Weapons;
using Scripts.UI.GameScreen;
using UnityEngine;

namespace Scripts.Infrastructure
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [Header("Systems")]
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private GeneratorEnemies _generatorEnemies;

        [Header("Prefabs")]
        [SerializeField] private Character _player;
        [SerializeField] private InputForShoot _shoot;

        [Header("UI & Data")]
        [SerializeField] private WindowEndGame _windowEndGame;
        [SerializeField] private ViewScore _viewScore;
        [SerializeField] private RestartButton _restartButton;

        private Game _game;
        private ScoreData _scoreData;

        private void Awake()
        {
            //Character character = Instantiate(_player);

            _scoreData = new ScoreData();

            _viewScore.Create(_scoreData);
            _generatorEnemies.Initialize(_scoreData);

            _game = new Game(_objectPool, _generatorEnemies, _player, _shoot, _windowEndGame, _restartButton, _scoreData);
        }

        private void Start()
        {
            _game.Initialize();
        }

        private void OnDestroy()
        {
            _game.Dispose();
            _objectPool.ClearPool();
        }
    }
}

