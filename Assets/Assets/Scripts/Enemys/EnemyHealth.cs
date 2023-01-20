using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health= 3f;
    int killCounter; 
    // Start is called before the first frame update
    private void Start()
    {
        killCounter = PlayerPrefs.GetInt("killCounter");
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        Debug.Log(health); 
        if (health <= 0f)
        {
            killCounter+=1;
            PlayerPrefs.SetInt("killCounter", killCounter);
            this.gameObject.SetActive(false);
        }
    }
}
