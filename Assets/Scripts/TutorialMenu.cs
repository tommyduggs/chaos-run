using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    [SerializeField] private TutorialController tutorialController;
    [SerializeField] private GameObject tutorialButtons;
    [SerializeField] private TutorialLevelManager tutorialLevelManager;
    // Start is called before the first frame update
    void Start()
    {
        tutorialButtons.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTutorial()
    {
        tutorialController.gameObject.SetActive(true);
        tutorialButtons.gameObject.SetActive(false);
    }

    public void SkipTutorial()
    {
        tutorialLevelManager.StartGame();
        tutorialButtons.gameObject.SetActive(false);
    }
}
