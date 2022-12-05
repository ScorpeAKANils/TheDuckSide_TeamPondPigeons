using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpDown : MonoBehaviour
{

    float mouseX;
    float xRotation; 
    [SerializeField] float sensitivity = 2f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse Y") * sensitivity; 
        xRotation +=mouseX;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        transform.localRotation = Quaternion.Euler( -0f, 0f, xRotation);
    }
}
