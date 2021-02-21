using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentSelectedItem = 0;
    public int currentItemCost = 200;

    private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            if(player != null)
            {
                UIManager.Instance.OpenShop(player.getGemCount());
            }

            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }
    }

    public void SelectItem(int item)
    {
        
        switch(item)
        {
            case 0: UIManager.Instance.UpdateShopSelection(36);
                currentSelectedItem = 0;
                currentItemCost = 200;
                break;
            case 1: UIManager.Instance.UpdateShopSelection(-65);
                currentSelectedItem = 1;
                currentItemCost = 400;
                break;
            case 2: UIManager.Instance.UpdateShopSelection(-166);
                currentSelectedItem = 2;
                currentItemCost = 100;
                break;
        }
    }

    public void BuyItem()
    {
        if(player.getGemCount() >= currentItemCost)
        {

            switch (currentSelectedItem)
            {
                case 0:
                    
                    break;
                case 1:
                    
                    break;
                case 2:
                    GameManager.Instance.HasKeyToCastle = true;
                    break;
            }

            player.decreaseGem(currentItemCost);
            Debug.Log(player.Diamonds);
            Debug.Log("item: " + currentSelectedItem);
            UIManager.Instance.OpenShop(player.getGemCount());
        }
        else
        {
            Debug.Log("You dont have enough gems");
            shopPanel.SetActive(false);
        }

    }


}
