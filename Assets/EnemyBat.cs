using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : Enemy
{

    [Header("Bat Specific")]
    [SerializeField] private Transform[] idlePoint;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsPlayer;
    private bool playerDetected;
    private Vector2 destination;
    private bool canBeAggressive=true;
    float defaultSpeed;

    protected override void Start()
    {
        base.Start();
        defaultSpeed = speed;
        destination = idlePoint[0].position;
        transform.position = destination;
        for(int i = 0; i < idlePoint.Length; i++)
        {
            idlePoint[i].GetComponent<SpriteRenderer>().enabled = false;
        }
    }


    // Update is called once per frame
    void Update()
    {

        anim.SetBool("canBeAggressive", canBeAggressive);
        anim.SetFloat("speed", speed);
        idleTimeCounter -= Time.deltaTime;
        if (idleTimeCounter > 0) return;
        playerDetected = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        if (playerDetected && !aggressive && canBeAggressive)
        {
            aggressive = true;
            canBeAggressive = false;
            if (player != null)
                destination = player.transform.position;
            else
            {
                aggressive = false;
                canBeAggressive = true;
            }
        }

        if (aggressive)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, destination) < .1f)
            {
                aggressive = false;
                int i = Random.Range(0, idlePoint.Length);
                destination = idlePoint[i].position;
                speed *= .5f;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, destination) < .1f)
            {
                if (!canBeAggressive)
                {
                    idleTimeCounter = idleTime;
                }
                canBeAggressive = true;
                speed = defaultSpeed;
            }
        }

        FlipController();
    }

    private void FlipController()
    {
        if (player != null) return;
        if (facingDirection == -1 && transform.position.x < destination.x)
        {
            Flip();
        }
        else if (facingDirection == 1 && transform.position.x > destination.x)
        {
            Flip();
        }
    }

    public override void Damage()
    {
        base.Damage();
        idleTimeCounter = 5;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}
