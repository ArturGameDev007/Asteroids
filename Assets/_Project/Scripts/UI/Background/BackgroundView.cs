using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.Background
{
    public class BackgroundView : IInitializable
    {
        private const int ORDER_IN_LAYER = -5;

        private readonly Canvas _canvas;
        private readonly Camera _mainCamera;

        public BackgroundView([Inject(Id = "Background_UI")] Canvas canvas, Camera mainCamera)
        {
            _canvas = canvas;
            _mainCamera = mainCamera;
        }

        public void Initialize()
        {
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.worldCamera = _mainCamera;
            _canvas.sortingOrder = ORDER_IN_LAYER;
        }
    }
}