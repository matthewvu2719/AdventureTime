using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Danger
{

    private Rigidbody2D rb;
    private float xspeed;
    private float yspeed;
    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(xspeed, yspeed);

    }

    public void SetupSpeed(float x,float y)
    {
        xspeed = x;
        yspeed = y;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        /*if(collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().Damage();
        }*/
        base.OnTriggerEnter2D(collision);
        Destroy(gameObject);
    }
}
