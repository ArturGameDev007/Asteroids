using System;

namespace _Project.Scripts.Services.Ads
{
    public interface IAdsService
    {
        public event Action<string> OnAdsFinished; 
        
        public void ShowAdsReward(string type);
        public void ShowAdsInterstitial();
    }
}