using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Transform cardDisplay;
    public GameObject cardUI;
    public TextMeshProUGUI priceText;
    public Button buyButton;

    private ShopItem currentItem;
    private bool isSold = false;

    public void Setup(ShopItem item)
    {
        currentItem = item;
        isSold = false;

        if (item.card == null)
        {
            // Verificar allCardsPool no ShopNodeHandler
            Debug.LogError("ShopItem recebeu um item com BaseCard nulo");
            return;
        }
        if (cardUI == null)
        {
            // Verificar o Prefab ShopItemUI
            Debug.LogError("Referência à cardUI está nula");
            return;
        }

        foreach(Transform child in cardDisplay)
        {
            Destroy(child.gameObject);
        }

        GameObject newCard = Instantiate(cardUI, cardDisplay);

        CardInstance cardInstance = newCard.GetComponent<CardInstance>();
        cardInstance.Initialize(item.card);


        priceText.text = $"R$: {item.price}";

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(TryBuyItem);

        CheckCanAfford();
    }

    private void CheckCanAfford()
    {
        if (isSold)
        {
            buyButton.interactable = false;
            priceText.text = "VENDIDO";

            return;
        }
    }

    private void TryBuyItem()
    {
        if (isSold) return;

        MarkAsSold();
    }

    private void MarkAsSold()
    {
        isSold = true;
        buyButton.interactable = false;
        priceText.text = "VENDIDO";
    }
}
