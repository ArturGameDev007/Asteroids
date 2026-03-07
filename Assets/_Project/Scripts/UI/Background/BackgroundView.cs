using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.Background
{
    public class BackgroundView
    {
        private const int ORDER_IN_LAYER = -5;
        
        private readonly Canvas _canvas;

        [Inject]
        public BackgroundView([Inject(Id = "Background_UI")] Canvas canvas, Camera mainCamera)
        {
            _canvas = canvas;
            _canvas.renderMode = RenderMode.ScreenSpaceCamera;
            _canvas.worldCamera = mainCamera;
            _canvas.sortingOrder = ORDER_IN_LAYER;
        }
    }
}