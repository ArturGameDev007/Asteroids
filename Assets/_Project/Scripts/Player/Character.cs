using System;
using _Project.Scripts.Enemies;
using Zenject;

namespace _Project.Scripts.Player
{
    public class Character : IInitializable, IDisposable
    {
        public event Action OnGameOver;

        // private readonly IControllable _controllable;
        private readonly ICollisionHandler _collisionHandler;

        public Character(ICollisionHandler collisionHandler)
        {
            // _controllable = controllable;
            _collisionHandler = collisionHandler;
        }

        public void Initialize()
        {
            if (_collisionHandler == null)
                return;

            _collisionHandler.OnCollisionDetected += ProcessCollision;
        }

        public void Dispose()
        {
            if (_collisionHandler != null)
                _collisionHandler.OnCollisionDetected -= ProcessCollision;
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