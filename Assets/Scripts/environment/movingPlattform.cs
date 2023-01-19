using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlattform : MonoBehaviour
{
    int index= 0;
    bool isOnPoint = false;
    [SerializeField] Transform[] WayPoints;
   float  speed = 15f;
    int WayPointCounter;
 
    // Start is called before the first frame update

    private void Start()
    {
        WayPointCounter = WayPoints.Length-1;
        Debug.Log("Waypoints in der scene, -1 gezählt, weil ein Array bei 0 anfängt; " + WayPointCounter); 

    }
    // Update is called once per frame
    void Update()
    {

        if (!isOnPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, WayPoints[index].position, speed * Time.deltaTime);
            //wenn der gegner am ziel ist, kriege neuen weg punkt 
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