using Assets.Scripts.Player.Weapons;
using Assets.Scripts.UI.GameScreen;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private InputForShoot _shoot;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private ScoreManager _manager;

    public bool IsGameOver = false;

    private void OnEnable()
    {
        _endGameScreen.RestartButtonClick += OnRestartButtonClick;
        _player.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _endGameScreen.RestartButtonClick -= OnRestartButtonClick;
        _player.GameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        _endGameScreen.Open();
        _manager.GetScore();
        IsGameOver = true;
        _shoot.enabled = false;
    }

    private void OnRestartButtonClick()
    {
        _endGameScreen.Close();
        StartGame();
    }

    private void StartGame()
    {
        _player.Reset();
        _shoot.enabled = true;
        IsGameOver = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
