using System;
using _Project.Scripts.Services.Save;
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

        private ISaveService _saveService;
        private Canvas _canvas;

        [Inject]
        public void Construct(ISaveService saveService)
        {
            _canvas = GetComponent<Canvas>();
            _saveService = saveService;
        }

        private void Start()
        {
            RestartButton?.onClick.AddListener(OnRestart);
        }

        private void OnDestroy()
        {
            RestartButton.onClick.RemoveListener(OnRestart);
        }

        public void SetScore(int score)
        {
            _textScore.text = $"Score: {score.ToString()}";
            
            ShowBestScoreResult(score);
        }

        private void ShowBestScoreResult(int currentScore)
        {
            SaveData data = _saveService.Load();

            if (currentScore > data.BestResult)
            {
                data.BestResult = currentScore;
                _saveService.Save(data);
            }

            _textBestResult.text = $"Best Result: {data.BestResult.ToString()}";
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