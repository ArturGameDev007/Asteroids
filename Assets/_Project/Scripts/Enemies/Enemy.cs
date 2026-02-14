using _Project.Scripts.Player.Weapons;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private IMovable _movable;
        
        private ObjectPool<Enemy> _pool;
        private IEnemyDeathListener  _deathListener;

        private void Awake()
        {
            _movable = GetComponent<IMovable>();
        }

        private void Update()
        {
            _movable?.Move();
        }

        public void Initialize(ObjectPool<Enemy> pool, IEnemyDeathListener  deathListener)
        {
            _pool = pool;
            _deathListener = deathListener;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet _) || other.TryGetComponent(out Laser _))
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