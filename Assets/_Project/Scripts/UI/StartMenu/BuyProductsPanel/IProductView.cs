using System;

namespace _Project.Scripts.UI.StartMenu
{
    public interface IProductView
    {
        public event Action OnBuyNoAds;
        public event Action OnCloseButton;
        
        public void SetActive(bool active);
        
        public void SetNoAdsButtonInteractable(bool active);
    }
}