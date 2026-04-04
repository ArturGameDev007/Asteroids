using _Project.Scripts.Configs.Enemies;
using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;

namespace _Project.Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        // private EnemyConfig _enemyConfig;
        private RemoteConfigsData _enemyRemoteConfig;
        
        private IObjectReturner<Enemy> _returner;
        private IEnemyDeathListener _deathListener;
        
        protected Rigidbody2D Head2D { get; private set; }

        private bool _canMove;

        private void Awake()
        {
            Head2D = GetComponent<Rigidbody2D>();
        }

        public void Construct(IObjectReturner<Enemy> returner, IEnemyDeathListener deathListener, RemoteConfigsData enemyConfig)
        {
            _returner = returner;
            _deathListener = deathListener;
            _enemyRemoteConfig = enemyConfig;
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
            _deathListener?.OnEnemyDeath(_enemyRemoteConfig);
            _returner?.ReturnPool(this);
        }
    }
}