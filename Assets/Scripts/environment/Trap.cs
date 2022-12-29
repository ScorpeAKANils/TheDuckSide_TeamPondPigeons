using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Animator fallenAnimation;
    int schadensvariable = 2;
    public bool falleActive = true;


    // Start is called before the first frame update
    void Start()
    {
        fallenAnimation = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && falleActive == true)
        {
            falleActive = false;
            other.GetComponent<PlayerHealth>().GetDamage(schadensvariable);
            fallenAnimation.SetTrigger("trapTrigger");
        }
    
    }

}
