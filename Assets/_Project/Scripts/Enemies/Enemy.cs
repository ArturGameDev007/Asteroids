using _Project.Scripts.Configs;
using _Project.Scripts.Player.Weapons;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        protected EnemyConfig EnemyConfig;
        protected Rigidbody2D Head2D { get; private set; }

        private IObjectReturner<Enemy> _returner;
        private IEnemyDeathListener _deathListener;

        private bool _canMove;

        private void Awake()
        {
            Head2D = GetComponent<Rigidbody2D>();
        }

        public void Initialize(IObjectReturner<Enemy> returner, IEnemyDeathListener deathListener,
            EnemyConfig enemyConfig)
        {
            _returner = returner;
            _deathListener = deathListener;
            EnemyConfig = enemyConfig;
        }

        private void FixedUpdate()
        {
            if (!_canMove)
                return;

            Move();
        }

        protected abstract void Move();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Bullet _) || other.TryGetComponent(out Laser _))
                Kill();
        }

        public void StopPhysics(bool isStop)
        {
            _canMove = !isStop;
            Head2D.simulated = !isStop;
            Head2D.velocity = Vector2.zero;
            Head2D.angularVelocity = 0f;
        }

        private void Kill()
        {
            _deathListener?.OnEnemyDeath(EnemyConfig);
            _returner?.ReturnPool(this);
        }
    }
}