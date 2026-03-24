using _Project.Scripts.Services.AsyncLoader;
using _Project.Scripts.UI.StartMenu;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace _Project.Scripts.Installers.StartMenu
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private AssetReference _startMenuView;
        [SerializeField] private AssetReference _loadingView;
        
        public override void InstallBindings()
        {
            Container.Bind<IResourceLoader>().To<AddressableResourceLoader>().AsSingle();
            
            Container.BindInterfacesTo<StartMenuPresenter>().AsSingle().WithArguments(_startMenuView);
            
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle().WithArguments(_loadingView);
        }
    }
}