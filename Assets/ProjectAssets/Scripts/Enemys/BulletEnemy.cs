using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] float damage = 1f; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().GetDamage(damage);
            this.gameObject.SetActive(false);
        }
        else if(!other.CompareTag("Gegner") && !other.gameObject.CompareTag("Untagged"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
