using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    Animator stickHunterAnimator;
    //positions angaben für Waypoints und Spieler 
    [SerializeField] Transform[] WayPoints;
    int index = 0; //index für den Waypoint, damit der gegner weiß, wo er hin muss 
    [SerializeField] Transform Player;
    Vector3 PlayerPos;
    //geschwindigkeit gegner
    private float basespeed = 0.2f;
    private float speed;
    //legt fest ob der gegner angreift
    bool attack = false;
    //ist der Gegner am Waypoint angekommen? 
    bool isOnPoint = false;
    //schaden den der gegner macht 
    float damage = 1f;
    float PlayerDirection;

    private void Start()
    {
        speed = basespeed;
        stickHunterAnimator = GetComponent<Animator>();
    }


    private void Update()
    {
        PlayerPos = new Vector3(Player.position.x, transform.position.y, transform.position.z);
        PlayerDirection = Player.position.x - transform.position.x;
    }

    void FixedUpdate()
    {
        //abfrage, ob der gegner schom am weg punkt ist
        if (transform.position != WayPoints[index].transform.position && !isOnPoint && attack == false)
        {
            //wenn nicht, dann soll er dahin gehen 
            moveToPos();
        }
        //wenn der Spieler in der nähe, greife an
        if (Vector3.Distance(transform.position, Player.transform.position) < 30f && attack == false)
        {
            attack = true;
        }

        //switches to attackanimation and stops movement when in range of player
        if (Vector3.Distance(transform.position, Player.transform.position) < 6f)
        {
            speed = 0;
            stickHunterAnimator.SetBool("isAttacking", true);
        }
        else
        {
            speed = basespeed;
            stickHunterAnimator.SetBool("isAttacking", false);
        }

        if (PlayerDirection < 0 && attack)
        {
            this.GetComponent<Transform>().transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        else if (PlayerDirection > 0 && attack)
        {
            this.GetComponent<Transform>().transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        //gegner greift an
        if (attack)
        {
            Debug.Log("auf in die schlacht");
            transform.position = Vector3.MoveTowards(transform.position, PlayerPos, speed);
            //this.transform.LookAt(lookAtPlayer, Vector3.up);
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
                this.GetComponent<Transform>().transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
                break;
            case 1:
                index = 0;
                isOnPoint = false;
                this.GetComponent<Transform>().transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
        if (this.transform.position == WayPoints[index].position)
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
        Player.gameObject.GetComponent<PlayerHealth>().GetDamage(damage);
        yield return new WaitForSeconds(0.5f);
    }

    //gets triggered on last frame of stickHunters attack. damages the duck if its in range
    private void hitDuck()
    {
        if (Vector3.Distance(transform.position, Player.transform.position) < 6f)
        {
            Player.gameObject.GetComponent<PlayerHealth>().GetDamage(damage);
        }
    }
}