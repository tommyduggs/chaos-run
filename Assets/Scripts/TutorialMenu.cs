using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    [SerializeField] private Tutorial tutorial;
    [SerializeField] private GameObject tutorialButtons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTutorial()
    {
        tutorial.gameObject.SetActive(true);
        tutorialButtons.gameObject.SetActive(false);
    }
}
