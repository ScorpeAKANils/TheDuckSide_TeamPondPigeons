using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [Tooltip("Value of the Slider when it is fully charged")]
    float maxCoolDown = 0.75f;
    [Tooltip("current value of the slider")]
    float currentCoolDown = 0f; 

    [SerializeField] Slider GunCoolDownSlider; 
 float time; 

    // Update is called once per frame
    void Update()
    {


        GunDir = m_GunObj.localRotation.z;
        PlayerDir = m_PlayerPos.rotation.y; 
       
        //abfragen ob Linke Maustaste gedrückt wurde
        if (reloaded)
        {
            if (Input.GetButton("Fire1") || fullAuto)
            {
                if (tripleShot)
                {
                    StartCoroutine(tripleshotRoutine());
                }
                else if (!tripleShot)
                {
                 
                    
                        currentCoolDown = 0f;
                        shoot();
                    

                }
                //StartCoroutine(shootRoutine());
            }
        }
        if (!tripleShot)
        {
            if (currentCoolDown >= maxCoolDown)
            {
                reloaded = true;
            }
            else
            {
                currentCoolDown += Time.deltaTime;
                currentCoolDown = Mathf.Clamp(currentCoolDown, 0f, maxCoolDown);
            }
        }

        GunCoolDownSlider.value = currentCoolDown / maxCoolDown;

        if (Input.GetButtonDown("Fire2") && BigShotReady)
        {
            if (GunDir > 0.3f || GunDir < -0.3f)
            {
                StartCoroutine(bigShootRoutine());
            }
        }
    }

    private void shoot()
    {
        reloaded = false;
        //spawnen der Bullet
        var bullet = Instantiate(Bullet, Gun.position, Gun.rotation);
        //bullet flug direction geben 
        bullet.GetComponent<Rigidbody>().AddForce(Gun.TransformDirection(Vector3.right) * 50f, ForceMode.VelocityChange);
        Destroy(bullet,bulletLifespan);
    }

    void BigShoot()
    {
            //spawnen der Bullet
            var bullet = Instantiate(Bullet, Gun.position, Gun.rotation);
            m_Player.velocity = Vector3.up * 34f;
            // m_Player.AddForce(m_PlayerPos.TransformDirection(Vector3.up) * 12f, ForceMode.Impulse);
            //bullet flug direction geben 
            bullet.GetComponent<Rigidbody>().AddForce(Gun.TransformDirection(Vector3.right) * 50f, ForceMode.VelocityChange);
            Destroy(bullet, bulletLifespan);
    }

    /*public IEnumerator shootRoutine()
    {
        reloaded = false;
        shoot();
       
        yield return new WaitForSeconds(reloadTime);
        // Debug.Log("ReloadTime: " + reloadTime);
        reloaded = true;
        GunCoolDownSlider.value = maxCoolDown/minCoolDown;
    }*/
    public IEnumerator tripleshotRoutine()
    {
        reloaded = false;
        for (int i = 0; i < 3; i++)
        {
            //currentCoolDown = 0f;
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
        yield return new WaitForSeconds(3.5f);
        // Debug.Log("ReloadTime: " + reloadTime);
        BigShotReady = true;
    }
}