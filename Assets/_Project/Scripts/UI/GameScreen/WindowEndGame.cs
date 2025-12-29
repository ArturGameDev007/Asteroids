using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.UI.GameScreen
{
    public class WindowEndGame : MonoBehaviour
    {
        public event Action OnRestartClick;

        //[field: SerializeField] public Canvas PanelCanvas { get; private set; }
        //[field: SerializeField] public Button ActionButton { get; private set; }

        [SerializeField] private Canvas _panelCanvas;
        [SerializeField] private Button _actionButton;

        private float _stopTimeGame = 0f;
        private float _startTimeGame = 1f;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            _panelCanvas = GetComponent<Canvas>();
            _actionButton = GetComponentInChildren<Button>();
            _actionButton.onClick.AddListener(OnButtonClick);
        }

        public void Cleanup()
        {
            _actionButton.onClick.RemoveListener(OnButtonClick);
        }

        public void OpenScreen()
        {
            Time.timeScale = _stopTimeGame;

            _panelCanvas.gameObject.SetActive(true);
            _actionButton.interactable = true;
        }

        public void CloseScreen()
        {
            Time.timeScale = _startTimeGame;

            _panelCanvas.gameObject.SetActive(false);
            _actionButton.interactable = false;
        }

        public void OnButtonClick()
        {
            OnRestartClick?.Invoke();
        }
    }
}
