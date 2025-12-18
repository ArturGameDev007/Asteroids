using UnityEngine;

namespace Assets.Scripts.EnemySpace
{
    public class AsteroidController : TypesOfEnemies
    {
        [SerializeField] private float _speed;

        private float _speedInput;

        private void Update()
        {
            Move();
        }

        protected override void Move()
        {
            _speedInput = _speed * Time.deltaTime;
            transform.Translate(Vector3.down * _speedInput);
        }
    }
}
