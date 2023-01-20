using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    public float maxDistance;
    public LayerMask WhatIsGround;
    
    void Start()
    {
        SurfaceAlignment();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.DrawRay(transform.position, Vector3.down, Color.red);
    }

    private void SurfaceAlignment()
    {
        Ray ray = new Ray(transform.position, -transform.forward);
        RaycastHit info = new RaycastHit();
        Quaternion RotationRef = Quaternion.Euler(0, 0, 0);

        

        if (Physics.Raycast(ray, out info, maxDistance, WhatIsGround))
        {
            Debug.Log(info.transform.position);
            RotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, info.normal), 1);
            //transform.rotation = Quaternion.Euler(RotationRef.eulerAngles.x, transform.eulerAngles.y, RotationRef.eulerAngles.z);
            transform.position = new Vector3(info.point.x, info.point.y, info.point.z);
        }
    }
}
