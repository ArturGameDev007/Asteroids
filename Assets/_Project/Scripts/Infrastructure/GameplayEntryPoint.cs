using System.Collections.Generic;
using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Infrastructure
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [Header("Prefabs UI")]
        [SerializeField] private GameObject _background;
        [SerializeField] private GameObject _performanceShip;
        [SerializeField] private GameObject _endGameScreen;

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
        [SerializeField] private EnemyDeathTracker _deathTracker;

        private CoordinateDisplay _coordinateDisplay;
        private ViewCurrentAmountLaser _viewCurrentAmountLaser;
        private Game _game;
        private Camera _mainCamera;
        private HierarchyScanner _hierarchyScanner;
        private RestartGame _restartGame;
        private ScoreData _scoreData;
        private Enemy _enemy;

        private void Awake()
        {
            _hierarchyScanner = new HierarchyScanner();
            _mainCamera = Camera.main;
            
            CreateBackground(_mainCamera);
            CreatePerformanceShip();
            CreateEndGameScreen();

            _restartGame = new RestartGame();
            _scoreData = new ScoreData();

            _viewScore.Create(_scoreData);
            _generatorEnemies.Initialize(_scoreData);
            _deathTracker.Initialize(_scoreData);

            _game = new Game(_objectPool, _generatorEnemies, _player, _controller, _shoot, _loseViewModel, _restartGame,
                _enemy, _scoreData);
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

        private void CreateBackground(Camera main)
        {
            int orderInLayer = -5;

            GameObject separator = new GameObject($"{new string('-', 10)} UI {new string('-', 10)}");
            separator.transform.SetSiblingIndex(2);

            GameObject background = Instantiate(_background);
            background.name = "UI - Background";
            background.transform.SetSiblingIndex(3);

            if (background.TryGetComponent(out Canvas canvas))
            {
                canvas.worldCamera = main;
                canvas.sortingOrder = orderInLayer;
            }
        }

        private void CreatePerformanceShip()
        {
            GameObject performanceShip = Instantiate(_performanceShip);
            performanceShip.name = "UI - Performance Ship";
            performanceShip.transform.SetSiblingIndex(4);
            
            GameObject separator = new GameObject(new string('-', 20));
            separator.transform.SetSiblingIndex(6);
            
            if (performanceShip.TryGetComponent(out CoordinateDisplay coordinateDisplay))
            {
                Rigidbody2D player = _player.GetComponent<Rigidbody2D>();

                coordinateDisplay?.Initialize(_controller, player);
            }

            if (_hierarchyScanner.TryGetInStack(performanceShip.transform, out ViewCurrentAmountLaser viewLaser))
                viewLaser?.Initialize();

            if (_hierarchyScanner.TryGetInStack(performanceShip.transform, out GenerateLaser laserLogic))
                _shoot?.Initialize(laserLogic);
        }

        private void CreateEndGameScreen()
        {
            GameObject gameScreen = Instantiate(_endGameScreen);
            gameScreen.name = "UI - EndGameScreen";
            gameScreen.transform.SetSiblingIndex(5);
            
            if (_hierarchyScanner.TryGetInStack(gameScreen.transform, out LoseViewModel loseViewModel))
                _loseViewModel = loseViewModel;
            
            if (_hierarchyScanner.TryGetInStack(gameScreen.transform, out ViewScore viewScore))
                _viewScore = viewScore;
        }
    }
}