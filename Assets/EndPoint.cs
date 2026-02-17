using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private InGameUI ingameUI;
    private void Start()
    {
        ingameUI =GameObject.Find("Canvas").GetComponent<InGameUI>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            GetComponent<Animator>().SetTrigger("activate");
            AudioManager.instance.PlaySFX(2);
            PlayerManager.instance.PlayerDeath();
            ingameUI.OnLevelFinish();
            GameManager.instance.SaveBestTime();
            GameManager.instance.SaveCollectedFruits();
            GameManager.instance.SaveLevelInfo();

        }
    }
    
}
