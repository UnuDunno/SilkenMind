using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanelUI : MonoBehaviour
{
    public Transform itemsContainer;
    public GameObject shopItemPrefab;
    public Button leaveButton;

    private ShopNodeHandler shopNodeHandler;

    private void Awake()
    {
        if (leaveButton != null) leaveButton.onClick.AddListener(CloseShop);
    }

    public void SetupShop(List<ShopItem> items, ShopNodeHandler handler)
    {
        shopNodeHandler = handler;

        foreach (Transform child in itemsContainer) Destroy(child.gameObject);

        foreach (ShopItem item in items)
        {
            GameObject itemGO = Instantiate(shopItemPrefab, itemsContainer);
            ShopItemUI itemUI = itemGO.GetComponent<ShopItemUI>();
            if (itemUI != null) itemUI.Setup(item);
        }
    }
    
    private void CloseShop()
    {
        if (shopNodeHandler != null) shopNodeHandler.EndEvent();
    }
}
