using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f; // Movement speed
    [SerializeField] float jumpForce = 5f; // Jump force
    [SerializeField] LayerMask groundLayer; // Layer to define what is ground
    [SerializeField] Transform groundCheck; // Empty GameObject to check if player is grounded
    [SerializeField] float groundCheckRadius = 0.2f; // Radius for ground checking
    private Rigidbody rb;
    private bool isGrounded;

    // Animator reference
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // Ensure Animator is attached to the same GameObject
    }

    void Update()
    {
        Move();
        Jump();
        HandleAnimations(); // Call animation handling method

        if (Time.timeScale == 1)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(transform.position + move * moveSpeed * Time.deltaTime);
    }

    void Jump()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void HandleAnimations()
    {
        // Get the player's movement magnitude
        float moveMagnitude = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).magnitude;

        // Set Animator parameters
        animator.SetFloat("Speed", moveMagnitude); // Assuming 'Speed' is a float parameter in the Animator for running/walking
        animator.SetBool("isGrounded", isGrounded); // Boolean parameter for jumping/grounded

        // Jumping animation trigger
        if (!isGrounded)
        {
            animator.SetTrigger("Jump"); // Assuming 'Jump' is a trigger in the Animator for the jump animation
        }
    }
}
