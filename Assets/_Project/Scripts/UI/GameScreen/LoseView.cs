using System;
using DG.Tweening;
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

        [Header("Button")]
        [SerializeField] private Button _restartButton;

        private Canvas _canvas;
        private Transform _buttonTransform;

        [Inject]
        public void Construct()
        {
            _canvas = GetComponent<Canvas>();

            if (_restartButton != null)
                _buttonTransform = _restartButton.transform;
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

            AnimateTexts(_textScore.transform, 0.5f);
            AnimateTexts(_textBestResult.transform, 0.5f);
        }

        public void ShowPanel()
        {
            _canvas.gameObject.SetActive(true);
            _restartButton.interactable = true;

            AnimateButton(_buttonTransform, 0.5f);
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

        private void AnimateTexts(Transform transformText, float duration)
        {
            transformText.localScale = Vector3.zero;
            transformText.DOScale(Vector3.one, duration).SetEase(Ease.OutBack);
        }

        private void AnimateButton(Transform button, float duration)
        {
            if (_buttonTransform ==  null)
                return;
            
            Vector2 targetPosition = button.localPosition;
            Vector2 startShift = targetPosition - new Vector2(0, 300f);
            
            button.DOLocalMove(targetPosition, duration).From(startShift).SetEase(Ease.OutBack).SetUpdate(true);
        }
    }
}