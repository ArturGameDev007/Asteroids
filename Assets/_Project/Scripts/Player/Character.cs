using System;
using _Project.Scripts.Enemies;
using Zenject;

namespace _Project.Scripts.Player
{
    public class Character : IInitializable, IDisposable
    {
        public event Action OnGameOver;

        private readonly IControllable _controllable;
        private readonly ICollisionHandler _collisionHandler;

        public Character(IControllable controllable, ICollisionHandler collisionHandler)
        {
            _controllable = controllable;
            _collisionHandler = collisionHandler;
        }

        public void Initialize()
        {
            _collisionHandler.OnCollisionDetected += ProcessCollision;
        }

        public void Dispose()
        {
            _collisionHandler.OnCollisionDetected -= ProcessCollision;
        }

        public void ClearState()
        {
            _controllable?.ResetState();
        }

        private void ProcessCollision(IEnemy enemy)
        {
            OnGameOver?.Invoke();
        }
    }
}