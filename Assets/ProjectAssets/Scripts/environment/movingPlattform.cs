using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlattform : MonoBehaviour
{
    int index= 0;
    bool isOnPoint = false;
    [SerializeField] Transform[] WayPoints;
   float  speed = 4f;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (WayPoints.Length <= 0)
        {
            return;
        }
        if (this.transform.position == WayPoints[index].position)
        {
            index++;
            if (index == WayPoints.Length) index = 0;
        }
        transform.position = Vector3.MoveTowards(transform.position, WayPoints[index].position, speed * Time.fixedDeltaTime);
    }
}