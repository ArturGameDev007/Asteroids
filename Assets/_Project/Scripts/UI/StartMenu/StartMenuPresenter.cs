using System;
using _Project.Scripts.Services.Analytics;
using _Project.Scripts.Services.AsyncLoader;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.UI.StartMenu
{
    public class StartMenuPresenter : IInitializable, IDisposable
    {
        private readonly IResourceLoader _resourceLoader;
        private readonly ISceneLoader _sceneLoader;
        private readonly IAnalyticsService _analyticsService;
        private readonly AssetReference _viewReferences;
        
        private StartMenuView _startMenuView;

        public StartMenuPresenter(IResourceLoader resourceLoader, ISceneLoader sceneLoader,  IAnalyticsService analyticsService, AssetReference viewReferences)
        {
            _resourceLoader = resourceLoader;
            _sceneLoader = sceneLoader;
            _analyticsService = analyticsService;
            _viewReferences = viewReferences;
        }

        public async void Initialize()
        {
            _startMenuView = await _resourceLoader.LoadAsset<StartMenuView>(_viewReferences);
            _startMenuView.StartButton.onClick.AddListener(OnStartClicked);
            
            // _startMenuView.StartButton.onClick.AddListener(OnStartClicked);
        }

        public void Dispose()
        {
            // _startMenuView.StartButton.onClick.RemoveListener(OnStartClicked);
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