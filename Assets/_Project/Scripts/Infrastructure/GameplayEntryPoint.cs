using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Infrastructure
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [Header("Prefabs")] 
        
        [Header("Systems")] 
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private GeneratorEnemies _generatorEnemies;

        [Header("Prefabs")] 
        [SerializeField] private Character _player;
        [SerializeField] private InputForShoot _shoot;
        [SerializeField] private PlayerController _controller;

        [Header("UI & Data")] 
        [SerializeField] private LoseViewModel _loseViewModel;
        [SerializeField] private ViewScore _viewScore;

        private Game _game;
        private RestartGame _restartGame;
        private ScoreData _scoreData;
        
        private void Awake()
        {
            _restartGame = new RestartGame();
            _scoreData = new ScoreData();

            _viewScore.Create(_scoreData);
            _generatorEnemies.Initialize(_scoreData);

            _game = new Game(_objectPool, _generatorEnemies, _player, _controller, _shoot, _loseViewModel, _restartGame,
                _scoreData);
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