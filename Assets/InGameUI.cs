using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    [Header("Menu Gameobjects")]
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject endLevelUI;


    [Header("Text Components")]
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI fruitText;
    [SerializeField] private TextMeshProUGUI endTimerText;
    [SerializeField] private TextMeshProUGUI bestTimerText;
    [SerializeField] private TextMeshProUGUI endFruitsText;

    private bool gamePaused;



    private void Start()
    {
        GameManager.instance.lvNum = SceneManager.GetActiveScene().buildIndex;
        PlayerManager.instance.ingameUI = this;
        Time.timeScale = 1;
        SwitchMenuTo(inGameUI);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIngameInfo();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckIfNotPaused();
        }
    }

    private bool CheckIfNotPaused()
    {
        if(!gamePaused)
        {
            gamePaused= true;
            Time.timeScale = 0;
            SwitchMenuTo(pauseUI);
            return true;
        }
        else
        {
            gamePaused= false;
            Time.timeScale = 1;
            SwitchMenuTo(inGameUI);
            return false;
        }
    }

    private void UpdateIngameInfo()
    {
        timerText.text = "Timer: " + GameManager.instance.timer.ToString("00") + " s";
        fruitText.text = PlayerManager.instance.fruits.ToString();
    }

    public void SwitchMenuTo(GameObject uiMenu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        uiMenu.SetActive(true);
    }


    public void OnDeath()
    {
        SwitchMenuTo(pauseUI);
    }
    public void OnLevelFinish()
    {
        endFruitsText.text = "Fruits: " + PlayerManager.instance.fruits;
        endTimerText.text = "Your time: " + GameManager.instance.timer.ToString("00") + " s";
        bestTimerText.text = "Best time: " + PlayerPrefs.GetFloat("Level"+GameManager.instance.lvNum + "BestTime", GameManager.instance.timer).ToString("00") + " s";
        SwitchMenuTo(endLevelUI);
    }

    public void LoadMainMenu()
    {
        Debug.Log("load main menu");
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void ReoadCurrentLevel()
    {
        Debug.Log("Retry");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
