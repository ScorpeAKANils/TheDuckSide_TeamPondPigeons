using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform[] RespawnPoints;
    PlayerHealth m_Health; 
    int PosIndex = -1;

    private void Start()
    {
        m_Health = this.GetComponent<PlayerHealth>(); 
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RespawnPoint"))
        {
      
            PosIndex++;
            other.gameObject.SetActive(false); 
        }

        if (other.CompareTag("DeadZone"))
        {
            m_Health.GetDamage(1);
            this.transform.position = RespawnPoints[PosIndex].position;
        }
    }
}
