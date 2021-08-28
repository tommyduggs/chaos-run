using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public GameManager gameManager;
    private float moveSpeed = 15f;

    // Update is called once per frame
    void Update()
    {
        if(gameManager.gameActive)
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }
}
