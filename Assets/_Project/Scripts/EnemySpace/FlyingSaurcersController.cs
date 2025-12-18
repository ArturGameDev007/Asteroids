using UnityEngine;

namespace Assets.Scripts.EnemySpace
{
    public class FlyingSaurcersController : TypesOfEnemies
    {
        [SerializeField] private Transform _player;

        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _rotationSpeed = 30f;

        private Vector3 _flyingPosition;
        private Vector2 _directionToPlayer;
        private Vector2 _directionMove;

        private float _distanceToPlayer;
        private float _minDistanceForDetected = 10f;

        private void Start()
        {
            _flyingPosition = transform.position;
        }

        private void Update()
        {
            if (_player != null)
                Move();
        }

        public void Construsct(Transform player)
        {
            _player = player;
        }

        protected override void Move()
        {
            if (_player != null)
            {
                _directionToPlayer = _player.transform.position - _flyingPosition;
                _distanceToPlayer = _directionToPlayer.magnitude;

                if (_distanceToPlayer < _minDistanceForDetected)
                {
                    RotateTowardPlayer();

                    _directionMove = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
                    transform.position = _directionMove;
                }
            }
        }

        private void RotateTowardPlayer()
        {
            Vector3 directionToPlayer = _player.transform.position - _flyingPosition;

            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}
