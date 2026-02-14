using _Project.Scripts.Enemies;
using UnityEngine;

namespace _Project.Scripts.Player.Weapons
{
    public class Bullet : MonoBehaviour
    {
        private ObjectPool<Bullet> _pool;

        public void Initialize(ObjectPool<Bullet> pool)
        {
            _pool = pool;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            ReturnToPool();
        }

        private void ReturnToPool()
        {
            if (_pool != null)
                _pool.PutObject(this);
        }
    }
}
