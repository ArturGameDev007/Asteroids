using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI.GameScreen
{
    public class LoseViewModel : MonoBehaviour
    {
        public event Action OnRestartClick;

        [SerializeField] private LoseView _loseView;

        [Inject]
        private void Construct()
        {
            if (_loseView != null && _loseView.RestartButton != null)
            {
                _loseView.RestartButton.onClick.AddListener(OnRestartClicked);
            }
        }

        private void Start()
        {
            Close();
        }

        private void OnDestroy()
        {
            if (_loseView != null && _loseView.RestartButton != null)
            {
                _loseView.RestartButton.onClick.RemoveListener(OnRestartClicked);
            }
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