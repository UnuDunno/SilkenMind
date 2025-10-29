using UnityEngine;

public class CardInstance : MonoBehaviour
{
    public BaseCard data;
    public bool corrupted;

    private CardUI cardUI;

    void Awake()
    {
        cardUI = GetComponent<CardUI>();
    }

    public void Initialize(BaseCard baseData)
    {
        data = baseData;
        corrupted = false;

        if(cardUI == null)
        {
            cardUI = GetComponent<CardUI>();
        }

        if (cardUI != null)
        {
            cardUI.ConfigureCard(this);
        }
        else
        {
            Debug.LogError($"Falha ao encontrar o componente CardUI no prefab da carta: {baseData.cardName}");
        }
    }

    public void UseCard()
    {
        if (!corrupted && data != null)
        {
            if(data.ExecuteEffect()) Destroy(gameObject);
        }
        else
        {
            Debug.Log($"Carta {data.cardName} está corrompida e causa um efeito negativo!");
        }
    }
}
