using TMPro;
using UnityEngine;

namespace Scripts.PerformanceShip
{
    public class CoordinateDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coordinateText;
        [SerializeField] private Transform _targetValue;

        [SerializeField] private Rigidbody2D _rigidbody2D;

        private void Update()
        {
            Performance();
        }

        private void Performance()
        {
            Vector3 direction = _targetValue.position;

            float rotationAngleZ = _targetValue.localEulerAngles.z;

            Vector2 velocity = _rigidbody2D.velocity;
            float speed = velocity.magnitude;

            string displayText = string.Format("<b>" + "X={0:F2}, Y={1:F2}\nAngleZ: {2:F1}\nSpeed: {3:F3}" + "</b>",
                _targetValue.position.x, _targetValue.position.y, rotationAngleZ, speed);

            _coordinateText.text = displayText;
        }
    }
}
