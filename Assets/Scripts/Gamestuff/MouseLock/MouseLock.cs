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
    void Update()
    {
        //Maus wird beim drücken von Escape entlocked -> um z.B mal in ein anderes programm zu gehen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        //maus wird beim drücken der linken maus taste wieder gelocked 
        if (Input.GetButtonDown("Fire1"))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
