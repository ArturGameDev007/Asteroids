using _Project.Scripts.Services.Analytics;
using _Project.Scripts.UI.StartMenu;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Installers.StartMenu
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private StartMenuView _startMenuView;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MenuStartLogger>().AsSingle().NonLazy();
            
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            
            Container.Bind<StartMenuView>().FromComponentInHierarchy().AsSingle();

            Container.BindInterfacesTo<StartMenuPresenter>().AsSingle();
        }
    }
}