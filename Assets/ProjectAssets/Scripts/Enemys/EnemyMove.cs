using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    Animator stickHunterAnimator;
    //positions angaben für Waypoints und Spieler 
    [SerializeField] Transform[] WayPoints;
    public int index = 0; //index für den Waypoint, damit der gegner weiß, wo er hin muss 
    [SerializeField] Transform Player;
    PlayerHealth m_health;
    Vector3 PlayerPos;
    [Tooltip("Is the WayPoint on the Left or right side of the Enemy?")]
    float WayPointDir;
    //geschwindigkeit gegner
    [SerializeField] private float basespeed = 10f;
    private float speed;
    //legt fest ob der gegner angreift
    bool attack = false;
    //ist der Gegner am Waypoint angekommen? 
    public bool isOnPoint = false;
    //schaden den der gegner macht 
    float damage = 2f;
    float PlayerDirection;
    public bool WallDetected;
    [Tooltip("Distance which defines, when the enemy should attack the player")]
    [SerializeField] float EnemyAttackDistance = 35f;
    //Base value of the attack distance; 
    float EADbase;
    [Tooltip("distance that defines, when the enemy should stop hounting the player")]
    [SerializeField] float GoesBackToPatrol = 45f;
    float GBPBase;
    [SerializeField] LayerMask LayerToCheck;
    [Tooltip("The Eye of the Enemy. It is not placed on the hight of the visible eyes, to detect lower Obstacles")]
    [SerializeField] Transform EnemyEye;
    bool canFlip;
    Vector3 localScale;
    [SerializeField] AudioClip[] audio;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        speed = basespeed;
        EADbase = EnemyAttackDistance;
        GBPBase = GoesBackToPatrol;
        stickHunterAnimator = GetComponent<Animator>();
        m_health = Player.gameObject.GetComponent<PlayerHealth>();
    }


    private void Update()
    {
        if (m_health.player_health <= 0)
        {
            return;
        }
        PlayerPos = new Vector3(Player.position.x, transform.position.y, transform.position.z);
        PlayerDirection = Player.position.x - transform.position.x;


        WayPointDir = WayPoints[index].position.x - this.transform.position.x;


    }

    void FixedUpdate()
    {
        if (m_health.player_health <= 0)
        {
            return;
        }
        //abfrage, ob der gegner schom am weg punkt ist
        if (transform.position != WayPoints[index].transform.position && attack == false)
        {
            //wenn nicht, dann soll er dahin gehen 
            moveToPos();
        }
        else if (this.transform.position == WayPoints[index].position && attack == false)
        {
            isOnPoint = true;
            GetPos();
        }
        //wenn der Spieler in der nähe, greife an
        if (Vector3.Distance(transform.position, Player.transform.position) < EnemyAttackDistance && attack == false && !WallDetected)
        {
            attack = true;
            EnemyAttackDistance = EADbase;
            GoesBackToPatrol = GBPBase;
        }
        DoFlipBro();
        //switches to attackanimation and stops movement when in range of player
        if (Vector3.Distance(transform.position, Player.transform.position) < 2f)
        {
            speed = 0;
            stickHunterAnimator.SetBool("isAttacking", true);
        }
        else
        {
            speed = basespeed;
            stickHunterAnimator.SetBool("isAttacking", false);
        }


        RaycastHit sight;
        Debug.DrawRay(EnemyEye.position, EnemyEye.TransformDirection(Vector3.right), Color.yellow);
        if (Physics.Raycast(EnemyEye.position, EnemyEye.TransformDirection(Vector3.right), out sight, 15f, LayerToCheck))
        {
            WallDetected = true;
            attack = false;
            EnemyAttackDistance = 5f;
            GoesBackToPatrol = 10f;
            canFlip = true;
            DoFlipBro();
            isOnPoint = false;
            //index = 1; 
        }
        else
        {
            WallDetected = false;

        }

        //gegner greift an
        if (attack && !WallDetected)
        {
            transform.position = Vector3.MoveTowards(transform.position, PlayerPos, (speed * Time.deltaTime));
        }
        //spieler hat den Usain Bolt gemacht, und ist zu weit weg? gehe wieder über zur patrollie 
        if (Vector3.Distance(transform.position, Player.transform.position) > GoesBackToPatrol)
        {
            attack = false;
        }
    }

    void GetPos()
    {
        //if (WallDetected)
        //{
        //    GoesBackToPatrol = GBPBase;
        //    EnemyAttackDistance = EADbase;
        //    WallDetected = false;

        //    switch (index)
        //    {
        //        case 0:
        //            index = 1;
        //            isOnPoint = false;
        //            canFlip = true;
        //            DoFlipBro();
        //            break;
        //        case 1:
        //            index = 0;
        //            isOnPoint = false;
        //            canFlip = true;
        //            DoFlipBro();
        //            break;
        //    }
        //    moveToPos();

        //}
        //else
        //{
        //    switch (index)
        //    {
        //        case 0:
        //            index = 1;
        //            isOnPoint = false;
        //            canFlip = true;
        //            DoFlipBro();
        //            break;
        //        case 1:
        //            index = 0;
        //            isOnPoint = false;
        //            canFlip = true;
        //            DoFlipBro();
        //            break;
        //    }
        //    moveToPos();
        //}


        //Debug.Log("nice, endlich da");
        //isOnPoint = true;
        index++;
        if (index == WayPoints.Length)
        {
            index = 0;
        }
        isOnPoint = false;
        //Debug.Log("ich gehe zu Punkt: " + index);





        ////laufe zur nächsten position 

    }

    void moveToPos()
    {
        /*//gegner läuft zum wegpunkt
        transform.position = Vector3.MoveTowards(transform.position, WayPoints[index].position, speed);
        //wenn der gegner am ziel ist, kriege neuen weg punkt 
        if (this.transform.position == WayPoints[index].position)
        {
            isOnPoint = true; 
            //GetPos();
        }*/

        transform.position = Vector3.MoveTowards(transform.position, WayPoints[index].position, (speed * Time.deltaTime));
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

    void DoFlipBro()
    {
        canFlip = true;
        if (canFlip)
        {
            if (PlayerDirection < 0 && attack)
            {
                canFlip = false;
                this.GetComponent<Transform>().transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            }
            else if (PlayerDirection > 0 && attack)
            {
                canFlip = false;
                this.GetComponent<Transform>().transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
            else if (WayPointDir < 0 && !attack)
            {
                canFlip = false;
                this.GetComponent<Transform>().transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
            }
            else if (WayPointDir > 0 && !attack)
            {
                canFlip = false;
                this.GetComponent<Transform>().transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }


        }
    }

    public void SchlagSound()
    {
        audioSource.PlayOneShot(audio[0]);
    }


    public void LaufSound()
    {
        audioSource.PlayOneShot(audio[1]);
    }
}
