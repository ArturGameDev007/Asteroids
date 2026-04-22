using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.UI.StartMenu.BuyProductsPanel
{
    public class ProductsView : MonoBehaviour, IProductView
    {
        public event Action OnBuyNoAds;
        public event Action OnCloseButton;
        
        [SerializeField] private Image _panelBackground;
        
        [Header("Buttons")]
        [SerializeField] private Button _buyNoAdsButton;
        [SerializeField] private Button _closeButton;
        
        private void Start()
        {
            _buyNoAdsButton.onClick.AddListener(OnBuyClick);
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDestroy()
        {
            _buyNoAdsButton.onClick.RemoveListener(OnBuyClick);
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }
        
        public void SetActive(bool active)
        {
            _panelBackground.gameObject.SetActive(active);
        }

        public void SetNoAdsButtonInteractable(bool active)
        {
            _buyNoAdsButton.interactable = active;
        }

        private void OnBuyClick()
        {
            OnBuyNoAds?.Invoke();
        }

        private void OnCloseButtonClicked()
        {
            OnCloseButton?.Invoke();
        }
    }
}
