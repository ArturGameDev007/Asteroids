using _Project.Scripts.Services.RemoteConfigs;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IPlayerController, IMovableEntity
    {
        private const float X_ANGLE = 0f;
        private const float Y_ANGLE = 0f;

        private IRemoteConfigs _remoteConfigs;
        private IInputService _controllerInput;
        private Rigidbody2D _head2D;

        private bool _isPaused;

        private Vector3 _startPosition;

        public Vector3 Position => transform.position;
        public float RotationAngleZ => transform.rotation.eulerAngles.z;
        public float Speed =>_head2D.velocity.magnitude;

        [Inject]
        public void Construct(IInputService controllerInput, IRemoteConfigs remoteConfigs)
        {
            _controllerInput = controllerInput;
            _remoteConfigs = remoteConfigs;

            if (_head2D == null)
                _head2D = GetComponent<Rigidbody2D>();

            _startPosition = transform.position;
            _isPaused = false;
        }

        private void Update()
        {
            if (_isPaused || _controllerInput == null)
                return;

            _controllerInput.UpdateHorizontalInput();
            _controllerInput.UpdateVerticalInput();

            HandleRotation();
        }

        private void FixedUpdate()
        {
            if (_isPaused || _controllerInput == null)
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

            if (_head2D != null)
            {
                _head2D.simulated = true;
                _head2D.velocity = Vector2.zero;
                _head2D.angularVelocity = 0f;
            }

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

                _head2D.AddForce(direction * _remoteConfigs.RemoteConfig.ForceInputShip);
            }
        }

        private void HandleRotation()
        {
            float rotationHorizontal = _controllerInput.HorizontalInput;
            float rotationAmount = -rotationHorizontal * _remoteConfigs.RemoteConfig.RotationSpeedShip * Time.deltaTime;

            transform.Rotate(X_ANGLE, Y_ANGLE, rotationAmount);
        }
    }
}