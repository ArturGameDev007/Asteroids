using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class AsteroidController : MonoBehaviour, IMovable
    {
        [SerializeField] private float _speed;

        private Vector2 _direction;

        private void Update()
        {
            Move();
        }

        public void SetDirection()
        {
            _direction = _direction.normalized;
        }

        public void Move()
        {
            float speedMove = _speed * Time.deltaTime;
            transform.Translate(_direction * speedMove);
        }
    }
}
