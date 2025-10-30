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

    public void Onable()
    {
        if (PlayerWallet.Instance != null)
        {
            PlayerWallet.Instance.onGoldChanged.AddListener(OnPlayerGoldChanged);
        }

        CheckCanAfford();
    }

    public void Osable()
    {
        if (PlayerWallet.Instance != null)
        {
            PlayerWallet.Instance.onGoldChanged.RemoveListener(OnPlayerGoldChanged);
        }
    }
    
    public void OnPlayerGoldChanged(int newGoldAmount)
    {
        CheckCanAfford();
    }

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

        if (PlayerWallet.Instance != null)
        {
            int playerGold = PlayerWallet.Instance.GetCurrentGold();
            bool canAfford = playerGold >= currentItem.price;

            buyButton.interactable = canAfford;

            priceText.color = canAfford ? Color.white : Color.red;
        }
        else
        {
            buyButton.interactable = false;
        }
    }

    private void TryBuyItem()
    {
        if (isSold) return;

        if (PlayerWallet.Instance == null) return;

        bool purchaseSuccessful = PlayerWallet.Instance.TrySpendGold(currentItem.price);

        if (purchaseSuccessful)
        {
            Debug.Log($"Comprou {currentItem.card.name} por {currentItem.price}");

            MarkAsSold();

            if (PlayerDeckManager.Instance != null)
            {
                PlayerDeckManager.Instance.AddCardToDeck(currentItem.card);
            }
            else
            {
                Debug.LogError("PlayerDeckManager não encontrado. Não foi possível adicionar a carta ao deck");
            }

            Debug.LogWarning($"Carta adicionada ao Deck: {currentItem.card.name}");
        }
        else
        {
            Debug.Log("Dinheiro insuficiente");
        }
    }

    private void MarkAsSold()
    {
        isSold = true;
        buyButton.interactable = false;
        priceText.text = "VENDIDO";
    }
}
