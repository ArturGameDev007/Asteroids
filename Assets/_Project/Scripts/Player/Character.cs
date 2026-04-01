using System;
using _Project.Scripts.Enemies;
using Zenject;

namespace _Project.Scripts.Player
{
    public class Character : IInitializable, IDisposable
    {
        public event Action OnGameOver;

        private readonly IPlayerProvider _playerProvider;

        private bool _isInitialized;
        private bool _isDead;

        public Character(IPlayerProvider playerProvider)
        {
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
        }

        public void Revive()
        {
            _isDead = false;
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

        private void ProcessCollision(IEnemy enemy)
        {
            if (_isDead)
                return;
            
            _isDead = true;
            OnGameOver?.Invoke();
        }
    }
}