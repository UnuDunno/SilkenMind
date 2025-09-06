using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<BaseCard> deck = new List<BaseCard>();
    public List<BaseCard> playingDeck = new List<BaseCard>();

    public GameObject cardUIPrefab;
    public Transform handUI;

    public List<CardInstance> hand = new();

    void Start()
    {
        if (deck.Count == 0)
        {
            CreateInitialDeck();
        }
    }

    private void CreateInitialDeck()
    {
        BaseCard bloqueio = Resources.Load<BaseCard>("Cards/Bloqueio");
        BaseCard golpe = Resources.Load<BaseCard>("Cards/Golpe");
        BaseCard investida = Resources.Load<BaseCard>("Cards/Investida");
        BaseCard olhosDeCacador = Resources.Load<BaseCard>("Cards/Olhos de Caçador");
        BaseCard teiaDaVida = Resources.Load<BaseCard>("Cards/Teia da Vida");
        BaseCard teiaProtetora = Resources.Load<BaseCard>("Cards/Teia Protetora");

        if (golpe == null || investida == null || bloqueio == null || olhosDeCacador == null || teiaProtetora == null || teiaDaVida == null)
        {
            Debug.LogError("Nem todas as cartas foram encontradas em Resources/Cartas/");
            return;
        }

        for (int i = 0; i < 6; i++) deck.Add(bloqueio);
        for (int i = 0; i < 6; i++) deck.Add(golpe);
        for (int i = 0; i < 2; i++) deck.Add(investida);
        for (int i = 0; i < 2; i++) deck.Add(olhosDeCacador);
        for (int i = 0; i < 2; i++) deck.Add(teiaDaVida);
        for (int i = 0; i < 2; i++) deck.Add(teiaProtetora);
    }

    public void BuildPlayingDeck()
    {
        if (playingDeck.Count != 0) playingDeck.Clear();

        foreach (BaseCard card in deck)
        {
            playingDeck.Add(card);
        }

        Shuffle();
    }

    public void Shuffle()
    {
        int rand;
        for (int i = 0; i < playingDeck.Count; i++)
        {
            rand = Random.Range(i, playingDeck.Count);

            (playingDeck[i], playingDeck[rand]) = (playingDeck[rand], playingDeck[i]);
        }
    }

    public void BuyCards(int quantity)
    {
        if (playingDeck.Count == 0)
        {
            BuildPlayingDeck();
        }

        for (int i = 0; i < quantity; i++)
        {
            BaseCard topDeck = playingDeck[0];
            // CardInstance instance = new CardInstance(topDeck);
            // hand.Add(instance);

            GameObject newCard = Instantiate(cardUIPrefab, handUI);

            CardInstance cardInstance = newCard.GetComponent<CardInstance>();
            cardInstance.Initialize(topDeck);

            hand.Add(cardInstance);

            playingDeck.RemoveAt(0);

            if (playingDeck.Count == 0) BuildPlayingDeck();
        }
    }

    public void DiscardHand()
    {
        foreach (var card in hand)
        {
            if (card != null) Destroy(card.gameObject);
        }

        hand.Clear();
    }

}
