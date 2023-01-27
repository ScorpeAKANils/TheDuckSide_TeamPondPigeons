using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform[] RespawnPoints;
    PlayerHealth m_Health; 
    int PosIndex = -1;
    float maxWalkingCoolDown = 1f;
    float currentWalkingCoolDown = 0f;
    bool isRespawned; 
    MOVE m_Move;
    Rigidbody m_PlayerRB;
    Rigidbody baseRB;

    private void Awake()
    {
        m_PlayerRB = this.GetComponent<Rigidbody>();
        baseRB = this.GetComponent<Rigidbody>();
        baseRB.constraints = m_PlayerRB.constraints;
    }
    private void Start()
    {
        m_Health = this.GetComponent<PlayerHealth>();
        m_Move = this.GetComponent<MOVE>();

        
        
    }

    private void Update()
    {
        if (currentWalkingCoolDown >= maxWalkingCoolDown)
        {
            m_PlayerRB.constraints &= ~RigidbodyConstraints.FreezePositionY & ~RigidbodyConstraints.FreezePositionX;
            m_Move.canWalk = true;
            isRespawned = false; 
           
        }
        else if(isRespawned) 
        {
            currentWalkingCoolDown += Time.deltaTime;
            currentWalkingCoolDown = Mathf.Clamp(currentWalkingCoolDown, 0f, maxWalkingCoolDown); 
        }
      

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
            currentWalkingCoolDown = 0f;
            m_Health.GetDamageByDeathZone(1);
            isRespawned = true;
            m_Move.canWalk = false;
            m_PlayerRB.constraints = RigidbodyConstraints.FreezeAll;  
            this.transform.position = RespawnPoints[PosIndex].position;
        }
    }
}
