using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(InputController))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _head2D;

        [SerializeField] private InputController _controllerInput;

        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _forceInput;

        private Vector3 _startPosition;

        private float _xAngle = 0f;
        private float _yAngle = 0f;

        private void Awake()
        {
            _head2D = GetComponent<Rigidbody2D>();
            _controllerInput = GetComponent<InputController>();
        }

        private void Start()
        {
            _startPosition = transform.position;

            Reset();
        }

        private void Update()
        {
            HandleRotation();
        }

        private void FixedUpdate()
        {
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

