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

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            
            if (RestartButton == null)
            {
                var scanner = new HierarchyScanner();
            
                if (scanner.TryGetInStack(this.transform, out Button foundButton))
                {
                    RestartButton = foundButton;
                }
                else
                {
                    Debug.LogError("Сканер прошел сквозь Backfon, но кнопку не увидел!");
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