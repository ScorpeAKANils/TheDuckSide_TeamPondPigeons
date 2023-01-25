using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenue : MonoBehaviour
{
    [SerializeField]GameObject[] m_MenuStuff;
    [SerializeField]GameObject[] m_Stats;
    [SerializeField] GameObject[] m_Option; 
    [SerializeField] TextMeshProUGUI killCounterTXT;
    [SerializeField] TextMeshProUGUI HighscoreTXT;
    int kills;
    float highscore; 
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        kills = PlayerPrefs.GetInt("killCounter");
        highscore = PlayerPrefs.GetFloat("Highscore");
    }
    //start Button
    public void Level1()
    {
        SceneManager.LoadScene(1); 
    }

    public void Stats()
    {
        foreach (GameObject menu in m_MenuStuff)
        {
            menu.SetActive(false); 
        }

        foreach (GameObject options in m_Option)
        {
            options.SetActive(false);
        }

        foreach (GameObject stats in m_Stats)
        {
            stats.SetActive(true); 
        }

        killCounterTXT.text = "Kills: " + kills.ToString();
        HighscoreTXT.text = "Highscore: " + highscore.ToString();        
    }


    public void back()
    {
        foreach (GameObject gameObject in m_MenuStuff)
        {
            gameObject.SetActive(true);
        }

        foreach (GameObject game in m_Stats)
        {
            game.SetActive(false);
        }

        foreach (GameObject gameObject in m_Option)
        {
            gameObject.SetActive(false);
        }
    }

    public void Options()
    {
        foreach (GameObject options in m_Option)
        {
            options.SetActive(true);
        }

        foreach (GameObject stats in m_Stats)
        {
            stats.SetActive(false);
        }


        foreach (GameObject menu in m_MenuStuff)
        {
            menu.SetActive(false);
        }
    }
    //Exit button
    public void Exit()
    {
     Application.Quit(); 
    }
}
