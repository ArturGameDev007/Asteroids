using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GameScreen
{
    [RequireComponent(typeof(Canvas))]
    public class WindowEndGame : MonoBehaviour
    {
        public event Action OnRestartClick;

        private Canvas _panelCanvas;
        private Button _actionButton;

        private float _startTimeGame = 1f;

        public bool IsPaused { get; private set; }

        private void Awake()
        {
            _panelCanvas = GetComponent<Canvas>();
            _actionButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _actionButton.onClick.RemoveListener(OnButtonClick);
        }

        public void Construct(Button button)
        {
            _actionButton = button;
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
