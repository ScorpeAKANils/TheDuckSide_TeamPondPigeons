using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCameraaround : MonoBehaviour
{

    float mouseX;
    float mouseY; 
    float xRotation;
    [SerializeField] GameObject Player; 
    
    [SerializeField] float sensitivity = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += 2f * Time.deltaTime * Vector3.forward; 
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= 2f * Time.deltaTime * Vector3.forward;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= 2f * Time.deltaTime * Vector3.right;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += 2f * Time.deltaTime * Vector3.right;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
            {
            transform.Rotate(Vector3.up * -sensitivity); 
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * sensitivity);
        }
    }
}
