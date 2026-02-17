using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitDroppedByEnemy : FruitDroppedByPlayer
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2[] dropDirection;
    [SerializeField] private float force;

    protected override void Start()
    {
        rb.GetComponentInParent<Rigidbody2D>();
        base.Start();
        int random = Random.Range(0,dropDirection.Length);
        rb.velocity = dropDirection[random] * force;

    }

    protected override IEnumerator BlinkImage()
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
        new BlinkStep(transparentColor, 0.2f),
        new BlinkStep(Color.white,      0.1f)
        };

        foreach (var s in steps)
        {
            sr.color = s.color;
            yield return new WaitForSeconds(s.wait);
        }

        anim.speed = 1f;
        canPickUp = true;
    }

}
