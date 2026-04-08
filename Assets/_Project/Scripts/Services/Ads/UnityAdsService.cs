using System;
using _Project.Scripts.Services.Save;
using UnityEngine.Advertisements;
using Zenject;

namespace _Project.Scripts.Services.Ads
{
    public class UnityAdsService : IInitializable, IAdsService, IUnityAdsShowListener, IUnityAdsLoadListener,
        IUnityAdsInitializationListener
    {
        public event Action<string> OnAdsFinished;

        private const string ANDROID_ID = "6078664";
        private const string REWARD_ADS = "Rewarded_Android";
        private const string INTERSTITIAL_ADS = "Interstitial_Android";
        
        private ISaveService _saveService;

        private string _adsRewardType;
        private bool _testMode = true;

        private bool _isRewardRequested;

        [Inject]
        public void Construct(ISaveService saveService)
        {
            _saveService = saveService;
        }

        public void Initialize()
        {
            Advertisement.Initialize(ANDROID_ID, _testMode, this);
        }

        public void OnInitializationComplete()
        {
            Advertisement.Load(REWARD_ADS, this);
            Advertisement.Load(INTERSTITIAL_ADS, this);
        }

        public void ShowAdsReward(string type)
        {
            _adsRewardType = type;
            _isRewardRequested = true;
            
            ShowAds(REWARD_ADS);
        }

        public void ShowAdsInterstitial()
        {
            ShowAds(INTERSTITIAL_ADS);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            Advertisement.Load(placementId, this);

            if (placementId == REWARD_ADS)
            {
                _isRewardRequested = false;
                ShowAdsInterstitial();
            }

            OnAdsFinished?.Invoke(string.Empty);
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            _isRewardRequested = false;

            Advertisement.Load(placementId, this);

            if (placementId == REWARD_ADS)
            {
                if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
                {
                    OnAdsFinished?.Invoke(_adsRewardType);
                }
                else
                {
                    ShowAdsInterstitial();
                }
            }
            else
            {
                OnAdsFinished?.Invoke(string.Empty);
            }
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            if (placementId == REWARD_ADS && _isRewardRequested)
            {
                _isRewardRequested = false;
                Advertisement.Show(placementId, this);
            }
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            OnAdsFinished?.Invoke(string.Empty);
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
        }
        
        private void ShowAds(string placementId)
        {
            if (Advertisement.isInitialized)
                Advertisement.Show(placementId, this);
            else
                OnAdsFinished?.Invoke(string.Empty);
        }
    }
}