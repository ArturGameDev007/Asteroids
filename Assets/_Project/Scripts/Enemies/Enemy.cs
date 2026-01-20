using _Project.Scripts.Player.Weapons;
using _Project.Scripts.UI.GameScreen;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private ObjectPool _pool;
        
        public void Initialize(ObjectPool pool)
        {
            _pool = pool;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                _pool.PutObject(this);
                Destroy(bullet.gameObject);
            }
            else if (other.TryGetComponent(out Laser laser))
            {
                _pool.PutObject(this);
            }
        }
    }
}
