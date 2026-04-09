using System;
using _Project.Scripts.Services.Analytics;
using _Project.Scripts.Services.Purchases;
using Cysharp.Threading.Tasks;
using Zenject;

namespace _Project.Scripts.UI.StartMenu
{
    public class StartMenuPresenter : IInitializable, IDisposable
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IAnalyticsService _analyticsService;
        private readonly StartMenuView _startMenuView;
        private readonly IIAPService  _apService;

        public StartMenuPresenter(StartMenuView startMenuView, ISceneLoader sceneLoader,  IAnalyticsService analyticsService,  IIAPService apService)
        {
            _startMenuView = startMenuView;
            _sceneLoader = sceneLoader;
            _analyticsService = analyticsService;
            _apService = apService;
        }

        public void Initialize()
        {
            _startMenuView.StartButton.onClick.AddListener(OnStartClicked);
            _startMenuView.BuyNoAdsButton.onClick.AddListener(OnBuyNoAdsClicked);

            _apService.OnPurchaseComplete += HandlePurchaseComplete;
        }

        public void Dispose()
        {
            _startMenuView.StartButton.onClick.RemoveListener(OnStartClicked);
            _startMenuView.BuyNoAdsButton.onClick.RemoveListener(OnBuyNoAdsClicked);
            
            _apService.OnPurchaseComplete -= HandlePurchaseComplete;
        }

        private void OnStartClicked()
        {
            _startMenuView.StartButton.interactable = false;
            
            _analyticsService.LogGameStart();
            _sceneLoader.LoadSceneAsync().Forget();
        }

        private void OnBuyNoAdsClicked()
        {
            _apService.BuyProduct(_apService.NoAdsID);
        }

        private void HandlePurchaseComplete()
        {
            _startMenuView.BuyNoAdsButton.interactable = false;
        }
    }
}