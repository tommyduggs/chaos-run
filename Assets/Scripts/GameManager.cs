using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject onScreenText;
    public GameObject level;
    public bool gameActive;
    private float moveSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        gameActive = true;

        DisplayMessage("Testing UI Message");
    }

    // Update is called once per frame
    void Update()
    {
        if(gameActive)
        {
            MoveGridLeft();
        }
    }

    public void RestartGame()
    {
        gameActive = true;
    }

    public void EndGame()
    {
        gameActive = false;
    }

    private void MoveGridLeft()
    {
        level.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }

    public void DisplayMessage(string text)
    {
        TextWriter.AddWriter_Static(messageText, text, 0.05f, true, true, EndMessages);
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
    private IEnumerator HideMessages()
    {
        yield return new WaitForSeconds(2f);
        onScreenText.SetActive(false);
    }
}
