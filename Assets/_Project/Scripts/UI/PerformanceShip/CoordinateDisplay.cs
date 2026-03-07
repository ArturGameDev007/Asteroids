using _Project.Scripts.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.UI.PerformanceShip
{
    public class CoordinateDisplay : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _coordinateText;
        
        // [Header("Character")]
        private PlayerController _shipController;
        private Rigidbody2D _rigidbody2D;
        
        [Inject]
        public void Construct(PlayerController shipController)
        {
            _shipController = shipController;
            _rigidbody2D = shipController.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Performance();
        }

        private void Performance()
        {
            Vector3 direction = _shipController.transform.position;

            float rotationAngleZ = _shipController.transform.localEulerAngles.z;
            float speed = _rigidbody2D.velocity.magnitude;

            string displayText = string.Format("<b>" + "X={0:F2}, Y={1:F2}\nAngleZ: {2:F1}\nSpeed: {3:F3}" + "</b>",
                direction.x, direction.y, rotationAngleZ, speed);

            _coordinateText.text = displayText;
        }
    }
}
