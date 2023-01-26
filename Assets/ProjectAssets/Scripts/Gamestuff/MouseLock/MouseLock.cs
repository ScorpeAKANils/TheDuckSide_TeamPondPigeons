using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLock : MonoBehaviour
{
    private void Start()
    {
        //locked maus cursor um das bedienen der maus im spiel zu erleichtern
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
  
}
