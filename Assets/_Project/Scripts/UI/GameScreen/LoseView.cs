using System;
using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace _Project.Scripts.UI.GameScreen
{
    [RequireComponent(typeof(Canvas))]
    public class LoseView : MonoBehaviour
    {
        [field: SerializeField] public Button RestartButton { get; private set; }

        private Canvas _canvas;

        private Canvas Canvas => _canvas ??= GetComponent<Canvas>();

        public void Construct(Button restartButton)
        {
            RestartButton = restartButton;
        }

        public void ShowPanel()
        {
            Canvas?.gameObject.SetActive(true);

            if (RestartButton != null)
                RestartButton.interactable = true;
        }

        public void HidePanel()
        {
            Canvas?.gameObject.SetActive(false);
 
            if (RestartButton != null)
                RestartButton.interactable = false;
        }
    }
}