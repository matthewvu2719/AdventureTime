using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    protected Animator anim;
    protected Rigidbody2D rb;

    protected int facingDirection = -1;

    [SerializeField] protected LayerMask ground;
    [SerializeField] protected LayerMask whatToIgnore;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;

    protected bool wallDetected;
    protected bool groundDetected;

    public bool invincible;

    [Header("Move Info")]
    [SerializeField] protected float speed;
    [SerializeField] protected float idleTime = 2;
    protected float idleTimeCounter;
    protected bool canMove = true;

    protected bool aggressive;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    protected virtual void WalkAround()
    {
        if (idleTimeCounter <= 0 && canMove)
        {
            rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
        }
        else rb.velocity = new Vector2(0, 0);
        idleTimeCounter -= Time.deltaTime;


        if (wallDetected || !groundDetected)
        {
            idleTimeCounter = idleTime;
            Flip();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Damage()
    {
        if(!invincible)
        {
            canMove= false;
            anim.SetTrigger("gotHit");
        }

        
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }


    protected virtual void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            Player player = collider.GetComponent<Player>();
            player.Knockback(transform);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            Player player = collider.GetComponent<Player>();
            player.Knockback(transform);
        }
    }

    protected virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void CollisionCheck()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, ground);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right*facingDirection, wallCheckDistance, ground);
    }


    protected virtual void OnDrawGizmos()
    {
        if(groundCheck!=null)
            Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        if(wallCheck!=null)
            Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance*facingDirection, wallCheck.position.y));
    }
}
