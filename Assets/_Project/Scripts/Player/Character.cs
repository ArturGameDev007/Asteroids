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
        
        private IControllable _player;
        private ICollisionHandler _collisionHandler;

        private void Awake()
        {
            _player = GetComponent<IControllable>();
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
            _player.IsPaused = false;
            _player?.ResetState();
        }

        private void ProcessCollision(IEnemy enemy)
        {
            _player.IsPaused = true;
            OnGameOver?.Invoke();
        }
    }
}
