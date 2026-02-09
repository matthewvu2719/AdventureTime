using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlant : Enemy
{

    [Header("Plant Specific")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private bool facingRight;
    
    protected override void Start()
    {
        base.Start();
        if (facingRight) Flip();
    }



    // Update is called once per frame
    void Update()
    {
        CollisionCheck();
        idleTimeCounter -= Time.deltaTime;
        bool playerDetected = playerDetection.collider.GetComponent<Player>() != null;
        bool playerDetected2 = playerDetection2.collider.GetComponent<Player>() != null;
        if (playerDetected) 
        {
            idleTimeCounter = idleTime;
            anim.SetTrigger("attack");
            
        }
        if(playerDetected2)
        {
            idleTimeCounter = idleTime;
            Flip();
            anim.SetTrigger("attack");
        }
        
    }

    private void AttackEvent()
    {
        GameObject newBullet = Instantiate(bulletPrefab, bulletOrigin.transform.position,bulletOrigin.transform.rotation);
        newBullet.GetComponent<Bullet>().SetupSpeed(bulletSpeed * facingDirection, 0);
        Destroy(newBullet, 3f);
    }
}
