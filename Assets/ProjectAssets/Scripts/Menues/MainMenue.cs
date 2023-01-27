using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class MainMenue : MonoBehaviour
{
    [SerializeField] GameObject[] m_MenuStuff;
    [SerializeField] GameObject[] m_Stats;
    [SerializeField] GameObject[] m_Option;
    [SerializeField] GameObject[] m_Sound;
    [SerializeField] GameObject[] m_Graphics;
    [SerializeField] TextMeshProUGUI killCounterTXT;
    [SerializeField] TextMeshProUGUI HighscoreTXT;
    int kills;
    bool hasBreak=false; 
    float highscore;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        kills = PlayerPrefs.GetInt("killCounter");
        highscore = PlayerPrefs.GetFloat("Highscore");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !hasBreak)
        {
            Time.timeScale = 0.000000f;
           //Debug.Log(Time.deltaTime);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true; 
            hasBreak = true;    
            foreach (GameObject game in m_MenuStuff)
            {
                game.SetActive(true);
            }
        }else if (Input.GetKeyDown(KeyCode.Escape) && hasBreak)
        {
            Time.timeScale = 1f;
            hasBreak = false; 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            CloseMenu();
        }
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

    public void BackToIGM()
    {
        foreach (GameObject Graphics in m_Graphics)
        {
            Graphics.SetActive(false);
        }

        foreach (GameObject menu in m_MenuStuff)
        {
            menu.SetActive(true);
        }

        foreach (GameObject sound in m_Sound)
        {
            sound.SetActive(false);
        }


        foreach (GameObject Option in m_Option)
        {
            Option.SetActive(false);
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

    public void SoundSettings()
    {
        foreach (GameObject sound in m_Sound)
        {
            sound.SetActive(true);
        }

        foreach (GameObject menu in m_MenuStuff)
        {
            menu.SetActive(false);
        }

        foreach (GameObject Graphic in m_Graphics)
        {
            Graphic.SetActive(false);
        }


        foreach (GameObject Option in m_Option)
        {
            Option.SetActive(false);
        }
    }

    public void GraphicSettings()
    {
        foreach (GameObject Graphics in m_Graphics)
        {
            Graphics.SetActive(true);
        }

        foreach (GameObject menu in m_MenuStuff)
        {
            menu.SetActive(false);
        }

        foreach (GameObject sound in m_Sound)
        {
            sound.SetActive(false);
        }


        foreach (GameObject Option in m_Option)
        {
            Option.SetActive(false);
        }
    }

    public void BackToSettings()
    {
        foreach (GameObject Graphics in m_Graphics)
        {
            Graphics.SetActive(false);
        }

        foreach (GameObject menu in m_MenuStuff)
        {
            menu.SetActive(false);
        }

        foreach (GameObject sound in m_Sound)
        {
            sound.SetActive(false);
        }


        foreach (GameObject Option in m_Option)
        {
            Option.SetActive(true);
        }
    }
    public void BackToMainMenue()
    {

        Time.timeScale = 1; 
        SceneManager.LoadScene(0); 
    }

    //Exit button

    public void CloseMenu()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        foreach (GameObject Graphics in m_Graphics)
        {
            Graphics.SetActive(false);
        }

        foreach (GameObject menu in m_MenuStuff)
        {
            menu.SetActive(false);
        }

        foreach (GameObject sound in m_Sound)
        {
            sound.SetActive(false);
        }


        foreach (GameObject Option in m_Option)
        {
            Option.SetActive(false);
        }
    }

    public void Restartlevel()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void Exit()
    {
     Application.Quit(); 
    }
}
