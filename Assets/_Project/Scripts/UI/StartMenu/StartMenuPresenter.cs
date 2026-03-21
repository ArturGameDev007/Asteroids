using System;
using _Project.Scripts.Services.Analytics;
using Zenject;

namespace _Project.Scripts.UI.StartMenu
{
    public class StartMenuPresenter : IInitializable, IDisposable
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly StartMenuView _startMenuView;
        private readonly IAnalyticsService _analyticsService;

        public StartMenuPresenter(ISceneLoader sceneLoader, StartMenuView startMenuView,  IAnalyticsService analyticsService)
        {
            _sceneLoader = sceneLoader;
            _startMenuView = startMenuView;
            _analyticsService = analyticsService;
        }

        public void Initialize()
        {
            _startMenuView.StartButton.onClick.AddListener(OnStartClicked);
        }

        public void Dispose()
        {
            _startMenuView.StartButton.onClick.RemoveListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            _analyticsService.LogGameStart();
            _sceneLoader.LoadScene();
        }
    }
}