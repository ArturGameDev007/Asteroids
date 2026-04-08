using System;

namespace _Project.Scripts.Services.Purchases
{
    public interface IIAPService
    {
        public event Action OnPurchaseComplete;
        
        public void BuyProduct(string productId);
        public void BuyNoAds();
    }
}