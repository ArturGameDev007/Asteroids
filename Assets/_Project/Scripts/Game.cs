using Assets._Project.Scripts.UI.GameScreen;
using Assets.Scripts.EnemySpace;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Weapons;
using UnityEngine.SceneManagement;

public class Game
{
    private ObjectPool _objectPool;
    private GeneratorEnemies _generatorEnemies;
    private Player _player;
    private InputForShoot _shoot;
    private WindowEndGame _windowEndGame;
    private ScoreData _scoreData;

    public Game(ObjectPool objectPool, GeneratorEnemies generatorEnemies, Player player, InputForShoot shoot, WindowEndGame windowEndGame, ScoreData scoreData)
    {
        _objectPool = objectPool;
        _generatorEnemies = generatorEnemies;
        _player = player;
        _shoot = shoot;
        _windowEndGame = windowEndGame;
        _scoreData = scoreData;
    }

    public void Initialize()
    {
        _scoreData.Reset();
        _objectPool.Initialize();

        Subscription();

        _generatorEnemies.StartSpawning();

        StartGame();
    }

    public void Dispose()
    {
        Unsubscription();
        _generatorEnemies.StopSpawning();
    }

    public void Subscription()
    {
        _windowEndGame.OnRestartClick += OnRestartButtonClick;
        _player.OnGameOver += OnGameOver;
    }


    public void Unsubscription()
    {
        _windowEndGame.OnRestartClick -= OnRestartButtonClick;
        _player.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        _windowEndGame.OpenScreen();
        _scoreData.GetScore();

        _shoot.enabled = false;
    }

    private void OnRestartButtonClick()
    {
        _windowEndGame.CloseScreen();
        StartGame();
    }

    private void StartGame()
    {
        _player.Reset();
        _shoot.enabled = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
