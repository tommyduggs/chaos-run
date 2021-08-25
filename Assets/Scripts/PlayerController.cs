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
    private float jumpForce = 13.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();

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
            isJumping = false;
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
        yield return new WaitForSeconds(0.5f);
        isSliding = false;
        animator.SetBool("isSliding", isSliding);
    }
}
