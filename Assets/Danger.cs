using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Danger : MonoBehaviour
{
    //this class gives damage to player
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            Player player = collision.GetComponent<Player>();
            player.Knockback(transform);


        }

    }

    protected virtual void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.GetComponent<Player>() != null)
        {
            Player player = collider.GetComponent<Player>();
            player.Knockback(transform);
        }
    }
}
