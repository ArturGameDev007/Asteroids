using System;

namespace _Project.Scripts.Services.Purchases
{
    public interface IIAPService
    {
        public event Action<string> OnPurchaseComplete;
        
        public void BuyProduct(IProductTypePurchase productId);
    }
}