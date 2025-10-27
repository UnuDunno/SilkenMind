using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCardUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text priceText;
    public Button buyButton;

    private BaseCard cardData;
    private int price;
    private ShopPanel shopPanel;

    public void Configure(BaseCard card, int price, ShopPanel panel)
    {
        this.cardData = card;
        this.price = price;
        this.shopPanel = panel;

        if (iconImage != null) iconImage.sprite = card.image;
        if (nameText != null) nameText.text = card.cardName;
        if (descriptionText != null) descriptionText.text = card.description;
        if (priceText != null) priceText.text = $"{price} Ouro";
        if (buyButton != null) buyButton.onClick.AddListener(() => shopPanel.BuyCard(card));
    }
}
