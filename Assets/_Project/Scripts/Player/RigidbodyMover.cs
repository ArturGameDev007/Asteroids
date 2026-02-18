using UnityEngine;

namespace _Project.Scripts.Player
{
    public class RigidbodyMover
    {
        private readonly Rigidbody2D _head2D;
        private readonly Transform _transform;
        private readonly float _speed;
        private readonly float _rotationSpeed;
        private Vector3 _startPos;

        public RigidbodyMover(Rigidbody2D head2D, float speed, float rotSpeed)
        {
            _head2D = head2D;
            _transform = head2D.transform;
            _speed = speed;
            _rotationSpeed = rotSpeed;
            _startPos = _transform.position;
        }
        
        
    }
}