using System;
using UnityEngine.Advertisements;
using Zenject;

namespace _Project.Scripts.Services.Ads
{
    public class UnityAdsService : IInitializable, IAdsService, IUnityAdsShowListener, IUnityAdsLoadListener, IUnityAdsInitializationListener
    {
        public event Action OnAdsFinished;
        
        private const string ANDROID_ID = "6078664";
        private const string REWARD_ADS = "Rewarded_Android";
        private const string INTRESTITIAL_ADS = "Interstitial_Android";
        
        private bool _testMode = true;

        public void Initialize()
        {
            Advertisement.Initialize(ANDROID_ID, _testMode, this);
        }

        public void ShowAdsReward()
        {
            ShowAds(REWARD_ADS);
        }

        public void ShowAdsInterstitial()
        {
            ShowAds(INTRESTITIAL_ADS);
        }

        private void ShowAds(string placementId)
        {
            if (Advertisement.isInitialized)
            {
                Advertisement.Load(placementId, this);
                Advertisement.Show(placementId, this);
            }
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            OnAdsFinished?.Invoke();
        }
        
        public void OnUnityAdsShowStart(string placementId){}
        
        public void OnUnityAdsShowClick(string placementId){}
        
        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            OnAdsFinished?.Invoke();
        }
        
        public void OnUnityAdsAdLoaded(string placementId)
        {
            Advertisement.Show(placementId, this);
        }
        
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message){}
        
        public void OnInitializationComplete(){}
        
        public void OnInitializationFailed(UnityAdsInitializationError error, string message){}
    }
}