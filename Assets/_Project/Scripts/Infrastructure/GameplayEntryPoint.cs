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
        [SerializeField] private GameFactory _gameFactory;
        
        [Header("Prefabs UI")]
        [SerializeField] private GameObject _background;
        [SerializeField] private GameObject _performanceShip;
        [SerializeField] private GameObject _endGameScreen;

        [Header("Prefab Player")]
        [SerializeField] private GameObject _ship;

        [Header("Systems Pool")]
        [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private GeneratorEnemies _generatorEnemies;

        [Header("UI & Data")]
        [SerializeField] private LoseViewModel _loseViewModel;
        [SerializeField] private ViewScore _viewScore;
        [SerializeField] private EnemyDeathTracker _deathTracker;

        private Character _player;
        private InputForShoot _shoot;
        private PlayerController _controller;

        private CoordinateDisplay _coordinateDisplay;
        private ViewCurrentAmountLaser _viewCurrentAmountLaser;
        private Game _game;
        private Camera _mainCamera;
        private HierarchyScanner _hierarchyScanner;
        private RestartGame _restartGame;
        private ScoreData _scoreData;

        private void Awake()
        {
            _gameFactory = GetComponent<GameFactory>();

            _hierarchyScanner = new HierarchyScanner();
            _mainCamera = Camera.main;

            _gameFactory.CreateBackground(_background, _mainCamera);
            _gameFactory.CreatePlayer(_ship, out _player, out _controller, out _shoot);
            _gameFactory.CreatePerformanceShip(_performanceShip, _player, _controller, _shoot, _coordinateDisplay, _viewCurrentAmountLaser, _hierarchyScanner);
            _gameFactory.CreateEndGameScreen(_endGameScreen, _hierarchyScanner, out _loseViewModel, out _viewScore);

            // CreateBackground(_mainCamera);
            // _player = CreatePlayer();
            // CreatePerformanceShip();
            // CreateEndGameScreen();

            _restartGame = new RestartGame();
            _scoreData = new ScoreData();

            _viewScore.Construct(_scoreData);
            _generatorEnemies.Initialize();
            _deathTracker.Initialize(_scoreData);

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

        // private void CreateBackground(Camera main)
        // {
        //     int orderInLayer = -5;
        //
        //     int indexSeparator = 3;
        //     int count = 10;
        //
        //     char dash = '-';
        //
        //     CreateSeparator(new string(dash, count), indexSeparator);
        //
        //     GameObject background = Instantiate(_background);
        //     background.name = "UI - Background";
        //     background.transform.SetSiblingIndex(4);
        //
        //     if (background.TryGetComponent(out Canvas canvas))
        //     {
        //         canvas.worldCamera = main;
        //         canvas.sortingOrder = orderInLayer;
        //     }
        // }
        //
        // private Character CreatePlayer()
        // {
        //     GameObject playerObject = Instantiate(_ship, Vector2.zero, Quaternion.identity);
        //     playerObject.name = "Ship_Player";
        //     playerObject.transform.SetSiblingIndex(2);
        //
        //     if (!playerObject.TryGetComponent(out Character characterComponent))
        //     {
        //         return null;
        //     }
        //
        //     if (!playerObject.TryGetComponent(out _controller))
        //     {
        //         Debug.LogError("Компонент PlayerController не найден на префабе _shipPrefab!");
        //     }
        //
        //     if (!playerObject.TryGetComponent(out _shoot))
        //     {
        //         Debug.LogError("Компонент InputForShoot не найден на префабе _ship!");
        //     }
        //
        //     return characterComponent;
        // }
        //
        // private void CreatePerformanceShip()
        // {
        //     GameObject performanceShip = Instantiate(_performanceShip);
        //     performanceShip.name = "UI - Performance Ship";
        //     performanceShip.transform.SetSiblingIndex(5);
        //
        //     if (performanceShip.TryGetComponent(out CoordinateDisplay coordinateDisplay))
        //     {
        //         Rigidbody2D player = _player?.GetComponent<Rigidbody2D>();
        //
        //         coordinateDisplay?.Initialize(_controller, player);
        //     }
        //
        //     if (_hierarchyScanner.TryGetInStack(performanceShip.transform, out ViewCurrentAmountLaser viewLaser))
        //         viewLaser?.Initialize();
        //
        //     WeaponShooter shooter = new WeaponShooter();
        //
        //     if (_hierarchyScanner.TryGetInStack(performanceShip.transform, out GenerateLaser laserLogic))
        //         _shoot?.Initialize(laserLogic, shooter);
        // }
        //
        // private void CreateEndGameScreen()
        // {
        //     GameObject gameScreen = Instantiate(_endGameScreen);
        //     gameScreen.name = "UI - EndGameScreen";
        //     gameScreen.transform.SetSiblingIndex(6);
        //
        //     int indexSeparator = 3;
        //     int count = 10;
        //
        //     char dash = '-';
        //
        //     CreateSeparator(new string(dash, count), indexSeparator);
        //
        //     if (_hierarchyScanner.TryGetInStack(gameScreen.transform, out LoseViewModel loseViewModel))
        //     {
        //         _loseViewModel = loseViewModel;
        //
        //         if (_hierarchyScanner.TryGetInStack(gameScreen.transform, out LoseView loseView))
        //         {
        //             if (_hierarchyScanner.TryGetInStack(loseView.transform, out Button button))
        //             {
        //                 loseView.Construct(button);
        //             }
        //
        //             _loseViewModel.Construct(loseView);
        //         }
        //     }
        //
        //     if (_hierarchyScanner.TryGetInStack(gameScreen.transform, out ViewScore viewScore))
        //         _viewScore = viewScore;
        // }
        //
        // private void CreateSeparator(string label, int index)
        // {
        //     var separator = new GameObject($"{label}");
        //     separator.transform.SetSiblingIndex(index);
        // }
    }
}