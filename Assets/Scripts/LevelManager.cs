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
    private int currentLevel = 0;
    // Start is called before the first frame update

    public void NextLevel()
    {
        currentLevel++;

        switch(currentLevel)
        {
            case 1:
                Instantiate(levelOne, levelOne.transform.position, levelOne.transform.rotation);
                break;
            case 2:
                Instantiate(levelTwo, levelOne.transform.position, levelOne.transform.rotation);
                break;
        }
    }
}
