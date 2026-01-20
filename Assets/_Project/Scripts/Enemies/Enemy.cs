using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private ObjectPool _pool;
        private IEnemyDeathListener  _deathListener;
        
        private Vector2 _direction;

        public void Initialize(IEnemyDeathListener listener, ObjectPool pool)
        {
            _deathListener = listener;
            _pool = pool;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                _deathListener?.NotifyEnemyKilled();
                _pool.PutObject(this);
                
                Destroy(bullet.gameObject);
            }
            else if (other.TryGetComponent(out Laser laser))
            {
                _deathListener?.NotifyEnemyKilled();
                _pool.PutObject(this);
            }
        }
    }
}
