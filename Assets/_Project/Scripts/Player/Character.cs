using System;
using _Project.Scripts.Enemies;
using Zenject;

namespace _Project.Scripts.Player
{
    public class Character : IInitializable, IDisposable
    {
        public event Action OnGameOver;

        // private readonly IControllable _controllable;
        // private readonly ICollisionHandler _collisionHandler;
        private readonly IPlayerProvider _playerProvider;

        private bool _isInitialized;

        public Character(IPlayerProvider playerProvider)
        {
            // _controllable = controllable;
            _playerProvider = playerProvider;
        }

        public void Initialize()
        {
            if (_playerProvider == null)
                return;

            _playerProvider.OnPlayerReady += Subscribe;
        }

        public void Dispose()
        {
            _playerProvider.OnPlayerReady -= Unsubscribe;
            
            // if (_playerProvider != null)
            //     _playerProvider.CollisionHandler.OnCollisionDetected -= ProcessCollision;
        }

        private void Subscribe()
        {
            if (!_isInitialized)
                _playerProvider.CollisionHandler.OnCollisionDetected += ProcessCollision;
        }

        private void Unsubscribe()
        {
            if (_playerProvider != null)
                _playerProvider.CollisionHandler.OnCollisionDetected -= ProcessCollision;
        }

        // public void ClearState()
        // {
        //     _controllable?.ResetState();
        // }

        private void ProcessCollision(IEnemy enemy)
        {
            OnGameOver?.Invoke();
        }
    }
}