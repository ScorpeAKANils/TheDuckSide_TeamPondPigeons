using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpDown : MonoBehaviour
{
    float mouseX;
    [SerializeField] float LockRot = 80f;
    float xRotation;
    [SerializeField] float sensitivity = 2f;
    // Update is called once per frame
    void Update()
    {
        mouseX = Input.GetAxis("Mouse Y") * sensitivity;
        xRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -LockRot, LockRot);
        transform.localRotation = Quaternion.Euler(-0f, 0f, xRotation);
    }
}