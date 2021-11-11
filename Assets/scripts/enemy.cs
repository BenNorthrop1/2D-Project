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

    public HealthBAR healthbar;



    public float speed = 10f;
    public float regular;
    public float stoppingDist = 2f;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        regular = speed;
        m_Animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        float ex = transform.position.x;
        float px = player.transform.position.x;
        float dist = ex - px;
        Vector2 velocity = rb.velocity;




        if (ex < px)
        {

            Helper.DoFaceLeft(gameObject, false);
        }
        else
        {

            Helper.DoFaceLeft(gameObject, true);

        }


        // move enemy towards player

        velocity.x = 0;
        if (dist < -2)
        {
            velocity.x = 2;
            m_Animator.SetBool("IsMoving" , true);
        }
        else
        {
            DoFight();
        }

        velocity.x = 0;
        if (dist < -2)
        {
            velocity.x = 2;
            m_Animator.SetBool("IsMoving", true);
        }
        else
        {
            DoFight();
        }


        if (dist > stoppingDist)
        {
            velocity.x = -stoppingDist;
        }


        rb.velocity = velocity;
    }


    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage(20);
        }
            
        
        
    }

    
    
    
    void CreateProjectile()
    {
        int dir = Helper.GetObjectDir(gameObject);

        if (dir == Right)       // get the player direction
        {
            Helper.MakeBullet(projectile, transform.position.x + 1f, transform.position.y + 1, 35, 4);  
        }
        else
        {
            Helper.MakeBullet(projectile, transform.position.x + 1f, transform.position.y + 1, -35, 4);
        }
    }

    void DoCollisons()
    {
        float rayLength = 0.5f;


        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength);

        Color hitColor = Color.white;

        isGrounded = false;

        if (hit.collider != null)
        {


            if (hit.collider.tag == "Ground")
            {
                hitColor = Color.green;
                isGrounded = true;
            }


            Debug.DrawRay(transform.position, -Vector2.up * rayLength, hitColor);
        }

    }


    void DoFight()
    {
        m_Animator.SetTrigger("Fight");
    }

    
    
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.setHealth(currentHealth);

        if (currentHealth == 0)
        {
            Destroy(gameObject , 1.00f);
            m_Animator.SetTrigger("DEATH");
            
        }
    }


}