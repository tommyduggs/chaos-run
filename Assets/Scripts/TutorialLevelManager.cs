using UnityEngine;
using UnityEngine.Tilemaps;

public class TutorialLevelManager : MonoBehaviour
{
    [SerializeField] private Tilemap tutorialTilemap;
    [SerializeField] private GameObject tutorialLevel;
    [SerializeField] private GameManager gameManager;
    private float tutorialRepeatWidth;
    private Vector3 tutorialStartPosition;
    private float moveSpeed = 15f;
    private bool tutorialActive = true;
    private bool startGamePending = false;
    // Start is called before the first frame update
    void Start()
    {
        tutorialRepeatWidth = tutorialTilemap.size.x / 2;

        tutorialStartPosition = tutorialLevel.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialActive)
        {
            if (tutorialLevel.transform.position.x < tutorialStartPosition.x - tutorialRepeatWidth)
            {
                if (startGamePending)
                {
                    gameManager.RestartGame();
                    tutorialActive = false;
                }
                
                tutorialLevel.transform.position = tutorialStartPosition;
            }
        }

        MoveGridLeft();
    }

    private void MoveGridLeft()
    {
        tutorialLevel.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    public void StartGame()
    {
        startGamePending = true;
    }
}
