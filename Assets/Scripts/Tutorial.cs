using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
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
        else if (Input.GetKeyDown(KeyCode.W) && currentMessage == 2)
        {
            currentMessage++;
            ShowNextMessage();
        }
        else if(Input.anyKeyDown && currentMessage == 3)
        {
            gameManager.HideMessagesImmediately();
            tutorialLevelManager.StartGame();
        }
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
            case 3:
                gameManager.DisplayMessageAndKeepOnScreen("That's it! Press any key to start.");
                break;
        }
    }
}
