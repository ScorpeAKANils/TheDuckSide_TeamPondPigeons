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
    Vector3 m_Move;
    Transform Player; 
    public bool isGrounded = true;
    Rigidbody playerrb;
    Animator anim;
    [SerializeField] float m_jumpForwardForce = 20f;
    [SerializeField] float m_DashForce = 2f;
    bool DashAllowed = true;
    [SerializeField] Animator wingAnim;
    bool MovesRight = true; 
    [SerializeField] GameObject Wing;
    [SerializeField] Animator wingAnim1;
    [SerializeField] GameObject Wing1;
    float currentYVelocity;
    float horizontal; 
    [SerializeField] float FallMultiplayer;
    bool isWalking = false;
    float time;
    bool isDashing;



    // Start is called before the first frame update
    void Start()
    {
   
        Player = this.GetComponent<Transform>(); 
        moveRight = new Vector3(0, 180, 0);
        moveLeft = new Vector3(0, 0, 0);
        playerrb = this.GetComponent<Rigidbody>();
        playerrb.position = this.transform.position;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isDashing)
        {
            return; 
        }
        horizontal = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyUp(KeyCode.A) | Input.GetKeyUp(KeyCode.D))
        {

            isWalking = false;
            anim.SetBool("isWalking", false);
            wingAnim.SetBool("isWalking", false);
            wingAnim1.SetBool("isWalking", false);
        }
        if (Input.GetKeyDown(KeyCode.R) && DashAllowed)
        {
            StartCoroutine(DashDuration());
        }

        DoFlipBro();

        if (Input.GetAxisRaw("Jump") == 1 && isGrounded)
        {
            anim.SetBool("isJumping", true);
            wingAnim1.SetBool("isJumping", true);
            wingAnim.SetBool("isJumping", true);
            //isGrounded = false;
            playerrb.velocity = Vector3.up * jump;
        }
        if (playerrb.velocity.y < 0)
        {
            playerrb.velocity += (FallMultiplayer - 1) * Physics.gravity.y * Vector3.up * Time.deltaTime;
        }
 
    }
    void FixedUpdate()
    {
        if (isDashing)
        {
            return; 
        }
            currentYVelocity = playerrb.velocity.y;
            //m_Move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
            playerrb.velocity = new Vector2(horizontal * playerMovementSpeed, playerrb.velocity.y);
          
        if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D))
        {
            isWalking = true;
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
        }
    }

    void DoFlipBro()
    {
        if (MovesRight && horizontal < 0f || !MovesRight && horizontal > 0f)
        {
            MovesRight = !MovesRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    IEnumerator DashDuration()
    {
        DashAllowed = false;
        isDashing = true;
        //playerrb.useGravity = false;
        playerrb.velocity = new Vector3(transform.localScale.x* m_DashForce, 0f, 0f);
        yield return new WaitForSeconds(0.2f);
        //playerrb.useGravity = true;
        isDashing = false;
        yield return new WaitForSeconds(1f);
        DashAllowed = true; 

    }
}