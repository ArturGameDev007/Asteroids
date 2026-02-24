using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class AsteroidController : Enemy
    {
        private Vector2 _direction;

        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;
        }

        protected override void Move()
        {
            Head2D.velocity = _direction * Speed;
        }
    }
}
