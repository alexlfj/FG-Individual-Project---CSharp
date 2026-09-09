using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = movementInput * movementSpeed;
    }

    public void Movement(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking",false);
            animator.SetFloat("LastInputX", movementInput.x);
            animator.SetFloat("LastInputY", movementInput.y);
        }
        movementInput = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", movementInput.x);
        animator.SetFloat("InputY", movementInput.y);
        
    }

}
