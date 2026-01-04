using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.GameScreen
{
    public class WindowEndGame : MonoBehaviour
    {
        public event Action OnRestartClick;

        [SerializeField] private Canvas _panelCanvas;
        [field: SerializeField] public Button ActionButton { get; private set; }

        private float _stopTimeGame = 0f;
        private float _startTimeGame = 1f;

        private void Awake()
        {
            _panelCanvas = GetComponent<Canvas>();
            ActionButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            ActionButton.onClick.RemoveListener(OnButtonClick);
        }

        public void OpenScreen()
        {
            Time.timeScale = _stopTimeGame;

            _panelCanvas.gameObject.SetActive(true);
            ActionButton.interactable = true;
        }

        public void CloseScreen()
        {
            Time.timeScale = _startTimeGame;

            _panelCanvas.gameObject.SetActive(false);
            ActionButton.interactable = false;
        }

        private void OnButtonClick()
        {
            OnRestartClick?.Invoke();
        }
    }
}
