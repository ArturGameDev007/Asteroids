using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScreen
{
    [RequireComponent(typeof(Canvas))]
    public class LoseView : MonoBehaviour
    {
        [field: SerializeField] public Button RestartButton { get; private set; }
        
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void Construct(Button button)
        {
            RestartButton = button;
        }

        public void ShowPanel()
        {
            _canvas.gameObject.SetActive(true);

            if (RestartButton != null)
            {
                RestartButton.interactable = true;
            }
        }

        public void HidePanel()
        {
            _canvas.gameObject.SetActive(false);

            if (RestartButton != null)
            {
                RestartButton.interactable = false;
            }
        }
    }
}