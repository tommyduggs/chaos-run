using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Grid grid;
    public bool gameActive;
    private float moveSpeed = 13f;
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
        grid.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
