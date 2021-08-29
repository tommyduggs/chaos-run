using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TutorialLevelManager tutorialLevelManager;
    private int currentMessage = 0;
    // Start is called before the first frame update
    void Start()
    {
        ShowNextMessage();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && currentMessage == 1)
        {
            ShowNextMessage();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && currentMessage == 2)
        {
            ShowNextMessage();
        }
        else if (Input.GetKeyDown(KeyCode.W) && currentMessage == 4)
        {
            ShowNextMessage();
        }
        else if (Input.GetKeyDown(KeyCode.R) && currentMessage == 6)
        {
            ShowNextMessage();
        }
        else if(Input.anyKeyDown && currentMessage == 7)
        {
            gameManager.HideMessagesImmediately();
            tutorialLevelManager.StartGame();
        }
        // remeber to delete this
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            gameManager.HideMessagesImmediately();
            tutorialLevelManager.StartGame();
        }

    }

    private void ShowNextMessage()
    {
        currentMessage++;

        switch(currentMessage)
        {
            case 1:
                gameManager.DisplayMessageAndKeepOnScreen("Welcome to the tutorial! Press any key to begin");
                break;
            case 2:
                gameManager.DisplayMessageAndKeepOnScreen("Press Q to jump");
                break;
            case 3:
                gameManager.DisplayMessageAndKeepOnScreen("You can press Q again in the air to double jump");
                break;
            case 4:
                gameManager.DisplayMessageAndKeepOnScreen("Press W to float jump");
                break;
            case 5:
                gameManager.DisplayMessageAndKeepOnScreen("Press E to start a torpedo jump. You'll have to press E again in the air to execute it");
                break;
            case 6:
                gameManager.DisplayMessageAndKeepOnScreen("Press R to slide");
                break;
            case 7:
                gameManager.DisplayMessageAndKeepOnScreen("Good job! Press any key to start the game");
                break;
        }
    }

    public void DoubleJump()
    {
        if (currentMessage == 3)
        {
            ShowNextMessage();
        }
    }

    public void TorpedoJump()
    {
        if (currentMessage == 5)
        {
            ShowNextMessage();
        }
    }
}
