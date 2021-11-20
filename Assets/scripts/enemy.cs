using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;
using static Helper;

public class enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject projectile;

    bool isGrounded;

    Animator m_Animator;

    int maxHealth = 100;
    int currentHealth;

    public HealthBAR healthbar; // this finds the HealthBar UI


    public float speed = 10f; // How fast the Enemy Moves
    private float regular; // the regular enemy speed
    public float stoppingDist = 3f; //the stopping distance between the player
    public GameObject player; // finds the target

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //gets the rigidbody component
        regular = speed;
        m_Animator = GetComponent<Animator>(); // gets the animator so you can use it in script
        currentHealth = maxHealth; // makes it so the current health is max at the begginning 
        healthbar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        float ex = transform.position.x; //enemy position
        float px = player.transform.position.x; //player position
        float dist = ex - px; // distance between them
        Vector2 velocity = rb.velocity;




        if (ex < px)// this checks the players postition and checks if the enemy should flip to face him
        {

            Helper.DoFaceLeft(gameObject, false); // makes you face right
        }
        else
        {

            Helper.DoFaceLeft(gameObject, true); // makes you face left

        }

        if (currentHealth == 0)
        {
            m_Animator.SetTrigger("DEATH");
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Destroy(gameObject, 5.00f);
        }

        // move enemy towards player

        velocity.x = 0;
        if (dist < -2)
        {
            velocity.x = 2;
            m_Animator.SetBool("IsMoving", true);
        }
       

        if (dist > 2)
        {
            velocity.x = -2;
            m_Animator.SetBool("IsMoving", true);

        }


        
        if(dist <= 3 && dist >= -2)
        {
            DoFight();
        }
        else
        {
            
        }

        if (dist > stoppingDist) // if the distance is bigger then the stopping distance
        {
            velocity.x = -stoppingDist; // stops the player

        }


        rb.velocity = velocity;
    }


    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile")) // checks if the projectile has the correct tag
        {
            TakeDamage(20); // takes damage
        } 
    }

    
    
    
    void CreateProjectile()
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

    void DoCollisons()
    {
        float rayLength = 0.5f;


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength); // shoots an invisible ray down and checks if the sprite has the correct tag

        Color hitColor = Color.white;

        isGrounded = false;

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Ground") // checks if sprite has a tag named ground
            {
                hitColor = Color.green;
                isGrounded = true; // makes the player grounded
            }
            Debug.DrawRay(transform.position, -Vector2.up * rayLength, hitColor);
        }

    }


    void DoFight()
    {
        m_Animator.SetTrigger("Fight" ); // plays the 
    }

    
    
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.setHealth(currentHealth);
    }


}