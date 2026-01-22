using System;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class EnemyDiedHandler : MonoBehaviour
    {
        private const string BULLET = "Bullet";
        private const string LASER = "Laser";
        
        public event Action<Enemy> OnEnemyKilled;
        
        private ObjectPool _pool;
        private Enemy _enemy;

        public void Initialize(ObjectPool pool)
        {
            _pool = pool;
            _enemy = GetComponent<Enemy>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(BULLET) || other.CompareTag(LASER))
            {
                Kill();

                if (other.CompareTag(BULLET))
                {
                    Destroy(other.gameObject);
                }
            }
        }

        private void Kill()
        {
            OnEnemyKilled?.Invoke(_enemy);
            _pool.PutObject(_enemy);
        }
    }
}