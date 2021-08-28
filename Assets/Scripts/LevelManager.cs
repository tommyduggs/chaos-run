using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelOne;
    [SerializeField] private GameObject levelTwo;
    [SerializeField] private GameManager gameManager;
    private GameObject currentLevelObj;
    private Vector3 lastCheckpointPosition;
    private int currentLevel = 0;
    // Start is called before the first frame update

    public void NextLevel()
    {
        currentLevel++;

        switch(currentLevel)
        {
            case 1:
                currentLevelObj = Instantiate(levelOne, levelOne.transform.position, levelOne.transform.rotation);
                break;
            case 2:
                currentLevelObj = Instantiate(levelTwo, levelTwo.transform.position, levelTwo.transform.rotation);
                break;
        }

        currentLevelObj.GetComponent<MoveLeft>().gameManager = gameManager;
    }

    public void RestartLevel()
    {
        GameObject.Destroy(currentLevelObj);

        switch(currentLevel)
        {
            case 1:
                currentLevelObj = Instantiate(levelOne, lastCheckpointPosition, levelOne.transform.rotation);
                break;
            case 2:
                currentLevelObj = Instantiate(levelTwo, lastCheckpointPosition, levelTwo.transform.rotation);
                break;
        }

        currentLevelObj.GetComponent<MoveLeft>().gameManager = gameManager;
    }

    public void SetCheckpoint()
    {
        lastCheckpointPosition = currentLevelObj.transform.position;
    }
}
