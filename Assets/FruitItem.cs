using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType
{
    apple,
    banana,
    cherry,
    kiwi,
    melon,
    orange,
    pineapple,
    strawberry
}
public class FruitItem : MonoBehaviour
{

    [SerializeField] private Sprite[] fruitImage;
    [SerializeField] protected SpriteRenderer sr;
    [SerializeField] protected Animator anim;
    public FruitType myFruitType;

    [SerializeField] private GameObject pickUpFx;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            PlayerManager.instance.fruits++;
            AudioManager.instance.PlaySFX(7);
            if(pickUpFx!= null)
            {
                GameObject newPickUpFx = Instantiate(pickUpFx,transform.position,transform.rotation);
                Destroy(newPickUpFx,.5f);
            }
            
            Destroy(gameObject);
        }
    }


    public void FruitSetup(int fruitIndex)
    {
        for (int i = 0; i < anim.layerCount; i++)
        {
            anim.SetLayerWeight(i, 0);
        }
        anim.SetLayerWeight(fruitIndex, 1);
    }

    /*    private void OnValidate()
        {
            sr.sprite = fruitImage[(int)myFruitType];
        }*/
}
