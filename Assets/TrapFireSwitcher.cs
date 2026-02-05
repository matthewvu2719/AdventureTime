using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFireSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public TrapFire myTrap;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>()!= null)
        {
            anim.SetTrigger("pressed");
            myTrap.FireSwitchAfter(7);
        }
    }
}
