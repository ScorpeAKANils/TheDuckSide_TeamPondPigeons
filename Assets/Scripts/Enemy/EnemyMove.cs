using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //positions angaben für Waypoints und Spieler 
    [SerializeField] Transform[] WayPoints;
    int index = 0; //index für den Waypoint, damit der gegner weiß, wo er hin muss 
    [SerializeField] Transform Player;
    Vector3 playerPos; 
    //geschwindigkeit gegner 
    [SerializeField] float speed;
    //legt fest ob der gegner angreift 
    bool attack = false;
    //ist der Gegner am Waypoint angekommen? 
    bool isOnPoint = false;
    //schaden den der gegner macht 
    float damage = 1f;

    private void Update()
    {
        playerPos = new Vector3(-20.3f, 9.9f, Player.position.z);
    }


    void FixedUpdate()
    {
      
        //abfrage, ob der gegner schom am weg punkt ist
        if (this.transform.position != WayPoints[index].transform.position && !isOnPoint | attack==false)
        {
            //wenn nicht, dann soll er dahin gehen 
            moveToPos();
        }
        //wenn der Spieler in der nähe, greife an
        if (Vector3.Distance(this.transform.position, playerPos) < 30f &&attack==false)
        {
            attack = true;
          
        }

        //gegner greift an
        if (attack)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, playerPos, speed);
            
        }
        //spieler hat den Usain Bolt gemacht, und ist zu weit weg? gehe wieder über zur patrollie 
        if (Vector3.Distance(this.transform.position, Player.position) > 55f)
        {
            attack = false; 
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
            transform.position = Vector3.MoveTowards(transform.position, WayPoints[index].position, speed);
            //wenn der gegner am ziel ist, kriege neuen weg punkt 
            if (this.transform.position == WayPoints[index].transform.position)
            {
                isOnPoint = true;
                GetPos();
            }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().GetDamage(damage); 
        }
    }


}

