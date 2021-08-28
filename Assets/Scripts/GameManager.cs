using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject onScreenText;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject startMenuFirst;
    [SerializeField] private GameObject tutorialFirst;
    private EventSystem eventSystem;
    public static bool menuOpen = false;
    public bool gameActive = false;
    // Start is called before the first frame update
    void Start()
    {
        eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameActive && !menuOpen && gameActive)
            {
                OpenStartMenu();
            }
            else if(menuOpen)
            {
                ResumeGame();
            }
        }
    }

    public void StartGame()
    {
        gameActive = true;
    }

    public void RestartGame()
    {
        levelManager.RestartLevel();
        gameActive = true;
    }

    public void StopGame()
    {
        gameActive = false;
    }

    public void SetCheckpoint()
    {
        levelManager.SetCheckpoint();
    }

    public void DisplayMessage(string text)
    {
        TextWriter.AddWriter_Static(messageText, text, 0.05f, true, true, EndMessages);
        StartMessage();
    }
    public void DisplayMessageAndKeepOnScreen(string text)
    {
        TextWriter.AddWriter_Static(messageText, text, 0.05f, true, true, null);
        StartMessage();
    }
    private void StartMessage()
    {
        //StartTalkingSound();
        onScreenText.SetActive(true);
    }
    private void EndMessages()
    {
        //StopTalkingSound();
        StartCoroutine(HideMessages());
    }
    public IEnumerator HideMessages()
    {
        yield return new WaitForSeconds(2f);
        onScreenText.SetActive(false);
    }
    public void HideMessagesImmediately()
    {
        onScreenText.SetActive(false);
    }

    private void OpenStartMenu()
    {
        menuOpen = true;
        gameActive = false;
        Time.timeScale = 0f;
        startMenu.SetActive(true);
        eventSystem.SetSelectedGameObject(startMenuFirst);
    }

    public void ResumeGame()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1f;
        gameActive = true;
        menuOpen = false;
    }

    public void CheatCodes()
    {

    }

    public void ExitGame()
    {

    }
}
