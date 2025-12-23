using Assets._Project.Scripts.UI.GameScreen;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Weapons;
using Assets.Scripts.UI.GameScreen;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private InputForShoot _shoot;
    [SerializeField] private EndGameScreen _endGameScreen;
    [SerializeField] private ScoreData _scoreData;

    [field: SerializeField] public bool IsGameOver = false;

    private void Start()
    {
        _scoreData.Reset();
    }

    private void OnEnable()
    {
        _endGameScreen.OnRestartButtonClick += OnRestartButtonClick;
        _player.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _endGameScreen.OnRestartButtonClick -= OnRestartButtonClick;
        _player.OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        _endGameScreen.Open();
        _scoreData.GetScore();

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
        _scoreData.Reset();

        _shoot.enabled = true;
        IsGameOver = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
