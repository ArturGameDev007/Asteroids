using UnityEngine;

namespace _Project.Scripts.UI.Background
{
    public class BackgroundView
    {
        private Canvas _canvas;

        public BackgroundView(Canvas canvas)
        {
            _canvas = canvas;
        }

        public void Construct(Camera mainCamera, int orderInLayer)
        {
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.worldCamera = mainCamera;
            _canvas.sortingOrder = orderInLayer;
        }
    }
}