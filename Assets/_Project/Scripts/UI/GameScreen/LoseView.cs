using System;
using UnityEngine;

namespace _Project.Scripts.UI.GameScreen
{
    public class LoseView : ILoseView, IDisposable
    {
        public event Action OnRestartRequested;
        private readonly LoseUIComponents _loseUIComponents;

        public LoseView(LoseUIComponents loseUIComponents)
        {
            _loseUIComponents = loseUIComponents;
            _loseUIComponents.RestartButton?.onClick.AddListener(OnRestart);
        }

        public void SetScore(int score)
        {
            _loseUIComponents.TextScore.text = $"Score: {score.ToString()}";
        }

        public void ShowPanel()
        {
            _loseUIComponents.Canvas.gameObject.SetActive(true);
            _loseUIComponents.RestartButton.interactable = true;
        }

        public void HidePanel()
        {
            _loseUIComponents.Canvas.gameObject.SetActive(false);
            _loseUIComponents.RestartButton.interactable = false;
        }

        public void Dispose()
        {
            _loseUIComponents.RestartButton.onClick.RemoveListener(OnRestart);
        }

        private void OnRestart()
        {
            OnRestartRequested?.Invoke();
        }
    }
}