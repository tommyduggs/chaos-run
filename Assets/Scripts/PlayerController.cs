using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D playerRb;
    private bool isRunning;
    public float jumpForce = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalVelocity = playerRb.velocity.y;
        animator.SetFloat("verticalVelocity", verticalVelocity);

        if(Input.GetKeyDown(KeyCode.Q))
        {
            Run();
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    private void Run()
    {
        isRunning = !isRunning;
        animator.SetBool("isRunning", isRunning);
    }

    private void Jump()
    {
        playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
