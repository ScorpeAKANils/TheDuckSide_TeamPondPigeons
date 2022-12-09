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
            other.GetComponent<Health>().GetDamage(damage);
            this.gameObject.SetActive(false);
        }
        else if(!other.CompareTag("Gegner") && !other.CompareTag("Map"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
