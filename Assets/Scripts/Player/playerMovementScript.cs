using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Animator animator;

    private playerStats stats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stats = GetComponent<playerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = movementInput * stats.moveSpeed;
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
