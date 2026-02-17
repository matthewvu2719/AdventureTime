using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropFruitController : MonoBehaviour
{
    [SerializeField] private GameObject fruit;

    [Range(1,10)]
    [SerializeField] private int dropAmount;

    public void DropFruits()
    {
        for(int i =0;i<dropAmount;i++)
        {
            GameObject newFruit = Instantiate(fruit,transform.position,transform.rotation); 
            Destroy(newFruit,10);
        }
    }
    // Start is called before the first frame update

}
