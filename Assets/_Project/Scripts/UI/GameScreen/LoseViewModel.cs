using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScreen
{
    public class LoseViewModel : MonoBehaviour
    {
        public event Action OnRestartClick;

        [SerializeField] private LoseView _loseView;

        public void Construct(LoseView loseView)
        {
            _loseView = loseView;

            Close();
        }

        private void Awake()
        {
            _loseView.RestartButton.onClick.AddListener(OnRestartClicked);
        }

        private void OnDestroy()
        {
            _loseView.RestartButton.onClick.RemoveListener(OnRestartClicked);
        }

        public void Open()
        {
            _loseView.ShowPanel();
        }

        public void Close()
        {
            _loseView.HidePanel();
        }

        private void OnRestartClicked()
        {
            OnRestartClick?.Invoke();
        }
    }
}