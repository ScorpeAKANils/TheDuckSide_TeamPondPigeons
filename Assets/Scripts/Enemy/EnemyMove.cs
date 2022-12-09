using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    //positions angaben für Waypoints und Spieler 
    [SerializeField] Transform[] WayPoints;
    int index = 0; //index für den Waypoint, damit der gegner weiß, wo er hin muss 
    [SerializeField] Transform Player;
    Vector3 PlayerPos; 
    //geschwindigkeit gegner 
    [SerializeField] float speed;
    //legt fest ob der gegner angreift
    Vector3 lookAtPlayer;
    bool attack = false;
    //ist der Gegner am Waypoint angekommen? 
    bool isOnPoint = false;
    //schaden den der gegner macht 
    float damage = 1f;
   [SerializeField] float rotationMod; 
    
    [SerializeField] float rotSpeed = 20; 
 

    private void Update()
    {
         
        PlayerPos = new Vector3(Player.position.x, transform.position.y, transform.position.z);
        lookAtPlayer = Player.transform.position - transform.position;

        /*StareDownPlayer();*/
        //lookAtPlayer = new Vector3(Player.rotation.x, Player.rotation.y, Player.rotation.z); 
    }

    void FixedUpdate()
    {
        
        //abfrage, ob der gegner schom am weg punkt ist
        if (transform.position != WayPoints[index].transform.position && !isOnPoint && attack==false)
        {
            //wenn nicht, dann soll er dahin gehen 
            moveToPos();
            
        }

        //wenn der Spieler in der nähe, greife an
        if (Vector3.Distance(transform.position, Player.transform.position) < 30f &&attack==false)
        {
          
            attack = true;
          
        }

        //gegner greift an
        if(attack)
        {
            Debug.Log("auf in die schlacht");
            transform.position = Vector3.MoveTowards(transform.position,PlayerPos, speed);
            this.transform.LookAt(lookAtPlayer, Vector3.up);
        }
        //spieler hat den Usain Bolt gemacht, und ist zu weit weg? gehe wieder über zur patrollie 
        if (Vector3.Distance(transform.position, Player.transform.position) > 55f)
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
                transform.Rotate(0, 180, 0);
                break;
            case 1:
                index = 0;
                isOnPoint = false;
                transform.Rotate(0, 180, 0);
                break;
                 

        }
      
        //laufe zur nächsten position 
        moveToPos(); 

    }
    /*void StareDownPlayer()
    {
        if (attack)
        {
            float angle = Mathf.Atan(lookAtPlayer.y) * Mathf.Deg2Rad - rotationMod;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, rotSpeed * Time.fixedDeltaTime);
        }
   
    }*/
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
           
            StartCoroutine(damageYield()); 
        }
    }

    IEnumerator damageYield()
    {
        Player.gameObject.GetComponent<Health>().GetDamage(damage);
        yield return new WaitForSeconds(0.5f); 
    }

}

