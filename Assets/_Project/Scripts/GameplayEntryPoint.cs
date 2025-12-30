using Assets._Project.Scripts.UI.GameScreen;
using Assets.Scripts.EnemySpace;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Weapons;
using UnityEngine;

public class GameplayEntryPoint : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private GeneratorEnemies _generatorEnemies;

    [SerializeField] private Player _player;
    [SerializeField] private InputForShoot _shoot;

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
