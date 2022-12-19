using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private float invulnerabilityBuffDuration = 15.0f;
    private float invincibilityBuffDuration = 15.0f;
    private float reducedDamageBuffDuration = 15.0f;
    private float tripleShotBuffDuration = 15.0f;
    private float movementSpeedBuffDuration = 15.0f;
    private float movementSpeedBuffed = 40.0f;
    Health healthscript;
    MOVE movescript;
    Shoot shootscript;

    void Start()
    {
        healthscript = GetComponent<Health>();
        movescript = GetComponent<MOVE>();
        shootscript = GetComponent<Shoot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.CompareTag("Powerup")) { } // sp?ter adden

        if (other.CompareTag("smallHeal"))
        {
            other.gameObject.SetActive(false);
            smallHeal();
        }
        if (other.CompareTag("fullHeal"))
        {
            fullHeal();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("invulnerability"))
        {
            invulnerability();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("reducedDamage"))
        {
            reducedDamage();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("movementSpeed"))
        {
            movementSpeed();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("tripleShot"))
        {
            tripleShot();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("invincibility"))
        {
            invincibility();
            other.gameObject.SetActive(false);
        }
        if (other.CompareTag("testfalle"))
        {
            testfalle(1);
        }
    }

    void testfalle(int damageamount)
    {
        healthscript.GetDamage(2);
    }

    void smallHeal()
    {
        if (healthscript.player_health < healthscript.max_player_health)
        {
            healthscript.player_health += 1.0f;
            healthscript.lifeBar[healthscript.healthSprites++].enabled = true;
        }
    }
    void fullHeal()
    {
        healthscript.player_health = healthscript.max_player_health;
        while (healthscript.healthSprites < healthscript.max_player_health)
        {
            healthscript.lifeBar[healthscript.healthSprites++].enabled = true;
        }
    }

    void invulnerability()
    {
        StartCoroutine(invulnerabilityDurationRoutine(invulnerabilityBuffDuration));
    }

    void reducedDamage()
    {
        StartCoroutine(reducedDamageRoutine(reducedDamageBuffDuration));
    }

    void movementSpeed()
    {
        StartCoroutine(movementSpeedRoutine(movementSpeedBuffDuration));
    }
    void tripleShot()
    {
        StartCoroutine(tripleShotRoutine(tripleShotBuffDuration));
    }
    void invincibility()
    {
        fullHeal();
        invulnerability();
        movementSpeed();
        StartCoroutine(invincibilityRoutine(invincibilityBuffDuration));
    }

    IEnumerator invulnerabilityDurationRoutine(float duration)
    {
        healthscript.vulnerable = false;
        yield return new WaitForSeconds(duration);
        healthscript.vulnerable = true;
    }

    IEnumerator reducedDamageRoutine(float duration)
    {
        healthscript.damageMultiplier = 0.5f;
        yield return new WaitForSeconds(duration);
        healthscript.damageMultiplier = 1.0f;
    }

    IEnumerator tripleShotRoutine(float duration)
    {
        shootscript.tripleShot = true;
        yield return new WaitForSeconds(duration);
        shootscript.tripleShot = false;
    }

    IEnumerator invincibilityRoutine(float duration)
    {
        shootscript.reloadTime = 0.3f;
        shootscript.fullAuto = true;
        yield return new WaitForSeconds(duration);
        shootscript.fullAuto = false;
        shootscript.reloadTime = shootscript.standartReloadTime;
    }

    IEnumerator movementSpeedRoutine(float duration)
    {
        movescript.playerMovementSpeed = movementSpeedBuffed;
        yield return new WaitForSeconds(duration);
        movescript.playerMovementSpeed = movescript.playerBaseMovementSpeed;
    }

}