using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] MOVE m_Move;
    [SerializeField] GameObject m_Player;
    BoxCollider m_Col; 
    


    // Start is called before the first frame update
    private void Start()
    {
        m_Col = m_Player.GetComponent<BoxCollider>(); 
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Map")&& other.gameObject.GetComponent<movingPlattform>())
        {
            Debug.Log("Yay, hab meine eltern gefunden!");
            m_Player.transform.parent = other.gameObject.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            if (other.gameObject.GetComponent<movingPlattform>())
            { 
                m_Player.transform.parent = null;
            }
        }
        
    }
}