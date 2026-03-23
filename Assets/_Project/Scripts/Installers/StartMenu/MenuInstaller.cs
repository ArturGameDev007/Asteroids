using _Project.Scripts.Services.AsyncLoader;
using _Project.Scripts.UI.StartMenu;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers.StartMenu
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private StartMenuView _startMenuView;
        [SerializeField] private LoadingView _loadingView;
        
        public override void InstallBindings()
        {
            BindStartMenu();
            BindLoadingView();
        }

        private void BindStartMenu()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<StartMenuView>().FromComponentInHierarchy().AsSingle();
            
            Container.BindInterfacesTo<StartMenuPresenter>().AsSingle();
            
            Container.Bind<IResourceLoader>().To<AddressableResourceLoader>().AsSingle();
        }

        private void BindLoadingView()
        {
            Container.Bind<ILoadingView>().To<LoadingView>().FromComponentInHierarchy().AsSingle();
        }
    }
}