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

        if (cardUI != null)
        {
            cardUI.ConfigureCard(this);
        }
    }

    public void UseCard()
    {
        if (!corrupted && data != null)
        {
            data.ExecuteEffect();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"Carta {data.cardName} está corrompida e causa um efeito negativo!");
        }
    }
}
