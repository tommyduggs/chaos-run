using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    private float moveSpeed = 5f;
    private bool isMoving = false;
    private float upperBound;
    private float lowerBound;
    private float flightRange = 2f;
    private bool movingUp;
    // Start is called before the first frame update
    void Start()
    {
        upperBound = transform.parent.position.y + flightRange;
        
        lowerBound = transform.parent.position.y - flightRange;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            MoveUpAndDown();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isMoving = true;
        }
    }

    private void MoveUpAndDown()
    {
        if(movingUp)
        {
            transform.parent.Translate(Vector2.up * moveSpeed * Time.deltaTime);

            if (transform.parent.position.y > upperBound)
            {
                movingUp = false;
            }
        }
        else
        {
            transform.parent.Translate(Vector2.down * moveSpeed * Time.deltaTime);

            if (transform.parent.position.y < lowerBound)
            {
                movingUp = true;
            }
        }
    }
}
