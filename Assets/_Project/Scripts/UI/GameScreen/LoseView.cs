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

        public void Construct(Button restartButton)
        {
            _canvas = GetComponent<Canvas>();
            RestartButton = restartButton;
        }

        public void ShowPanel()
        {
            _canvas.gameObject.SetActive(true);
            RestartButton.interactable = true;
        }

        public void HidePanel()
        {
            _canvas.gameObject.SetActive(false);
            RestartButton.interactable = false;
        }
    }
}