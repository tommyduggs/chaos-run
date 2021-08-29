using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private GameObject bgOne;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TutorialLevelManager tutorialLevelManager;
    private float bgOneRepeatWidth;
    private Vector3 bgOneStartPosition;
    private float moveSpeed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        bgOneRepeatWidth = bgOne.GetComponent<BoxCollider2D>().size.x / 2;
        bgOneStartPosition = bgOne.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (bgOne.transform.position.x < bgOneStartPosition.x - bgOneRepeatWidth && tutorialLevelManager.tutorialActive)
        {
            // if (startGamePending)
            // {
            //     gameManager.StartGame();
            //     levelManager.NextLevel();
            //     tutorialActive = false;
            // }
            
            bgOne.transform.position = bgOneStartPosition;
        }

        MoveBGLeft();
    }

    private void MoveBGLeft()
    {
        if(gameManager.gameActive || tutorialLevelManager.tutorialActive)
        {
            bgOne.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }
}
