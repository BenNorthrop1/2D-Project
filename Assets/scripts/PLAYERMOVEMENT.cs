using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class PLAYERMOVEMENT : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject projectile;
    public GameObject DeathScreen;
    private BoxCollider2D coll;
    [SerializeField] LayerMask jumpableGround;

    public static int maxHealth = 100;
    public static int currentHealth;

    public HealthBAR healthbar;

    Animator m_Animation;

    public float speed = 10f;
    public float jumpheight = 10f;
    public float sprint = 12f;
    float regular;
    public float stoneSpeed = 1f;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    public AudioSource JumpSound;
    public AudioSource Shoot;
    public AudioSource Death;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        regular = speed;
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        DeathScreen.SetActive(false);
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Animation = gameObject.GetComponent<Animator>();

        DoCollisons();
        DoJump();
        DoMove();
        DoFight();

        if (currentHealth == 0)
        {
            
            m_Animation.SetTrigger("DEATH");
            Death.Play();
            this.enabled = false;
        }

    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }



    void DoCollisons()
    {
        float rayLength = 0.2f;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.down , 1f);

        Vector2 velocity = rb.velocity;

        Color hitColor = Color.white;

        if (hit.collider != null)
        {

            if (hit.collider.tag == "Enemy")
            {
                hitColor = Color.green;
                rb.AddForce(transform.up * 25);
                m_Animation.SetTrigger("Jumping");
            }

            if (hit.collider.tag == "SPIKES")
            {
               if(Time.time >= nextAttackTime)
                {
                    hitColor = Color.green;
                    TakeDamage(5);
                    m_Animation.SetTrigger("Jumping");
                    JumpSound.Play();
                    rb.AddForce(transform.up * 50);
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }


            Debug.DrawRay(transform.position, -Vector2.up * rayLength, hitColor);
        }





    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Vector2 velocity = rb.velocity;


        if (other.CompareTag("EnemyProjectile"))
        {
            TakeDamage(20);
            
        }


      
        if (other.CompareTag("InstantSpike"))
        {
            TakeDamage(100);
        }

    }


    void DoJump()
        {
            Vector2 velocity = rb.velocity;

            // check for jump
            if (Input.GetKey("space") && IsGrounded())
            {
                if (velocity.y < 0.01f)
                {
                    velocity.y = jumpheight;
                    m_Animation.SetTrigger("Jumping");
                    JumpSound.Play();
            }
            }

            rb.velocity = velocity;

        }



        void DoFaceLeft(bool faceleft)
        {
            if (faceleft == true)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }


       


        void DoMove()
        {
            Vector2 velocity = rb.velocity;

            // stop player sliding when not pressing left or right
            velocity.x = 0;

            // check for moving lefts
            if (Input.GetKey("a"))
            {
                velocity.x = -speed;
                m_Animation.SetBool("IsMoving", true);
            }
            

            // check for moving right
            else if (Input.GetKey("d"))
            {
                velocity.x = speed;
                m_Animation.SetBool("IsMoving", true);
            }
            else
            {
                m_Animation.SetBool("IsMoving", false);
            }

            if (Input.GetButton("Sprint"))
            {
                speed = sprint;
            }
            else
            {
                speed = regular;
            }


            if (velocity.x < -0.5f)
            {
                Helper.DoFaceLeft(gameObject, true);
            }
            if (velocity.x > 0.5f)
            {
                Helper.DoFaceLeft(gameObject, false);
            }



            rb.velocity = velocity;

        }



        void CreateRock()
        {

            int dir = Helper.GetObjectDir(gameObject);


        if (dir == Right)       // get the player direction
        {
            Helper.MakeBullet(projectile, transform.position.x + 1f, transform.position.y + 1, 50, 4);
        }
        else
        {
            Helper.MakeBullet(projectile, transform.position.x + 1f, transform.position.y + 1, -50, 4);
        }

        }


        void DoFight()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                m_Animation.SetTrigger("Fight");
                Shoot.Play();
            }

        }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.setHealth(currentHealth);

        if (currentHealth == 0)
        {
            
            m_Animation.SetTrigger("DEATH");
            DeathScreen.SetActive(true);
        }
    }






}