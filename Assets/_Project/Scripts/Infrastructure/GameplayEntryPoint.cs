using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using _Project.Scripts.Utils;
using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [Header("Prefabs UI")]
        [SerializeField] private GameObject _background;
        [SerializeField] private PerformanceShipView _performanceShip;
        [SerializeField] private EndGameView _endGameScreen;

        [Header("Prefab Player")]
        [SerializeField] private Character _ship;

        [Header("Systems Pool")]
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private GeneratorEnemies _generatorEnemies;

        [Header("UI & Data")]
        [SerializeField] private EnemyDeathTracker _deathTracker;
        
        private IGameFactory _gameFactory;
        private IInstantiator _instantiator;
        
        private LoseViewModel _loseViewModel;
        private ViewScore _viewScore;

        private Character _player;
        private InputForShoot _shoot;
        private PlayerController _controller;

        private CoordinateDisplay _coordinateDisplay;
        private ViewCurrentAmountLaser _viewCurrentAmountLaser;
        private HierarchyScanner _hierarchyScanner;
        
        private Game _game;
        private Camera _mainCamera;
        private RestartGame _restartGame;
        private ScoreData _scoreData;

        private void Awake()
        {
            _instantiator = GetComponent<IInstantiator>();

            _gameFactory = new GameFactory(_instantiator);
            
            _hierarchyScanner = new HierarchyScanner();
            _mainCamera = Camera.main;
            
            CreateGameEntities();
            SetupDataAndUI();

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

        private void CreateGameEntities()
        {
            _gameFactory.CreateBackground(_background, _mainCamera);
            _gameFactory.CreatePlayer(_ship, out _player, out _controller, out _shoot);
            _gameFactory.CreatePerformanceShip(_performanceShip, _player, _controller, _shoot, _hierarchyScanner);
            _gameFactory.CreateEndGameScreen(_endGameScreen, _hierarchyScanner, out _loseViewModel, out _viewScore);
        }

        private void SetupDataAndUI()
        {
            _restartGame = new RestartGame();
            _scoreData = new ScoreData();

            _viewScore.Construct(_scoreData);
            _generatorEnemies.Initialize();
            _deathTracker.Initialize(_scoreData);
        }
    }
}