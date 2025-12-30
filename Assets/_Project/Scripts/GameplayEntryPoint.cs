using Scripts.EnemySpace;
using Scripts.GameScreen;
using Scripts.Player;
using Scripts.Player.Weapons;
using Scripts.UI.GameScreen;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private GeneratorEnemies _generatorEnemies;

    [Header("Player & Combat")]
    [SerializeField] private Player _player;
    [SerializeField] private InputForShoot _shoot;

    [Header("UI & Data")]
    [SerializeField] private WindowEndGame _windowEndGame;
    [SerializeField] private ScoreData _scoreData;

    private Game _game;

    private void Awake()
    {
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
