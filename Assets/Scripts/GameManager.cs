using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject onScreenText;
    public bool gameActive = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RestartGame()
    {
        gameActive = true;
    }

    public void EndGame()
    {
        gameActive = false;
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
}
