using _Project.Scripts.Services.AsyncLoader;
using _Project.Scripts.UI.StartMenu;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Installers.StartMenu
{
    public class MenuInstaller : MonoInstaller
    {
        // [SerializeField] private StartMenuView _startMenuView;
        // [SerializeField] private LoadingView _loadingView;
        
        [SerializeField] private AssetReference _startMenuView;
        [SerializeField] private AssetReference _loadingView;
        
        public override void InstallBindings()
        {
            // BindStartMenu();
            // BindLoadingView();
            
            Container.Bind<IResourceLoader>().To<AddressableResourceLoader>().AsSingle();
            
            Container.BindInterfacesTo<StartMenuPresenter>().AsSingle().WithArguments(_startMenuView);
            
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle().WithArguments(_loadingView);
        }

        private void BindStartMenu()
        {
            // Container.Bind<StartMenuView>().FromComponentInHierarchy().AsSingle();
            //
            // Container.BindInterfacesTo<StartMenuPresenter>().AsSingle();
            //
            // Container.Bind<IResourceLoader>().To<AddressableResourceLoader>().AsSingle();
            //
            // Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle().WithArguments(_loadingView);
        }

        // private void BindLoadingView()
        // {
        //     Container.Bind<ILoadingView>().To<LoadingView>().FromComponentInHierarchy().AsSingle();
        // }
    }
}