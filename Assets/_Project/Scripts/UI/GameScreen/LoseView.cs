using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI.GameScreen
{
    [RequireComponent(typeof(Canvas))]
    public class LoseView : MonoBehaviour, ILoseView, IDispose
    {
        public event Action OnRestartRequested;
        
        [SerializeField] private TextMeshProUGUI _textScore;
        [field: SerializeField] public Button RestartButton { get; private set; }
        
        private Canvas _canvas;
        
        [Inject]
        public void Construct()
        {
            _canvas = GetComponent<Canvas>();
            
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
            _canvas.gameObject.SetActive(false);
            RestartButton.interactable = false;
        }

        public void Dispose()
        {
            RestartButton.onClick.RemoveListener(OnRestart);
        }

        private void OnRestart()
        {
            OnRestartRequested?.Invoke();
        }
    }
}