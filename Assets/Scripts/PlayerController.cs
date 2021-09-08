using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    //[SerializeField] private TextMeshProUGUI gemText;
    //[SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TutorialController tutorialController;
    [SerializeField] private TutorialLevelManager tutorialLevelManager;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource doubleJumpSound;
    [SerializeField] private AudioSource floatSound;
    [SerializeField] private AudioSource torpedoJumpSound;
    [SerializeField] private AudioSource torpedoAttackSound;
    [SerializeField] private AudioSource slideSound;
    [SerializeField] private AudioSource hurtSound;
    [SerializeField] private AudioSource runningSound;
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
    private int lives = 99;
    private Vector3 initialPlayerPosition;
    private int gemCount = 0;
    public float bounceForce = 18f;
    private float playerInitialPosX;
    private bool isPlayerHurt = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();

        initialGravityScale = playerRb.gravityScale;
        initialPlayerPosition = transform.position;
        
        //livesText.text = "LIVES: " + lives;

        playerInitialPosX = transform.position.x - 1.0f;

        Run();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < playerInitialPosX && gameManager.gameActive && !isPlayerHurt)
        {
            PlayerHurt();
        }
        float verticalVelocity = playerRb.velocity.y;
        animator.SetFloat("verticalVelocity", verticalVelocity);

        ProcessInputs();
    }

    private void ProcessInputs()
    {
        if(!gameManager.menuOpen)
        {
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
    }

    public void GroundPlayer()
    {
        if((isFloating || isTorpedoJumping) && !isPlayerHurt)
        {
            animator.SetTrigger("run");
        }
        
        if(!isPlayerHurt)
        {
            runningSound.Play();
        }
        playerRb.gravityScale = initialGravityScale;
        isJumping = false;
        isFloating = false;
        isTorpedoJumping = false;
        isTorpedoAttacking = false;
        usedDoubleJump = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Checkpoint"))
        {
            gameManager.SetCheckpoint();
        }
        else if (other.CompareTag("Enemy") && !isPlayerHurt)
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
        else if(other.CompareTag("FloatTrigger"))
        {
            FloatJump();
        }
        else if(other.CompareTag("TorpedoTrigger"))
        {
            TorpedoJump();
        }
        else if(other.CompareTag("SlideTrigger"))
        {
            Slide();
        }
        else if (other.CompareTag("EndGameTrigger"))
        {
            runningSound.Stop();
            gameManager.StopGame();
            gameManager.DisplayMessageAndKeepOnScreen("Wow, you did it! Congratulations!");
            gameManager.BeatGame();
            animator.SetTrigger("idle");
            animator.SetBool("isRunning", false);
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

        //gemText.text = gemCount.ToString();
        //livesText.text = "LIVES: " + lives;
    }

    private void PlayerHurt()
    {
        runningSound.Pause();
        hurtSound.Play();
        isPlayerHurt = true;
        animator.SetTrigger("playerDied");
        lives--;
        //livesText.text = "LIVES: " + lives;
        gameManager.StopGame();

        StartCoroutine(SpawnFromLastCheckpoint());

        // if (lives == 0 && !GameManager.testingMode)
        // {
        //     // TODO: Build logic that shows Game Over text
        // }
        // else
        // {
        //     StartCoroutine(SpawnFromLastCheckpoint());
        // }
    }

    private void Run()
    {
        isRunning = !isRunning;
        animator.SetBool("isRunning", isRunning);
        runningSound.Play();
    }

    private void Jump()
    {
        if (!isJumping && !isSliding && !isFloating && !isTorpedoJumping && !isPlayerHurt)
        {
            runningSound.Pause();
            jumpSound.Play();
            playerRb.velocity = Vector2.zero;
            animator.SetTrigger("jump");
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumping = true;
        }
        // Double Jump
        else if (isJumping && !isTorpedoJumping && !usedDoubleJump && !isPlayerHurt)
        {
            doubleJumpSound.Play();
            animator.SetTrigger("jump");
            playerRb.velocity = Vector2.zero;
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            usedDoubleJump = true;

            // for tutorial
            if(tutorialLevelManager.tutorialActive)
            {
                tutorialController.DoubleJump();
            }
        }
    }

    private void FloatJump()
    {
        if (!isJumping && !isSliding && !isFloating && !isPlayerHurt)
        {
            runningSound.Pause();
            floatSound.Play();
            animator.SetTrigger("float");
            playerRb.gravityScale = floatJumpGravity;
            playerRb.AddForce(Vector2.up * floatJumpForce, ForceMode2D.Impulse);
            isFloating = true;
        }
    }

    private void TorpedoJump()
    {
        if (!isJumping && !isSliding && !isFloating && !isPlayerHurt)
        {
            runningSound.Pause();
            torpedoJumpSound.Play();
            playerRb.gravityScale = torpedoJumpGravity;
            playerRb.AddForce(Vector2.up * torpedoJumpForce, ForceMode2D.Impulse);
            isJumping = true;
            isTorpedoJumping = true;
            animator.SetTrigger("torpedoJump");
        }

        else if (isJumping && isTorpedoJumping && !isTorpedoAttacking && !isPlayerHurt)
        {
            torpedoAttackSound.Play();
            isTorpedoAttacking = true;
            playerRb.gravityScale = 0.0f;
            playerRb.velocity = Vector2.zero;
            animator.SetTrigger("torpedoAttack");
            StartCoroutine(TorpedoTimeout());

            // for tutorial
            if(tutorialLevelManager.tutorialActive)
            {
                tutorialController.TorpedoJump();
            }
        }
    }

    private void Slide()
    {
        if (!isJumping && !isSliding && !isFloating && !isPlayerHurt)
        {
            runningSound.Pause();
            slideSound.Play();
            isSliding = true;
            animator.SetBool("isSliding", isSliding);
            StartCoroutine(SlideTimeout());
        }
    }

    public void Bounce()
    {
        jumpSound.Play();
        playerRb.gravityScale = initialGravityScale;
        playerRb.velocity = Vector2.zero;
        playerRb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
    }
    IEnumerator SpawnFromLastCheckpoint()
    {
        yield return new WaitForSeconds(2.0f);
        isPlayerHurt = false;
        isJumping = false;
        isFloating = false;
        isTorpedoJumping = false;
        isTorpedoAttacking = false;
        usedDoubleJump = false;
        playerRb.gravityScale = initialGravityScale;
        transform.position = initialPlayerPosition;
        animator.SetTrigger("run");
        gameManager.RestartGame();
    }
    IEnumerator SlideTimeout()
    {
        yield return new WaitForSeconds(1.0f);
        if (!isPlayerHurt)
        {
            runningSound.Play();
            isSliding = false;
            animator.SetBool("isSliding", isSliding);
        }
        else
        {
            isSliding = false;
            animator.SetBool("isSliding", false);
        }
    }

    IEnumerator TorpedoTimeout()
    {
        yield return new WaitForSeconds(0.5f);
        if (!isPlayerHurt)
        {
            playerRb.gravityScale = torpedoJumpGravity;
            if(isTorpedoJumping)
            {
                animator.SetTrigger("torpedoJump");
            }
        }
    }
}
