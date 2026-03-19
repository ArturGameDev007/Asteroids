using System;
using Zenject;

namespace _Project.Scripts.UI.StartMenu
{
    public class StartMenuPresenter : IInitializable, IDisposable
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly StartMenuView _startMenuView;

        public StartMenuPresenter(ISceneLoader sceneLoader, StartMenuView startMenuView)
        {
            _sceneLoader = sceneLoader;
            _startMenuView = startMenuView;
        }

        public void Initialize()
        {
            _startMenuView.StartButton.onClick.AddListener(OnStartClicked);
        }

        public void Dispose()
        {
            _startMenuView.StartButton.onClick.RemoveListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            _sceneLoader.LoadScene();
        }
    }
}