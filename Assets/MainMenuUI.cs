using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    [SerializeField] private VolumnControllerUI[] volumnController;

    public void Start()
    {
        bool showButton = PlayerPrefs.GetInt("Level" + 2 + "Unlocked") == 1;
        continueButton.SetActive(showButton);
        AudioManager.instance.PlayBGM(0);

        for(int i=0;i< volumnController.Length;i++)
        {
            volumnController[i].GetComponent<VolumnControllerUI>().SetUpVolumnSlider();
        }
    }
    public void SwitchMenuTo(GameObject uiMenu)
    {
        for(int i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        AudioManager.instance.PlaySFX(4);
        uiMenu.SetActive(true);
    }
 
}
