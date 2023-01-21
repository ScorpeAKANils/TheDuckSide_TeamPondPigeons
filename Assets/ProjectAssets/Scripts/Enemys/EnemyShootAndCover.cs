using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAndCover : MonoBehaviour
{
    [SerializeField] Transform CoverPos;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform rifleMuzzle;
    [SerializeField] Transform ShootPos;
    [SerializeField] float despawnTime;
    Vector3 PlayerPos;
    Transform Enemy;
    Vector3 CoverSize;
    Vector3 ShootSize;
    bool standUpAllowed = true;
    bool takeCoverAllowed = false;
    [SerializeField] Transform Player;
    //[SerializeField] Transform PlayerPos;
    Animator rifleHunterAnimator;

    bool shootAble = true;
    // Start is called before the first frame update
    void Start()
    {
        despawnTime = 3f;
        Enemy = GetComponent<Transform>();
        //CoverSize = new Vector3(0.5f, 0.25f, 0.5f);
        //ShootSize = EnemySize.localScale;
        rifleHunterAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        //Debug.Log("Start of update enemy shoot and cover");
        //PlayerPos = new Vector3(Player.position.x, transform.position.y, transform.position.z);
        if (Vector3.Distance(transform.position, Player.transform.position) < 20f)
        {
            //Debug.Log("spieler ist nahe j‰ger");
            rifleHunterAnimator.SetBool("playerCloseBy", true);
        }
        else
        {
            //Debug.Log("spieler ist fern vom j‰ger");
            rifleHunterAnimator.SetBool("playerCloseBy", false);
        }
    }

    /*
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerPos.position) < 20f)
        {
            AttackPlayer();
        }
        else
        {
            Cover(); 
        }
    }

    void Cover()
    {
        takeCoverAllowed = false; 
        EnemySize.localScale = CoverSize;
        this.transform.position = CoverPos.position;
    }

    void GetUpAndShoot()
    {
        standUpAllowed = false;
        EnemySize.localScale = ShootSize;
        this.transform.position = ShootPos.position;
        shoot();
        StartCoroutine(shootingYield());
    }

    void AttackPlayer()
    {
        if (standUpAllowed)
        {
            StartCoroutine(standUp());
        }

        if (takeCoverAllowed)
        {
            StartCoroutine(takeCover()); 
        }
    }
*/
    void shoot()
    {
        var Bullet = Instantiate(bullet, rifleMuzzle.position, rifleMuzzle.rotation);
        Bullet.GetComponent<Rigidbody>().AddForce(-rifleMuzzle.right * 50f, ForceMode.VelocityChange);
        Destroy(Bullet, despawnTime);
    }

    /*
    IEnumerator shootingYield()
    {
        yield return new WaitForSeconds(0.5f);
        shootAble = true;
    }

    IEnumerator standUp()
    {
        GetUpAndShoot();
        yield return new WaitForSeconds(1.0f);
        takeCoverAllowed = true; 
    }

    IEnumerator takeCover()
    {
        Cover();
        yield return new WaitForSeconds(1.0f);
        standUpAllowed = true;
    }
    */
}