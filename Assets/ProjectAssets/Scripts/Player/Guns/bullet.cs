using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float damage = 1f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gegner"))
        {
            other.GetComponent<EnemyHealth>().GetDamage(damage);
            this.gameObject.SetActive(false);
        }
        //To-Do PowerUp Tags ebenfalls durch fliegbar machen 
        else if(!other.CompareTag("Player") && !other.gameObject.CompareTag("Untagged"))
        {
            this.gameObject.SetActive(false);
        }
    }
}
