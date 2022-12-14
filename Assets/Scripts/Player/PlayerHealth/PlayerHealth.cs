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

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        player_health = max_player_health;
        healthSprites = (int)max_player_health;
    }

    public void GetDamage(float damage)
    {
        if (vulnerable)
        {
            StartCoroutine(invulnerabilityFrame()); // invulnerable after taking damage
            player_health -= damage * damageMultiplier; //take damage
            //Debug.Log("PlayerHealth: " + player_health);
            if (player_health <= 0) SceneManager.LoadScene(sceneIndex); // in case of death restart level
            for (int i = 0; i < damage * damageMultiplier; i++)
            {
                lifeBar[healthSprites---1].enabled = false;
            }
        }
    }

    IEnumerator invulnerabilityFrame()
    {
        vulnerable = false;
        yield return new WaitForSeconds(invulnerabilityFrameDuration);
        vulnerable = true;
    }
}