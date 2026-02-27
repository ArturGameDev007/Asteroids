using UnityEngine;

namespace _Project.Scripts.Player
{
    public class InputProvider
    {
        private const string HORIZONTAL = "Horizontal";
        private const string VERTICAL = "Vertical";

        public float HorizontalInput { get; private set; }
        public float VerticalInput { get; private set; }

        public void UpdateHorizontalInput()
        {
            HorizontalInput = Input.GetAxis(HORIZONTAL);
        }

        public void UpdateVerticalInput()
        {
            VerticalInput = Input.GetAxis(VERTICAL);
        }
    }
}