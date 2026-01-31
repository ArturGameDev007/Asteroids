using System;
using _Project.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.GameScreen
{
    [RequireComponent(typeof(Canvas))]
    public class LoseView : MonoBehaviour
    {
        [field: SerializeField] public Button RestartButton { get; private set; }

        private Canvas _canvas;
        private HierarchyScanner _hierarchyScanner =  new HierarchyScanner();

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            
            if (RestartButton == null)
            {
                if (_hierarchyScanner.TryGetInStack(this.transform, out Button buttonComponent))
                {
                    RestartButton = buttonComponent;
                    Debug.LogWarning("Кнопка найдена через сканер, но лучше назначить ее в инспекторе!");
                }
                else
                {
                    Debug.LogError("Кнопка перезапуска не найдена ни в инспекторе, ни через сканер!");
                }
            }
        }

        // public void Construct(Button button)
        // {
        //     RestartButton = button;
        // }

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