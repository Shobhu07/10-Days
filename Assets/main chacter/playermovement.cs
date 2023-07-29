using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using Cinemachine;*/

public class playermovement : MonoBehaviour
{
    Rigidbody2D rb;
    //Animator animator;
    bool candash = true;
    bool isdashinng;
    float dashingpower = 25f;
    float dashingtime = 0.2f;
    float dashingcooldown = 1f;
    float timestamp = 0f, timedefrence = 2f;
    Animator animator;
    

    public Transform groundCheckCollider;

    public LayerMask groundLayer;
    public Transform wallCheckCollider;
    public LayerMask wallLayer;

    const float groundCheckRadius = 0.2f;
    const float wallCheckRadius = 0.2f;
    [SerializeField] float speed = 4;
    [SerializeField] float jumpPower = 500;
    [SerializeField] float slideFactor = 0.2f;
    public int totalJumps;
    int availableJumps;
    float horizontalValue;
    float runSpeedModifier = 2f;
    private bool isGrounded = true;
    bool isRunning;
    bool facingRight = true;
    bool multipleJump;
    bool coyoteJump;
    bool isSliding;
  
   




    void Awake()
    {
        availableJumps = totalJumps;

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    { 
        timestamp += Time.deltaTime;
        if (timestamp >= timedefrence)
        {
            showvelo();
            timestamp = 0;
        }
        if (isdashinng)
        {
            return;
        }

        //Store the horizontal value
        horizontalValue = Input.GetAxis("Horizontal");

        //If LShift is clicked enable isRunning
        if (Input.GetKeyDown(KeyCode.LeftShift))
            isRunning = true;
        //If LShift is released disable isRunning
        if (Input.GetKeyUp(KeyCode.LeftShift))
            isRunning = false;

        //If we press Jump button enable jump 
        if (Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftAlt) && candash  )
        {
            animator.SetBool("dash", true);
            StartCoroutine(Dash());

        }


        //Set the yVelocity Value
        animator.SetFloat("yvelocity", rb.velocity.y);

        //Check if we are touching a wall to slide on it
        WallCheck();
    }

    void FixedUpdate()
    {
        if (isdashinng)
        {

            return;
        }

        GroundCheck();



        Move(horizontalValue);



    }





    void GroundCheck()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;
        //Check if the GroundCheckObject is colliding with other
        //2D Colliders that are in the "Ground" Layer
        //If yes (isGrounded true) else (isGrounded false)
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
            if (!wasGrounded)
            {
                availableJumps = totalJumps;
                multipleJump = false;

                //AudioManager.instance.PlaySFX("landing");
            }


        }

        //As long as we are grounded the "Jump" bool
        //in the animator is disabled
        animator.SetBool("Jump", !isGrounded);

    }

    void WallCheck()
    {
        //If we are touching a wall
        //and we are moving towards the wall
        //and we are falling
        //and we are not grounded
        //Slide on the wall
        if (Physics2D.OverlapCircle(wallCheckCollider.position, wallCheckRadius, wallLayer)
            && Mathf.Abs(horizontalValue) > 0
            && rb.velocity.y < 0
            && !isGrounded)
        {
            if (!isSliding)
            {
                availableJumps = totalJumps;
                multipleJump = false;
            }

            Vector2 v = rb.velocity;
            v.y = -slideFactor;
            rb.velocity = v;
            isSliding = true;

            if (Input.GetButtonDown("Jump"))
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
        else
        {
            isSliding = false;
        }
    }

    #region Jump
    IEnumerator CoyoteJumpDelay()
    {
        coyoteJump = true;
        yield return new WaitForSeconds(0.2f);
        coyoteJump = false;
    }

    void Jump()
    {
        if (isGrounded)
        {
            multipleJump = true;
            availableJumps--;

            rb.velocity = Vector2.up * jumpPower;
            animator.SetBool("Jump", true);
            ;
        }
        else
        {
            if (coyoteJump)
            {
                multipleJump = true;
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }

            if (multipleJump && availableJumps > 0)
            {
                availableJumps--;

                rb.velocity = Vector2.up * jumpPower;
                animator.SetBool("Jump", true);
            }
        }
    }
    #endregion

    void Move(float dir)
    {


        #region Move & Run
        //Set value of x using dir and speed
        float xVal = dir * speed * 100 * Time.fixedDeltaTime;

        //If we are running mulitply with the running modifier (higher)
        if (isRunning)
            xVal *= runSpeedModifier;
        //If we are running mulitply with the running modifier (higher)

        //Create Vec2 for the velocity
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        //Set the player's velocity
        rb.velocity = targetVelocity;
      
        //If looking right and clicked left (flip to the left)
        if (facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
            
           
           

          

        }
        //If looking left and clicked right (flip to rhe right)
        else if (!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
           

        }

        //(0 idle , 4 walking , 8 running)
        //Set the float xVelocity according to the x value 
        //of the RigidBody2D velocity 
        animator.SetFloat("velocity", Mathf.Abs(rb.velocity.x));
        #endregion
    }
    private IEnumerator Dash()
    {
        candash = false;
        isdashinng = true;
        float origanalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingpower, 0f);
        yield return new WaitForSeconds(dashingtime);
        rb.gravityScale = origanalGravity;
        isdashinng = false;
        animator.SetBool("dash", false);

        yield return new WaitForSeconds(dashingcooldown);

        candash = true;
    }

    void showvelo()
    {
        Debug.Log("velocityis" + rb.velocity.x);

    }

  
}