using Assets._Project.Scripts.UI.GameScreen;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Weapons;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private InputForShoot _shoot;
    [SerializeField] private WindowEndGame _windowEndGame;
    [SerializeField] private ScoreData _scoreData;

    private void Start()
    {
        _scoreData.Reset();
        _windowEndGame.OnRestartClick += OnRestartButtonClick;
        _player.OnGameOver += OnGameOver;
    }

    private void OnDestroy()
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
        _scoreData.Reset();

        _shoot.enabled = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
