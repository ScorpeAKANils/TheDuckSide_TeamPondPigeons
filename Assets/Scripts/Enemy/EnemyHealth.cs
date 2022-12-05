using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float health= 3f;
    // Start is called before the first frame update


    public void GetDamage(float damage)
    {
        health -= damage;
        Debug.Log(health); 
        if (health <= 0f)
        {
            this.gameObject.SetActive(false); 
        }
    }
}
