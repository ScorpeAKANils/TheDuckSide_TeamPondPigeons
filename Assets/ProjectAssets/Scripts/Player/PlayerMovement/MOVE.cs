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
    Vector3 localScale;
    public bool canWalk = true;
    float distToGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask ignoreLayer;
    int colCount = 0;
    bool groundedoderso;
    [SerializeField] GameObject[] Menu;
    bool hasBreak = false;
    [SerializeField] MainMenue main; 
    // Start is called before the first frame update

    void Start()
    {
        Time.timeScale = 1f; 
        localScale = transform.localScale;
        distToGround = this.GetComponent<SphereCollider>().bounds.extents.y;
        Player = this.GetComponent<Transform>();
        moveRight = new Vector3(0, 180, 0);
        moveLeft = new Vector3(0, 0, 0);
        playerrb = this.GetComponent<Rigidbody>();
        playerrb.position = this.transform.position;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
       
            //Physics.Raycast(groundCheck.position, -Vector3.up, distToGround + 0.2f, ignoreLayer);
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

            if (Input.GetButtonDown("Jump") && Grounded())
            {
                anim.SetBool("isJumping", true);
                wingAnim1.SetBool("isJumping", true);
                wingAnim.SetBool("isJumping", true);
                //isGrounded = false;
                playerrb.velocity = Vector3.up * jump;
            }
            if (playerrb.velocity.y < 0)
            {
                playerrb.velocity += (FallMultiplayer - 1) * (Physics.gravity.y * Time.deltaTime * Vector3.up);
            }
            if (canWalk)
            {
                currentYVelocity = playerrb.velocity.y;
                //m_Move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
                //playerrb.velocity = new Vector2(horizontal * playerMovementSpeed, playerrb.velocity.y);
                playerrb.AddForce(horizontal * playerMovementSpeed * Time.deltaTime * Player.right);

                if (Input.GetKeyDown(KeyCode.A) | Input.GetKeyDown(KeyCode.D))
                {
                    DoFlipBro();
                    isWalking = true;
                    anim.SetBool("isWalking", true);
                    wingAnim.SetBool("isWalking", true);
                    wingAnim1.SetBool("isWalking", true);
                }

            }
    }
        /*void FixedUpdate()
        {
            if (!hasBreak)
            {
                if (isDashing)
                {
                    return;
                }
            
            }


        }*/
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

        bool Grounded()
        {
            RaycastHit hit;
            Debug.DrawRay(groundCheck.position, -Vector3.up, Color.black, 3f);
        if (Physics.Raycast(groundCheck.position, -Vector3.up, out hit, distToGround - 0.01f))
        {
            if (hit.transform.gameObject.CompareTag("Map"))
            {
                Debug.Log("grounded");
                return true;
            }

        }
        else
        {
            Debug.Log("not on the ground"); 
        }
            return false;

        }

        void DoFlipBro()
        {
            if (MovesRight && horizontal < 0f || !MovesRight && horizontal > 0f)
            {
                MovesRight = !MovesRight;
                localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
        IEnumerator DashDuration()
        {
            DashAllowed = false;
            isDashing = true;
            //playerrb.useGravity = false;
            playerrb.AddForce(transform.right * (m_DashForce * localScale.x));
            yield return new WaitForSeconds(0.2f);
            //playerrb.useGravity = true;
            isDashing = false;
            yield return new WaitForSeconds(1f);
            DashAllowed = true;

        }

    }

