using UnityEngine;

public class InputController : MonoBehaviour
{
    private readonly string _horizontal = "Horizontal";
    private readonly string _vertical = "Vertical";

    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }

    private void Update()
    {
        Move();
        RotationDuringMovement();
    }

    private void Move()
    {
        HorizontalInput = Input.GetAxis(_horizontal);
    }

    private void RotationDuringMovement()
    {
        VerticalInput = Input.GetAxis(_vertical);
    }
}

