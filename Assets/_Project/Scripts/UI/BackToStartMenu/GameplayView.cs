using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.BackToStartMenu
{
    [RequireComponent(typeof(Canvas))]
    public class GameplayView : MonoBehaviour, IGameplayView
    {
        [field: SerializeField] public Button BackToStartMenuButton { get; private set; }
        
        private Canvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        private void Start()
        {
            if (_canvas != null)
            {
                _canvas.sortingOrder = 5;
            }
        }
    }
}