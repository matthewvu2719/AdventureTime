using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlueBird : Enemy
{
    private RaycastHit2D ceillingDetected;

    [Header("Blue Bird specific")]
    [SerializeField] private float ceilingDistance;
    [SerializeField] private float groundDistance;
    private float flyForce;
    [SerializeField] private float flyUpForce;
    [SerializeField] private float flyDownForce;

    private bool canFly = true;


    public override void Damage()
    {
        canFly = false;
        rb.velocity = new Vector2(0, 0);
        rb.gravityScale = 0;
        base.Damage();

    }

    protected override void Start()
    {
        base.Start();
        flyForce = flyUpForce;
    }

    public void FlyUpEvent()
    {
        if(canFly)
        {
            rb.velocity = new Vector2(speed * facingDirection, flyForce);
        }

    }


    // Update is called once per frame
    void Update()
    {
        CollisionCheck();
        if(ceillingDetected)
        {
            flyForce = flyDownForce;
        }else if(groundDetected)
        {
            flyForce= flyUpForce;
        }

        if (wallDetected)
        {
            Flip();
        }
    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();
        ceillingDetected = Physics2D.Raycast(transform.position, Vector2.up, groundDistance, ground);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + groundDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - ceilingDistance));
    }
}
