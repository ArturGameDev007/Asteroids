using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScreen
{
    [RequireComponent(typeof(Canvas))]
    public class LoseView : MonoBehaviour
    {
        private Canvas _canvas;
        [field: SerializeField] public Button RestartButton { get; set; }

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public void ShowPanel()
        {
            _canvas.gameObject.SetActive(true);
            RestartButton.interactable = true;
        }

        public void HidePanel()
        {
            _canvas.gameObject.SetActive(false);
            RestartButton.interactable = true;
        }
    }
}