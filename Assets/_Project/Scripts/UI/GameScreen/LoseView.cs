using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScreen
{
    [RequireComponent(typeof(Canvas))]
    public class LoseView : MonoBehaviour, ILoseView
    {
        public event Action OnRestartRequested;

        [SerializeField] private TextMeshProUGUI _textScore;
        
        [field: SerializeField] public Button  RestartButton {get; private set;}

        private Canvas _canvas;

        public void Construct(Button button)
        {
            _canvas = GetComponent<Canvas>();
            
            RestartButton = button;
            RestartButton?.onClick.AddListener(OnRestart);
        }

        public void SetScore(int score)
        {
            _textScore.text = $"Score: {score.ToString()}";
        }

        public void ShowPanel()
        {
            _canvas.gameObject.SetActive(true);
            RestartButton.interactable = true;
        }

        public void HidePanel()
        {
            _canvas.gameObject.SetActive(true);
            RestartButton.interactable = false;
        }

        private void OnDestroy()
        {
            RestartButton.onClick.RemoveListener(OnRestart);
        }

        private void OnRestart()
        {
            OnRestartRequested?.Invoke();
        }
    }
}