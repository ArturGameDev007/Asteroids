using Assets.Scripts.EnemySpace;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(HandlerCrashWithEnemy))]
    public class Player : MonoBehaviour
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
            Subscribstion();
        }

        private void OnDestroy()
        {
            Unsubscribtion();
        }

        public void Reset()
        {
            _player.Reset();
        }

        private void Subscribstion()
        {
            _collisionHandler.OnCollisionHandler += ProcessCollision;
        }

        private void Unsubscribtion()
        {
            _collisionHandler.OnCollisionHandler -= ProcessCollision;
        }

        private void ProcessCollision(IEnemy enemy)
        {
            if (enemy is Enemy)
            {
                OnGameOver?.Invoke();
            }
        }
    }
}
