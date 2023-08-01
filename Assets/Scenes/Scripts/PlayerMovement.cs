using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
   public Animator animator;
    private Transform pTr;
    private Vector3 pStart;
    private SpriteRenderer sprite;
    private float horVal,horVal1, Movespeed= 4;
    [SerializeField]
    private GameObject LevelClear;


    [SerializeField]
    private float pMv = 1f;
    [SerializeField]
    private float Pf = 200f;

    public bool iFr = true;
    private Vector3 pStartoffset = new Vector3(0, 0, 0);
    private Rigidbody2D rb;

    public int maxHealth = 40;
    public int currentHealth { get; private set; }
    public HealthBar healthBar;

    public GameObject Gun;
    public GameObject GunUI;

    bool facingright = true;


    [SerializeField]
    private float KBForce;

    [SerializeField]
    private float KBCounter;

    [SerializeField]
    private float KBTotalTime;

    private bool KnockFromRight;
    // Start is called before the first frame update

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    void Start()
    {
        pTr = GetComponent<Transform>();
        transform.position = pTr.position + pStartoffset;
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetMaxHealth(maxHealth);
        Gun.gameObject.SetActive(false);
        GunUI.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        Movement();
        Jump(); 
        UpdateAnimationState();

        if (horVal > 0f && !facingright)
        {
            flip();
        }
        if (horVal < 0f && facingright)
        {
            flip();
        }

      
         
        




       
       
    }

    private void Movement()
    {
        
        
            horVal = Input.GetAxisRaw("Horizontal");


            if (KBCounter <= 0)
            {
                transform.position += new Vector3(horVal, 0f, 0f) * Movespeed * Time.deltaTime;
            }

            else
            {
                if (KnockFromRight == true)
                {
                    rb.velocity = new Vector2(KBForce, KBForce);


                }

                if (KnockFromRight == false)
                {
                    rb.velocity = new Vector2(-KBForce, KBForce);

                }

                KBCounter -= Time.deltaTime;
            }


        }

    private void Jump()
    {
        // if (TutorialActive)
        //{
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f) 
        {
            rb.AddForce(new Vector2(0f, Pf), ForceMode2D.Impulse);


        }
       
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        healthBar.SetHealth(currentHealth);

        if (currentHealth > 0)
        {
            // player hurt
        }
        else
        {
            SceneManager.LoadScene("Death");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "gun")
        {
            
            TakeDamage(10);
        }
        if (collision.gameObject.tag == "Gun")
        {
            GunUI.gameObject.SetActive(true);
            Gun.gameObject.SetActive(true);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "LevelClear")
        {
            LevelClear.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag== "WPenemy")
        {
            TakeDamage(40);
        }
           if (collision.gameObject.CompareTag("Traps"))
           {
            KBCounter = KBTotalTime;

            if (collision.transform.position.x <= transform.position.x)
            {
                KnockFromRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                KnockFromRight = false;
            }
            TakeDamage(20);

           }
    }

    private void UpdateAnimationState()
    {
        if (horVal < 0f)
        {
            animator.SetBool("run", true);

        }

        else if (horVal > 0f)
        {
            animator.SetBool("run", true);
           
        }
        else
        {
            animator.SetBool("run", false);
        }

        if (rb.velocity.y > 0.1f)
        {
            animator.SetBool("jump", true);
            animator.SetBool("run", false);
        }

        if (rb.velocity.y == 0)
        {
            animator.SetBool("jump", false);

        }


    }
    void flip()
    {
        Vector3 currentscale = gameObject.transform.localScale;
        currentscale.x *= -1;
        gameObject.transform.localScale = currentscale;
        facingright = !facingright;
    }


}
