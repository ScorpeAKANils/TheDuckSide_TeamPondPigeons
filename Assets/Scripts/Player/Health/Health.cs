using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
  public float health = 3f;
    bool ableToGetDamage = true;
    int sceneIndex; 

    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex; 
    }



    public void GetDamage(float damage)
    {

            
        if (ableToGetDamage)
        {
            ableToGetDamage = false;
            health -= damage;
            Debug.Log("PlayerHealth" + health);
            StartCoroutine(damageYield());
            if (health <= 0)
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
        

            if (health <= 0)
            {
                SceneManager.LoadScene(sceneIndex); 
            }
     }
    

    IEnumerator damageYield()
    {
        yield return new WaitForSeconds(0.2f);
        ableToGetDamage = true; 
    }
}
