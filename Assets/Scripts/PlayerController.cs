using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private Animator animator;
    private Rigidbody2D playerRb;
    private bool isRunning = false;
    private bool isJumping = false;
    private bool isSliding = false;
    private bool isTorpedoJumping = false;
    private bool usedDoubleJump = false;
    public float jumpForce = 13f;
    public float floatJumpForce = 9f;
    public float floatJumpGravity = 1f;
    public float torpedoJumpForce = 28f;
    public float torpedoJumpGravity = 7f;
    private float initialGravityScale;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();

        initialGravityScale = playerRb.gravityScale;

        Run();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalVelocity = playerRb.velocity.y;
        animator.SetFloat("verticalVelocity", verticalVelocity);

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            FloatJump();
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            TorpedoJump();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            Slide();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("JumpTrigger"))
        {
            Jump();
        }
        else if(other.CompareTag("SlideTrigger"))
        {
            Slide();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            playerRb.gravityScale = initialGravityScale;
            isJumping = false;
            isTorpedoJumping = false;
            usedDoubleJump = false;
        }
        else if (col.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("playerDied");
            gameManager.EndGame();
        }
    }

    private void Run()
    {
        isRunning = !isRunning;
        animator.SetBool("isRunning", isRunning);
    }

    private void Jump()
    {
        if (!isJumping && !isSliding)
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
        // Double Jump
        else if (isJumping && !isTorpedoJumping && !usedDoubleJump)
        {
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            usedDoubleJump = true;
        }
    }

    private void FloatJump()
    {
        if (!isJumping && !isSliding)
        {
            playerRb.gravityScale = floatJumpGravity;
            playerRb.AddForce(Vector2.up * floatJumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void TorpedoJump()
    {
        if (!isJumping && !isSliding)
        {
            playerRb.gravityScale = torpedoJumpGravity;
            playerRb.AddForce(Vector2.up * torpedoJumpForce, ForceMode2D.Impulse);
            isJumping = true;
            isTorpedoJumping = true;
        }

        else if (isJumping && isTorpedoJumping)
        {
            playerRb.gravityScale = 0.0f;
            playerRb.velocity = Vector2.zero;
            StartCoroutine(TorpedoTimeout());
        }
    }

    private void Slide()
    {
        if (!isJumping && !isSliding)
        {
            isSliding = true;
            animator.SetBool("isSliding", isSliding);
            StartCoroutine(SlideTimeout());
        }
    }

    IEnumerator SlideTimeout()
    {
        yield return new WaitForSeconds(1.0f);
        isSliding = false;
        animator.SetBool("isSliding", isSliding);
    }

    IEnumerator TorpedoTimeout()
    {
        yield return new WaitForSeconds(1.0f);
        playerRb.gravityScale = torpedoJumpGravity;
    }
}
