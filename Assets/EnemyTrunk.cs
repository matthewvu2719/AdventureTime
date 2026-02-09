using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrunk : Enemy
{

    [Header("Trunk Specific")]
    [SerializeField] private float retreatTime;
    private float retreatTimeCounter;
    
    [Header("Collision specific")]
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private Transform groundBehindCheck;
    private bool groundBehind;
    private bool wallBehind;
    private bool playerDetected;

    [Header("Bullet specific")]
    [SerializeField] private float attackCooldown;
    private float attackCooldownCounter;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private GameObject bulletPrefab;


    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

        CollisionCheck();
        if (!canMove)
        {
            rb.velocity = new Vector2(0, 0);
        }
        attackCooldownCounter -= Time.deltaTime;
        retreatTimeCounter -= Time.deltaTime;

        if (playerDetected && retreatTimeCounter<0) 
            retreatTimeCounter = retreatTime;

        if (playerDetection.collider !=null &&playerDetection.collider.GetComponent<Player>() != null)
        {
            if (attackCooldownCounter < 0)
            {
                attackCooldownCounter = attackCooldown;
                anim.SetTrigger("attack");
                canMove = false;
            }
            else if (playerDetection.distance<3)
            {
                MoveBackwards(1.5f);
            }
        }
        else
        {
            if (retreatTimeCounter>0)
            {
                MoveBackwards(4);
            }
            else
            {
                WalkAround();
            }

        }

        anim.SetFloat("xVelocity", rb.velocity.x);

    }

    private void MoveBackwards(float multiplier)
    {
        /*if (wallBehind || !groundBehind)
        {
            Debug.Log("Not moving");
            return;
        }
        rb.velocity = new Vector2(speed*mutiplier *-facingDirection, rb.velocity.y);*/
        if (player == null) return;
        if (wallBehind || !groundBehind)
        {
            rb.velocity = new Vector2(0f, 0f);
            return;
        }
        if (transform.position.x < player.transform.position.x && facingDirection == -1)
        {
            rb.velocity = new Vector2(speed * multiplier * facingDirection, rb.velocity.y);
            Flip();
        }
        else if (transform.position.x > player.transform.position.x && facingDirection == 1)
        {
            rb.velocity = new Vector2(speed * multiplier * facingDirection, rb.velocity.y);
            Flip();
        }
        else
        {
            rb.velocity = new Vector2(speed * multiplier * -facingDirection, rb.velocity.y);
        }

    }

    private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletOrigin.transform.position, bulletOrigin.transform.rotation);
        newBullet.GetComponent<Bullet>().SetupSpeed(bulletSpeed*facingDirection,0);
        Destroy(newBullet, 3f);
    }

    private void ReturnMovement()
    {
        canMove = true;
    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();
        playerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        groundBehind = Physics2D.Raycast(groundBehindCheck.position, Vector2.down, groundCheckDistance, ground);
        wallBehind = Physics2D.Raycast(wallCheck.position,Vector2.right*-facingDirection,wallCheckDistance, ground);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, checkRadius);
        Gizmos.DrawLine(groundBehindCheck.position,new Vector2(groundBehindCheck.position.x,groundBehindCheck.position.y-groundCheckDistance));
    }
}
