using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float max_player_health = 7;
    public float player_health;
    public int healthSprites;
    public bool vulnerable = true;
    public float invulnerabilityFrameDuration = 0.2f;
    public float damageMultiplier = 1.0f;
    public Image[] lifeBar;
    public Sprite lifeSprite;
    int sceneIndex;
    float fullDamage;
    Animator playerAnim;
    AudioSource playAudio;
    public AudioClip quack;
    [SerializeField] AudioSource DeathMucke;
    [SerializeField] AudioSource anderemucke;
    [SerializeField] GameObject DeathScreenIMG;
    private void Awake()
    {
        DeathScreenIMG.SetActive(false);
        playerAnim = this.GetComponent<Animator>();
    }

    private void Start()
    {
        player_health = max_player_health;
        healthSprites = (int)max_player_health;
        playAudio = this.GetComponent<AudioSource>();
    }

    public void GetDamage(float damage)
    {
        if (vulnerable)
        {
            playAudio.PlayOneShot(quack); 
            StartCoroutine(invulnerabilityFrame()); // invulnerable after taking damage
            player_health -= damage * damageMultiplier;  //take damage
            Debug.Log("PlayerHealth: " + player_health);
            if (player_health <= 0)
            {
                playerAnim.SetTrigger("isDead");
             
              
            }//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // in case of death restart level
            for (int i = 0; i < (damage * damageMultiplier); i++)
            {
                lifeBar[healthSprites---1].enabled = false;
            }
        }

    }

    public void GetDamageByDeathZone(float damage)
    {
        if (vulnerable)
        {
            playAudio.PlayOneShot(quack);
            StartCoroutine(invulnerabilityFrame()); // invulnerable after taking damage
            player_health -= damage;  //take damage
            Debug.Log("PlayerHealth: " + player_health);
            if (player_health <= 0)
            {
                playerAnim.SetTrigger("isDead");
              
           
            }//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // in case of death restart level
            for (int i = 0; i < (damage); i++)
            {
                lifeBar[healthSprites-- - 1].enabled = false;
            }
        }

    }

    void deathScreen()
    {
        DeathMucke.Play();
        anderemucke.Stop(); 
        Time.timeScale = 0; 
        DeathScreenIMG.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    IEnumerator invulnerabilityFrame()
    {
        vulnerable = false;
        yield return new WaitForSeconds(invulnerabilityFrameDuration);
        vulnerable = true;
    }
}