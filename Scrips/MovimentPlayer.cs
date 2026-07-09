using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float walkSpeed = 5.0f;
    public float sprintSpeed = 8.5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;

    [Header("Mirada (Ratón)")]
    public float mouseSensitivity = 2.0f;
    public float topLookLimit = -90.0f;
    public float bottomLookLimit = 90.0f;

    [Header("Agarre de Objetos")]
    public float grabDistance = 6.0f;
    public Transform holdPoint;

    private CharacterController characterController;
    private Transform playerCamera;
    private float verticalRotation = 0f;
    private Vector3 moveDirection = Vector3.zero;
    private Animator animator;

    private Object currentObject;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>().transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
        HandleInteraction();
    }

    void HandleMovement()
    {
        float currentSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) { currentSpeed = sprintSpeed; }
        else { currentSpeed = walkSpeed; }

        float forwardSpeed = Input.GetAxis("Vertical") * currentSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * currentSpeed;

        Vector3 speedDirection = (transform.forward * forwardSpeed) + (transform.right * sideSpeed);

        moveDirection.x = speedDirection.x;
        moveDirection.z = speedDirection.z;

        if (characterController.isGrounded)
        {
            animator.SetFloat("Jump", 0f);

            if (moveDirection.y < 0)
            {
                moveDirection.y = -2f;
            }
            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
                animator.SetFloat("Jump", 1f);
            }
        }
        else
        {
            //(moveDirection.y < -1f)
        }

        moveDirection.y += gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        Vector3 localVelocity = transform.InverseTransformDirection(characterController.velocity);
        animator.SetFloat("Postx", localVelocity.x);
        animator.SetFloat("Postz", localVelocity.z);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, topLookLimit, bottomLookLimit);

        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentObject != null)
            {
                currentObject.Drop();
                currentObject = null;
            }
            else
            {
                RaycastHit hit;
                if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, grabDistance))
                {
                    Object pickable = hit.collider.GetComponent<Object>();

                    if (pickable != null)
                    {
                        currentObject = pickable;
                        currentObject.Grab(holdPoint);
                    }
                }
            }
        }
    }
}