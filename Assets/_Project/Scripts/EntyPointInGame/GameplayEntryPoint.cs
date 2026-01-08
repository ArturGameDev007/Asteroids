using Scripts.EnemySpace;
using Scripts.GameScreen;
using Scripts.Player;
using Scripts.Player.Weapons;
using Scripts.UI.GameScreen;
using UnityEngine;

namespace Scripts.EntryPointInGame
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [Header("Systems")]
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private GeneratorEnemies _generatorEnemies;

        [Header("Player & Combat")]
        [SerializeField] private Character _player;
        [SerializeField] private InputForShoot _shoot;

        [Header("UI & Data")]
        [SerializeField] private WindowEndGame _windowEndGame;
        [SerializeField] private ViewScore _viewScore;

        private Game _game;
        private ScoreData _scoreData;

        private void Awake()
        {
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
        }
    }
}

