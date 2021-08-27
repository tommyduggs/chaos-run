using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : MonoBehaviour
{
    private float moveSpeed = 15f;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MoveLeft();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
        }
    }

    private void MoveLeft()
    {
        transform.parent.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    public void StopMovement()
    {
        isMoving = false;
    }
}
