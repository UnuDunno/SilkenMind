using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    public Transform cardsContainer;
    public GameObject shopCardPrefab;
    public TMP_Text goldText;
    public Button exitButton;

    public int gold = 50;
    public int cardPrice = 20;

    private CardManager cardManager;
    private PlayerMapProgress playerMapProgress;
    private List<BaseCard> avaiableCards = new List<BaseCard>();

    void Start()
    {
        cardManager = FindObjectOfType<CardManager>();
        playerMapProgress = FindObjectOfType<PlayerMapProgress>();

        if (exitButton != null) exitButton.onClick.AddListener(Close);

        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        UpdateGoldDisplay();
        GenerateCards();
    }

    private void UpdateGoldDisplay()
    {
        if (goldText != null) goldText.text = $"Ouro: ";
    }

    private void GenerateCards()
    {
        foreach (Transform c in cardsContainer)
        {
            Destroy(c.gameObject);
        }

        BaseCard[] allCards = Resources.LoadAll<BaseCard>("Cards/Shop");

        List<BaseCard> candidates = new List<BaseCard>(allCards);
        avaiableCards.Clear();

        int quantity = Mathf.Min(3, candidates.Count);
        int index;
        for (int i = 0; i < quantity; i++)
        {
            index = Random.Range(0, candidates.Count);
            avaiableCards.Add(candidates[index]);
            candidates.RemoveAt(index);
        }

        foreach (BaseCard card in avaiableCards)
        {
            GameObject newCard = Instantiate(shopCardPrefab, cardsContainer);
            ShopCardUI cardUI = newCard.GetComponent<ShopCardUI>();
            cardUI.Configure(card, cardPrice, this);
        }
    }

    public void BuyCard(BaseCard card)
    {
        if (gold < cardPrice)
        {
            Debug.Log("Ouro insuficiente!");
            return;
        }

        gold -= cardPrice;
        UpdateGoldDisplay();

        if (cardManager != null)
        {
            cardManager.playingDeck.Add(card);

            Debug.Log($"Carta {card.cardName} adicionada ao deck!");
        }

        GenerateCards();
    }
    
    public void Close()
    {
        gameObject.SetActive(false);

        if (playerMapProgress != null) playerMapProgress.EndCurrentEvent();
    }
}
