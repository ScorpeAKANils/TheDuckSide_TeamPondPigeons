using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //spawn Point der Bullet
    [SerializeField] Transform Gun;
    [SerializeField] Transform m_GunObj;
    [SerializeField] Rigidbody m_Player;

    //bullet Object 
    [SerializeField]GameObject Bullet;
    // Start is called before the first frame update
    bool shootAble = true;
    float despawnTime = 3f; 
    [SerializeField] float coolDown = 0.5f; 
    // Update is called once per frame
    void Update()
    {
        //abfragen ob Linke Maustaste gedrückt wurde
        if (Input.GetButton("Fire1") && shootAble)
        {
            shoot();
            StartCoroutine(shootingYield()); 
        }

        if (Input.GetButton("Fire2") && shootAble)
        {
            BigShoot(); 
            StartCoroutine(shootingYield());
        }
    }

    private void shoot()
    {
        shootAble = false;
        //spawnen der Bullet
        var bullet = Instantiate(Bullet, Gun.position, Gun.rotation);
        //bullet flug direction geben 
        bullet.GetComponent<Rigidbody>().AddForce(Gun.right * 50f, ForceMode.VelocityChange);
        Destroy(bullet, despawnTime);
    }

    void BigShoot()
    {
        shootAble = false;
        Debug.Log("Boom, knock back");
        //spawnen der Bullet
        var bullet = Instantiate(Bullet, Gun.position, Gun.rotation);
        m_Player.AddForce(m_GunObj.TransformDirection(-m_GunObj.up) * 12000f);
        
        //bullet flug direction geben 
        bullet.GetComponent<Rigidbody>().AddForce(Gun.right * 50f, ForceMode.VelocityChange);
        Destroy(bullet, despawnTime);
    }

    public IEnumerator shootingYield()
    {
        yield return new WaitForSeconds(coolDown);
        shootAble=true; 
    }
}
