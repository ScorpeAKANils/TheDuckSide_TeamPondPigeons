using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenue : MonoBehaviour
{
    public bool EndLevel; 


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EndLevel = true; 
            SceneManager.LoadScene(0);
            
        }
    }
}
