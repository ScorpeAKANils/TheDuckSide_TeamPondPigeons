using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    float timeCounter;
    float Highscore;
    [SerializeField] TextMeshProUGUI TimeCountertxt;
    [SerializeField] TextMeshProUGUI Highscoretxt;
 

    [SerializeField] BackToMenue endLevel; 
    private void Start()
    {
        Highscore = PlayerPrefs.GetFloat("Highscore");
        Highscoretxt.text = "Highscore: " + Highscore.ToString(); 
    }
  
    // Update is called once per frame
    void FixedUpdate()
    {
        //timeCounter += Time.deltaTime;
        timeCounter = Mathf.Round((timeCounter + Time.deltaTime) * 100) / 100; 
        TimeCountertxt.text = "Time: " + timeCounter.ToString(); 
       
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<BackToMenue>())
        {
            if (Highscore <= 0 && endLevel.EndLevel || Highscore > timeCounter && endLevel.EndLevel)
            {
                Highscore = timeCounter;
                PlayerPrefs.SetFloat("Highscore", Highscore);
            }
        }
    }
}
