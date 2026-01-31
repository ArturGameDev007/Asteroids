using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScreen
{
    [RequireComponent(typeof(Canvas))]
    public class LoseViewModel : MonoBehaviour
    {
        public event Action OnRestartClick;

        [SerializeField] private LoseView _loseView;

        private Canvas _canvas;

        public void Construct(LoseView loseView)
        {
            _loseView = loseView;
            
            if (_loseView != null && _loseView.RestartButton != null)
            {
                _loseView.RestartButton.onClick.RemoveAllListeners();
                _loseView.RestartButton.onClick.AddListener(OnRestartClicked);
            }
        }

        // private void Awake()
        // {
        //     _loseView.RestartButton.onClick.AddListener(OnRestartClicked);
        // }

        private void Start()
        {
            _canvas = GetComponent<Canvas>();
            Close();
        }

        private void OnDestroy()
        {
            if (_loseView?.RestartButton != null)
                _loseView.RestartButton.onClick.RemoveListener(OnRestartClicked);
        }

        public void Open()
        {
            if (_canvas != null)
            {
                _canvas.gameObject.SetActive(true);
            }

            if (_loseView?.RestartButton != null)
                _loseView.RestartButton.interactable = true;

            // _loseView?.ShowPanel();
        }

        public void Close()
        {
            if (_canvas != null)
            {
                _canvas.gameObject.SetActive(false);
            }
            
            if (_loseView?.RestartButton != null)
                _loseView.RestartButton.interactable = false;
            // _loseView.HidePanel();
        }

        private void OnRestartClicked()
        {
            OnRestartClick?.Invoke();
        }
    }
}