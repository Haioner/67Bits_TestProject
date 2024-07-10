using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 7f;

    [Header("CACHE")]
    [SerializeField] private Transform cameraTransform;

    private CharacterController _controller;
    private Vector2 _moveInput;

    //Invoke on speed
    public delegate void OnSpeedChange(float speed);
    public static event OnSpeedChange onSpeedChange;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();

        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        CalculateMovement();
        CalculateRotation();
    }

    private void CalculateMovement()
    {
        Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y);
        move = cameraTransform.TransformDirection(move);
        move.y = 0;

        //Normalize movement
        if (move.magnitude > 1f) move.Normalize();

        //Set Movement
        _controller.Move(move * speed * Time.deltaTime);

        //Update Speed Delegate
        float currentSpeed = move.magnitude * speed;
        onSpeedChange?.Invoke(currentSpeed);
    }

    private void CalculateRotation()
    {
        Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y);
        move = cameraTransform.TransformDirection(move);
        move.y = 0;

        //Rotate towards move direction
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * 100);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }
}
