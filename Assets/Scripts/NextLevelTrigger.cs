using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EndLevelTrigger"))
        {
            Debug.Log("Next level trigger");
            levelManager.NextLevel();    
        }
    }
}
