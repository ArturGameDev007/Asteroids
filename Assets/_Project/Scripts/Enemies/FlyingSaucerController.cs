using UnityEngine;

namespace _Project.Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FlyingSaucerController : MonoBehaviour, IMovable
    {
        [SerializeField] private Transform _player;

        [SerializeField] private float _speed = 7f;
        [SerializeField] private float _rotationSpeed = 30f;

        private Rigidbody2D _head2D;
        
        private void Awake()
        {
            _head2D = GetComponent<Rigidbody2D>();

            _head2D.gravityScale = 0f;
        }

        public void Construct(Transform player)
        {
            _player = player;
        }

        public void Move()
        {
            Vector2 currentPosition = _head2D.position;
            Vector2 targetPosition = _player.position;
            
            Vector2 directionToPlayer = (targetPosition - currentPosition).normalized;
            RotateTowardPlayer(directionToPlayer);

            Vector2 directionMove = Vector2.MoveTowards(currentPosition, targetPosition, _speed * Time.deltaTime);
            _head2D.MovePosition(directionMove);

        }

        private void RotateTowardPlayer(Vector2 directionToPlayer)
        {
            float rotateX = 0;
            float rotateY = 0;

            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(rotateX, rotateY, angle));

            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}