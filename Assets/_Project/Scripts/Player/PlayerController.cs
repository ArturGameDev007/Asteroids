using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 70f;
        [SerializeField] private float _forceInput = 1.5f;

        private Rigidbody2D _head2D;
        private InputController _controllerInput;

        private Vector3 _startPosition;

        private float _xAngle = 0f;
        private float _yAngle = 0f;

        private void Awake()
        {
            _head2D = GetComponent<Rigidbody2D>();
            _controllerInput = new InputController();
        }

        private void Start()
        {
            _startPosition = transform.position;

            Reset();
        }

        private void Update()
        {
            _controllerInput.RotationDuringMovement();
            HandleRotation();
        }

        private void FixedUpdate()
        {
            _controllerInput.Move();
            Move();
        }

        public void Reset()
        {
            transform.position = _startPosition;
            transform.rotation = Quaternion.identity;
            _head2D.velocity = Vector2.zero;
        }

        private void Move()
        {
            float moveVertical = _controllerInput.VerticalInput;

            if (moveVertical > 0)
            {
                Vector2 direction = transform.up;

                _head2D.AddForce(_forceInput * direction);
            }
        }

        private void HandleRotation()
        {
            float rotationHorizontal = _controllerInput.HorizontalInput;
            float rotationAmount = -rotationHorizontal * _rotationSpeed * Time.deltaTime;

            transform.Rotate(_xAngle, _yAngle, rotationAmount);
        }
    }
}

