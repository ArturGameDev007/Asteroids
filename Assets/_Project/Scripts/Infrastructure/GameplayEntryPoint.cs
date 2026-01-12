using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
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

        private Game _game;
        private ScoreData _scoreData;

        private void Awake()
        {
            //Character character = Instantiate(_player);

            _scoreData = new ScoreData();

            _viewScore.Create(_scoreData);
            _generatorEnemies.Initialize(_scoreData);

            _game = new Game(_objectPool, _generatorEnemies, _player, _shoot, _windowEndGame, _scoreData);
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

