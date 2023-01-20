using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpThroughPlattform : MonoBehaviour
{

    [SerializeField] Transform m_PlayerPos;
    [SerializeField] BoxCollider BoxCol;
    float PlayerRelativePos; 

   

    // Update is called once per frame
    void Update()
    {
        PlayerRelativePos = this.transform.position.y - m_PlayerPos.position.y;
    

        if (PlayerRelativePos < 0)
        {
            BoxCol.enabled = true;
        }
        else if (PlayerRelativePos > 0)
        {
            BoxCol.enabled = false;
        }
    }
}
