using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform Gun; // spawnpoint of the bullet
    [SerializeField] GameObject Bullet;
    public float standartReloadTime = 2f;
    public float reloadTime = 2f;
    bool reloaded = true;
    float bulletLifespan = 3f;
    public bool fullAuto = false;
    public bool tripleShot = false;

    void Update()
    {
        if (reloaded)
        {
            if (Input.GetButton("Fire1") || fullAuto)
            {
                if (tripleShot) StartCoroutine(tripleshotRoutine());
                else StartCoroutine(shootRoutine());
            }
        }
    }

    private void shoot()
    {
        var bullet = Instantiate(Bullet, Gun.position, Gun.rotation); //spawn bullet
        bullet.GetComponent<Rigidbody>().AddForce(Gun.right * 50f, ForceMode.VelocityChange); // accelerate bullet
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
}
