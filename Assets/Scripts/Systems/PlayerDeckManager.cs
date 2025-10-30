using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDeckManager : MonoBehaviour
{
    public static PlayerDeckManager Instance { get; private set; }

    public List<BaseCard> startingDeck;

    private List<BaseCard> playerDeck;

    public UnityEvent<List<BaseCard>> onDeckChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        playerDeck = new List<BaseCard>();
    }

    void Start()
    {
        foreach (BaseCard card in startingDeck)
        {
            AddCardToDeck(card);
        }
    }

    private void NotifyDeckChanged()
    {
        if (onDeckChanged != null)
        {
            onDeckChanged.Invoke(playerDeck);
        }
    }

    public void AddCardToDeck(BaseCard cardToAdd)
    {
        if (cardToAdd == null)
        {
            return;
        }

        playerDeck.Add(cardToAdd);
        Debug.Log($"<color=green>Carta Adicionada:</color> {cardToAdd.cardName}. Total de cartas no deck: {playerDeck.Count}");

        NotifyDeckChanged();
    }

    public void RemoveRandomCard()
    {
        if (playerDeck.Count == 0)
        {
            Debug.LogWarning("Tentativa de remover carta de um deck vazio");
            return;
        }

        int randIndex = Random.Range(0, playerDeck.Count);
        BaseCard card = playerDeck[randIndex];

        playerDeck.RemoveAt(randIndex);

        Debug.Log($"<color=red>Carta Removida:</color> {card.cardName}. Total de cartas no deck: {playerDeck.Count}");
        NotifyDeckChanged();
    }
    
    public List<BaseCard> GetDeck()
    {
        return new List<BaseCard>(playerDeck);
    }
}
