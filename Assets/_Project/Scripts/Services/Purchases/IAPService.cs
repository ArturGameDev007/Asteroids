using System;
using _Project.Scripts.Services.Save;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace _Project.Scripts.Services.Purchases
{
    public class IAPService : IInitializable, IIAPService, IStoreListener
    {
        public event Action OnPurchaseComplete;

        private const string NO_ADS_ID = "no_ads";

        private readonly ISaveService _saveService;
        private IStoreController _storeController;

        public IAPService(ISaveService saveService)
        {
            _saveService = saveService;
        }

        public void Initialize()
        {
            var init = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            init.AddProduct(NO_ADS_ID, ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, init);
        }

        public void BuyProduct(string productId)
        {
            if (_storeController != null)
            {
                var product = _storeController.products.WithID(productId);

                if (product != null && product.availableToPurchase)
                {
                    _storeController.InitiatePurchase(product);
                }
            }
        }

        public void BuyNoAds()
        {
            BuyProduct(NO_ADS_ID);
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            if (args.purchasedProduct.definition.id == NO_ADS_ID)
            {
                var data = _saveService.Load();

                data.IsNoAdsPurchased = true;

                _saveService.Save(data);

                OnPurchaseComplete?.Invoke();

                Debug.Log($"Покупка отключение рекламы сохранена.");
            }

            return PurchaseProcessingResult.Complete;
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _storeController = controller;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message = null)
        {
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
        }
    }
}