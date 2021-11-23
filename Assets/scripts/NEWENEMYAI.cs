using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Globals;

public class NEWENEMYAI : MonoBehaviour
{
    private float dist;
    private Rigidbody2D rb;

    public LayerMask GroundLayer;

    public GameObject projectile;
    public Collider2D wallDetection;

    public Transform GroundCheckPoint;
    public Transform player;
    public Transform ShootPosition;

    public HealthBAR healthbar;

    Animator Animator;

    bool isPatrolling;
    bool isGrounded;
    bool isflipped;

    public float stoppingDist = 3f;
    public float speed;

    int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isPatrolling = true;
        currentHealth = maxHealth; 
        healthbar.SetMaxHealth(maxHealth);
    }


    void Update()
    {
        Vector2 velocity = rb.velocity;

        dist = Vector2.Distance(transform.position, player.position);

        if (isPatrolling)
        {
            Patrol();
        }

        if (currentHealth == 0)
        {
            Animator.SetTrigger("DEATH");
            GetComponent<Collider2D>().enabled = false;
            this.enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Destroy(gameObject, 5.00f);
        }

        if (dist <= stoppingDist)
        {
            if (player.position.x > transform.position.x && transform.localScale.x < 0 || player.position.x < transform.position.x && transform.localScale.x > 0)
            {
                Flip();
            }

            isPatrolling = false;
            velocity.x = 0;
            DoFight();

        }
        else
        {
            isPatrolling = true;
            velocity.x = speed;
            Animator.SetBool("IsMoving", true);
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

    private void FixedUpdate()
    {
        if(isPatrolling)
        {
            isflipped = !Physics2D.OverlapCircle(GroundCheckPoint.position, 0.1f, GroundLayer);

        }
    }

    void Patrol()
    {
        Vector2 velocity = rb.velocity;

        if (isflipped || wallDetection.IsTouchingLayers(GroundLayer))
        {
            Flip();
        }
        velocity.x = speed;

        rb.velocity = velocity;
    }

    void Flip()
    {
        isPatrolling = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;
        isPatrolling = true;
    }

    void CreateProjectile()
    {
   
            GameObject MakeBullet = Instantiate(projectile,ShootPosition.position ,Quaternion.identity );
            SetVelocity(MakeBullet, ShootPosition);
    }

    void SetVelocity(GameObject makeBullet, Transform shootPosition)
    {
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(20, 5);
    }

    void DoFight()
    {
        Animator.SetTrigger("Fight");
        
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthbar.setHealth(currentHealth);
    }

}

