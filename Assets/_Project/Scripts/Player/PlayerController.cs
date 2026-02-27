using UnityEngine;

namespace _Project.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private const float X_ANGLE = 0f;
        private const float Y_ANGLE = 0f;

        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _forceInput;

        private IInputService _controllerInput;
        private Rigidbody2D _head2D;

        private bool _isPaused;

        private Vector3 _startPosition;

        public void Construct(IInputService controllerInput)
        {
            _controllerInput = controllerInput;
        }

        private void Awake()
        {
            if (_head2D == null)
                _head2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            if (_isPaused)
                return;

            _controllerInput.UpdateHorizontalInput();
            _controllerInput.UpdateVerticalInput();

            HandleRotation();
        }

        private void FixedUpdate()
        {
            if (_isPaused)
                return;

            Move();
        }

        public void SetPaused(bool paused)
        {
            _isPaused = paused;
        }

        public void ResetState()
        {
            _isPaused = false;

            _head2D.simulated = true;
            _head2D.velocity = Vector2.zero;

            transform.position = _startPosition;
            transform.rotation = Quaternion.identity;
        }

        public void StopPhysics()
        {
            _isPaused = true;

            _head2D.velocity = Vector2.zero;
            _head2D.angularVelocity = 0f;
            _head2D.simulated = false;
        }

        private void Move()
        {
            float moveVertical = _controllerInput.VerticalInput;

            if (moveVertical > 0)
            {
                Vector2 direction = transform.up;

                _head2D.AddForce(direction * _forceInput);
            }
        }

        private void HandleRotation()
        {
            float rotationHorizontal = _controllerInput.HorizontalInput;
            float rotationAmount = -rotationHorizontal * _rotationSpeed * Time.deltaTime;

            transform.Rotate(X_ANGLE, Y_ANGLE, rotationAmount);
        }
    }
}