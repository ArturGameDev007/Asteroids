using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScreen
{
    [RequireComponent(typeof(Canvas))]
    public class WindowEndGame : MonoBehaviour
    {
        public event Action OnRestartClick;

        [SerializeField] private Button _actionButton;
        private Canvas _panelCanvas;

        private void Awake()
        {
            _panelCanvas = GetComponent<Canvas>();
        }

        private void OnValidate()
        {
            _actionButton = GetComponent<Button>();
            _actionButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            _actionButton.onClick.RemoveListener(OnButtonClick);
        }

        public void OpenScreen()
        {
            _panelCanvas.gameObject.SetActive(true);
            _actionButton.interactable = true;
        }

        public void CloseScreen()
        {
            _panelCanvas.gameObject.SetActive(false);
            _actionButton.interactable = false;
        }

        private void OnButtonClick()
        {
            OnRestartClick?.Invoke();
        }
    }
}