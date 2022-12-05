using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vsync : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //sorgt für unbegrenzte frame rate 
        QualitySettings.vSyncCount = 0; 
    }

  
}
