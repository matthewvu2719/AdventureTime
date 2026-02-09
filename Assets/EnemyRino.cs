using UnityEngine;

public class EnemyRino : Enemy
{
    [Header("Rino specific")]
    [SerializeField] private float aggroSpeed;


    [SerializeField] private float shockTime;
    private float shockTimeCounter;

    

    
    protected override void Start()
    {
        base.Start();
        invincible = true;
    }


    // Update is called once per frame
    void Update()
    {
        CollisionCheck();
        if (playerDetection.collider.GetComponent<Player>() != null)
        {
            aggressive= true;
        }
        if (!aggressive)
        {
            WalkAround();
        }
        else
        {
            if(!groundDetected)
            {
                aggressive = false;
                Flip();
            }
            rb.velocity = new Vector2(aggroSpeed * facingDirection, rb.velocity.y);
            if(wallDetected && invincible)
            {
                invincible= false;
                shockTimeCounter = shockTime;
            }

            if(shockTimeCounter<=0 && !invincible)
            {
                invincible= true;
                Flip();
                aggressive = false;
            }
            shockTimeCounter -= Time.deltaTime;
        }


       
        anim.SetBool("invincible", invincible);
        anim.SetFloat("xVelocity", rb.velocity.x);

    }


}
