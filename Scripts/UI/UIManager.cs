using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    

    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("Error UIManager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public Text playerGemCountText;
    
    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = gemCount + "G";
    }

    public Image selectionImg;

    public void UpdateShopSelection(int yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    public Text gemCountHud;

    public void UpdateGemHud(int totalGem)
    {
        gemCountHud.text = "" + totalGem;
    }

    public Image[] healthImg;

    public void UpdateHealthHud(int currentHealth)
    {
        if(currentHealth > 3)
        {
            return;
        }
        else if(currentHealth >2)
        {
            healthImg[3].gameObject.SetActive(false);
        }
        else if(currentHealth > 1)
        {
            healthImg[3].gameObject.SetActive(false);
            healthImg[2].gameObject.SetActive(false);
        }
        else if( currentHealth > 0)
        {
            healthImg[3].gameObject.SetActive(false);
            healthImg[2].gameObject.SetActive(false);
            healthImg[1].gameObject.SetActive(false);
        }
        else if( currentHealth <= 0)
        {
            healthImg[3].gameObject.SetActive(false);
            healthImg[2].gameObject.SetActive(false);
            healthImg[1].gameObject.SetActive(false);
            healthImg[0].gameObject.SetActive(false);
        }
    }

}
