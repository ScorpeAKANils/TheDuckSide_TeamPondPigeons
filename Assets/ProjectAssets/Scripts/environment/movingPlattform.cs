using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlattform : MonoBehaviour
{
    int index= 0;
    bool isOnPoint = false;
    [SerializeField] Transform[] WayPoints;
   float  speed = 15f;
    // Update is called once per frame
    void Update()
    {

        if (!isOnPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, WayPoints[index].position, speed * Time.deltaTime);
           
            if (this.transform.position == WayPoints[index].position)
            {

                isOnPoint = true;
                if (index < WayPoints.Length - 1)
                {
                    index++;
                }
                else
                {
                    index = 0; 
                }
                isOnPoint = false;

            }

        }
    

    }
}