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

        private PlayerController _player;
        private HandlerCrashWithEnemy _collisionHandler;

        private void Awake()
        {
            _player = GetComponent<PlayerController>();
            _collisionHandler = GetComponent<HandlerCrashWithEnemy>();
        }

        private void Start()
        {
            _collisionHandler.OnCollisionHandler += ProcessCollision;
        }

        private void OnDestroy()
        {
            _collisionHandler.OnCollisionHandler -= ProcessCollision;
        }

        public void ClearState()
        {
            _player?.Restart();
        }

        private void ProcessCollision(Enemy enemy)
        {
            OnGameOver?.Invoke();
        }
    }
}
