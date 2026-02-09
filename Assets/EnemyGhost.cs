using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhost : Enemy
{

    [Header("Ghost specific")]
    [SerializeField] private float activeTime;
    private float activeTimeCounter =4;

    private SpriteRenderer sr;

    [SerializeField] private float[] xOffSet;
    public override void Damage()
    {
        if(aggressive)
            base.Damage();
    }

    protected override void Start()
    {
        base.Start();
        sr = GetComponent<SpriteRenderer>();
        aggressive= true;
        invincible= true;
    }


    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            anim.SetTrigger("disappear");
            return;
        }

        activeTimeCounter -= Time.deltaTime;
        idleTimeCounter -= Time.deltaTime;

        if (activeTimeCounter > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }

        if (activeTimeCounter < 0 && idleTimeCounter < 0 && aggressive)
        {
            anim.SetTrigger("disappear");
            aggressive = false;
            idleTimeCounter = idleTime;
        }
        if (activeTimeCounter < 0 && idleTimeCounter < 0 && !aggressive)
        {
            ChoosePosition();
            anim.SetTrigger("appear");
            aggressive = true;
            activeTimeCounter = activeTime;
        }

        FlipController();
    }

    private void FlipController()
    {
        if (player == null) return;
        if (facingDirection == -1 && transform.position.x < player.transform.position.x)
        {
            Flip();
        }
        else if (facingDirection == 1 && transform.position.x > player.transform.position.x)
        {
            Flip();
        }
    }

    private void ChoosePosition()
    {   
        float xOffset = xOffSet[Random.Range(0,xOffSet.Length)];
        float yOffset = Random.Range(-7, 7);
        transform.position = new Vector2(player.transform.position.x + xOffset, player.transform.position.y + yOffset);
    }


    public void Disappear()
    {
        sr.enabled = false;
    }

    public void Appear()
    {
        sr.enabled = true;
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if(aggressive)
            base.OnTriggerEnter2D(collider);
    }

    protected override void OnTriggerStay2D(Collider2D collider)
    {
        if (aggressive)
            base.OnTriggerStay2D(collider);
    }
}
