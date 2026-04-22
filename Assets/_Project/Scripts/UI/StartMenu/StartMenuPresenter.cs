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
        private readonly IProductView _panelProducts;

        public StartMenuPresenter(StartMenuView startMenuView, ISceneLoader sceneLoader,
            IAnalyticsService analyticsService, IProductView panelProducts)
        {
            _startMenuView = startMenuView;
            _sceneLoader = sceneLoader;
            _analyticsService = analyticsService;
            _panelProducts = panelProducts;
        }

        public void Initialize()
        {
            _startMenuView.StartButton.onClick.AddListener(OnStartClicked);
            _startMenuView.BuyProducts.onClick.AddListener(OnBuyProductsClicked);
        }

        public void Dispose()
        {
            _startMenuView.StartButton.onClick.RemoveListener(OnStartClicked);
            _startMenuView.BuyProducts.onClick.RemoveListener(OnBuyProductsClicked);
        }

        private void OnStartClicked()
        {
            _startMenuView.StartButton.interactable = false;

            _analyticsService.LogGameStart();
            _sceneLoader.LoadSceneAsync().Forget();
        }

        private void OnBuyProductsClicked()
        {
            _panelProducts.SetActive(true);
        }
    }
}