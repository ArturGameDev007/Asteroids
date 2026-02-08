using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class FlyingSaucerController : MonoBehaviour, IMovable
    {
        [SerializeField] private Transform _player;

        [SerializeField] private float _speed = 2f;
        [SerializeField] private float _rotationSpeed = 30f;

        private Vector3 _flyingPosition;

        public void Construct(Transform player)
        {
            _player = player;
        }
        
        private void Start()
        {
            _flyingPosition = transform.position;
        }

        public void Move()
        {
            Vector2 directionToPlayer = (_player.transform.position - _flyingPosition).normalized;

            RotateTowardPlayer(directionToPlayer);

            Vector2 directionMove = Vector2.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            transform.position = directionMove;
        }

        private void RotateTowardPlayer(Vector2 directionToPlayer)
        {
            float rotateX = 0;
            float rotateY = 0;
            
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(new Vector3(rotateX, rotateY, angle));

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}