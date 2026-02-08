using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : Enemy
{


    protected override void Start()
    {
        base.Start();
        //facingDirection = -1;
    }

    private void Update()
    {
        WalkAround();
        CollisionCheck();

        anim.SetFloat("xVelocity",rb.velocity.x);
    }
}
