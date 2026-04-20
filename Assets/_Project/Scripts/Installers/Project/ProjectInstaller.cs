using _Project.Scripts.Infrastructure;
using _Project.Scripts.Services.Ads;
using _Project.Scripts.Services.Analytics;
using _Project.Scripts.Services.CloudSave;
using _Project.Scripts.Services.Purchases;
using _Project.Scripts.Services.RemoteConfigs;
using _Project.Scripts.Services.Save;
using _Project.Scripts.UI.StartMenu.SavesViewPanel;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private RemoteConfigsRoot _remoteConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<ICloudInitialize>().To<CloudInitializer>().AsSingle();
            Container.Bind<ICloudSaveSample>().To<CloudSaveSample>().AsSingle();
            Container.Bind<ISaveSynchronization>().To<SaveSynchronizationService>().AsSingle();
            
            Container.Bind<ISaveService>().To<LocalSaveService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<FirebaseAnalyticsService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<UnityAdsService>().AsSingle();
            Container.Bind<IAdsRewardsType>().To<AdsRewardsType>().AsSingle();

            Container.Bind<IRemoteConfigs>().To<FirebaseRemoteConfig>().AsSingle();
            Container.BindInstance(_remoteConfig).AsSingle();

            Container.BindInterfacesAndSelfTo<IAPService>().AsSingle();
            
            
        }
    }
}