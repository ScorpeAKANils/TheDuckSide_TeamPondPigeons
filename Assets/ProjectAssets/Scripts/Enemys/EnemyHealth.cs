using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health = 3f;
    Animator EnemyAnim;
    int killCounter;
    float timeCounter;
    float dieTime;
    // Start is called before the first frame update
    private void Start()
    {
        killCounter = PlayerPrefs.GetInt("killCounter");
        EnemyAnim = this.GetComponent<Animator>();
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        //Debug.Log(health); 
        if (health <= 0f)
        {
            killCounter += 1;
            PlayerPrefs.SetInt("killCounter", killCounter);
            EnemyAnim.SetTrigger("isDead");
        }
    }

    void SetInactive()
    {

        this.gameObject.SetActive(false);
    }
}
