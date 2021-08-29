using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject menuOptions;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject menuFirst;
    [SerializeField] GameObject creditsFirst;
    private EventSystem eventSystem;
    void Start()
    {
        eventSystem = EventSystem.current;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Credits()
    {
        credits.SetActive(true);
        menuOptions.SetActive(false);
        eventSystem.SetSelectedGameObject(creditsFirst);
    }
    public void CloseCredits()
    {
        credits.SetActive(false);
        menuOptions.SetActive(true);
        eventSystem.SetSelectedGameObject(menuFirst);
    }
}
