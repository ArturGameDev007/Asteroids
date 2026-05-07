using _Project.Scripts.Player.Weapons;
using _Project.Scripts.Services.Effects;
using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        private IRemoteConfigs _enemyRemoteConfig;
        private IObjectReturner<Enemy> _returner;
        private IEnemyDeathListener _deathListener;
        private IEffectService _effectService;
        
        protected Rigidbody2D Head2D { get; private set; }

        private bool _canMove;

        private void Awake()
        {
            Head2D = GetComponent<Rigidbody2D>();
        }

        [Inject]
        public void Construct(IObjectReturner<Enemy> returner, IEnemyDeathListener deathListener, IRemoteConfigs enemyConfig, IEffectService effectService)
        {
            _returner = returner;
            _deathListener = deathListener;
            _enemyRemoteConfig = enemyConfig;
            _effectService = effectService;
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
            _effectService.PlayExplosionForKill(transform.position);
            _deathListener?.OnEnemyDeath(_enemyRemoteConfig);
            _returner?.ReturnPool(this);
        }
    }
}