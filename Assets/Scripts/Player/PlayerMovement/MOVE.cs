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
        Vector3 m_Move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        playerrb.velocity = m_Move * playerMovementSpeed;

        if (Input.GetKeyUp(KeyCode.A) | Input.GetKeyUp(KeyCode.D))
        {


            anim.SetBool("isWalking", false);
            wingAnim.SetBool("isWalking", false);
            wingAnim1.SetBool("isWalking", false);

        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        //laufen
       
        Vector3 m_Jump = new Vector3(0f, 1f, 0f);
        //playerrb.MovePosition(playerrb.position + (m_Move * speed * Time.fixedDeltaTime));


        //spieler dreht sich in lauf richtung
        if (Input.GetKey(KeyCode.D))
        {
            lookRight = true; 
            transform.rotation = Quaternion.Euler(moveRight);
            anim.SetBool("isWalking", true);
            wingAnim.SetBool("isWalking", true);
            wingAnim1.SetBool("isWalking", true);
        }

        if (Input.GetKey(KeyCode.A))
        {
            lookRight = false; 
            transform.rotation = Quaternion.Euler(moveLeft);
            anim.SetBool("isWalking", true);
            wingAnim.SetBool("isWalking", true);
            wingAnim1.SetBool("isWalking", true);

        }

        //springen
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            isGrounded = false;
            anim.SetBool("isJumping", true);
            wingAnim1.SetBool("isJumping", true);
            wingAnim.SetBool("isJumping", true);
            playerrb.MovePosition(playerrb.position + jump * Time.deltaTime * m_Jump);
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