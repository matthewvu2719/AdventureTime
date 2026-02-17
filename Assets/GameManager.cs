using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Timer")]
    public float timer;
    public bool startTimer;

    [Header("Lv info")]
    public int lvNum;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (instance == null) instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        //PlayerPrefs.SetInt("Fruits", 50);
        Debug.Log(PlayerPrefs.GetFloat("Level" + lvNum + "Best time", timer));
    }

    private void Update()
    {
        if(startTimer)
        {
            timer += Time.deltaTime;
        }

    }


    public void SaveBestTime()
    {
        startTimer = false;
        float lastTime = PlayerPrefs.GetFloat("Level" + lvNum + "BestTime", 999);
        if(lastTime>timer)
            PlayerPrefs.SetFloat("Level" + lvNum + "BestTime", timer);
        timer = 0;
    }

    public void SaveCollectedFruits()
    {
        int totalFruits = PlayerPrefs.GetInt("TotalFruitsCollected");
        int newTotalFruits = totalFruits + PlayerManager.instance.fruits;
        PlayerPrefs.SetInt("TotalFruitsCollected", newTotalFruits);
        PlayerPrefs.SetInt("Level" +lvNum +"FruitsCollected",PlayerManager.instance.fruits);
        PlayerManager.instance.fruits = 0;
    }


    public void SaveLevelInfo()
    {
        int nextLevelNumber = lvNum+ 1;
        PlayerPrefs.SetInt("Level" + nextLevelNumber + "Unlocked", 1);
    }
}
