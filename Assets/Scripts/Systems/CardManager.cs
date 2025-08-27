using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [Header("Configuração do Baralho")]
    public List<BaseCard> starterDeck;

    [Header("UI")]
    public GameObject cardUIPrefab;
    public Transform handUI;

    public List<CardInstance> hand = new();

    void Start()
    {
        BuyCards(5);
    }

    public void BuyCards(int quantity)
    {
        for (int i = 0; i < quantity && i < starterDeck.Count; i++)
        {
            var instance = new CardInstance(starterDeck[i]);
            hand.Add(instance);

            GameObject newCard = Instantiate(cardUIPrefab, handUI);
            newCard.GetComponent<CardUI>().ConfigureCard(instance);
        }
    }

    public void PlayCard(int index) {
        if(index >= 0 && index < hand.Count) {
            hand[index].UseCard();
            hand.RemoveAt(index);
        }
    }
}
