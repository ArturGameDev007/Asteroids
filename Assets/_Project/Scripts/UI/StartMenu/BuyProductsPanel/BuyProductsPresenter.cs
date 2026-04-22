using System;
using _Project.Scripts.Services.Purchases;
using Zenject;

namespace _Project.Scripts.UI.StartMenu.BuyProductsPanel
{
    public class BuyProductsPresenter : IInitializable, IDisposable
    {
        private readonly IProductView _productView;
        private readonly IIAPService _apService;
        private readonly IProductTypePurchase _productTypePurchase;
        
        public BuyProductsPresenter(IProductView productView, IAPService apService, IProductTypePurchase productTypePurchase)
        {
            _productView = productView;
            _apService = apService;
            _productTypePurchase = productTypePurchase;
        }

        public void Initialize()
        {
            _productView.OnBuyNoAds += OnBuyNoAdsClicked;
            _apService.OnPurchaseComplete += HandlePurchaseComplete;
            _productView.OnCloseButton += OnCloseButtonClick;
        }

        public void Dispose()
        {
            _productView.OnBuyNoAds -= OnBuyNoAdsClicked;
            _apService.OnPurchaseComplete -= HandlePurchaseComplete;
            _productView.OnCloseButton -= OnCloseButtonClick;
        }
        
        private void OnBuyNoAdsClicked()
        {
            _apService.BuyProduct(_productTypePurchase);
        }
        
        private void HandlePurchaseComplete(string purchasedId)
        {
            if (purchasedId == _productTypePurchase.NoAdsID)
                _productView.SetNoAdsButtonInteractable(false);
        }

        private void OnCloseButtonClick()
        {
            _productView.SetActive(false);
        }
    }
}