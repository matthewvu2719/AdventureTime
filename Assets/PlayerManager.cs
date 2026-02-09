using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject playerPrefab;
    public GameObject currentPlayer;

    private void Awake()
    {
        instance = this;
        PlayerRespawn();
    }

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayerRespawn()
    {
        if (currentPlayer == null)
        {
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
        }
    }
}
