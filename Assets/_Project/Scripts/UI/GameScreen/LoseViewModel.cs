using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScreen
{
    public class LoseViewModel : MonoBehaviour
    {
        public event Action OnRestartClick;

        [SerializeField] private LoseView _loseView;

        private void Awake()
        {
            // if (_loseView != null && _loseView.RestartButton != null)
            // {
            //     _loseView.RestartButton.onClick.AddListener(OnRestartClicked);
            // }            

            _loseView.RestartButton.onClick.AddListener(OnRestartClicked);
        }

        private void Start()
        {
            Close();
        }

        private void OnDestroy()
        {
            // if (_loseView != null && _loseView.RestartButton != null)
            // {
            //     _loseView.RestartButton.onClick.RemoveListener(OnRestartClicked);
            // }            

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