using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAndCover : MonoBehaviour
{
    [SerializeField] Transform CoverPos;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform GunPos;
    [SerializeField] Transform ShootPos;
    [SerializeField] float despawnTime;
    Transform EnemySize;
    Vector3 CoverSize;
    Vector3 ShootSize;
    bool standUpAllowed = true;
    bool takeCoverAllowed = false;
    [SerializeField] Transform PlayerPos; 

    bool shootAble = true;
    // Start is called before the first frame update
    void Start()
    {

        EnemySize = GetComponent<Transform>();
        CoverSize = new Vector3(0.5f, 0.25f, 0.5f);
        ShootSize = EnemySize.localScale;

    


    }

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
    void shoot()
    {
        if (shootAble)
        {
            shootAble = false;
            var Bullet = Instantiate(bullet, GunPos.position, GunPos.rotation);
            Bullet.GetComponent<Rigidbody>().AddForce(-GunPos.right * 50f, ForceMode.VelocityChange);
            Destroy(Bullet, despawnTime);
            
        }


    }
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
}
