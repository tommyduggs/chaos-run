using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject level;
    public bool gameActive;
    private float moveSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameActive)
        {
            MoveGridLeft();
        }
    }

    public void EndGame()
    {
        gameActive = false;
    }

    private void MoveGridLeft()
    {
        level.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
