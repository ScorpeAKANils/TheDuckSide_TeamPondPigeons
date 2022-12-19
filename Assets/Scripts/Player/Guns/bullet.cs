using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float damage = 1f;
    int killCounter;

    private void Start()
    {
        
        killCounter = PlayerPrefs.GetInt("killCounter");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gegner"))
        {
            other.GetComponent<EnemyHealth>().GetDamage(damage);
            killCounter++;
            PlayerPrefs.SetInt("killCounter", killCounter);
            this.gameObject.SetActive(false);
        }
        //To-Do PowerUp Tags ebenfalls durch fliegbar machen 
        else if(!other.CompareTag("Player") && !other.CompareTag("Map"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
