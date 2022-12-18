using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //spawn Point der Bullet
    [SerializeField] Transform Gun;
    [SerializeField] Transform m_GunObj;
    [SerializeField] Rigidbody m_Player;
    [SerializeField] Transform m_PlayerPos;
    //bullet Object 
    [SerializeField]GameObject Bullet;
    float GunDir;
    float PlayerDir; 
    public float standartReloadTime = 2f;
    public float reloadTime = 2f;
    bool reloaded = true;
    bool BigShotReady = true; 
    float bulletLifespan = 3f;
    public bool fullAuto = false;
    public bool tripleShot = false;

    // Update is called once per frame
    void Update()
    {

        GunDir = m_GunObj.localRotation.z;
        PlayerDir = m_PlayerPos.rotation.y; 
        Debug.Log(GunDir); 
        //abfragen ob Linke Maustaste gedrückt wurde
        if (reloaded)
        {
            if (Input.GetButton("Fire1") || fullAuto)
            {
                if (tripleShot) StartCoroutine(tripleshotRoutine());
                else StartCoroutine(shootRoutine());
            }
        }

        if (Input.GetButton("Fire2") && BigShotReady)
        {
           
            StartCoroutine(bigShootRoutine());
        }
    }

    private void shoot()
    {
        reloaded = false;
        //spawnen der Bullet
        var bullet = Instantiate(Bullet, Gun.position, Gun.rotation);
        //bullet flug direction geben 
        bullet.GetComponent<Rigidbody>().AddForce(Gun.right * 50f, ForceMode.VelocityChange);
        Destroy(bullet,bulletLifespan);
    }

    void BigShoot()
    {
      
        Debug.Log("Boom, knock back");
        //spawnen der Bullet
        var bullet = Instantiate(Bullet, Gun.position, Gun.rotation);

        if (GunDir <= 0.3f && GunDir >= -0.3f && PlayerDir == 1)
        {

            m_Player.AddForce(m_PlayerPos.TransformDirection((Vector3.left)) * 12000f);
        }
        if (GunDir <= 0.3f && GunDir >= -0.3f && PlayerDir == 0)
        {

            m_Player.AddForce(m_PlayerPos.TransformDirection(Vector3.right) * (-12000f));
        }
        if (GunDir > 0.3f || GunDir < -0.3f /*&&PlayerDir == 0*/)
        {

            m_Player.AddForce(m_PlayerPos.TransformDirection(Vector3.up) * 12000f);
        }
       /* if (GunDir < -2f && PlayerDir == 1 || GunDir > 2f && PlayerDir == 1)
        {

            m_Player.AddForce(m_PlayerPos.TransformDirection(Vector3.up) * 12000f);
        }*/



        //bullet flug direction geben 
        bullet.GetComponent<Rigidbody>().AddForce(Gun.right * 50f, ForceMode.VelocityChange);
        Destroy(bullet, bulletLifespan);
    }

    public IEnumerator shootRoutine()
    {
        reloaded = false;
        shoot();
        yield return new WaitForSeconds(reloadTime);
        // Debug.Log("ReloadTime: " + reloadTime);
        reloaded = true;
    }
    public IEnumerator tripleshotRoutine()
    {
        reloaded = false;
        for (int i = 0; i < 3; i++)
        {
            shoot();
            yield return new WaitForSeconds(0.07f);
        }
        yield return new WaitForSeconds(1.8f);
        reloaded = true;
    }

    public IEnumerator bigShootRoutine()
    {

        BigShotReady = false;
        BigShoot(); 
        yield return new WaitForSeconds(reloadTime);
        // Debug.Log("ReloadTime: " + reloadTime);
        BigShotReady = true;
    }
}