using UnityEngine;

namespace Scripts.Player
{
    public class InputController
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }

        public void Move()
        {
            HorizontalInput = Input.GetAxis(Horizontal);
        }

        public void RotationDuringMovement()
        {
            VerticalInput = Input.GetAxis(Vertical);
        }
    }
}


