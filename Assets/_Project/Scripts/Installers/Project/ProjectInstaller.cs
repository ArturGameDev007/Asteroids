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
            // Container.Bind<IAnalyticsService>().To<FirebaseAnalyticsService>().AsSingle();
            Container.BindInterfacesTo<FirebaseAnalyticsService>().AsSingle();
            Container.Bind<AnalyticsService>().AsSingle().NonLazy();
        }
    }
}