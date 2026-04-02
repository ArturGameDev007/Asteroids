using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI.GameScreen
{
    [RequireComponent(typeof(Canvas))]
    public class LoseView : MonoBehaviour, ILoseView
    {
        public event Action OnRestartRequested;

        [Header("Texts Scores")]
        [SerializeField] private TextMeshProUGUI _textScore;
        [SerializeField] private TextMeshProUGUI _textBestResult;

        [field: Header("Button")]
        [field: SerializeField] public Button RestartButton { get; private set; }

        private Canvas _canvas;

        [Inject]
        public void Construct()
        {
            _canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            RestartButton?.onClick.AddListener(OnRestart);
        }

        private void OnDestroy()
        {
            RestartButton.onClick.RemoveListener(OnRestart);
        }

        public void SetScore(int score, int  bestScore)
        {
            _textScore.text = $"Score: {score.ToString()}";
            _textBestResult.text = $"Best Result: {bestScore.ToString()}";
        }

        public void ShowPanel()
        {
            _canvas.gameObject.SetActive(true);
            RestartButton.interactable = true;
        }

        public void HidePanel()
        {
            _canvas.gameObject.SetActive(false);
            RestartButton.interactable = false;
        }

        private void OnRestart()
        {
            OnRestartRequested?.Invoke();
        }
    }
}