using System;
using _Project.Scripts.Enemies;
using Zenject;

namespace _Project.Scripts.Player
{
    public class Character
    {
        public event Action OnGameOver;
        
        private IControllable _controllable;
        private ICollisionHandler _collisionHandler;

        [Inject]
        public Character(IControllable controllable, ICollisionHandler collisionHandler)
        {
            _controllable = controllable;
            _collisionHandler = collisionHandler;
            
            _collisionHandler.OnCollisionDetected += ProcessCollision;
        }

        public void Destruct()
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
