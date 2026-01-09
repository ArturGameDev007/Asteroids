using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GameScreen
{
    public class WindowEndGame : MonoBehaviour
    {
        public event Action OnRestartClick;

        [SerializeField] private Canvas _panelCanvas;
        [SerializeField] private Button _actionButton;

        private float _startTimeGame = 1f;

        public bool IsPaused { get; private set; }

        private void Awake()
        {
            _actionButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _actionButton.onClick.RemoveListener(OnButtonClick);
        }

        public void OpenScreen()
        {
            IsPaused = true;

            _panelCanvas.gameObject.SetActive(true);
            _actionButton.interactable = true;
        }

        public void CloseScreen()
        {
            IsPaused = false;

            Time.timeScale = _startTimeGame;

            _panelCanvas.gameObject.SetActive(false);
            _actionButton.interactable = false;
        }

        private void OnButtonClick()
        {
            OnRestartClick?.Invoke();
        }
    }
}
