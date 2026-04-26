using UnityEngine;
using Zenject;

namespace _Project.Scripts.Player
{
    public class ScreenWrap : MonoBehaviour
    {
        private float _screenWidth;
        private float _screenHeight;
        private float _objectWidth;
        private float _objectHeight;

        private Camera _cameraMain;
        private Vector3 _position;

        [Inject]
        public void Construct(Camera cameraMain)
        {
            _cameraMain = cameraMain;
        }

        private void Start()
        {
            BoundsScreen();
        }

        private void Update()
        {
            CheckForScreenWrap();
        }

        private void BoundsScreen()
        {
            if (_cameraMain == null)
                return;
            
            Vector3 screenBounds =
                _cameraMain.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _position.z));

            _screenWidth = screenBounds.x;
            _screenHeight = screenBounds.y;
        }

        private void CheckForScreenWrap()
        {
            Vector3 newPosition = transform.position;

            if (newPosition.x > _screenWidth + _objectWidth)
                newPosition.x = -_screenWidth - _objectWidth;
            else if (newPosition.x < -_screenWidth - _objectWidth)
                newPosition.x = _screenWidth + _objectWidth;

            if (newPosition.y > _screenHeight + _objectHeight)
                newPosition.y = -_screenHeight - _objectHeight;
            else if (newPosition.y < -_screenHeight - _objectHeight)
                newPosition.y = _screenHeight + _objectHeight;

            transform.position = newPosition;
        }
    }
}