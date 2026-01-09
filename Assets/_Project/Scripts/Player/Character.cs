using Scripts.EnemySpace;
using System;
using UnityEngine;

namespace Scripts.Player
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

        public void Reset()
        {
            _player.Reset();
        }

        private void ProcessCollision(IEnemy enemy)
        {
            OnGameOver?.Invoke();
        }
    }
}
