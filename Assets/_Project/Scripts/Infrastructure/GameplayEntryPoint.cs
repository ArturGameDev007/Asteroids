using _Project.Scripts.Enemies;
using _Project.Scripts.Player;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using _Project.Scripts.UI.PerformanceShip;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Infrastructure
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [Header("Prefabs UI")] [SerializeField]
        private GameObject _background;

        [SerializeField] private GameObject _performanceShip;

        [Header("Systems")] [SerializeField] private ObjectPool _objectPool;
        [SerializeField] private GeneratorEnemies _generatorEnemies;

        [Header("Prefabs")] [SerializeField] private Character _player;
        [SerializeField] private InputForShoot _shoot;
        [SerializeField] private PlayerController _controller;

        [Header("UI & Data")] [SerializeField] private LoseViewModel _loseViewModel;
        [SerializeField] private ViewScore _viewScore;
        [SerializeField] private EnemyDeathTracker _deathTracker;

        private CoordinateDisplay _coordinateDisplay;
        private ViewCurrentAmountLaser _viewCurrentAmountLaser;
        private Game _game;
        private Camera _mainCamera;
        private RestartGame _restartGame;
        private ScoreData _scoreData;
        private Enemy _enemy;

        private void Awake()
        {
            _mainCamera = Camera.main;

            CreateBackground(_mainCamera);
            CreatePerformanceShip();

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

            GameObject background = Instantiate(_background);
            background.name = "UI - Background";

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

            if (performanceShip.TryGetComponent(out CoordinateDisplay coordinateDisplay))
            {
                Rigidbody2D player = _player.GetComponent<Rigidbody2D>();

                coordinateDisplay?.Initialize(_controller, player);
            }

            if (performanceShip.TryGetComponent(out ViewCurrentAmountLaser laser))
            {
                laser?.Initialize();

                GenerateLaser laserLogic = performanceShip.GetComponent<GenerateLaser>();

                if (laserLogic != null && _shoot != null)
                {
                    _shoot.Initialize(laserLogic);
                }
                else
                {
                    Debug.LogError("Не удалось связать Лазер с Системой Стрельбы!");
                }
            }
        }
    }
}