using System;

namespace _Project.Scripts.Services.Ads
{
    public interface IAdsService
    {
        public event Action OnAdsFinished;
        
        public void ShowAdsReward();
        public void ShowAdsInterstitial();
    }
}