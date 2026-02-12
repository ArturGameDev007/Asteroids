using UnityEngine;

namespace _Project.Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AsteroidController : MonoBehaviour, IMovable
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _head2D;
        private Vector2 _direction;

        private void Awake()
        {
            _head2D = GetComponent<Rigidbody2D>();
            
            _head2D.gravityScale = 0f;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;
        }

        public void Move()
        {
            _head2D.velocity = _direction * _speed;
        }
    }
}
