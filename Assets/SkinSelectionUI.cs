using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkinSelectionUI : MonoBehaviour
{


    [SerializeField] private int[] priceForSkin;
    
    
    [SerializeField] private bool[] skinPurchased;
    private int skinId;


    [Header("Components")]
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject selectButton;
    [SerializeField] private Animator anim;
    [SerializeField] private TextMeshProUGUI bankText;



    private void OnEnable()
    {
        SetUpSkinInfo();    
    }

    private void OnDisable()
    {
        selectButton.SetActive(false);
    }
    private void SetUpSkinInfo()
    {

        skinPurchased[0] = true;
        
        for(int i=1;i<skinPurchased.Length;i++)
        {
            bool skinUnlocked = PlayerPrefs.GetInt("SkinPurchased" + i) == 1;
            if (skinUnlocked)
            {
                skinPurchased[i] = true;
            }
        }
        bankText.text= PlayerPrefs.GetInt("TotalFruitsCollected").ToString();
        buyButton.SetActive(!skinPurchased[skinId]);
        selectButton.SetActive(skinPurchased[skinId]);
        buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price: " + priceForSkin[skinId];
        anim.SetInteger("skinId", skinId);
    }

    public bool EnoughMoney()
    {
        int totalFruits = PlayerPrefs.GetInt("TotalFruitsCollected");
        if(totalFruits > priceForSkin[skinId])
        {
            totalFruits -= priceForSkin[skinId];
            PlayerPrefs.SetInt("TotalFruitsCollected", totalFruits);
            AudioManager.instance.PlaySFX(5);
            return true;
        }

        AudioManager.instance.PlaySFX(6);
        return false;
    }

    public void NextSkin()
    {
        skinId++;
        if (skinId > 3) skinId = 0;
        SetUpSkinInfo();
    }


    public void PreviousSkin()
    {
        skinId--;
        if (skinId <0 ) skinId = 3;
        SetUpSkinInfo();
    }

    public void Buy()
    {
        if (EnoughMoney())
        {
            PlayerPrefs.SetInt("SkinPurchased" + skinId, 1);
            SetUpSkinInfo();
        }
        else
            Debug.Log("Not enough");
    }

    public void Select()
    {
        PlayerManager.instance.chosenSkinId= skinId;
        Debug.Log("Purchased!");
    }


    public void SwitchSelectButton(GameObject button)
    {
        selectButton = button;
    }
}
