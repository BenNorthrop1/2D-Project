using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class PLAYERMOVEMENT : MonoBehaviour
{
    private Rigidbody2D rb; //THis defines the rigidbody
    private BoxCollider2D coll; //this defines the boxcollider
    
    public GameObject projectile; //this allows you to enter the prefab you want to use
    public GameObject DeathScreen; // this gets the deathscreen component
    
    public HealthBAR healthbar; // this gets components from the health bar script

    Animator m_Animation; //this defines the animator so it can be used in script

    public AudioSource JumpSound; //this allows you to add your sounds 
    public AudioSource Shoot;   //this allows you to add your sounds 
    public AudioSource Death;   //this allows you to add your sounds 


    public static int maxHealth = 100; // this is your beggining max health
    public static int currentHealth; //this is the current health of the player

    public float speed = 10f; //this is the players speed
    public float jumpheight = 10f; // this is the players jump height
    public float sprint = 12f; // this is the sprint speed
    public float attackRate = 2f; //this the rate of attack damage
    float nextAttackTime = 0f; //this is the next time it attacks
    float regular; //this defines the normal walking speed

    [SerializeField] LayerMask jumpableGround;


    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //this gets the rigidbody component
        coll = GetComponent<BoxCollider2D>(); //this gets the box collider component
        m_Animation = gameObject.GetComponent<Animator>(); //this gets the animator component
        regular = speed; //this makes it so regular speed equals the regular speed
        currentHealth = maxHealth; // this makes it so the current health always starts at max
        healthbar.SetMaxHealth(maxHealth); // this makes it so the health bar starts at full health
        DeathScreen.SetActive(false); // this makes it so the deathscreen doesn't show until you die
    }

    void Update()
    {

        DoCollisons(); //this calls the method and everything in it
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



    void DoCollisons()
    {
        float rayLength = 0.5f;

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


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }


    void DoJump()
        {
            Vector2 velocity = rb.velocity;

            // check for jump
            if (Input.GetKey("space") && IsGrounded())
            {
                
                    velocity.y = jumpheight;
                    m_Animation.SetBool("Jumping" , true);
                    JumpSound.Play();
                
            }
                else
            {
                m_Animation.SetBool("Jumping", false);
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
            Vector2 velocity = rb.velocity;

        if (Input.GetButtonDown("Fire1"))
        {
            if (velocity.x < 0)
            {
                m_Animation.SetTrigger("Fight");
                Shoot.Play();
            }
            else
            {
                m_Animation.SetTrigger("MoveFight");
                Shoot.Play();
            }
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