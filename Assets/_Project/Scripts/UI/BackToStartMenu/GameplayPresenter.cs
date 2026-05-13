using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Scripts.UI.BackToStartMenu
{
    public class GameplayPresenter : IInitializable, IDisposable
    {
        private readonly IGameplayView _gameplayView;

        public GameplayPresenter(IGameplayView gameplayView)
        {
            _gameplayView = gameplayView;
        }

        public void Initialize()
        {
            if (_gameplayView.BackToStartMenuButton != null)
                _gameplayView.BackToStartMenuButton.onClick.AddListener(OnClickBackButton);
        }

        public void Dispose()
        {
            if (_gameplayView.BackToStartMenuButton != null)
                _gameplayView.BackToStartMenuButton.onClick.RemoveListener(OnClickBackButton);
        }

        private void OnClickBackButton()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}