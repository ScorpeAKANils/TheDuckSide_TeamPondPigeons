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
    void Update()
    {
        timeCounter += Time.deltaTime;
        TimeCountertxt.text = "Time: " + timeCounter.ToString(); 
        if (Highscore<= 0 && endLevel.EndLevel || Highscore > timeCounter && endLevel.EndLevel)
        {
            Highscore = timeCounter;
            PlayerPrefs.SetFloat("Highscore", Highscore); 
        }
    }
}
