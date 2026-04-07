using _Project.Scripts.Services.Ads;
using _Project.Scripts.Services.Analytics;
using _Project.Scripts.Services.RemoteConfigs;
using _Project.Scripts.Services.Save;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private RemoteConfigsRoot _remoteConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<ISaveService>().To<LocalSaveService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<FirebaseAnalyticsService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<UnityAdsService>().AsSingle();
            Container.Bind<IAdsRewardsType>().To<AdsRewardsType>().AsSingle();

            Container.Bind<IRemoteConfigs>().To<FirebaseRemoteConfig>().AsSingle();
            Container.BindInstance(_remoteConfig).AsSingle();
        }
    }
}