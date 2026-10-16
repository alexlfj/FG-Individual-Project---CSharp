using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movementInput;
    private Animator animator;
    private playerStats stats;
    [SerializeField] private AudioClip[] footsteps;
    private float lastStepTime;

    [SerializeField] private float stepSoundCooldown = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stats = GetComponent<playerStats>();
        lastStepTime = 0;
    }

    
    void FixedUpdate()
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

    public void PlayFootstep()
    {
        float diff = Time.time - lastStepTime;

        if (diff < stepSoundCooldown) { return; }

        if (SoundFXManager.Instance == null)
        {
            Debug.LogWarning("SoundFXManager not found in scene -> footstep sound skipped.");
            return;
        }
        else
        {
            SoundFXManager.Instance.PlayRandomAudioClips(footsteps, transform, 2f);
            lastStepTime = Time.time;
        }
    }
}
