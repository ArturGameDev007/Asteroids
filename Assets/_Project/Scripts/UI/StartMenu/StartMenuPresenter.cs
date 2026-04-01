using System;
using _Project.Scripts.Services.Analytics;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.UI.StartMenu
{
    public class StartMenuPresenter : IInitializable, IDisposable
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAnalyticsService _analyticsService;
        private readonly StartMenuView _startMenuView;

        public StartMenuPresenter(StartMenuView startMenuView, ISceneLoader sceneLoader,  IAnalyticsService analyticsService)
        {
            _startMenuView = startMenuView;
            _sceneLoader = sceneLoader;
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
            _startMenuView.StartButton.interactable = false;
            
            _analyticsService.LogGameStart();
            _sceneLoader.LoadSceneAsync().Forget();
        }
    }
}