using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOVE : MonoBehaviour
{


    [SerializeField] float speed = 1f;
    [SerializeField] float jump = 3f;
    Vector3 moveRight;
    Vector3 moveLeft;
    [SerializeField]bool isGrounded = true;
    Rigidbody playerrb;
    Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        moveRight = new Vector3(0, 90, 0);
        moveLeft = new Vector3(0, 270, 0);
        playerrb = this.GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
     


    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
        //laufen
            Vector3 m_Move = new Vector3(0, 0, Input.GetAxis("Horizontal"));
        Vector3 m_Jump = new Vector3(0f, 1f, 0f); 
            playerrb.MovePosition(this.transform.position + m_Move * Time.deltaTime * speed);

       //spieler dreht sich in lauf richtung
        if (Input.GetKey(KeyCode.D))
        {

            transform.localRotation = Quaternion.Euler(moveRight);
            anim.SetBool("isWalking", true);
        }

        if (Input.GetKey(KeyCode.A))
        {

            transform.localRotation = Quaternion.Euler(moveLeft);
            anim.SetBool("isWalking", true);

        }

      
     
        //springen
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
             
            isGrounded = false;
            playerrb.MovePosition(this.transform.position + jump * Time.deltaTime * m_Jump); 
        }
    }
    //groundcheck
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            isGrounded = true;
            
        }
       
    }
}

