using System;
using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Player
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(HandlerCrashWithEnemy))]
    public class Character : MonoBehaviour
    {
        public event Action OnGameOver;
        
        private IControllable _controllable;
        private ICollisionHandler _collisionHandler;

        private void Awake()
        {
            var player = GetComponent<PlayerController>();
            
            _controllable = new PlayerControllerAdapter(player);
            
            _collisionHandler = GetComponent<ICollisionHandler>();
        }

        private void Start()
        {
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
            // _controllable.SetPaused(true);
            OnGameOver?.Invoke();
        }
    }
}
