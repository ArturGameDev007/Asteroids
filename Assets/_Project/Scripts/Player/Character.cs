using System;
using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class Character : MonoBehaviour
    {
        public event Action OnGameOver;
        
        private IControllable _controllable;
        private ICollisionHandler _collisionHandler;
        
        public void Construct(IControllable controllable, ICollisionHandler collisionHandler)
        {
            _controllable = controllable;
            _collisionHandler = collisionHandler;
            
            _collisionHandler.OnCollisionDetected += ProcessCollision;
        }

        private void OnDestroy()
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
