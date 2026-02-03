using _Project.Scripts.Player;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class CoordinateDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coordinateText;
        
        [Header("Character")]
        [SerializeField] private PlayerController _targetValue;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        
        public void Initialize(PlayerController shipController, Rigidbody2D head)
        {
            _targetValue = shipController;
            _rigidbody2D = head;
        }

        private void Update()
        {
            Performance();
        }

        private void Performance()
        {
            Vector3 direction = _targetValue.transform.position;

            float rotationAngleZ = _targetValue.transform.localEulerAngles.z;
            float speed = _rigidbody2D.velocity.magnitude;

            string displayText = string.Format("<b>" + "X={0:F2}, Y={1:F2}\nAngleZ: {2:F1}\nSpeed: {3:F3}" + "</b>",
                direction.x, direction.y, rotationAngleZ, speed);

            _coordinateText.text = displayText;
        }
    }
}
