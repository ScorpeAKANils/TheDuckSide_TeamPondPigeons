using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOVE : MonoBehaviour
{


    public float playerBaseMovementSpeed = 20.0f;
    public float playerMovementSpeed = 20.0f;
    [SerializeField] float jump = 3f;
    Vector3 moveRight;
    Vector3 moveLeft;
    [SerializeField] bool isGrounded = true;
    Rigidbody playerrb;
    Animator anim;
    bool lookRight; 
    [SerializeField] Animator wingAnim;
    [SerializeField] GameObject Wing;
    [SerializeField] Animator wingAnim1;
    [SerializeField] GameObject Wing1;
    Vector3 m_Jump;
    bool isJumping;
    float currentYVelocity;
    float currentXVelocity; 
    [SerializeField]  float FallMultiplayer;
   

    // Start is called before the first frame update
    void Start()
    {
        m_Jump = new Vector3(0f, 1f, 0f);
        moveRight = new Vector3(0, 180, 0);
        moveLeft = new Vector3(0, 0, 0);
        playerrb = this.GetComponent<Rigidbody>();
        playerrb.position = this.transform.position;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        currentYVelocity = playerrb.velocity.y;
        Vector3 m_Move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        playerrb.velocity = m_Move * playerMovementSpeed;
        playerrb.velocity = new Vector3(playerrb.velocity.x, currentYVelocity, 0); 

        if (Input.GetKeyUp(KeyCode.A) | Input.GetKeyUp(KeyCode.D))
        {


            anim.SetBool("isWalking", false);
            wingAnim.SetBool("isWalking", false);
            wingAnim1.SetBool("isWalking", false);

        }

   
            if (Input.GetAxisRaw("Jump")==1&& isGrounded)
            {
            isJumping = true; 
 
            anim.SetBool("isJumping", true);
            wingAnim1.SetBool("isJumping", true);
            wingAnim.SetBool("isJumping", true);

            //playerrb.AddForce(Vector2.up * playerrb.mass * jump);
   

            //playerrb.velocity = new Vector2(playerrb.velocity.x, jump);
            //
        }

        if (playerrb.velocity.y < 0)
        {
            playerrb.velocity += (FallMultiplayer-1) * Physics.gravity.y * Vector3.up*Time.deltaTime;
        }

      


    }
    void FixedUpdate()
    {
     

        //laufen


        //playerrb.MovePosition(playerrb.position + (m_Move * speed * Time.fixedDeltaTime));


        //spieler dreht sich in lauf richtung
        if (Input.GetKey(KeyCode.A))
        {
            lookRight = false; 
            transform.rotation = Quaternion.Euler(moveRight);
            anim.SetBool("isWalking", true);
            wingAnim.SetBool("isWalking", true);
            wingAnim1.SetBool("isWalking", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            lookRight = true; 
            transform.rotation = Quaternion.Euler(moveLeft);
            anim.SetBool("isWalking", true);
            wingAnim.SetBool("isWalking", true);
            wingAnim1.SetBool("isWalking", true);
        }

        if (isJumping)
        {
         
            isJumping = false;
            isGrounded = false;
            playerrb.velocity = Vector3.up * jump; 
        }
    }

    //groundcheck
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
            wingAnim.SetBool("isJumping", false);
           wingAnim1.SetBool("isJumping", false);
        }
    }
}