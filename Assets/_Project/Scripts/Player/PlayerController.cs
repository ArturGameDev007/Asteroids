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

        private Rigidbody2D _head2D;
        private InputController _controllerInput;

        private Vector3 _startPosition;

        private void Awake()
        {
            _head2D = GetComponent<Rigidbody2D>();
            _controllerInput = new InputController();
        }

        private void Start()
        {
            _startPosition = transform.position;

            Restart();
        }

        private void Update()
        {
            _controllerInput.UpdateHorizontalInput();
            HandleRotation();
        }

        private void FixedUpdate()
        {
            _controllerInput.UpdateVerticalInput();
            Move();
        }

        public void Restart()
        {
            transform.position = _startPosition;
            transform.rotation = Quaternion.identity;
            _head2D.velocity = Vector2.zero;
        }

        public void StopPhysics()
        {
            _head2D.velocity = Vector2.zero;
            _head2D.angularDrag = 0f;
            _head2D.isKinematic = true;
        }

        private void Move()
        {
            float moveVertical = _controllerInput.VerticalInput;
        
            if (moveVertical > 0)
            {
                Vector2 direction = transform.up;
        
                _head2D.AddForce(direction * (_forceInput * Time.fixedDeltaTime));
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

