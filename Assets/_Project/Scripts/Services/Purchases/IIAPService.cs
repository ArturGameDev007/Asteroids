using System;

namespace _Project.Scripts.Services.Purchases
{
    public interface IIAPService
    {
        public event Action OnPurchaseComplete;
        
        public string NoAdsID { get; }
        
        public void BuyProduct(string productId);
    }
}