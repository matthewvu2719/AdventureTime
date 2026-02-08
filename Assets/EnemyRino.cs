using UnityEngine;

public class EnemyRino : Enemy
{
    [Header("Rino specific")]
    [SerializeField] private float aggroSpeed;


    [SerializeField] private float shockTime;
    private float shockTimeCounter;

    

    private RaycastHit2D playerDetection;

    protected override void Start()
    {
        base.Start();
        invincible = true;
    }


    // Update is called once per frame
    void Update()
    {
        playerDetection = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, 100, ~whatToIgnore);
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



        CollisionCheck();
        anim.SetBool("invincible", invincible);
        anim.SetFloat("xVelocity", rb.velocity.x);

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerDetection.distance * facingDirection, wallCheck.position.y));
    }
}
