using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingPlattform : MonoBehaviour
{
    int index = 0;
    bool isOnPoint = false;
    [SerializeField] Transform[] WayPoints;
   float  speed = 10f; 
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {

        if (transform.position != WayPoints[index].transform.position && !isOnPoint)
        {
            //wenn nicht, dann soll er dahin gehen
            moveToPos();
        }
    }

    //hier holt man sich eine neue position her 
    void GetPos()
    {
        switch (index)
        {
            case 0:
                index = 1;
                isOnPoint = false;
                break;
            case 1:
                index = 0;
                isOnPoint = false;
                break;
        }

        //laufe zur nächsten position 
        moveToPos();
    }

    void moveToPos()
    {
        //gegner läuft zum wegpunkt
        transform.position = Vector3.MoveTowards(transform.position, WayPoints[index].position, speed*Time.deltaTime);
        //wenn der gegner am ziel ist, kriege neuen weg punkt 
        if (this.transform.position == WayPoints[index].position)
        {
            isOnPoint = true;
            GetPos();

        }

        
    }
}