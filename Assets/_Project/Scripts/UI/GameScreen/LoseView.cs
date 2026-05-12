using System;
using _Project.Scripts.UI.GameScreen.AnamationPanel;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI.GameScreen
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(Canvas))]
    public class LoseView : MonoBehaviour, ILoseView
    {
        public event Action OnRestartRequested;

        [Header("Texts Scores")]
        [SerializeField] private TextMeshProUGUI _textScore;
        [SerializeField] private TextMeshProUGUI _textBestResult;

        [Header("Button")]
        [SerializeField] private Button _restartButton;
        
        [SerializeField] private RectTransform _panelTransform;

        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        
        private Sequence _sequence;
        private IAnimationWindow _windowAnimation;
        

        [Inject]
        public void Construct(IAnimationWindow  windowAnimation)
        {
            _canvas = GetComponent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            
            _windowAnimation = windowAnimation;
        }

        private void Start()
        {
            _restartButton?.onClick.AddListener(OnRestart);
        }

        private void OnDestroy()
        {
            _restartButton?.onClick.RemoveListener(OnRestart);
        }

        public void SetScore(int score, int bestScore)
        {
            _textScore.text = $"Score: {score.ToString()}";
            _textBestResult.text = $"Best Result: {bestScore.ToString()}";
        }

        public void ShowPanel()
        {
            _canvas.gameObject.SetActive(true);
            _restartButton.interactable = true;

            _windowAnimation.AnimatePanel(_panelTransform, _canvasGroup, 0.8f);
        }

        public void HidePanel()
        {
            _canvas.gameObject.SetActive(false);
            _restartButton.interactable = false;
        }

        private void OnRestart()
        {
            OnRestartRequested?.Invoke();
        }
    }
}