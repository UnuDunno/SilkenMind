using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ShopNodeHandler : MonoBehaviour
{
    public GameObject shopPanel;
    public ShopPanelUI shopPanelUI;

    public List<BaseCard> allCardsPool;
    public int itemsToSell = 3;
    public int baseCardPrice = 100;

    public UnityEvent onShopClosed;

    public void Initiate(Node node)
    {
        List<ShopItem> inventory = GenerateInventory();

        if (shopPanelUI == null)
        {
            shopPanelUI = shopPanel.GetComponent<ShopPanelUI>();
        }

        shopPanelUI.SetupShop(inventory, this);

        shopPanel.SetActive(true);
    }

    private List<ShopItem> GenerateInventory()
    {
        List<ShopItem> items = new List<ShopItem>();
        List<BaseCard> avaiableCards = new List<BaseCard>(allCardsPool);

        int randIndex;
        for (int i = 0; i < itemsToSell; i++)
        {
            if (avaiableCards.Count == 0) break;

            randIndex = Random.Range(0, avaiableCards.Count);
            BaseCard card = avaiableCards[randIndex];
            avaiableCards.RemoveAt(randIndex);

            items.Add(new ShopItem { card = card, price = baseCardPrice });
        }

        return items;
    }

    public void EndEvent()
    {
        shopPanel.SetActive(false);
        onShopClosed.Invoke();
    }
}

[System.Serializable]
public class ShopItem
{
    public BaseCard card;
    public int price;
}
