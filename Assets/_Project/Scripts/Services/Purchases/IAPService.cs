using System;
using System.Collections.Generic;
using _Project.Scripts.Services.Save;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

namespace _Project.Scripts.Services.Purchases
{
    public class IAPService : IInitializable, IIAPService, IStoreListener
    {
        public event Action<string> OnPurchaseComplete;

        private readonly ISaveService _saveService;
        private readonly IProductTypePurchase _productTypePurchase;

        private IStoreController _storeController;

        private Dictionary<string, Func<UniTaskVoid>> _purchaseAction;

        public IAPService(ISaveService saveService, IProductTypePurchase productTypePurchase)
        {
            _saveService = saveService;
            _productTypePurchase = productTypePurchase;
        }

        public void Initialize()
        {
            var init = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            
            _purchaseAction = new Dictionary<string, Func<UniTaskVoid>>();

            AddProducts(init, _purchaseAction);

            UnityPurchasing.Initialize(this, init);
        }

        private void AddProducts(ConfigurationBuilder init, Dictionary<string,  Func<UniTaskVoid>> typePurchase)
        {
            PurchaseData[] catalog =
            {
                new PurchaseData(_productTypePurchase.NoAdsID, ApplyNoAds)
            };

            for (int i = 0; i < catalog.Length; i++)
            {
                var item = catalog[i];

                typePurchase.TryAdd(item.Id, item.TypeAction);

                init.AddProduct(item.Id, ProductType.NonConsumable);
            }
        }

        public void BuyProduct(IProductTypePurchase productId)
        {
            if (_storeController != null)
            {
                var product = _storeController.products.WithID(productId.NoAdsID);

                if (product != null && product.availableToPurchase)
                {
                    _storeController.InitiatePurchase(product);
                }
            }
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            string productId = args.purchasedProduct.definition.id;

            if (_purchaseAction.TryGetValue(productId, out var action))
            {
                action.Invoke().Forget();
            }
            else
            {
                Debug.LogWarning("Куплен неизвестный продукт.");
            }

            OnPurchaseComplete?.Invoke(productId);

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

        private async UniTaskVoid ApplyNoAds()
        {
            var data = await _saveService.Load();
            data.IsNoAdsPurchased = true;

            await _saveService.Save(data);
        }
    }
}