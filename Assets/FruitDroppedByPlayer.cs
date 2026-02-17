using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDroppedByPlayer : FruitItem
{

    [SerializeField] private Vector2 speed;
    [SerializeField] protected Color transparentColor;
    

    protected bool canPickUp;

    protected virtual void Start()
    {
        StartCoroutine(BlinkImage());
    }

    private void Update()
    {
        transform.position += new Vector3(speed.x, speed.y) * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(canPickUp) 
            base.OnTriggerEnter2D(collision);
    }
    /*private IEnumerator BlinkImage()
    {
        anim.speed = 0;
        sr.color = transparentColor;
        speed.x*=-1;
        yield return new WaitForSeconds(.1f);
        sr.color = Color.white;
        
        speed.x *= -1;
        yield return new WaitForSeconds(.1f);
        sr.color = transparentColor;
        
        speed.x *= -1;
        yield return new WaitForSeconds(.1f);
        sr.color = Color.white;
        
        speed.x *= -1;
        yield return new WaitForSeconds(.1f);
        sr.color = transparentColor;
        
        speed.x *= -1;
        yield return new WaitForSeconds(.2f);
        sr.color = Color.white;
        
        speed.x *= -1;
        yield return new WaitForSeconds(.2f);
        sr.color = transparentColor;

        speed.x *= -1;
        yield return new WaitForSeconds(.3f);
        sr.color = Color.white;

        speed.x *= -1;
        yield return new WaitForSeconds(.3f);
        sr.color = transparentColor;

        speed.x *= -1;
        yield return new WaitForSeconds(.3f);
        sr.color = Color.white;

        speed.x *= -1;
        yield return new WaitForSeconds(.3f);
        sr.color = transparentColor;

        speed.x *= -1;
        yield return new WaitForSeconds(.3f);
        sr.color = Color.white;

        speed.x *= -1;
        yield return new WaitForSeconds(.3f);
        speed.x *= 0;
        anim.speed = 1;
        canPickUp = true;

    }*/

    protected struct BlinkStep
    {
        public Color color;
        public float wait;
        public BlinkStep(Color c, float w) { color = c; wait = w; }
    }

    protected virtual IEnumerator BlinkImage()
    {
        anim.speed = 0f;
        canPickUp = false;

        var steps = new BlinkStep[]
        {
        new BlinkStep(transparentColor, 0.1f),
        new BlinkStep(Color.white,      0.1f),
        new BlinkStep(transparentColor, 0.1f),
        new BlinkStep(Color.white,      0.1f),
        new BlinkStep(transparentColor, 0.2f),
        new BlinkStep(Color.white,      0.2f),
        new BlinkStep(transparentColor, 0.3f),
        new BlinkStep(Color.white,      0.3f),
        new BlinkStep(transparentColor, 0.3f),
        new BlinkStep(Color.white,      0.3f),
        new BlinkStep(transparentColor, 0.3f),
        new BlinkStep(Color.white,      0.3f),
        };

        foreach (var s in steps)
        {
            sr.color = s.color;
            speed.x *= -1f;
            yield return new WaitForSeconds(s.wait);
        }

        speed.x = 0f;
        anim.speed = 1f;
        canPickUp = true;
    }

}
