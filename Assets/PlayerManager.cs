using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public InGameUI ingameUI; 

    [Header("Player Info")]
    [SerializeField] private GameObject deathFX;

    public static PlayerManager instance;
    public int fruits;
    [SerializeField] public Transform respawnPoint;

    [SerializeField] private GameObject fruitPrefab;
    [SerializeField] private GameObject playerPrefab;
    public GameObject currentPlayer;
    public int chosenSkinId;

    [Header("Camera shake FX")]
    [SerializeField] private CinemachineImpulseSource impulse;
    [SerializeField] private Vector3 shakeDirection;
    [SerializeField] private float forceMultiplier;

    public void ScreenShake(int facingDirection)
    {
        impulse.m_DefaultVelocity = new Vector3(shakeDirection.x * facingDirection, shakeDirection.y) * forceMultiplier;
        impulse.GenerateImpulse();

    }


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerRespawn();
        }
    }

    private bool HaveEnoughFruit()
    {
        if(fruits >0)
        {
            fruits--;
            if (fruits < 0)
            {
                fruits= 0;
            }
            DropFruit();
            return true;
        }
        return false;
    }

    public void OnFalling()
    {
        PlayerDeath();
        if (HaveEnoughFruit())
            Invoke("PlayerRespawn",1);
        else ingameUI.OnDeath();
    }

    public void OnTakingDamage()
    {
        if (!HaveEnoughFruit())
        {
            PlayerDeath();
            ingameUI.OnDeath();
        }
    }
    public void PlayerRespawn()
    {
        if (currentPlayer == null)
        {
            AudioManager.instance.PlaySFX(11);
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
        }
    }

    public void PlayerDeath()
    {
        AudioManager.instance.PlaySFX(0);
        GameObject newDeath = Instantiate(deathFX,currentPlayer.transform.position, currentPlayer.transform.rotation);
        Destroy(newDeath,.4f);
        Destroy(currentPlayer);

    }

    private void DropFruit()
    {
        int fruitIndex = UnityEngine.Random.Range(0, Enum.GetNames(typeof(FruitType)).Length);
        GameObject newFruit = Instantiate(fruitPrefab,currentPlayer.transform.position,transform.rotation);
        newFruit.GetComponent<FruitDroppedByPlayer>().FruitSetup(fruitIndex);
        Destroy(newFruit,20);
    }
}
