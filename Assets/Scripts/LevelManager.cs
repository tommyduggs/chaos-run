using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Tilemap tutorialTilemap;
    [SerializeField] private GameObject tutorialLevel;
    [SerializeField] private GameManager gameManager;
    private float tutorialRepeatWidth;
    private Vector3 tutorialStartPosition;
    private float moveSpeed = 15f;
    private int currentMessage = 0;
    // Start is called before the first frame update
    void Start()
    {
        tutorialRepeatWidth = tutorialTilemap.size.x / 2;

        tutorialStartPosition = tutorialLevel.transform.position;

        ShowNextMessage();
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialLevel.transform.position.x < tutorialStartPosition.x - tutorialRepeatWidth)
        {
            tutorialLevel.transform.position = tutorialStartPosition;
        }

        MoveGridLeft();

        if(Input.anyKeyDown && currentMessage == 0)
        {
            currentMessage++;
            ShowNextMessage();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currentMessage == 1)
        {
            currentMessage++;
            ShowNextMessage();
        }
    }

    private void MoveGridLeft()
    {
        tutorialLevel.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    private void ShowNextMessage()
    {
        switch(currentMessage)
        {
            case 0:
                gameManager.DisplayMessageAndKeepOnScreen("Welcome to the tutorial");
                break;
            case 1:
                gameManager.DisplayMessageAndKeepOnScreen("Press Q to jump");
                break;
            case 2:
                gameManager.DisplayMessageAndKeepOnScreen("Press W to float jump");
                break;
        }
    }
}
