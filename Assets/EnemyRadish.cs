using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadish : Enemy
{
    private RaycastHit2D groundBelowDetected;
    private bool groundAboveDetected;

    [Header("Radish specific")]
    [SerializeField] private float ceilingDistance;
    [SerializeField] private float groundDistance;
    [SerializeField] private float flyForce;
    [SerializeField] private float aggroTime;
    private float aggroTimeCounter;


    protected override void Start()
    {
        base.Start();
    }



    // Update is called once per frame
    void Update()
    {
        aggroTimeCounter -= Time.deltaTime;
        if(aggroTimeCounter < 0 && !groundAboveDetected)
        {
            rb.gravityScale = 1;
            aggressive= false;
        }
        if (!aggressive)
        {
            if(groundBelowDetected && !groundAboveDetected)
            {
                rb.velocity = new Vector2(0, flyForce);
            }
        }
        else
        {
            if(groundBelowDetected.distance<=1.25f)
                WalkAround();
        }
        CollisionCheck();
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetBool("aggressive", aggressive);
    }

    public override void Damage()
    {
        if (!aggressive)
        {
            aggroTimeCounter = aggroTime;
            rb.gravityScale= 12;
            aggressive= true;
        }
        else
        {
            base.Damage();
        }

    }

    protected override void CollisionCheck()
    {
        base.CollisionCheck();
        groundAboveDetected = Physics2D.Raycast(transform.position, Vector2.up, groundDistance,ground);
        groundBelowDetected = Physics2D.Raycast(transform.position, Vector2.down, ceilingDistance, ground);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x,transform.position.y + groundDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - ceilingDistance));
    }


}
