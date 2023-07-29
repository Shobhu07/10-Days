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
    private float horVal,horVal1;

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
    }

    // Update is called once per frame
    void Update()
    {
        
        horVal = Input.GetAxis("Horizontal");
        transform.position += Vector3.right * pMv * Time.deltaTime * horVal;

        if (horVal < 0 && iFr)
        {
            flip();
        }
        else if (horVal > 0 && !iFr)
        {
            flip();
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector2(0, Pf), ForceMode2D.Impulse);
           
        }
        if(Mathf.Abs(rb.velocity.y)>0)
        {
          animator.SetBool("jump", true);
        }
        else
        {
            animator.SetBool("jump", false);
        }

        void flip()
        {
            iFr = !iFr;
            transform.Rotate(0f, 180f, 0f);
        }
        horVal1 = Mathf.Abs(horVal);
         
        animator.SetFloat("run", horVal1);
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag== "WPenemy")
        {
            TakeDamage(40);
        }

    }
  
}
