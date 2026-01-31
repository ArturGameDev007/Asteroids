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

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            
            if (RestartButton == null)
            {
                if (new ButtonScanner().TryGetInStack(transform, out Button found))
                {
                    RestartButton = found;
                    Debug.Log("Кнопка найдена!");
                }
                else
                {
                    Debug.Log("Кнопка не найдена!");
                    
                }
            }
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