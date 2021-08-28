using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject level;
    private Animator animator;
    private Rigidbody2D playerRb;
    private bool isRunning = false;
    private bool isJumping = false;
    private bool isFloating = false;
    private bool isSliding = false;
    private bool isTorpedoJumping = false;
    private bool isTorpedoAttacking = false;
    private bool usedDoubleJump = false;
    public float jumpForce = 13f;
    public float floatJumpForce = 9f;
    public float floatJumpGravity = 1f;
    public float torpedoJumpForce = 28f;
    public float torpedoJumpGravity = 7f;
    private float initialGravityScale;
    private float lastCheckpointPosition;
    private int lives = 3;
    private Vector3 initialPlayerPosition;
    private int gemCount = 0;
    public float bounceForce = 18f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();

        initialGravityScale = playerRb.gravityScale;
        initialPlayerPosition = transform.position;

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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            if(isFloating || isTorpedoJumping)
            {
                animator.SetTrigger("run");
            }
            playerRb.gravityScale = initialGravityScale;
            isJumping = false;
            isFloating = false;
            isTorpedoJumping = false;
            isTorpedoAttacking = false;
            usedDoubleJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Checkpoint"))
        {
            lastCheckpointPosition = level.transform.position.x;
        }
        else if (other.CompareTag("Enemy"))
        {
            PlayerHurt();
        }
        else if(other.CompareTag("Gem"))
        {
            GetGem(other.gameObject);
        }
        else if(other.CompareTag("JumpTrigger"))
        {
            Jump();
        }
        else if(other.CompareTag("SlideTrigger"))
        {
            Slide();
        }
    }

    private void GetGem(GameObject gem)
    {
        Destroy(gem);
        gemCount++;
        if(gemCount == 10)
        {
            lives++;
            gemCount = 0;
        }
    }

    private void PlayerHurt()
    {
        animator.SetTrigger("playerDied");
        lives--;
        gameManager.EndGame();

        if (lives == 0)
        {
            // TODO: Build logic that shows Game Over text
        }
        else
        {
            StartCoroutine(SpawnFromLastCheckpoint());
        }
    }

    private void Run()
    {
        isRunning = !isRunning;
        animator.SetBool("isRunning", isRunning);
    }

    private void Jump()
    {
        if (!isJumping && !isSliding && !isFloating)
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
        if (!isJumping && !isSliding && !isFloating)
        {
            animator.SetTrigger("float");
            playerRb.gravityScale = floatJumpGravity;
            playerRb.AddForce(Vector2.up * floatJumpForce, ForceMode2D.Impulse);
            isFloating = true;
        }
    }

    private void TorpedoJump()
    {
        if (!isJumping && !isSliding && !isFloating)
        {
            playerRb.gravityScale = torpedoJumpGravity;
            playerRb.AddForce(Vector2.up * torpedoJumpForce, ForceMode2D.Impulse);
            isJumping = true;
            isTorpedoJumping = true;
            animator.SetTrigger("torpedoJump");
        }

        else if (isJumping && isTorpedoJumping && !isTorpedoAttacking)
        {
            isTorpedoAttacking = true;
            playerRb.gravityScale = 0.0f;
            playerRb.velocity = Vector2.zero;
            animator.SetTrigger("torpedoAttack");
            StartCoroutine(TorpedoTimeout());
        }
    }

    private void Slide()
    {
        if (!isJumping && !isSliding && !isFloating)
        {
            isSliding = true;
            animator.SetBool("isSliding", isSliding);
            StartCoroutine(SlideTimeout());
        }
    }

    public void Bounce()
    {
        playerRb.velocity = Vector2.zero;
        playerRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }
    IEnumerator SpawnFromLastCheckpoint()
    {
        yield return new WaitForSeconds(3.0f);
        level.transform.position = new Vector3(lastCheckpointPosition, level.transform.position.y, level.transform.position.z);
        transform.position = initialPlayerPosition;
        gameManager.RestartGame();
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
        animator.SetTrigger("torpedoJump");
    }
}
