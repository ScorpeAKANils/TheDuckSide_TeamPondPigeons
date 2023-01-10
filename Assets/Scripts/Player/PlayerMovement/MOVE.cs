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
    [SerializeField] float m_jumpForwardForce = 20f;
    [SerializeField] float m_DashForce = 40f;
    bool DashAllowed = true;
    [SerializeField] Animator wingAnim;
    [SerializeField] GameObject Wing;
    [SerializeField] Animator wingAnim1;
    [SerializeField] GameObject Wing1;
    float currentYVelocity;
    [SerializeField] float FallMultiplayer;
    bool isWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        moveRight = new Vector3(0, 180, 0);
        moveLeft = new Vector3(0, 0, 0);
        playerrb = this.GetComponent<Rigidbody>();
        playerrb.position = this.transform.position;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
      
        if (Input.GetKeyUp(KeyCode.A) | Input.GetKeyUp(KeyCode.D))
        {

            isWalking = false;
            anim.SetBool("isWalking", false);
            wingAnim.SetBool("isWalking", false);
            wingAnim1.SetBool("isWalking", false);
        }
        if (Input.GetKeyDown(KeyCode.E) && DashAllowed)
        {
            DashAllowed = false;
            StartCoroutine(DashDuration());
        }

        

        if (Input.GetAxisRaw("Jump") == 1 && isGrounded)
        {
            if (isWalking)
            {
                playerrb.AddForce(this.gameObject.transform.right * m_jumpForwardForce, ForceMode.VelocityChange);
                isWalking = false;
            }
            anim.SetBool("isJumping", true);
            wingAnim1.SetBool("isJumping", true);
            wingAnim.SetBool("isJumping", true);
            isGrounded = false;
            playerrb.velocity = Vector3.up * jump;
        }
        if (playerrb.velocity.y < 0)
        {
            playerrb.velocity += (FallMultiplayer - 1) * Physics.gravity.y * Vector3.up * Time.deltaTime;
        }
   
    }
    void FixedUpdate()
    {

        if (isGrounded)
        {
            currentYVelocity = playerrb.velocity.y;
            Vector3 m_Move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            playerrb.velocity = m_Move * playerMovementSpeed;
            //playerrb.AddForce(m_Move * playerMovementSpeed, ForceMode.VelocityChange); 
            playerrb.velocity = new Vector3(playerrb.velocity.x, currentYVelocity, 0);
        }

        //spieler dreht sich in lauf richtung
        if (Input.GetKey(KeyCode.A))
        {
            isWalking = true;
            transform.rotation = Quaternion.Euler(moveRight);
            anim.SetBool("isWalking", true);
            wingAnim.SetBool("isWalking", true);
            wingAnim1.SetBool("isWalking", true);
        }
        if (Input.GetKey(KeyCode.D))
        {
            isWalking = true;
            transform.rotation = Quaternion.Euler(moveLeft);
            anim.SetBool("isWalking", true);
            wingAnim.SetBool("isWalking", true);
            wingAnim1.SetBool("isWalking", true);
        }
    }
    //groundcheck
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
       
            anim.SetBool("isJumping", false);
            wingAnim.SetBool("isJumping", false);
            wingAnim1.SetBool("isJumping", false);
            if (collision.gameObject.GetComponent<movingPlattform>())
            {
                this.gameObject.transform.parent = collision.gameObject.transform;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {

            isGrounded = true; 
        }
    }
    IEnumerator DashDuration()
    {
        playerrb.AddForce(this.gameObject.transform.right * m_DashForce, ForceMode.VelocityChange);
        yield return new WaitForSeconds(0.5f);
        DashAllowed = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Map"))
        {
            if (collision.gameObject.GetComponent<movingPlattform>())
            {

                this.gameObject.transform.parent = null;
            }

        }
    }
}