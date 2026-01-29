using System;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private const string BULLET = "Bullet";
        private const string LASER = "Laser";

        private ObjectPool _pool;
        private IEnemyDeathListener  _deathListener;

        public void Initialize(ObjectPool pool, IEnemyDeathListener  deathListener)
        {
            _pool = pool;
            _deathListener = deathListener;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(BULLET) || other.CompareTag(LASER))
            {
                Kill();
            }
        }

        private void Kill()
        {
            _deathListener?.OnEnemyDeath();
            _pool.PutObject(this);
        }
    }
}