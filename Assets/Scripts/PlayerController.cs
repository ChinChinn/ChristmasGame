using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed;
    public float jumpForce;
    private float input;
    bool facingRight = true; 
    bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    bool wallJumping; 
    public float xWallForce;
    public float ywallForce;
    public float wallJumptime;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input* speed, rb.velocity.y);
        if (input > 0 && facingRight == false){
            flip();
        }else if (input < 0 && facingRight == true )
        {
            flip();
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if (isTouchingFront == true && isGrounded == false && input != 0)
        {
            wallSliding = true;
        }else
        {
            wallSliding = false;
        }
        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && wallSliding == true){
            wallJumping = true;
            Invoke("setWallJumpingToFalse", wallJumptime);
        }
        if (wallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -input, ywallForce);
        }
    }

    void flip (){
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    void setWallJumpingToFalse(){
        wallJumping = false;
    }

}
