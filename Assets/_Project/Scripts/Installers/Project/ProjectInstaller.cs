using _Project.Scripts.Services.Ads;
using _Project.Scripts.Services.Analytics;
using _Project.Scripts.Services.Save;
using Zenject;

namespace _Project.Scripts.Installers.Project
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISaveService>().To<LocalSaveService>().AsSingle();
            Container.BindInterfacesAndSelfTo<FirebaseAnalyticsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityAdsService>().AsSingle();
        }
    }
}