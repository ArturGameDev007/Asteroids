using UnityEngine;

namespace _Project.Scripts.Enemies
{
    public class AsteroidController : MonoBehaviour, IMovable
    {
        [SerializeField] private float _speed;

        private float _speedInput;

        private void Update()
        {
            Move();
        }

        public void Move()
        {
            _speedInput = _speed * Time.deltaTime;
            transform.Translate(Vector3.down * _speedInput);
        }
    }
}
