using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _head2D;

    [SerializeField] private InputController _controllerInput;

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _thrustForce;

    private Vector3 _startPosition;

    private float _xAngle = 0f;
    private float _yAngle = 0f;

    private void Awake()
    {
        _head2D = GetComponent<Rigidbody2D>();
        _controllerInput = GetComponent<InputController>();
    }

    private void Start()
    {
        _startPosition = transform.position;

        Reset();
    }

    private void Update()
    {
        HandleRotation();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            _head2D.AddForce(transform.up * (_thrustForce * Time.deltaTime));

        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.back * (_rotationSpeed * Time.deltaTime), transform.rotation.x);

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.forward * (_rotationSpeed * Time.deltaTime), transform.rotation.x);
    }

    //private void Move()
    //{
    //    float moveVertical = _controllerInput.VerticalInput;

    //    if (moveVertical > 0)
    //    {
    //        Vector2 thrustDirection = transform.up;

    //        _head2D.AddForce(thrustDirection * _thrustForce);
    //    }
    //}

    private void HandleRotation()
    {
        float rotationHorizontal = _controllerInput.HorizontalInput;
        float rotationAmount = -rotationHorizontal * _rotationSpeed * Time.deltaTime;
        transform.Rotate(_xAngle, _yAngle, rotationAmount);
    }

    public void Reset()
    {
        transform.position = _startPosition;
        transform.rotation = Quaternion.identity;
        _head2D.velocity = Vector2.zero;
    }
}

