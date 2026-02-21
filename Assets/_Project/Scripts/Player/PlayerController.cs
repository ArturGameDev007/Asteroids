using UnityEngine;

namespace _Project.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController: MonoBehaviour, IControllable
    {
        private const float X_ANGLE = 0f;
        private const float Y_ANGLE = 0f;

        [SerializeField] private float _rotationSpeed;
        [SerializeField] private float _forceInput;

        private Rigidbody2D _head2D;
        private InputController _controllerInput;
        
        public bool IsPaused { get; set; } =  false;

        private Vector3 _startPosition;

        private void Awake()
        {
            if (_head2D == null)
                _head2D = GetComponent<Rigidbody2D>();
            
            _controllerInput = new InputController();
        }

        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            if (IsPaused)
                return;
            
            _controllerInput.UpdateHorizontalInput();
            _controllerInput.UpdateVerticalInput();

            HandleRotation();
        }

        private void FixedUpdate()
        {
            if (IsPaused)
                return;
            
            Move();
        }


        public void ResetState()
        {
            IsPaused = false;
            
            _head2D.simulated = true;
            _head2D.velocity = Vector2.zero;
            
            transform.position = _startPosition;
            transform.rotation = Quaternion.identity;
        }

        public void EnableControl()
        {
            // enabled = true;
            IsPaused = false;
        }

        public void DisableControl()
        {
            // enabled = false;
            IsPaused = true;
        }

        public void StopPhysics()
        {
            IsPaused = true;
            
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