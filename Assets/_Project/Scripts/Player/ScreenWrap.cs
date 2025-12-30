using UnityEngine;

namespace Scripts.Player
{
    public class ScreenWrap : MonoBehaviour
    {
        private float _screenWidth;
        private float _screenHeight;
        private float _objectWidth;
        private float _objectHeight;

        private Camera _main;
        private Vector3 _position;

        private void Start()
        {
            _main = GetComponent<Camera>();
            _main = Camera.main;

            BoundsScreen();
        }

        private void Update()
        {
            CheckForScreenWrap();
            _position = _main.transform.position;
        }

        private void BoundsScreen()
        {
            Vector3 screenBounds = _main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _position.z));
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

