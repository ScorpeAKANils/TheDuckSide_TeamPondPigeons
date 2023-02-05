using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] MOVE m_Move;
    [SerializeField] GameObject m_Player;
    BoxCollider m_Col;
    bool needParent=true;
    public bool isOnPlattform = false; 
    


    // Start is called before the first frame update
    private void Start()
    {
        m_Col = m_Player.GetComponent<BoxCollider>(); 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Map")&& other.gameObject.GetComponent<movingPlattform>() && needParent)
        {
            isOnPlattform = true; 
            //Debug.Log("Yay, hab meine eltern gefunden!");
            m_Player.transform.parent = other.gameObject.transform;
            needParent = false; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            if (other.gameObject.GetComponent<movingPlattform>())
            {
                isOnPlattform = false;
                m_Player.transform.parent = null;
                needParent = true;
            }
        }
        
    }
}