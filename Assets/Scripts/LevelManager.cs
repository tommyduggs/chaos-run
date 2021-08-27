using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelOne;
    [SerializeField] private GameManager gameManager;
    private float moveSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.gameActive)
        {
            Debug.Log("Game is active in level manager");
            MoveGridLeft();
        }
    }

    public void StartLevel()
    {

    }

    private void MoveGridLeft()
    {
        levelOne.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
