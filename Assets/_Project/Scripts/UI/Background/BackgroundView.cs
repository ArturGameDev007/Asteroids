using UnityEngine;

namespace _Project.Scripts.UI.Background
{
    [RequireComponent((typeof(Canvas)))]
    public class BackgroundView : MonoBehaviour
    {
        private Canvas _canvas;

        public void Construct(Camera mainCamera, int orderInLayer)
        {
            if (_canvas == null) 
                _canvas = GetComponent<Canvas>();

            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.worldCamera = mainCamera;
            _canvas.sortingOrder = orderInLayer;
        }
    }
}